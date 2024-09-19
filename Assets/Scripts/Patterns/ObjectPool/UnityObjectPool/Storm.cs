using System;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Patterns.ObjectPool.UnityObjectPool
{
    public class Storm : MonoBehaviour
    {
        public SnowFlake snowFlakePrototype;
        public int initialNumberOfSnowFlakes = 1;
        public int maxNumberOfSnowFlakes = 10;
        public bool snowing = false;
        public float snowFlakesPerSecond;
        public float maxSnowFlakeSpeed = 1f;
        public float spreadAreaExtent = 30f;

        private float _halfSpreadAreaExtent;
        private float _lastSnowFlake;
        private ObjectPool<SnowFlake> _snowFlakesPool;

        private void Awake()
        {
            _snowFlakesPool = new ObjectPool<SnowFlake>(CreateSnowFlake, GetSnowFlake, ReleaseSnowFlake, DestroySnowFlake,false, initialNumberOfSnowFlakes, maxNumberOfSnowFlakes);
            _halfSpreadAreaExtent = spreadAreaExtent / 2;
        }

        private void DestroySnowFlake(SnowFlake obj)
        {
            Destroy(obj.gameObject);
        }

        private SnowFlake CreateSnowFlake()
        {
            SnowFlake snowFlake = Instantiate(snowFlakePrototype);
            return snowFlake;
        }
        
        private void GetSnowFlake(SnowFlake obj)
        {
            obj.gameObject.SetActive(true);
            
            float xDisp = Random.value * spreadAreaExtent - _halfSpreadAreaExtent;
            float zDisp = Random.value * spreadAreaExtent - _halfSpreadAreaExtent;
            obj.transform.localPosition = Vector3.zero + Vector3.right * xDisp + Vector3.forward * zDisp;
            obj.speed = 1 + (Random.value * (maxSnowFlakeSpeed - 1));
            obj.transform.SetParent(transform, false);
            obj.OnFloorReached = OnFloorReached;
        }

        private void OnFloorReached(object sender, EventArgs e)
        {
            SnowFlake snowFlake = (SnowFlake)sender;
            _snowFlakesPool.Release(snowFlake);
        }

        private void ReleaseSnowFlake(SnowFlake obj)
        {
            obj.gameObject.SetActive(false);
        }
        
        private void Update()
        {
            if (snowing)
            {
                _lastSnowFlake += Time.deltaTime;
                if (_lastSnowFlake >= 1)
                {
                    for (int i = 0; i < snowFlakesPerSecond; i++)
                    {
                        _snowFlakesPool.Get();
                    }
                }
            }
        }
        
        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 300, 30), $"Total pool size: {_snowFlakesPool.CountAll}");
            GUI.Label(new Rect(10, 40, 300, 30), $"Active pool size: {_snowFlakesPool.CountActive}");
        }
    }
}