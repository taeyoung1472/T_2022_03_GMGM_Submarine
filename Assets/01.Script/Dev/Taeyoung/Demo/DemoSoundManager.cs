using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSoundManager : MonoBehaviour
{
    public static DemoSoundManager instance;
    [SerializeField] private AudioClip postiveClick;
    [SerializeField] private AudioClip negativeClick;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void Click(bool isPostive)
    {
        if (isPostive)
            AudioPoolManager.instance.Play(postiveClick, transform.position);
        else
            AudioPoolManager.instance.Play(negativeClick, transform.position);
    }
}
