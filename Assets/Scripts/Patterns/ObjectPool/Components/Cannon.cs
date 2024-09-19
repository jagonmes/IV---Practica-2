using System;
using System.Collections;
using Patterns.ObjectPool.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace Patterns.ObjectPool.Components
{
    public class Cannon : MonoBehaviour
    {
        public Bala prototipo;
        public int numeroInicial = 1;  
        public bool permitirNuevos = false;

        private ObjectPool pool;

        private void Start()
        {
            Assert.IsTrue(prototipo is IPooleableObject);
            pool = new ObjectPool((IPooleableObject)prototipo, numeroInicial, permitirNuevos);
        }

        private void Update()
        {
            IPooleableObject bala = pool.Get();
            //Debug.Log("Hace cosas");
            delayedRelease(2, bala);
        }
        
        IEnumerator delayedRelease(float time, IPooleableObject obj)
        {
            yield return new WaitForSeconds(time);
            pool.Release(obj);
        }
    }
}