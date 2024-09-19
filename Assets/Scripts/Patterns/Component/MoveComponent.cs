using Patterns.Component.Interfaces;
using UnityEngine;
namespace Patterns.Component
{
    public class MoveComponent:IMoveComponent
    {
        private float speed = 300f;
        
        public void Move(AGObject objeto, float dTime)
        {
            if (objeto.position.x < objeto.halfSize)
            {
                objeto.direction.x = -objeto.direction.x;
                objeto.position.x = objeto.halfSize;

            } else if (objeto.position.x > Screen.width - objeto.halfSize)
            {
                objeto.direction.x = -objeto.direction.x;
                objeto.position.x = Screen.width - objeto.halfSize;
            }
            
            if (objeto.position.y < objeto.halfSize)
            {
                objeto.direction.y = -objeto.direction.y;
                objeto.position.y = objeto.halfSize;

            } else if (objeto.position.y > Screen.height - objeto.halfSize)
            {
                objeto.direction.y = -objeto.direction.y;
                objeto.position.y = Screen.height - objeto.halfSize;
            }

            objeto.position += (objeto.direction * (speed * dTime));
        }
    }
}