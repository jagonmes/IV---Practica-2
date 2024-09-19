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

using System;
using Patterns.Singleton.Components.Data;
using Patterns.Singleton.Components.Interfaces;
using UnityEngine;

namespace Patterns.Singleton
{
    public class GameState : ASingleton<GameState>, ISaveableGameObject
    {
        // C# Observer pattern, this is the subject
        public EventHandler<float> OnGoldChanged;
        public EventHandler<Vector3> OnPlayerPositionChanged;

        // ISaveableGameObject
        public bool IsDirty { get; private set; }

        public bool restaurando = false;
        
        public object GetData()
        {
            PlayerData playerData = new PlayerData()
            {
                goldAmount = _gold,
                positionX = _playerPosition.x,
                positionY = _playerPosition.y,
                positionZ = _playerPosition.z
            };
            
            return playerData;
        }

        public void Restore(object data)
        {
            restaurando = true;
            PlayerData playerData = (PlayerData)data;
            Gold = playerData.goldAmount;
            PlayerPosition = new Vector3(playerData.positionX, playerData.positionY, playerData.positionZ);
            _player.transform.position = _playerPosition;
            restaurando = false;
        }
        
        // Player State Data
        private float _gold;
        private Vector3 _playerPosition;

        public float Gold
        {
            get { return _gold; }

            set
            {
                _gold = value;
                IsDirty = true;
                OnGoldChanged?.Invoke(this, _gold);
            }
        }

        public Vector3 PlayerPosition
        {
            get { return _playerPosition; }
            set
            {
                _playerPosition = value;
                OnPlayerPositionChanged?.Invoke(this, _playerPosition);
            }
        }

        private Transform _player;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        private void Update()
        {
            if ((_playerPosition - _player.position).sqrMagnitude >= 0.70)
            {
                _playerPosition = _player.position;
                IsDirty = true;
            }
        }
    }
}