using Patterns.Command.Interfaces;
using UnityEngine;

namespace Patterns.Command.Components
{
    public class Player : MonoBehaviour, IMoveableReceiver
    {
        public float speed;
        public bool IsMoving => _moving;
        
        private bool _moving;
        private Vector3 _newPosition;
        private CommandManager _commandManager;
        private Animator _animator;
        private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _commandManager = new CommandManager();
        }

        public void Move(Vector3 direction)
        {
            if (!_moving)
            {
                Vector3 dir = new Vector3(direction.x * transform.right.x, 0, direction.z * transform.forward.z);
                _newPosition = transform.position + dir.normalized * 1.05f; 
                _moving = true;
                _animator.SetFloat(MoveSpeed, speed);
            }
        }
        
        private void FixedUpdate()
        {
            if (_moving)
            {
                transform.position = Vector3.MoveTowards(transform.position, _newPosition, 
                    speed * Time.fixedDeltaTime);

                if (Vector3.Distance(transform.position, _newPosition) < speed * Time.fixedDeltaTime)
                {
                    transform.position = _newPosition;
                    _moving = false;
                    _animator.SetFloat(MoveSpeed, 0);
                }
            }
        }
    }
}