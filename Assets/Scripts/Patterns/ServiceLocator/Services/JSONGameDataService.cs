﻿#region Copyright
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
using System.IO;
using Patterns.ServiceLocator.Interfaces;
using UnityEngine;

namespace Patterns.ServiceLocator.Services
{
    public class JSONGameDataService : IGameDataSaver
    {
        [Serializable]
        private class JsonSerializableData<T>
        {
            public string className;
            public T data;
        }
        
        private string _dataFolder;
        
        public void Initialize()
        {
            _dataFolder = Application.dataPath + "/Data/JSON";
            if(!Directory.Exists(_dataFolder)) Directory.CreateDirectory(_dataFolder);
        }

        public void Save<T>(string storage, T data)
        {
            string filePath = $"{_dataFolder}/{storage}.json";
            string className = data.GetType().FullName;
            // string jsonData = $"{{\"className\":\"{className}\",\"data\": {JsonUtility.ToJson(data)}}}";
            JsonSerializableData<T> serializableData = new JsonSerializableData<T>
            {
                className = className,
                data = data
            };
            
            string jsonData = JsonUtility.ToJson(serializableData);
            StreamWriter writer = new StreamWriter(filePath);
            writer.Write(jsonData);
            writer.Flush();
            writer.Close();
        }

        public bool Load<T>(string storage, out T data)
        {
            string filePath = $"{_dataFolder}/{storage}.json";
            if(!File.Exists(filePath))
            {
                data = default(T);
                return false;
            }
            
            StreamReader reader = new StreamReader(filePath);
            string jsonData = reader.ReadToEnd();
            reader.Close();
            
            JsonSerializableData<T> serializableData = new JsonSerializableData<T>();
            JsonUtility.FromJsonOverwrite(jsonData, serializableData);
            data = serializableData.data;
            return true;
        }
    }
}