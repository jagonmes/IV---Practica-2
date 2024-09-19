using Patterns.State.Interfaces;
using UnityEngine;

namespace Patterns.State.States
{
    public class Atacando: AZombieState
    {
        
        private Transform playerTransform;
        private Transform currentTransform;
        
        private float umbralDeAtaque = 0.5f;

        private float temporizador = 2;
        
        public Atacando(IZombie zombie) : base(zombie)
        {
            
        }

        public override void Enter()
        {
            currentTransform = zombie.GetGameObject().transform;
            playerTransform = zombie.PlayerAtSight().transform;
            Debug.Log($"Zombie {zombie.GetGameObject().name} atacando al jugador");
        }

        public override void Exit()
        {
            Debug.Log($"Zombie {zombie.GetGameObject().name} dejando de atacar al jugador");
        }

        public override void Update()
        {
            if (temporizador >= 2)
            {
                Debug.Log($"Zombie {zombie.GetGameObject().name} AL ATAQUER!!!!");
                temporizador = 0;
            }

            temporizador += Time.deltaTime;
        }

        public override void FixedUpdate()
        {
            if (Vector3.Distance(currentTransform.position, playerTransform.position) > umbralDeAtaque)
            {
                zombie.SetState(new ChasingPlayer(zombie));
            }
        }
    }
}