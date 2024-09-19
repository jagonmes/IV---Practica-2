using System;
using System.Transactions;
using Patterns.State.Interfaces;
using Patterns.State.States;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using IState = Patterns.State.Interfaces.IState;

namespace Patterns.State.Components
{
    public class ZombieController : MonoBehaviour, IZombie
    {
        public int viewingAngle;
        public float WanderSpeed;
        public float ChaseSpeed;
        public float RotateSpeed;

        private IState currentState;

        public Transform currentWaypoint;
        public Transform[] waypoints;

        private Animator animator;
        public Transform eyesTransform;

        private GameObject playerAtSight;

        private void Awake()
        {
            Assert.IsTrue(waypoints.Length > 0, "Waypoints must be greater than 1");
            if (currentWaypoint == null)
            {
                currentWaypoint = waypoints[0];
            }

            animator = gameObject.GetComponent<Animator>();
            
            SetState(new SearchingForWaypoint(this));
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        #region Get & Set speeds
        public float GetRotateSpeed()
        {
            return RotateSpeed;
        }
        
        public float GetWanderSpeed()
        {
            return WanderSpeed;
        }

        public float GetChaseSpeed()
        {
            return ChaseSpeed;
        }

        public void SetCurrentSpeed(float speed)
        {
            animator.SetFloat("MoveSpeed", speed);
        }
        #endregion

        #region Get & set Waypoints
        public Transform[] GetWayPoints()
        {
            return waypoints;
        }
        
        public Transform GetCurrentWayPoint()
        {
            return currentWaypoint;
        }

        public void SetCurrentWayPoint(Transform waypoint)
        {
            this.currentWaypoint = waypoint;
        }
        #endregion

        #region Set y Get State, state pattern
        public IState GetState()
        {
            return currentState;
        }

        public void SetState(IState state)
        {
            // Exit old state
            if (currentState != null)
            {
                currentState.Exit();    
            }
            
            // Set current state and enter
            currentState = state;
            currentState.Enter();
        }
        
        private void Update()
        {
            currentState.Update();
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdate();
        }
        #endregion
        
        #region Player at sight calculations
        public GameObject PlayerAtSight()
        {
            return playerAtSight;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerAtSight = PlayerIsOnSight(other.gameObject);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerAtSight = PlayerIsOnSight(other.gameObject);
            }            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerAtSight = null;
            }
        }

        private GameObject PlayerIsOnSight(GameObject player)
        {
            Vector3 playerDirection = (player.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, playerDirection);
            
            if (angle < viewingAngle / 2)
            {
                RaycastHit hit;

                Vector3 endPosition = player.transform.position;
                endPosition.y = eyesTransform.position.y;
                
                if (Physics.Linecast(eyesTransform.position, player.transform.position, out hit))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        return hit.collider.gameObject;
                    }                
                }
            }

            return null;
        }
        #endregion

        public void MoveTo(Transform target, float speed, float rotationSpeed)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0;    // No direction in vertical axis
            Quaternion toTargetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toTargetRotation,
                rotationSpeed * Time.deltaTime);
            
            transform.Translate(0, 0, speed * Time.fixedDeltaTime, Space.Self);
        }
    }
}