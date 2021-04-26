using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NSTools.Binders
{
    public class ZBindAudioSource : MonoBehaviour
    {
        public AudioClip[] clips;

        private Dictionary<string, AudioClip> _clips;
        private AudioSource source;

        void Awake()
        {
            source = GetComponent<AudioSource>();
            Game.Event.Bind<string>("sound_play", Play);
            Game.Data.Bind<float>("sound_volume", UpdateVolume);
            _clips = clips.ToDictionary(a => a.name, b => b);
        }

        private void UpdateVolume(float val = 1f)
        {
            source.volume = val;
        }

        private void Play(string arg)
        {
            if (source.volume > 0)
                source.PlayOneShot(_clips[arg]);
        }
    }
}
