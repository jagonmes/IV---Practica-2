using UnityEngine;

namespace Patterns.ServiceLocator.Components
{
    public class CoinRotation : MonoBehaviour
    {
        [SerializeField]
        public float RotationSpeed = 10;
        
        void Update()
        {
            transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime, Space.World);
        }
    }
}