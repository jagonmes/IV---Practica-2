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

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Patterns.DirtyFlag.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Patterns.DirtyFlag
{
    public class GameStateSaver
    {
        private bool _gameStateDirty = false;
        private Dictionary<string, object> objects;
        
        private GameObject guardando;

        public void SetDirty()
        {
            _gameStateDirty = true;
        }
        
        public void Save(Scene scene, string saveFile)
        {
            if (guardando == null)
            {
                GameObject.Find("Guardando");
            }
            
            

            Debug.Log("Saving Scene");
            objects = new Dictionary<string, object>();
            
            GameObject[] sceneObjects = scene.GetRootGameObjects();
            for (int i = 0; i < sceneObjects.Length; i++)
            {
                GameObject sceneObject = sceneObjects[i];
                ISaveableGameObject obj = sceneObject.GetComponent<ISaveableGameObject>();
                if (obj != null && obj.IsDirty())
                {
                    SetDirty();
                    objects[sceneObject.name] = obj.GetData();
                }
            }

            if (_gameStateDirty)
            {
                if (guardando != null)
                {
                    Debug.Log("EXISTE LA SEÑAL");
                    guardando.SetActive(true);
                }

                
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fileStream = File.Create(saveFile);
                formatter.Serialize(fileStream, objects);
                fileStream.Flush();
                fileStream.Close();
                
                _gameStateDirty = false;
                if (guardando != null)
                    guardando.SetActive(false);
            }
            else
            {
                Debug.Log("No need to save, game state not dirty");
            }
        }

        public void Restore(string saveFile)
        {
            if (File.Exists(saveFile))
            {
                Debug.Log($"{saveFile} found, starting restore.");
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fileStream = File.OpenRead(saveFile);
                objects = (Dictionary<string, object>)formatter.Deserialize(fileStream);
                fileStream.Close();

                foreach (var objectData in objects)
                {
                    GameObject gameObject = GameObject.Find(objectData.Key);
                    if (gameObject != null)
                    {
                        ISaveableGameObject saveableGameObject = gameObject.GetComponent<ISaveableGameObject>();
                        if (saveableGameObject != null)
                        {
                            saveableGameObject.Restore(objectData.Value);
                            Debug.Log($"Restored {objectData.Key}: {JsonUtility.ToJson(objectData.Value)}");
                        }
                        else
                        {
                            Debug.LogError($"{objectData.Key} is not saveable.");
                        }
                            
                    }
                }
            }
            else
            {
                Debug.Log($"Save file {saveFile} not found");
            }
        }
    }
}