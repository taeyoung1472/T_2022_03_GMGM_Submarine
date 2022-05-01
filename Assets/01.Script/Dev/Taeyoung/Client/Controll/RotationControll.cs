using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationControll : ControllAbleObject
{
    [SerializeField] private Vector3 positiveAngle;
    [SerializeField] private Vector3 negativeAngle;
    public void Reset()
    {
        positiveAngle = transform.localEulerAngles;
        negativeAngle = transform.localEulerAngles;
    }
    public override void ControllNegative(Action callBack)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalRotate(negativeAngle, time));
        seq.AppendCallback(() => callBack());
    }

    public override void ControllPositive(Action callBack)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalRotate(positiveAngle, time));
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
