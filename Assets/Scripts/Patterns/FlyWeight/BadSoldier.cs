using Patterns.FlyWeight.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace Patterns.FlyWeight
{
    public class BadSoldier : ISoldier
    {        
        Camera cam;
        public GameObject gameObject;
        public Vector3 direction;
        public Vector3 position;
        public bool isAlive;
        private float speed = 300f;
        private float size = 0f;
        private float halfSize = 0f;
        private byte[] soldierData;
        private string soldierType;

        public BadSoldier(string type, int MBOfData)
        {
            Assert.IsTrue(type=="RedArmy" || type=="WhiteArmy");
            soldierType = type;
            soldierData = new byte[MBOfData * 1024 * 1024];
        }

        public bool IsAlive()
        {
            return isAlive;
        }

        public string GetArmy()
        {
            return soldierType;
        }

        public void Create()
        {
            isAlive = true;
            cam = GameObject.FindObjectOfType<Camera>();
            gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            
            Color color = Color.red;
            if (soldierType == "WhiteArmy")
            {
                color = Color.white;
            }
            
            gameObject.GetComponent<Renderer>().material.color = color;
            GameObject.Destroy(gameObject.GetComponent<SphereCollider>());

            size = (Mathf.Rad2Deg * Screen.height) / (175f * GameObject.FindObjectOfType<Camera>().fieldOfView);
            halfSize = size / 2;
            direction = new Vector3((Random.value * 2f) - 1f, (Random.value * 2f) - 1f, 0f);
            position = new Vector3(Random.value * Screen.width, Random.value * Screen.height, 175f);
        }

        public void Move(float deltaTime)
        {
            if (position.x < halfSize)
            {
                direction.x = -direction.x;
                position.x = halfSize;

            } else if (position.x > Screen.width - halfSize)
            {
                direction.x = -direction.x;
                position.x = Screen.width - halfSize;
            }
            
            if (position.y < halfSize)
            {
                direction.y = -direction.y;
                position.y = halfSize;

            } else if (position.y > Screen.height - halfSize)
            {
                direction.y = -direction.y;
                position.y = Screen.height - halfSize;
            }

            position += (direction * (speed * deltaTime));
        }
        
        public void ProcessCollissions(ISoldier other)
        {
            BadSoldier otherSoldier = (BadSoldier)other; 
            
            if (!other.Equals(this))
            {
                Vector3 collisionVector = otherSoldier.position - this.position;
                float distance = collisionVector.magnitude;
                if (distance < size)
                {
                    // if (GetArmy() != other.GetArmy())
                    // {
                    //     isAlive = false;
                    //     gameObject.SetActive(false);
                    //     
                    //     otherSoldier.isAlive = false;
                    //     otherSoldier.gameObject.SetActive(false);
                    //     return;
                    // }
                    
                    Vector3 un = collisionVector.normalized;
                    Vector3 ut = new Vector3(-un.y, un.x, 0);
                
                    float v1n = Vector3.Dot(un, this.direction);
                    float v1t = Vector3.Dot(ut, this.direction);
                    float v2n = Vector3.Dot(un, otherSoldier.direction);
                    float v2t = Vector3.Dot(ut, otherSoldier.direction);

                    float v1t_after = v1t;
                    float v2t_after = v2t;
                    float v1n_after = v2n;
                    float v2n_after = v1n;

                    direction = (v1n_after * un) + (v1t_after * ut);
                    otherSoldier.direction = (v2n_after * un) + (v2t_after * ut);

                    float sep = (distance / 2) - halfSize;
                    position += collisionVector.normalized * sep;
                    otherSoldier.position -= collisionVector.normalized * sep;
                }      
            }
        }

        public void Render()
        {
            gameObject.transform.position = cam.ScreenToWorldPoint(position);
        }

    }
}