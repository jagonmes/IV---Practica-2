using Patterns.Command.Interfaces;
using UnityEngine;

namespace Patterns.Command.Commands
{
    public class MoveLeft : ICommand
    {
        private IMoveableReceiver receiver;
        
        public MoveLeft(IMoveableReceiver receiver)
        {
            this.receiver = receiver;
        }
        
        public void Execute()
        {
            receiver.Move(Vector3.left);
        }

        public void Undo()
        {
            receiver.Move(Vector3.right);
        }
    }
}