using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPacket : MonoBehaviour
{
    public static AudioPacket Instance;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private GameObject audioObject;
    Queue<AudioSource> audios = new Queue<AudioSource>();
    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        for (int i = 0; i < 10; i++)
        {
            audios.Enqueue(Instantiate(audioObject, Vector3.zero, Quaternion.identity).GetComponent<AudioSource>());
        }
    }
    public void PlayAudio(int index, Vector3 pos)
    {
        AudioSource source;
        if (audios.Count != 0)
        {
            source = audios.Dequeue();
            source.gameObject.SetActive(true);
            source.clip = audioClips[index];
            source.Play();
        }
        else
        {
            source = Instantiate(audioObject, pos, Quaternion.identity).GetComponent<AudioSource>();
            source.clip = audioClips[index];
            source.Play();
        }
        StartCoroutine(DequeueObjcet(source.gameObject, source.clip.length * 0.1f));
    }
    IEnumerator DequeueObjcet(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
        audios.Enqueue(obj.GetComponent<AudioSource>());
    }
}