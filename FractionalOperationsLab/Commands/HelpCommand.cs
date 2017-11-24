using System;

namespace FractionalOperationsLab.Commands
{
    class HelpCommand : ICommand
    {
        private readonly Handler handler;
        public string Name => "help";
        public string ShortDescription => "вывести общую справку";
        public string Description => "Выводит список доступных комманд и их краткое описание.";
        public string Usage => "\'help\'.";

        public HelpCommand(Handler handler)
        {
            this.handler = handler;
        }

        public void Execute(params string[] arguments)
        {
            if (arguments.Length != 0)
            {
                Console.WriteLine("Ошибка - вызов команды должен осуществляться без аргументов!");
                return;
            }
            foreach (var command in handler.Commands)
            {
                Console.WriteLine("{0} - {1}", command.Name, command.ShortDescription);
            }
        }
    }
}