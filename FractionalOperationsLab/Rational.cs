using System;
using System.CodeDom;
using System.Linq;
using System.Text.RegularExpressions;

namespace FractionalOperationsLab
{
    /// <summary>
    ///     Структура для хранения рациональных дробей.
    ///     Общий вид: N:D, N - числитель дроби, D - знаменатель дроби
    ///     В строковом представлении может иметь вид Z.N:D, где Z - выделенная целая часть
    /// </summary>
    public struct Rational
    {
        /// <summary>
        ///     Числитель дроби
        /// </summary>
        public int Numerator { get; set; }

        /// <summary>
        ///     Знаменатель дроби
        /// </summary>
        public int Denominator { get; set; }

        /// <summary>
        ///     Целая часть Z числа Z.N:D
        ///     Получается делением числителя на знаменатель без остатка
        /// </summary>
        public int Base => Numerator / Denominator;

        /// <summary>
        ///     Дробная часть N числа Z.N:D
        ///     Получается взятием остатка от деления числителя на знаменатель
        /// </summary>
        public int Fraction => Numerator % Denominator;

        /// <summary>
        ///     Возвращает дробь сравнимую по модулю с текущей, но с противоположным знаком
        /// </summary>
        /// <returns>Рациональная дробь с противоположным знаком</returns>
        public Rational Negate()
        {
            return new Rational {Denominator = Denominator, Numerator = -Numerator};
        }

        /// <summary>
        ///     Возвращает дробь обратную к текущей (полученную сменой числителя и знаменателя)
        /// </summary>
        /// <returns>Рациональная дробь обратная текущей</returns>
        public Rational Invert()
        {
            return new Rational {Denominator = Numerator, Numerator = Denominator};
        }

        /// <summary>
        ///     Приводит текущую дробь сокращая числитель и знаменатель на общий делитель
        /// </summary>
        public void Even()
        {
            var greatestCommonDivisor = GetGreatestCommonDivisor(Numerator, Denominator);
            Numerator /= greatestCommonDivisor;
            Denominator /= greatestCommonDivisor;
        }

        /// <summary>
        ///     Возвращает сумму текущей рациональной дроби и рациональной дроби x
        /// </summary>
        /// <param name="x">Дробь прибавляемая к текущей</param>
        /// <returns>Рациональная дробь - результат сложения</returns>
        public Rational Add(Rational x)
        {
            var denominator = GetLeastCommonMultiple(Denominator, x.Denominator);
            var numerator = x.Numerator * denominator / x.Denominator +
                            Numerator * denominator / Denominator;
            var result = new Rational {Denominator = denominator, Numerator = numerator};
            result.Even();
            return result;
        }

        /// <summary>
        ///     Возвращает произведение текущей рациональной дроби на рациональную дробь x
        /// </summary>
        /// <param name="x">Дробь умножаемая на текущую</param>
        /// <returns>Рациональная дробь - результат умножения</returns>
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

        /// <summary>
        ///     Возвращает частное от деления текущей рациональной дроби на рациональную дробь x
        /// </summary>
        /// <param name="x">Дробь на которую делится текущая</param>
        /// <returns>Рациональная дробь - результат деления</returns>
        public Rational DivideBy(Rational x)
        {
            return Multiply(x.Invert());
        }

        /// <summary>
        ///     Находит наибольший общий делитель двух чисел
        /// </summary>
        /// <param name="first">Первое число</param>
        /// <param name="second">Второе число</param>
        /// <returns>Целое число - наибольший общий делитель</returns>
        private static int GetGreatestCommonDivisor(int first, int second)
        {
            while (second != 0)
            {
                var temp = second;
                second = first % second;
                first = temp;
            }
            return Math.Abs(first);
        }

        /// <summary>
        ///     Находит наименьшее общее кратное двух чисел
        /// </summary>
        /// <param name="first">Первое число</param>
        /// <param name="second">Второе число</param>
        /// <returns>Целое число - наименьшее общее кратное</returns>
        private static int GetLeastCommonMultiple(int first, int second)
        {
            return Math.Abs(first * second / GetGreatestCommonDivisor(first, second));
        }

        /// <summary>
        ///     Воpdращает строковое представление дроби в подходящем виде
        ///     Если возможно представления в виде целого числа - в виде Z
        ///     Если числитель меньше знаменателя - в виде N:D, где : обозначает знак деления
        ///     В противном случае в полной форме Z.N:D, где . отделяет целую часть
        /// </summary>
        /// <returns>Строковое представление дроби</returns>
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
            result = (Rational) 0;
            const string pattern = @"^[-]?\d+([.]?\d+[:]\d+|[:]\d+)?$";
            var regex = new Regex(pattern, RegexOptions.Compiled);

            if (!regex.IsMatch(input))
            {
                return false;
            }

            // Здесь уже только целые числа
            var nums = (from object token in Regex.Matches(input, @"\d+")
                select Convert.ToInt32(token.ToString())).ToList();
            result.Denominator = nums.Count > 1 ? nums[nums.Count - 1] : 1;
            result.Numerator = nums.Count > 2 ? nums[0] * result.Denominator + nums[1] : nums[0];
            result.Numerator *= Regex.IsMatch(input, @"[-]") ? -1 : 1;
            return result.Denominator != 0; // Проверка на ноль в знаменателе
        }

        public static Rational operator +(Rational first, Rational second)
        {
            return first.Add(second);
        }

        public static Rational operator +(Rational first, int second)
        {
            var secondRational = (Rational) second;
            return first + secondRational;
        }

        public static Rational operator +(int first, Rational second)
        {
            var firstRational = (Rational) first;
            return firstRational + second;
        }

        public static Rational operator -(Rational first, Rational second)
        {
            return first.Add(second.Negate());
        }

        public static Rational operator -(Rational first, int second)
        {
            var secondRational = (Rational) second;
            return first - secondRational;
        }

        public static Rational operator -(int first, Rational second)
        {
            var firstRational = (Rational) first;
            return firstRational - second;
        }

        public static Rational operator *(Rational first, Rational second)
        {
            return first.Multiply(second);
        }

        public static Rational operator *(Rational first, int second)
        {
            var secondRational = (Rational) second;
            return first * secondRational;
        }

        public static Rational operator *(int first, Rational second)
        {
            var firstRational = (Rational) first;
            return firstRational * second;
        }

        public static Rational operator /(Rational first, Rational second)
        {
            return first.DivideBy(second);
        }

        public static Rational operator /(Rational first, int second)
        {
            var secondRational = (Rational) second;
            return first / secondRational;
        }

        public static Rational operator /(int first, Rational second)
        {
            var firstRational = (Rational) first;
            return firstRational / second;
        }

        public static explicit operator int(Rational rational)
        {
            return rational.Base;
        }

        public static implicit operator Rational(int integer)
        {
            return new Rational {Denominator = 1, Numerator = integer};
        }
    }
}