using System;
using UnityEngine;

namespace Patterns.ObjectPool.UnityObjectPool
{
    public class SnowFlake : MonoBehaviour
    {
        [HideInInspector] public float speed = 1f;
        public EventHandler OnFloorReached;
        
        private void Update()
        {
            if (transform.position.y < 0 || Physics.CheckSphere(transform.position, 0.25f,
                    LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore))
            {
                OnFloorReached?.Invoke(this, null);
            }
        }

        private void FixedUpdate()
        {
            transform.position += Vector3.down * (speed * Time.fixedDeltaTime);
        }
    }
}