using System;
using System.Linq;

namespace FractionalOperationsLab
{
    public struct Rational
    {
        /// Числитель дроби
        public int Numerator { get; set; }

        /// Знаменатель дроби
        public int Denominator { get; set; }

        /// Целая часть числа Z.N:D, Z. получается делением числителя на знаменатель и
        /// отбрасыванием остатка
        public int Base => Numerator / Denominator;

        /// Дробная часть числа Z.N:D, N:D
        public int Fraction => Numerator % Denominator;

        /// Возвращает наибольший общий делитель
        private static int GetGreatestCommonDivisor(int a, int b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        /// Возвращает наименьшее общее кратное
        private static int GetLeastCommonMultiple(int a, int b)
        {
            return a * b / GetGreatestCommonDivisor(a, b);
        }

        private Rational Invert()
        {
            return new Rational {Denominator = Numerator, Numerator = Denominator};
        }

        /// Операция сложения, возвращает новый объект - рациональное число,
        /// которое является суммой чисел c и this
        public Rational Add(Rational c)
        {
            var denominator = GetLeastCommonMultiple(Denominator, c.Denominator);
            var numerator = c.Numerator * denominator / c.Denominator +
                            Numerator * denominator / Denominator;
            var result = new Rational {Denominator = denominator, Numerator = numerator};
            result.Even();
            return result;
        }

        /// Операция смены знака, возвращает новый объект - рациональное число,
        /// которое являтеся разностью числа 0 и this
        public Rational Negate()
        {
            return new Rational {Denominator = Denominator, Numerator = -Numerator};
        }

        /// Операция умножения, возвращает новый объект - рациональное число,
        /// которое является результатом умножения чисел x и this
        public Rational Multiply(Rational x)
        {
            var result = new Rational
            {
                Denominator = Denominator * x.Denominator,
                Numerator = Numerator * x.Numerator
            };
            result.Even();
            return result;
        }

        /// Операция деления, возвращает новый объект - рациональное число,
        /// которое является результатом деления this на x
        public Rational DivideBy(Rational x)
        {
            return Multiply(x.Invert());
        }

        /// Вовзращает строковое представление числа в виде Z.N:D, где
        /// Z — целая часть N и D — целые числа, числитель и знаменатель, N &lt; D
        /// '.'— символ, отличающий целую часть от дробной,
        /// ':' — символ, обозначающий знак деления
        /// если числитель нацело делится на знаменатель, то
        /// строковое представление не отличается от целого числа
        /// (исчезает точка и всё, что после точки)
        /// Если Z = 0, опускается часть представления до точки включительно
        public override string ToString()
        {
            if (Numerator == 0)
            {
                return "0";
            }

            var sign = Numerator < 0 ? "-" : "";

            if (Fraction == 0)
            {
                return sign + Math.Abs(Base);
            }
            if (Denominator == 1)
            {
                return sign + Numerator;
            }
            if (Base == 0)
            {
                return sign + Math.Abs(Numerator) + ":" + Denominator;
            }
            return sign + Math.Abs(Base) + '.' + Math.Abs(Fraction) + ':' + Denominator;
        }

        /// Создание экземпляра рационального числа из строкового представления Z.N:D
        /// допускается N > D, также допускается
        /// Строковое представления рационального числа
        /// Результат конвертации строкового представления в рациональное
        /// число
        /// true, если конвертация из строки в число была успешной,
        /// false если строка не соответствует формату
        public static bool TryParse(string input, out Rational result)
        {
            result = new Rational {Denominator = 0, Numerator = 0};
            if (input.Contains('.') && !input.Contains(':'))
            {
                return false;
            }
            var rawInput = input.Split(':', '.');
            try
            {
                var sign = 1;
                if (rawInput[0][0] == '-')
                {
                    sign = -1;
                    rawInput[0] = rawInput[0].Substring(1);
                }
                switch (rawInput.Length)
                {
                    case 1:
                        result.Denominator = 1;
                        result.Numerator = Convert.ToInt32(rawInput[0]);
                        result.Numerator *= sign;
                        return true;
                    case 2:
                        if (rawInput[1][0] == '-')
                        {
                            return false;
                        }
                        result.Denominator = Convert.ToInt32(rawInput[1]);
                        if (result.Denominator == 0)
                        {
                            return false;
                        }
                        result.Numerator = Convert.ToInt32(rawInput[0]);
                        result.Numerator *= sign;
                        return true;
                    case 3:
                        if (rawInput[1][0] == '-' || rawInput[2][0] == '-')
                        {
                            return false;
                        }
                        result.Denominator = Convert.ToInt32(rawInput[2]);
                        if (result.Denominator == 0)
                        {
                            return false;
                        }
                        result.Numerator = Convert.ToInt32(rawInput[0]) * result.Denominator +
                                           Convert.ToInt32(rawInput[1]);
                        result.Numerator *= sign;
                        return true;
                    default:
                        return false;
                }
            }
            catch (Exception e) when (e is FormatException || e is OverflowException)
            {
                return false;
            }
        }

        /// Приведение дроби - сокращаем дробь на общие делители числителя
        /// и знаменателя. Вызывается реализацией после каждой арифметической операции
        private void Even()
        {
            var greatestCommonDivisor = GetGreatestCommonDivisor(Numerator, Denominator);
            Numerator /= greatestCommonDivisor;
            Denominator /= greatestCommonDivisor;
        }
    }
}