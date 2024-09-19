using Patterns.Prototype.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace Patterns.Prototype
{
    public abstract class AbstractSoldier : IPrototypeSoldier
    {        
        Camera cam;
        public GameObject gameObject;
        public bool isAlive;

        private float _distance = 175f;
        private float _speed = 300f;
        private float _size = 0f;
        private float _halfSize = 0f;
        private string _soldierType;

        private Vector2 _position;
        public Vector2 position
        {
            get { return _position; }
            set { _position = value; }
        }
        
        private Vector2 _direction;
        public Vector2 direction
        {
            get { return _direction; }
            set { _direction = value;  }
        }

        public AbstractSoldier(string type)
        {
            Assert.IsTrue(type=="RedArmy" || type=="WhiteArmy" || type=="BlueArmy");
            _soldierType = type;
            isAlive = true;
        }

        public bool IsAlive()
        {
            return isAlive;
        }

        public string GetArmy()
        {
            return _soldierType;
        }

        public abstract IPrototypeSoldier Clone();
        
        public void Create()
        {
            isAlive = true;
            cam = GameObject.FindObjectOfType<Camera>();
            gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gameObject.SetActive(false);
            
            Color color = (_soldierType == "WhiteArmy") ? Color.white : Color.red;
            color = (_soldierType == "BlueArmy") ? Color.blue : color;
            gameObject.GetComponent<Renderer>().material.color = color;
            GameObject.Destroy(gameObject.GetComponent<SphereCollider>());

            _size = (Mathf.Rad2Deg * Screen.height) / (175f * GameObject.FindObjectOfType<Camera>().fieldOfView);
            _halfSize = _size / 2;
            direction = new Vector2((Random.value * 2f) - 1f, (Random.value * 2f) - 1f);
            position = new Vector2(Random.value * Screen.width, Random.value * Screen.height);
        }

        public void Move(float deltaTime)
        {
            if (position.x < _halfSize)
            {
                _direction.x = -direction.x;
                _position.x = _halfSize;

            } else if (position.x > Screen.width - _halfSize)
            {
                _direction.x = -direction.x;
                _position.x = Screen.width - _halfSize;
            }
            
            if (position.y < _halfSize)
            {
                _direction.y = -direction.y;
                _position.y = _halfSize;

            } else if (position.y > Screen.height - _halfSize)
            {
                _direction.y = -direction.y;
                _position.y = Screen.height - _halfSize;
            }

            position += (direction * (_speed * deltaTime));
        }
        
        public bool ProcessCollissions(IPrototypeSoldier otherSoldier)
        {
            if (!otherSoldier.Equals(this))
            {
                Vector2 collisionVector = otherSoldier.position - this.position;
                float distance = collisionVector.magnitude;
                if (distance < _size)
                {
                    if (GetArmy() != otherSoldier.GetArmy())
                    {
                        Kill();
                        otherSoldier.Kill();
                        return false;
                    }
                    
                    Vector2 un = collisionVector.normalized;
                    Vector2 ut = new Vector3(-un.y, un.x, 0);
                
                    float v1n = Vector2.Dot(un, this.direction);
                    float v1t = Vector2.Dot(ut, this.direction);
                    float v2n = Vector2.Dot(un, otherSoldier.direction);
                    float v2t = Vector2.Dot(ut, otherSoldier.direction);

                    float v1t_after = v1t;
                    float v2t_after = v2t;
                    float v1n_after = v2n;
                    float v2n_after = v1n;

                    direction = (v1n_after * un) + (v1t_after * ut);
                    otherSoldier.direction = (v2n_after * un) + (v2t_after * ut);

                    float sep = (distance / 2) - _halfSize;
                    position += collisionVector.normalized * sep;
                    otherSoldier.position -= collisionVector.normalized * sep;

                    return true;
                }      
            }

            return false;
        }

        public void Render()
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            Vector3 pos = new Vector3(_position.x, _position.y, _distance);
            gameObject.transform.position = cam.ScreenToWorldPoint(pos);
        }

        public void Kill()
        {
            isAlive = false;
            Object.Destroy(gameObject);
        }
    }
}