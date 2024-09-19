using Patterns.State.Interfaces;
using UnityEngine;

namespace Patterns.State.States
{
    public class ChasingPlayer : AZombieState
    {
        private Transform playerTransform;
        private Transform currentTransform;

        private float rotationSpeed;
        private float chaseSpeed;

        private float umbralDeAtaque = 0.5f;
        
        public ChasingPlayer(IZombie zombie) : base(zombie)
        {
        }

        public override void Enter()
        {
            currentTransform = zombie.GetGameObject().transform;
            playerTransform = zombie.PlayerAtSight().transform;
            rotationSpeed = zombie.GetRotateSpeed();
            chaseSpeed = zombie.GetChaseSpeed();
            Debug.Log($"Zombie {zombie.GetGameObject().name} started chasing player");
        }

        public override void Exit()
        {
            Debug.Log($"Zombie {zombie.GetGameObject().name} ended chasing player");
        }

        public override void Update()
        {
            if (Vector3.Distance(currentTransform.position, playerTransform.position) < umbralDeAtaque)
            {
                zombie.SetState(new Atacando(zombie));
            }
            Debug.Log(Vector3.Distance(currentTransform.position, playerTransform.position));
        }

        public override void FixedUpdate()
        {
            if (zombie.PlayerAtSight())
            {
                zombie.MoveTo(playerTransform, chaseSpeed, rotationSpeed);
            }
            else
            {
                zombie.SetState(new WalkingToWaypoint(zombie));
            }
        }
    }
}