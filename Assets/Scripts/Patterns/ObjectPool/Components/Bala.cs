using System;
using System.Collections;
using Patterns.ObjectPool.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Patterns.ObjectPool.Components
{
    public class Bala:MonoBehaviour, IPooleableObject
    {
        public IPooleableObject Clone()
        {
            GameObject newObject = Instantiate(gameObject);
            return newObject.GetComponent<Bala>();
        }

        private void Update()
        {
            this.transform.position = this.transform.position + this.transform.forward * Time.deltaTime;
        }

        public bool Active { get; set; }
        public void Reset()
        {
            transform.localPosition = Vector3.zero;
        }

        public void Destroy()
        {
            Object.Destroy(this.gameObject);
        }
        
    }
}