using UnityEngine;

namespace Patterns.State.Interfaces
{
    public abstract class AZombieState : IState
    {
        protected IZombie zombie;
        
        public AZombieState(IZombie zombie)
        {
            this.zombie = zombie;
        }
        
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
        public abstract void FixedUpdate();
    }
}