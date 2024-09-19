using Patterns.Command.Interfaces;
using UnityEngine;

namespace Patterns.Command.Commands
{
    public class MoveRight : ICommand
    {
        private IMoveableReceiver receiver;

        public MoveRight(IMoveableReceiver receiver)
        {
            this.receiver = receiver;
        }
        
        public void Execute()
        {
            receiver.Move(Vector3.right);
        }

        public void Undo()
        {
            receiver.Move(Vector3.left);
        }
    }
}