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

using TMPro;
using UnityEngine;

namespace Patterns.ScriptableObjects_Flyweight.Components
{
    public class EnemyController : MonoBehaviour
    {

        [SerializeField] private Enemy tipo;
        private GameObject _player;
        private MeshRenderer _shape;
        private TextMeshPro _nameText;
        private float _speed = 0.5f;
        
        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _nameText = transform.Find("Name").GetComponent<TextMeshPro>();
            _shape = transform.Find("Shape").GetComponent<MeshRenderer>();

            _nameText.text = $"{tipo.Name}";
            _shape.material.color = tipo.SkinColor;
            _speed = tipo.SearchSpeed;
        }

        private void FixedUpdate()
        {
            Vector3 playerDirection = _player.transform.position - transform.position;
            playerDirection.y = 0;
            transform.position += playerDirection.normalized * (_speed * Time.fixedDeltaTime);
        }
    }
}