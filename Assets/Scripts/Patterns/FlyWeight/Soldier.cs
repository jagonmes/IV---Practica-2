using Patterns.FlyWeight.Interfaces;
using UnityEngine;

namespace Patterns.FlyWeight
{
    public class Soldier : ISoldier
    {
        public Vector3 direction;
        public Vector3 position;
        public GameObject gameObject;
        public bool isAlive;
        public float currentHealth;
        
        private FlyWeightSoldier _flyWeightSoldier;

        public Soldier(string type, int MBOfData)
        {
            _flyWeightSoldier = FlyWeightSoldierFactory.GetSoldier(type, MBOfData);
            isAlive = true;
        }

        public bool IsAlive()
        {
            return isAlive;
        }

        public string GetArmy()
        {
            return _flyWeightSoldier.soldierType;
        }

        public void Create()
        {
            _flyWeightSoldier.Create(this);
        }
        
        public void Move(float deltaTime)
        {
            _flyWeightSoldier.Move(deltaTime, this);
        }

        public void ProcessCollissions(ISoldier other)
        {
            _flyWeightSoldier.ProcessCollissions(this, (Soldier)other);
        }

        public void Render()
        {
            _flyWeightSoldier.Render(this);
        }
    }
}