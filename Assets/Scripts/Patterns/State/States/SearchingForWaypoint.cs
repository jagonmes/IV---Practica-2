using System;
using Patterns.State.Interfaces;
using UnityEngine;

namespace Patterns.State.States
{
    public class SearchingForWaypoint : AZombieState
    {
        private Transform nextWaypoint;
        private Transform currentTransform;
        private Vector3 nextWayPointDirection;
        private Quaternion nextWaypointRotation;
        private float rotateSpeed;
        
        public SearchingForWaypoint(IZombie zombie) : base(zombie)
        {
        }

        public override void Enter()
        {
            Transform[] waypoints = zombie.GetWayPoints();
            int nextWayPointIndex = (Array.IndexOf(waypoints, zombie.GetCurrentWayPoint()) + 1) % waypoints.Length;
            nextWaypoint = waypoints[nextWayPointIndex];
            Debug.Log($"Current waypoint {zombie.GetCurrentWayPoint()}, going to waypoint {nextWayPointIndex}");
            zombie.SetCurrentWayPoint(nextWaypoint);

            rotateSpeed = zombie.GetRotateSpeed();   
            currentTransform = zombie.GetGameObject().transform; 
            nextWayPointDirection = (nextWaypoint.transform.position - currentTransform.position).normalized;
            nextWaypointRotation = Quaternion.LookRotation(nextWayPointDirection, currentTransform.up);
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
            currentTransform.transform.rotation = Quaternion.RotateTowards(currentTransform.rotation, 
                nextWaypointRotation,
                rotateSpeed * Time.fixedDeltaTime);

            float angle = Quaternion.Angle(currentTransform.rotation, nextWaypointRotation);
            if (angle < rotateSpeed * Time.fixedDeltaTime)
            {
                currentTransform.rotation = nextWaypointRotation;
                zombie.SetState(new WalkingToWaypoint(zombie));
            }
        }
    }
}