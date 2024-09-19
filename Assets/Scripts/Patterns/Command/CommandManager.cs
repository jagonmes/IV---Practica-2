using System.Collections.Generic;
using Patterns.Command.Interfaces;
using UnityEngine;

namespace Patterns.Command
{
    public class CommandManager
    {
        private List<ICommand> executedCommands = new List<ICommand>();
        private int lastExecutedCommand = 0;
        private bool runningCommand = false;

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();

            if (lastExecutedCommand < executedCommands.Count - 1)
            {
                for (int i = executedCommands.Count - 1; i > lastExecutedCommand; i--)
                {
                    executedCommands.RemoveAt(i);
                }
            }
        
            executedCommands.Add(command);
            lastExecutedCommand = executedCommands.Count - 1;
            Debug.Log($"EXECUTED {command.GetType().Name}  [Last executed command: {lastExecutedCommand}]");
        }

        public void UndoCommand()
        {
            if (lastExecutedCommand > -1)
            {
                ICommand lastCommand = executedCommands[lastExecutedCommand];
                lastCommand.Undo();
                lastExecutedCommand -= 1;
                Debug.Log($"UNDONE {lastCommand.GetType().Name} [Last executed command: {lastExecutedCommand}]");
            }
            else
            {
                Debug.Log("No more commands to UNDO");
            }
        }

        public void RedoCommand()
        {
            if (lastExecutedCommand < executedCommands.Count - 1)
            {
                ICommand lastCommand = executedCommands[lastExecutedCommand + 1];
                lastCommand.Execute();
                lastExecutedCommand += 1;
                Debug.Log($"REDONE {lastCommand.GetType().Name} [Last executed command: {lastExecutedCommand}]");
            }
            else
            {
                Debug.Log("No more commands to REDO");
            }
        }
    }
}