namespace Patterns.Command.Interfaces
{
    public interface ICommand
    {
        public void Execute();
        public void Undo();
    }
}