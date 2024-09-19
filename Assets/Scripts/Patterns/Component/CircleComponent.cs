using UnityEngine;
using Random = UnityEngine.Random;

namespace Patterns.Component
{
    public class CircleComponent: AGObject
    {
        public CircleComponent()
        {
            createComponent = new CreateCircleComponent();
            collisionComponent = new CollisionComponent();
            renderComponent = new RenderComponent();
            moveComponent = new MoveComponent();
        }
    }
}