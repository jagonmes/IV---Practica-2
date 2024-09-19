using Patterns.DirtyFlag.Components.Interfaces;
using UnityEngine;

namespace Patterns.ScriptableObjects_Flyweight.Components
{
    public class SoundSystem : MonoBehaviour
    {
        private AudioSource coinAudio;
        
        private void Awake()
        {
            GameObject player = GameObject.FindWithTag("Player");
            GoldBag goldBag = player.GetComponent<GoldBag>();
            goldBag.GoldChanged += GoldChanged;
            coinAudio = GetComponent<AudioSource>();
        }

        private void GoldChanged(object sender, float e)
        {
            coinAudio?.Play();
        }
    }
}