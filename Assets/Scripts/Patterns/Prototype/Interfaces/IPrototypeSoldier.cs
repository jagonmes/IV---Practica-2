
using UnityEngine;

namespace Patterns.Prototype.Interfaces
{
    public interface IPrototypeSoldier
    {
        // Este método implementa el patrón prototype
        public IPrototypeSoldier Clone();
        
        // Otros métodos
        public bool IsAlive();
        public string GetArmy();
        public Vector2 position { get; set; }
        
        public Vector2 direction { get; set; }
        
        public void Create();
        public void Move(float deltaTime);
        public bool ProcessCollissions(IPrototypeSoldier other);
        public void Render();

        public void Kill();
    }
}