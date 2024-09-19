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

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Patterns.Singleton.Components
{
    public class StateSaver : MonoBehaviour
    {
        public string saveFileName = "savedata.save";
        public float saveTimeInterval = 5.0f;
        private float _timeCounter = 0;
        private bool _gameRestored = false;

        private string FileName
        {
            get => $"{Application.dataPath}/{saveFileName}";
        }

        private void Start()
        {
            _timeCounter = saveTimeInterval;
        }
        
        private void Update()
        {
            if (!_gameRestored)
            {
                RestoreGameState();
                _gameRestored = true;
            }
            
            _timeCounter -= Time.deltaTime;
            
            if (_timeCounter <= 0)
            {
                _timeCounter = saveTimeInterval;

                if (GameState.Instance.IsDirty)
                {
                    SaveGameSate();
                }
            }
        }

        private void SaveGameSate()
        {
            object playerData = GameState.Instance.GetData();
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Create(FileName);
            formatter.Serialize(fileStream, playerData);
            fileStream.Flush();
            fileStream.Close();
        }
        
        private void RestoreGameState()
        {
            if (File.Exists(FileName))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fileStream = File.OpenRead(FileName);
                object playerData = formatter.Deserialize(fileStream);
                fileStream.Close();
                GameState.Instance.Restore(playerData);
            }
            else
            {
                Debug.LogWarning($"Saved state file {FileName} does not exist.");
            }
        }
        
         
    }
}