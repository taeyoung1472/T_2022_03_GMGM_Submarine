using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    public void PlaySound()
    {
        AudioPoolManager.instance.Play(clips, transform.position);
    }
}
