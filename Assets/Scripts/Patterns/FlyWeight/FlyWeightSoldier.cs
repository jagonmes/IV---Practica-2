using UnityEngine;
using UnityEngine.Assertions;

namespace Patterns.FlyWeight
{
    public class FlyWeightSoldier
    {
        private Camera cam;
        private float speed = 300f;
        private float size;
        private float halfSize;

        private float maxHealth = 300f;
        
        private byte[] soldierData;
        public string soldierType;

        public FlyWeightSoldier(string type, int MBOfData)
        {
            Assert.IsTrue(type=="RedArmy" || type=="WhiteArmy");
            soldierType = type;
            cam = GameObject.FindObjectOfType<Camera>();
            size = (Mathf.Rad2Deg * Screen.height) / (175f * GameObject.FindObjectOfType<Camera>().fieldOfView);
            halfSize = size / 2;
        }

        public void Create(Soldier soldier)
        {
            Color color = Color.red;
            if (soldierType == "WhiteArmy")
            {
                color = Color.white;
            }
            
            soldier.gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            soldier.gameObject.GetComponent<Renderer>().material.color = color;
            Object.Destroy(soldier.gameObject.GetComponent<SphereCollider>());

            soldier.direction = new Vector3((Random.value * 2f) - 1f, (Random.value * 2f) - 1f, 0f);
            soldier.position = new Vector3(Random.value * Screen.width, Random.value * Screen.height, 175f);

            soldier.currentHealth = maxHealth;
        }
        
        public void Move(float deltaTime, Soldier soldier)
        {
            if (soldier.position.x < halfSize)
            {
                soldier.direction.x = -soldier.direction.x;
                soldier.position.x = halfSize;

            } else if (soldier.position.x > Screen.width - halfSize)
            {
                soldier.direction.x = -soldier.direction.x;
                soldier.position.x = Screen.width - halfSize;
            }
            
            if (soldier.position.y < halfSize)
            {
                soldier.direction.y = -soldier.direction.y;
                soldier.position.y = halfSize;

            } else if (soldier.position.y > Screen.height - halfSize)
            {
                soldier.direction.y = -soldier.direction.y;
                soldier.position.y = Screen.height - halfSize;
            }

            soldier.position += (soldier.direction * (speed * deltaTime));
        }
        
        public void ProcessCollissions(Soldier thisSoldier, Soldier other)
        {
            if (!other.Equals(this))
            {
                Vector3 collisionVector = other.position - thisSoldier.position;
                float distance = collisionVector.magnitude;
                if (distance < size)
                {
                    Vector3 un = collisionVector.normalized;
                    Vector3 ut = new Vector3(-un.y, un.x, 0);
                    
                    float v1n = Vector3.Dot(un, thisSoldier.direction);
                    float v1t = Vector3.Dot(ut, thisSoldier.direction);
                    float v2n = Vector3.Dot(un, other.direction);
                    float v2t = Vector3.Dot(ut, other.direction);

                    float v1t_after = v1t;
                    float v2t_after = v2t;
                    float v1n_after = v2n;
                    float v2n_after = v1n;

                    thisSoldier.direction = (v1n_after * un) + (v1t_after * ut);
                    other.direction = (v2n_after * un) + (v2t_after * ut);

                    float sep = (distance / 2) - halfSize;
                    thisSoldier.position += collisionVector.normalized * sep;
                    other.position -= collisionVector.normalized * sep;
                    
                    if (thisSoldier.GetArmy() != other.GetArmy())
                    {
                        thisSoldier.currentHealth -= 25f;
                        other.currentHealth -= 25f;
                        if (other.currentHealth <= 0)
                        {
                            other.isAlive = false;
                            other.gameObject.SetActive(false);
                        }

                        if (thisSoldier.currentHealth <= 0)
                        {
                            thisSoldier.isAlive = false;
                            thisSoldier.gameObject.SetActive(false);
                        }
 
                    }
                }      
            }
        }
        
        public void Render(Soldier soldier)
        {
            soldier.gameObject.transform.position = cam.ScreenToWorldPoint(soldier.position);
        }
    }
}