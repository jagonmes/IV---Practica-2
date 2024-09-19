using Patterns.Command.Interfaces;
using UnityEngine;

namespace Patterns.Command.Commands
{
    public class MoveBack : ICommand
    {
        private IMoveableReceiver receiver;

        public MoveBack(IMoveableReceiver receiver)
        {
            this.receiver = receiver;
        }

        public void Execute()
        {
            receiver.Move(Vector3.back);
        }

        public void Undo()
        {
            receiver.Move(Vector3.forward);
        }
    }
}