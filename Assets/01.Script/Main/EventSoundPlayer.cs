using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    public void PlaySound()
    {
        AudioPoolManager.instance.Play(clips[Random.Range(0, clips.Length)], transform.position);
    }
}
