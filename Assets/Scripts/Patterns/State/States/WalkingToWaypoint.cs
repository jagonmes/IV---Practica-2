using Patterns.State.Interfaces;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Apple;

namespace Patterns.State.States
{
    public class WalkingToWaypoint : AZombieState
    {
        private Transform currentWaypoint;
        private Transform currentTransform;
        private float speed;
        private float rotationSpeed;
        private Vector3 destination;

        private float secondsToSeek = 1f;
        private float lastSeek = 0f;

        public override void Enter()
        {
            currentTransform = zombie.GetGameObject().transform;
            currentWaypoint = zombie.GetCurrentWayPoint();
            speed = zombie.GetWanderSpeed();
            rotationSpeed = zombie.GetRotateSpeed();
            
            zombie.SetCurrentSpeed(speed);
            Debug.Log($"Zombie {zombie.GetGameObject().name} started going to waypoint {currentWaypoint.name}");
        }

        public WalkingToWaypoint(IZombie zombie) : base(zombie)
        {
        }

        public override void Exit()
        {
            Debug.Log($"Zombie {zombie.GetGameObject().name} ended going to waypoint {currentWaypoint.name}");
        }
        
        public override void Update()
        {
            lastSeek += Time.deltaTime;
            
            if (lastSeek >= secondsToSeek)
            {
                zombie.SetState(new SearchingForPlayer(zombie));
                lastSeek = 0f;
                Debug.Log("Seeking for enemy");
            }
            
        }

        public override void FixedUpdate()
        {
            zombie.MoveTo(currentWaypoint, speed, rotationSpeed);

            Vector3 toWaypoint = currentWaypoint.position - currentTransform.position;
            toWaypoint.y = 0;
            float distanceToWaypoint = toWaypoint.magnitude;
            
            // Debug.Log($"Distance to waypoint: {distanceToWaypoint}");
            if (distanceToWaypoint <= speed)
            {
                Debug.Log($"Waypoint {currentWaypoint.name} reached");
                zombie.SetState(new SearchingForWaypoint(zombie));
            } 
        }
    }
}