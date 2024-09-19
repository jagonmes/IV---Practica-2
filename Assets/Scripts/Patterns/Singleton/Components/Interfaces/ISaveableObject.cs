
namespace Patterns.Singleton.Components.Interfaces
{
    public interface ISaveableGameObject
    {
        public bool IsDirty { get; }
        
        public object GetData();
        public void Restore(object data); 
    }
}
