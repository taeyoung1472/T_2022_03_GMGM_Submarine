using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorControll : ControllAbleObject
{
    [SerializeField] private Vector3 closePos;
    [SerializeField] private Vector3 openPos;
    public void Reset()
    {
        closePos = transform.localPosition;
        openPos = transform.localPosition;
    }
    public override void ControllNegative(Action callBack)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMove(closePos, time));
        seq.AppendCallback(() => callBack());
    }

    public override void ControllPositive(Action callBack)
    {
        print("A");
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMove(openPos, time));
        seq.AppendCallback(() => callBack());
    }
    protected override IEnumerator ControllingNegative()
    {
        yield return null;
    }

    protected override IEnumerator ControllingPositive()
    {
        yield return null;
    }
}
