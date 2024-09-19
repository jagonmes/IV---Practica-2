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
using Patterns.ServiceLocator.Components.Data;
using Patterns.ServiceLocator.Components.Interfaces;
using Patterns.ServiceLocator.Interfaces;
using UnityEngine;

namespace Patterns.ServiceLocator.Components
{
    public class PlayerStateManager : ASingleton<PlayerStateManager>
    {
        public float gold;
        
        private Vector3 _lastPosition;
        IGameDataSaver _gameDataSaver;

        private string _storage = "player_data";
        
        private EventHandler<float> _onGoldChanged;
        public event EventHandler<float> OnGoldChanged
        {
            add
            {
                _onGoldChanged += value;
                value.Invoke(this, gold);
            }

            remove
            {
                _onGoldChanged -= value;
            }
        }

        private void Awake()
        {
            base.Awake();
            
            _gameDataSaver = ServiceLocator.Instance.GetService<IGameDataSaver>();
            
            if (_gameDataSaver.Load<PlayerData>(_storage, out PlayerData data))
            {
                gold = data.goldAmount;
                transform.position = new Vector3(data.positionX, data.positionY, data.positionZ);
            }

            _lastPosition = transform.position;
        }

        public void OnTriggerEnter(Collider other)
        {
            IItem pickable = other.GetComponent<IItem>();
            
            if(pickable != null) 
            {
                gold += pickable.Pick();
                _onGoldChanged?.Invoke(this, gold);
                Destroy(pickable.GetGameObject());
                SavePlayerData();
            }
        }

        public void Update()
        {
            if((transform.position - _lastPosition).sqrMagnitude > 0.01)
            {
                _lastPosition = transform.position;
                SavePlayerData();
            }
        }
        
        private void SavePlayerData()
        {
            PlayerData playerData = new PlayerData()
            {
                goldAmount = gold,
                positionX = transform.position.x,
                positionY = transform.position.y,
                positionZ = transform.position.z
            };
            
            _gameDataSaver.Save<PlayerData>(_storage, playerData);
        }
    }
}