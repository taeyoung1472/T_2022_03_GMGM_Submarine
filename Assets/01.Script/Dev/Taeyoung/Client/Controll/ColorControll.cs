using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColorControll : ControllAbleObject
{
    [SerializeField] private Color positiveColor = Color.white;
    [SerializeField] private Color negativeColor = Color.white;
    Light light;
    private void Awake()
    {
        light = GetComponent<Light>();
    }
    public override void ControllNegative(Action callBack)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(light.DOColor(negativeColor, time));
        seq.AppendCallback(() => callBack());
    }

    public override void ControllPositive(Action callBack)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(light.DOColor(positiveColor, time));
        seq.AppendCallback(() => callBack());
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
