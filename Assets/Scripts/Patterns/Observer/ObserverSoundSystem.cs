using Patterns.Observer.Interfaces;
using UnityEngine;

namespace Patterns.Observer
{
    public class ObserverSoundSystem : MonoBehaviour, IObserver<float>
    {
        private AudioSource coinAudio;
        
        private void Awake()
        {
            GameObject player = GameObject.FindWithTag("Player");
            SubjectGoldBag subjectGoldBag = player.GetComponent<SubjectGoldBag>();
            subjectGoldBag.AddObserver(this);
            
            coinAudio = GetComponent<AudioSource>();
        }

        public void UpdateObserver(float data)
        {
            coinAudio?.Play();
        }
    }
}