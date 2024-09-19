using Patterns.State.Interfaces;
using UnityEngine;

namespace Patterns.State.States
{
    public class SearchingForPlayer : AZombieState
    {
        public SearchingForPlayer(IZombie zombie) : base(zombie)
        {
        }

        public override void Enter()
        {
            Debug.Log($"Zombie {zombie.GetGameObject().name} started searching for player");
        }

        public override void Exit()
        {
            Debug.Log($"Zombie {zombie.GetGameObject().name} ended searching for player");
        }

        public override void Update()
        {
            if (zombie.PlayerAtSight() != null)
            {
                zombie.SetState(new ChasingPlayer(zombie));
            }
            else
            {
                zombie.SetState(new WalkingToWaypoint(zombie));
            }
        }

        public override void FixedUpdate()
        {
        }
    }
}