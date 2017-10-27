using System;

namespace FractionalOperationsLab.Commands
{
    class DivCommand : ICommand
    {
        public string Name => "div";
        public string ShortDescription => "делит две дроби";
        public string Description => "Делит две рациональные дроби и выводит результат.";

        public string Usage => "\'div x y\', где x, y - рациональные дроби.\n" +
                               "Рациональные дроби могут быть представлены как в виде числа (0, 23, -100), так и в специфическом виде.\n" +
                               "Специфический вид подразумевает шаблон Z.N:D, где Z - целая часть (может отсутствовать), " +
                               "N - числитель, D - знаменатель. Z, N, D - целые! Перед числом может стоять знак \"-\".";

        public void Execute(params string[] arguments)
        {
            if (arguments.Length == 2)
            {
                if (Rational.TryParse(arguments[0], out var first) &&
                    Rational.TryParse(arguments[1], out var second))
                {
                    if (second.Numerator != 0)
                    {
                        Console.WriteLine("Результат деления: {0}", first/second);
                        return;
                    }
                    Console.WriteLine("Ошибка - деление на ноль невозможно!");
                }
                else
                {
                    Console.WriteLine("Ошибка - некорректный ввод чисел!");
                }
            }
            else
            {
                Console.WriteLine("Ошибка - некорректное количество аргументов!");
            }
            Console.WriteLine("Попробуйте \'usage mul\'");
        }
    }
}