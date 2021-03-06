﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FractionalOperationsLab.Commands
{
    class DivCommand : CalcBaseCommand
    {
        public override string Name => "div";
        public override string ShortDescription => "поделить дроби";
        public override string Description => "Делит рациональные дроби и выводит результат.";

        public override string Usage => "\'div x y ...\', где x, y, ... - рациональные дроби.\n" +
                                        "Рациональные дроби могут быть представлены как в виде числа (0, 23, -100), так и в специфическом виде.\n" +
                                        "Специфический вид подразумевает шаблон Z.N:D, где Z - целая часть (может отсутствовать), " +
                                        "N - числитель, D - знаменатель. Z, N, D - целые! Перед числом может стоять знак \"-\".";

        protected override void Calculate(List<Rational> args)
        {
            try
            {
                var result = args.Aggregate((current, next) => current / next);
                Console.WriteLine("Результат деления: {0}", result);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Ошибка - деление на ноль!");
            }
        }
    }
}