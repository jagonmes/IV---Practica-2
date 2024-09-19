using System;
using Patterns.ScriptableObjects_Flyweight.Components.Interfaces;
using UnityEngine;

namespace Patterns.ScriptableObjects_Flyweight.Components
{
    public class GoldBag : MonoBehaviour
    {
        [SerializeField] public float Gold = 0f;
        
        public EventHandler<float> GoldChanged;

        // REFACTORIZACION DE LA RECOGIDA DE MONEDAS
        public void OnTriggerEnter(Collider other)
        {
            IPickableCoin pickable = other.GetComponent<IPickableCoin>();
            
            if(pickable != null) 
            {
                Gold += pickable.Pick();
                GoldChanged.Invoke(this, Gold);
            }
        }
    }
}