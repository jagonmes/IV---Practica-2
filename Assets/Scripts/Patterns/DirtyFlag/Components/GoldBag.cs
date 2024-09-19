using System.Collections.Generic;
using UnityEngine;

using Components.Interfaces;
using Patterns.DirtyFlag.Components.Data;
using Patterns.DirtyFlag.Components.Interfaces;
using Patterns.DirtyFlag.Interfaces;

namespace Patterns.DirtyFlag.Components
{
    public class GoldBag : MonoBehaviour, ISubject<float>, ISaveableGameObject
    {
        [SerializeField] public float Gold = 0f;

        public bool _isDirty = false;

        // REFACTORIZACION DE LA RECOGIDA DE MONEDAS
        public void OnTriggerEnter(Collider other)
        {
            IPickableCoin pickable = other.GetComponent<IPickableCoin>();
            
            if(pickable != null) 
            {
                Gold += pickable.Pick();
                NotifyObservers();
                _isDirty = true;
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

        // IMPLEMENTACION DEL PATRÓN DIRTY FLAG
        public bool IsDirty()
        {
            return _isDirty;
        }

        public object GetData()
        {
            _isDirty = false;
            var position = this.transform.position;
            
            return new GoldBagData()
            {
                goldAmount = Gold,
                positionX = position.x,
                positionY = position.y,
                positionZ = position.z,
            };
        }

        public void Restore(object data)
        {
            GoldBagData d = (GoldBagData)data;
            transform.position = new Vector3(d.positionX, d.positionY, d.positionZ);
            Gold = d.goldAmount;
            NotifyObservers();
        }
    }
}