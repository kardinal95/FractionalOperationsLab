using System;
using System.Collections.Generic;
using System.Linq;

namespace FractionalOperationsLab.Commands
{
    class SubCommand : CalcBaseCommand
    {
        public override string Name => "sub";
        public override string ShortDescription => "вычесть дроби";
        public override string Description => "Вычитает рациональные дроби и выводит результат.";

        public override string Usage => "\'sub x y ...\', где x, y, ... - рациональные дроби.\n" +
                                        "Рациональные дроби могут быть представлены как в виде числа (0, 23, -100), так и в специфическом виде.\n" +
                                        "Специфический вид подразумевает шаблон Z.N:D, где Z - целая часть (может отсутствовать), " +
                                        "N - числитель, D - знаменатель. Z, N, D - целые! Перед числом может стоять знак \"-\".";

        protected override void Calculate(List<Rational> args)
        {
            var result = args.Aggregate((current, next) => current + next.Negate());
            Console.WriteLine("Результат вычитания: {0}", result);
        }
    }
}