using System;

namespace FractionalOperationsLab.Commands
{
    class ExitCommand : ICommand
    {
        private readonly Handler _handler;
        public string Name => "exit";
        public string ShortDescription => "завершить работу с приложением";
        public string Description => "Завершает работу с текущим приложением.";
        public string Usage => "\'exit\'.";

        public ExitCommand(Handler handler)
        {
            _handler = handler;
        }

        public void Execute(params string[] arguments)
        {
            if (arguments.Length == 0)
            {
                _handler.Running = false;
                return;
            }
            Console.WriteLine("Ошибка - вызов команды должен осуществляться без аргументов!");
            Console.WriteLine("Попробуйте \"usage exit\"");
        }
    }
}