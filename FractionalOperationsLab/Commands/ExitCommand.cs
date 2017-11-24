using System;

namespace FractionalOperationsLab.Commands
{
    class ExitCommand : ICommand
    {
        private readonly Handler handler;
        public string Name => "exit";
        public string ShortDescription => "завершить работу с приложением";
        public string Description => "Завершает работу с текущим приложением.";
        public string Usage => "\'exit\'.";

        public ExitCommand(Handler handler)
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
            handler.Running = false;
        }
    }
}