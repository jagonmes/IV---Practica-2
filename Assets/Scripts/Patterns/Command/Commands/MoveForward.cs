using Patterns.Command.Interfaces;
using UnityEngine;

namespace Patterns.Command.Commands
{
    public class MoveForward : ICommand
    {
        private IMoveableReceiver receiver;

        public MoveForward(IMoveableReceiver receiver)
        {
            this.receiver = receiver;
        }
        
        public void Execute()
        {
            receiver.Move(Vector3.forward);
        }

        public void Undo()
        {
            receiver.Move(Vector3.back);
        }
    }
}