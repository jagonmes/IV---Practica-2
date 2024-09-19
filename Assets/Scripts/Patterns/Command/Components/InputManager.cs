#region Copyright
// MIT License
// 
// Copyright (c) 2023 David María Arribas
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using Patterns.Command.Commands;
using Patterns.Command.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace Patterns.Command.Components
{
    public class InputManager : MonoBehaviour
    {
        private IMoveableReceiver _currentPlayer;
        private IMoveableReceiver _player1;
        private IMoveableReceiver _player2;
        private int _currentPlayerId;

        public int MovesPerPlayer = 2;
        public int _currentPlayerMoves = 0;

        public uint _currentTurn = 1;
        
        private CommandManager _commandManager;

        private void Awake()
        {
            _commandManager = new CommandManager();
            
            GameObject player = GameObject.FindWithTag("Player");
            Assert.IsNotNull(player, "Player tagged game object not found");
            
            GameObject player2 = GameObject.Find("Player2");
            Assert.IsNotNull(player2, "Player2 game object not found");
            
            _player1 = player.GetComponent<Player>();
            Assert.IsNotNull(_player1, "Player 1 should have Player component");
            
            _player2 = player2.GetComponent<Player>();
            Assert.IsNotNull(_player2, "Player 2 should have Player component");
            
            _currentPlayer = _player1;
            Assert.IsNotNull(_currentPlayer, "Current player should have Player component");

            _currentPlayerMoves = MovesPerPlayer;
            _currentPlayerId = 1;
        }

        private void Update()
        {
            if (_currentPlayer.IsMoving && Input.anyKey)
            {
                return;
            }

            if (_currentPlayerMoves == 0)
            {
                switch (_currentPlayerId)
                {
                    case 1:
                        _currentPlayer = _player2;
                        _currentPlayerId = 2;
                        _currentPlayerMoves = MovesPerPlayer;
                        break;
                    case 2:
                        _currentPlayer = _player1;
                        _currentPlayerId = 1;
                        _currentPlayerMoves = MovesPerPlayer;
                        break;
                }

                _currentTurn++;
            }
            
            if (_currentPlayerMoves > MovesPerPlayer)
            {
                switch (_currentPlayerId)
                {
                    case 1:
                        _currentPlayer = _player2;
                        _currentPlayerId = 2;
                        _currentPlayerMoves = MovesPerPlayer - 1;
                        break;
                    case 2:
                        _currentPlayer = _player1;
                        _currentPlayerId = 1;
                        _currentPlayerMoves = MovesPerPlayer - 1;
                        break;
                }

                _currentTurn--;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                ICommand command = new MoveForward(_currentPlayer);
                _commandManager.ExecuteCommand(command);
                _currentPlayerMoves--;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                ICommand command = new MoveBack(_currentPlayer);
                _commandManager.ExecuteCommand(command);
                _currentPlayerMoves--;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                ICommand command = new MoveLeft(_currentPlayer);
                _commandManager.ExecuteCommand(command);
                _currentPlayerMoves--;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                ICommand command = new MoveRight(_currentPlayer);
                _commandManager.ExecuteCommand(command);
                _currentPlayerMoves--;
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                _commandManager.UndoCommand();
                if (_currentTurn > 1)
                {
                    _currentPlayerMoves++; 
                }
                else if (_currentPlayerMoves < MovesPerPlayer)
                {
                    _currentPlayerMoves++; 
                }
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                _commandManager.RedoCommand();
                _currentPlayerMoves--;
            }

        }
    }
}