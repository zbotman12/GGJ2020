using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectHandler : MonoBehaviour
{
    [System.Serializable]
    public struct SoundEffect {
        public string name;
        public AudioClip clip;
    }

    AudioSource _source;

    [SerializeField]
    List<SoundEffect> _soundEffects = null;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlaySound(string sound)
    {
        _source.PlayOneShot(
            _soundEffects.Find(x => x.name.ToLower().Trim() == sound.ToLower().Trim())
            .clip);
    }
}
