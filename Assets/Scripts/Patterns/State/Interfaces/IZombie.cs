using UnityEngine;

namespace Patterns.State.Interfaces
{
    public interface IZombie
    {
        public GameObject GetGameObject();
        public void SetState(IState state);
        public IState GetState();
        
        public GameObject PlayerAtSight();

        // Movement management
        public float GetRotateSpeed();
        public float GetWanderSpeed();
        public float GetChaseSpeed();
        public void SetCurrentSpeed(float speed);
        public void MoveTo(Transform target, float speed, float rotateSpeed);

        // Waypoints management
        public Transform[] GetWayPoints();
        public Transform GetCurrentWayPoint();
        public void SetCurrentWayPoint(Transform waypoint);
    }
}