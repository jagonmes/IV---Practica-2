using UnityEngine;

using Patterns.DirtyFlag.Components.Interfaces;

namespace Patterns.DirtyFlag.Components
{
    public class SoundSystem : MonoBehaviour, IObserver<float>
    {
        private AudioSource coinAudio;
        
        private void Awake()
        {
            GameObject player = GameObject.FindWithTag("Player");
            GoldBag goldBag = player.GetComponent<GoldBag>();
            goldBag.AddObserver(this);
            coinAudio = GetComponent<AudioSource>();
        }

        public void UpdateObserver(float data)
        {
            coinAudio?.Play();
        }
    }
}