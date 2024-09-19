namespace Patterns.DirtyFlag.Components.Interfaces
{
    public interface IObserver<T>
    {
        public void UpdateObserver(T data);
    }
}