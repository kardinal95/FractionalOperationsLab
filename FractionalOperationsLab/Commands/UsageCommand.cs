using System;

namespace FractionalOperationsLab.Commands
{
    class UsageCommand : ICommand
    {
        private readonly Handler _handler;
        public string Name => "usage";
        public string ShortDescription => "вывести информацию о комманде";
        public string Description => "Выводит полную информацию о команде и ее использование.";

        public string Usage =>
            "\'usage command\', где command - имя команды для получения информации.";

        public UsageCommand(Handler handler)
        {
            _handler = handler;
        }

        public void Execute(params string[] arguments)
        {
            if (arguments.Length == 1)
            {
                if (_handler.CommandMap.ContainsKey(arguments[0]))
                {
                    var command = _handler.CommandMap[arguments[0]];
                    Console.WriteLine("Имя комманды - {0}", command.Name);
                    Console.WriteLine(command.Description);
                    Console.WriteLine("Использование - {0}", command.Usage);
                    return;
                }
                Console.WriteLine("Ошибка - команда {0} не найдена!", arguments[0]);
            }
            else
            {
                Console.WriteLine("Ошибка - неправильно введены аргументы для функции!");
            }
            Console.WriteLine("Попробуйте \"usage usage\"");
        }
    }
}