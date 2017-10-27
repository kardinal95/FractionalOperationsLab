namespace FractionalOperationsLab
{
    interface ICommand
    {
        string Name { get; } // Имя команды в help
        string ShortDescription { get; } // Описание команды в help
        string Description { get; } // Полное описание при выводе usage
        string Usage { get; } // Образец использования при выводе usage
        void Execute(params string[] arguments); // Вызов команды
    }
}