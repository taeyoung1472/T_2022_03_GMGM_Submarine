using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPacket : MonoBehaviour
{
    public static AudioPacket Instance;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private GameObject audioObject;
    public void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void PlayAudio(int index, Vector3 pos)
    {
        AudioSource source = Instantiate(audioObject, pos, Quaternion.identity).GetComponent<AudioSource>();
        source.clip = audioClips[index];
        source.Play();
    }
}
public enum AudioPacketId
{
    Test,
}
