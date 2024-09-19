using UnityEngine;
using Random = UnityEngine.Random;

namespace Patterns.Component
{
    public class RectangleComponent: AGObject
    {
        public RectangleComponent()
        {
            createComponent = new CreateRectangleComponent();
            collisionComponent = new CollisionComponent();
            renderComponent = new RenderComponent();
            moveComponent = new MoveComponent();
        }
    }
}