using Patterns.ObjectPool.Interfaces;
using UnityEngine;

namespace Patterns.ObjectPool.Components
{
    public class SnowFlake : MonoBehaviour, IPooleableObject
    {
        [HideInInspector] public float speed = 1f;
        public IObjectPool pool;
        
        private void Update()
        {
            if (transform.position.y < 0 || Physics.CheckSphere(transform.position, 0.25f,
                    LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore))
            {
                pool?.Release(this);
            }
        }

        private void FixedUpdate()
        {
            transform.position += Vector3.down * (speed * Time.fixedDeltaTime);
        }

        public bool Active
        {
            get
            {
                return gameObject.activeSelf;
            }
            
            set
            {
                gameObject.SetActive(value);
            }
        }

        public void Reset()
        {
            transform.localPosition = Vector3.zero;
            // Debug.Log($"New SnowFlake initialized at: {transform.position}");
        }

        public void Destroy()
        {
            Object.Destroy(this.gameObject);
        }

        public IPooleableObject Clone()
        {
            GameObject newObject = Instantiate(gameObject);
            SnowFlake snowFlake = newObject.GetComponent<SnowFlake>();
            return snowFlake; 
        }
    }
}