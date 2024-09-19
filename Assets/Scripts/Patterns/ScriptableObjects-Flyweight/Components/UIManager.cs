using TMPro;
using UnityEngine;

namespace Patterns.ScriptableObjects_Flyweight.Components
{
    public partial class UIManager : MonoBehaviour
    {
        private TextMeshProUGUI _goldText;
        private void Awake()
        {
            _goldText = transform.Find("GoldText").GetComponent<TextMeshProUGUI>();
            
            GameObject player = GameObject.FindWithTag("Player");
            GoldBag goldBag = player.GetComponent<GoldBag>();
            goldBag.GoldChanged += UpdateGold;
        }

        private void UpdateGold(object sender, float data)
        {
            _goldText.text = $"Gold: {data}";
        }
    }
}
