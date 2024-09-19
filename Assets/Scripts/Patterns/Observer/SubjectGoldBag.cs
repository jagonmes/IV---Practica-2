using System.Collections.Generic;
using Components.Interfaces;
using Patterns.Observer.Interfaces;
using UnityEngine;

namespace Patterns.Observer
{
    public class SubjectGoldBag : MonoBehaviour, ISubject<float>
    {
        [SerializeField] public float Gold = 0f;

        // REFACTORIZACION DE LA RECOGIDA DE MONEDAS
        public void OnTriggerEnter(Collider other)
        {
            IPickableCoin pickable = other.GetComponent<IPickableCoin>();
            
            if(pickable != null) 
            {
                Gold += pickable.Pick();
                NotifyObservers();
            }
        }

        // IMPLEMENTACION DEL PATRON OBSERVER
        private List<IObserver<float>> _observers = new List<IObserver<float>>();

        public void AddObserver(IObserver<float> observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver<float> observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (IObserver<float> observer in _observers)
            {
                observer?.UpdateObserver(Gold);
            }
        }
    }
}