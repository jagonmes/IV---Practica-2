using TMPro;
using UnityEngine;

using Patterns.DirtyFlag.Components.Interfaces;

namespace Patterns.DirtyFlag.Components
{
    public class UIManager : MonoBehaviour, IObserver<float>
    {
        private TextMeshProUGUI _goldText;
        private void Awake()
        {
            _goldText = transform.Find("GoldText").GetComponent<TextMeshProUGUI>();
            
            GameObject player = GameObject.FindWithTag("Player");
            DirtyFlag.Components.GoldBag goldBag = player.GetComponent<DirtyFlag.Components.GoldBag>();
            goldBag.AddObserver(this);
        }
        
        public void UpdateObserver(float data)
        {
            _goldText.text = $"Gold: {data}";
        }
    }
}
