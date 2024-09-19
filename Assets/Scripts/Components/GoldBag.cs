using Components.Interfaces;
using TMPro;
using UnityEngine;

namespace Components
{
    public class GoldBag : MonoBehaviour
    {
        private float _gold = 0f;

        private UIManager _uiManager;
        private SoundSystem _soundSystem;

        private void Awake()
        {
            GameObject goldUI = GameObject.Find("GameUI");
            _uiManager = goldUI.GetComponent<UIManager>();

            GameObject soundSystem = GameObject.Find("SoundSystem");
            _soundSystem = soundSystem.GetComponent<SoundSystem>();
        }

        public void OnTriggerEnter(Collider other)
        {
            IPickableCoin pickable = other.GetComponent<IPickableCoin>();
            
            if(pickable != null) 
            {
                _gold += pickable.Pick();
                Debug.Log($"Gold: {_gold}");
                
                _uiManager.UpdateGoldText(_gold);
                _soundSystem.PlayCoinSound();
            }
        }
    }
}
