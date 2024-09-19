using Patterns.ObjectPool.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace Patterns.ObjectPool.Components
{
    public class Storm : MonoBehaviour
    {
        public MonoBehaviour snowFlakePrototype;
        public int initialNumberOfSnowFlakes = 1;  
        public bool allowAddNewSnowFlakes = false;
        public bool snowing = false;
        public float snowFlakesPerSecond;
        public float maxSnowFlakeSpeed = 1f;
        public float spreadAreaExtent = 30f;

        private float _halfSpreadAreaExtent;
        private float _lastSnowFlake;
        private ObjectPool _snowFlakesPool;

        private AudioSource _audioSource;
        

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            Assert.IsTrue(snowFlakePrototype is IPooleableObject);
            _snowFlakesPool = new ObjectPool((IPooleableObject)snowFlakePrototype, initialNumberOfSnowFlakes, allowAddNewSnowFlakes);
            _halfSpreadAreaExtent = spreadAreaExtent / 2;
        }

        private void Update()
        {
            if (snowing)
            {
                if (!_audioSource.isPlaying)
                {
                    _audioSource.Play();    
                }

                
                _lastSnowFlake += Time.deltaTime;
                if (_lastSnowFlake >= 1)
                {
                    for (int i = 0; i < snowFlakesPerSecond; i++)
                    {
                        SnowFlake snowFlake = CreateSnowFlake();
                    }
                }
            }
            else
            {
                if (_audioSource.isPlaying)
                {
                    _audioSource.Stop();
                }
                
                _snowFlakesPool.eliminarExcedente(initialNumberOfSnowFlakes);
            }
        }

        private SnowFlake CreateSnowFlake()
        {
            SnowFlake snowFlake = (SnowFlake) _snowFlakesPool.Get();

            if (snowFlake)
            {
                snowFlake.pool = _snowFlakesPool;
                float xDisp = Random.value * spreadAreaExtent - _halfSpreadAreaExtent;
                float zDisp = Random.value * spreadAreaExtent - _halfSpreadAreaExtent;
                snowFlake.transform.localPosition = Vector3.zero + Vector3.right * xDisp + Vector3.forward * zDisp;
                snowFlake.speed = 1 + (Random.value * (maxSnowFlakeSpeed - 1));
                snowFlake.transform.SetParent(transform, false);
            }

            return snowFlake;
        }

        private void OnGUI()
        {
            GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
            guiStyle.fontSize = 22;
            guiStyle.fontStyle = FontStyle.Bold;
            guiStyle.normal.textColor = Color.black;

            GUI.Label(new Rect(10, 10, 300, 30), $"Total pool size: {_snowFlakesPool.GetCount()}", guiStyle);
            GUI.Label(new Rect(10, 40, 300, 30), $"Active pool size: {_snowFlakesPool.GetActive()}", guiStyle);
        }
    }
}