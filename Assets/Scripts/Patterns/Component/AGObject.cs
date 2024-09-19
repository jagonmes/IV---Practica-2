using Patterns.Component.Interfaces;
using UnityEngine;
namespace Patterns.Component
{
    public abstract class AGObject
    {
        public Camera cam;
        public GameObject gameObject;
        public Vector3 direction;
        public Vector3 position;  
        public float size = 0f;
        public float halfSize = 0f;

        protected ICreateComponent createComponent;
        protected ICollisionComponent collisionComponent;
        protected IRenderComponent renderComponent;
        protected IMoveComponent moveComponent;

        public void Create()
        {
            createComponent.Create(this);
        }

        public void Move(float deltaTime)
        {
            moveComponent.Move(this, deltaTime);
        }

        public void ProcessCollissions(AGObject other)
        {
            collisionComponent.ProcessCollisions(this, other);
        }

        public void Render()
        {
            renderComponent.Render(this);
        }

    }
}