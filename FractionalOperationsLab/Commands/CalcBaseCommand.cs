using System;
using System.Collections.Generic;

namespace FractionalOperationsLab.Commands
{
    /// <summary>
    /// Базовый класс для вычислений
    /// Наследуемые классы должны реализовывать метод Calculate
    /// </summary>
    abstract class CalcBaseCommand : ICommand
    {
        public abstract string Name { get; }
        public abstract string ShortDescription { get; }
        public abstract string Description { get; }
        public abstract string Usage { get; }

        /// <summary>
        /// Проверяет входную строку аргументов на ошибки (некорректные значения)
        /// Выводит их при наличии
        /// При отсутствии приводит входные значение к типу <see cref="Rational"/>
        /// И передает значения в метод <see cref="Calculate"/>
        /// </summary>
        /// <param name="arguments"></param>
        public void Execute(params string[] arguments)
        {
            if (arguments.Length < 2)
            {
                Console.WriteLine("Ошибка - некорректное количество аргументов!");
                return;
            }
            var incorrect = new List<string>();
            var rationals = new List<Rational>();
            foreach (var input in arguments)
            {
                if (!Rational.TryParse(input, out var num))
                {
                    incorrect.Add(input);
                }
                else
                {
                    rationals.Add(num);
                }
            }
            if (incorrect.Count != 0)
            {
                Console.WriteLine("Некорректный ввод: {0}", string.Join(", ", incorrect));
            }
            else
            {
                Calculate(rationals);
            }
        }

        /// <summary>
        /// Реализация метода должна выполнять определенные вычисления
        /// Результат должен быть выведен на консоль
        /// </summary>
        /// <param name="args"></param>
        protected abstract void Calculate(List<Rational> args);
    }
}