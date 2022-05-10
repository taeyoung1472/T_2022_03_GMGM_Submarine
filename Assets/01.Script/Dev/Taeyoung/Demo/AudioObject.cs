using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    public void Play(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
    }
}
