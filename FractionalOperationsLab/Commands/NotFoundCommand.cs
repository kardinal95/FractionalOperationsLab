using System;

namespace FractionalOperationsLab.Commands
{
    class NotFoundCommand : ICommand
    {
        private readonly Handler handler;
        public string Name { get; set; }
        public string ShortDescription => "команда не найдена";
        public string Description => "";
        public string Usage => "";

        public NotFoundCommand(Handler handler)
        {
            this.handler = handler;
        }

        public void Execute(params string[] arguments)
        {
            Console.WriteLine("Ошибка - команда {0} не найдена!", Name);
        }
    }
}