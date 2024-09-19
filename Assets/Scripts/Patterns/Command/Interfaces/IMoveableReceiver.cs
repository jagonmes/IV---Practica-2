using UnityEngine;

namespace Patterns.Command.Interfaces
{
    public interface IMoveableReceiver
    {
        public void Move(Vector3 direction);
        public bool IsMoving { get; }
    }
}