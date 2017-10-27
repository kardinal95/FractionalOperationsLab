using System;

namespace FractionalOperationsLab.Commands
{
    class HelpCommand : ICommand
    {
        private readonly Handler _handler;
        public string Name => "help";
        public string ShortDescription => "вывести общую справку";
        public string Description => "Выводит список доступных комманд и их краткое описание.";
        public string Usage => "\'help\'.";

        public HelpCommand(Handler handler)
        {
            _handler = handler;
        }

        public void Execute(params string[] arguments)
        {
            if (arguments.Length == 0)
            {
                foreach (var command in _handler.Commands)
                {
                    Console.WriteLine("{0} - {1}", command.Name, command.ShortDescription);
                }
                return;
            }
            Console.WriteLine("Ошибка - вызов команды должен осуществляться без аргументов!");
            Console.WriteLine("Попробуйте \"usage help\"");
        }
    }
}