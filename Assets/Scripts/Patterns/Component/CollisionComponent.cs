using Patterns.Component.Interfaces;
using UnityEngine;
namespace Patterns.Component
{
    public class CollisionComponent:ICollisionComponent
    {
        public void ProcessCollisions(AGObject obj, AGObject other)
        {
            if (!other.Equals(obj))
            {
                Vector3 collisionVector = other.position - obj.position;
                float distance = collisionVector.magnitude;
                if (distance < obj.size)
                {
                    Vector3 un = collisionVector.normalized;
                    Vector3 ut = new Vector3(-un.y, un.x, 0);
                    
                    float v1n = Vector3.Dot(un, obj.direction);
                    float v1t = Vector3.Dot(ut, obj.direction);
                    float v2n = Vector3.Dot(un, other.direction);
                    float v2t = Vector3.Dot(ut, other.direction);

                    float v1t_after = v1t;
                    float v2t_after = v2t;
                    float v1n_after = v2n;
                    float v2n_after = v1n;

                    obj.direction = (v1n_after * un) + (v1t_after * ut);
                    other.direction = (v2n_after * un) + (v2t_after * ut);

                    float sep = (distance / 2) - obj.halfSize;
                    obj.position += collisionVector.normalized * sep;
                    other.position -= collisionVector.normalized * sep;
                }      
            }
        }
    }
}