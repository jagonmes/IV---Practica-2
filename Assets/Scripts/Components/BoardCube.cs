using UnityEngine;

namespace Patterns.Command.Components
{
    public class BoardCube : MonoBehaviour
    {
        public int Row;
        public int Col;
        
        private Material _currentMaterial;

        private void Awake()
        {
            Renderer renderer = GetComponent<Renderer>();
            _currentMaterial = renderer.material;
        }

        public void Activate()
        {
            _currentMaterial.color = Color.green;
        }

        public void Deactivate()
        {
            _currentMaterial.color = Color.white;
        }
    }
}