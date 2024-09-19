
using System.Runtime.Serialization;

namespace Patterns.DirtyFlag.Interfaces
{
    public interface ISaveableGameObject
    {
        public bool IsDirty();
        public object GetData();
        public void Restore(object data); 
    }
}
