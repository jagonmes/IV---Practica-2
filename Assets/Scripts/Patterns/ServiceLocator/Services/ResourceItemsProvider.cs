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
using System.Linq;
using Patterns.ServiceLocator.Components.Interfaces;
using Patterns.ServiceLocator.Interfaces;
using UnityEngine;

namespace Patterns.ServiceLocator.Services
{
    public class ResourceItemsProvider : IItemsProvider
    {
        private Dictionary<string, IItem> _items;

        public void Initialize()
        {
            _items = new Dictionary<string, IItem>();
            
            GameObject[] gameObjects = Resources.LoadAll<GameObject>("Items");
            foreach (GameObject gameObject in gameObjects)
            {
                IItem item = gameObject.GetComponent<IItem>();
                
                if (item != null)
                {
                    _items.Add(gameObject.name, item);
                    Debug.Log($"ItemsProvider Service: Item {gameObject.name} loaded");
                }
            }
        }

        public IItem Get(string name)
        {
            return _items[name];
        }

        public IItem RandomGet()
        {
            return _items.ElementAt(Random.Range(0, _items.Count)).Value;
        }
    }
}