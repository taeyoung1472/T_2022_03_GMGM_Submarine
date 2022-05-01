using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SoundControll : ControllAbleObject
{
    [SerializeField] private GameObject audioObject;
    [SerializeField] private AudioClip positiveClip;
    [SerializeField] private AudioClip negativeClip;
    public override void ControllNegative(Action callBack)
    {
        if (negativeClip == null)
            return;
        GameObject obj = Instantiate(audioObject);
        AudioSource source = obj.GetComponent<AudioSource>();
        source.clip = negativeClip;
        source.Play();
        callBack();
    }

    public override void ControllPositive(Action callBack)
    {
        if (positiveClip == null)
            return;
        GameObject obj = Instantiate(audioObject);
        AudioSource source = obj.GetComponent<AudioSource>();
        source.clip = positiveClip;
        source.Play();
        callBack();
    }

    protected override IEnumerator ControllingNegative()
    {
        throw new NotImplementedException();
    }

    protected override IEnumerator ControllingPositive()
    {
        throw new NotImplementedException();
    }
}
