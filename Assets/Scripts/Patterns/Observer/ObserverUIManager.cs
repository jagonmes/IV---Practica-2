using Patterns.Observer.Interfaces;
using TMPro;
using UnityEngine;

namespace Patterns.Observer
{
    public class ObserverUIManager : MonoBehaviour, IObserver<float>
    {
        private TextMeshProUGUI _goldText;
        private void Awake()
        {
            _goldText = transform.Find("GoldText").GetComponent<TextMeshProUGUI>();
            
            GameObject player = GameObject.FindWithTag("Player");
            SubjectGoldBag subjectGoldBag = player.GetComponent<SubjectGoldBag>();
            subjectGoldBag.AddObserver(this);
        }
        
        public void UpdateObserver(float data)
        {
            _goldText.text = $"Gold: {data}";
        }
    }
}
