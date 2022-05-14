using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPacket : MonoBehaviour
{
    public static AudioPacket Instance;
    [SerializeField] private AudioClip[] audioClips;
    public void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void PlayAudio(int index, Vector3 pos)
    {
        AudioPoolManager.instance.Play(audioClips[index], pos);
    }
}