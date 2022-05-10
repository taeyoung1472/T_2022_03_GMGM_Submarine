using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightControll : ControllAbleObject
{
    Light light;
    private void Awake()
    {
        light = GetComponent<Light>();
    }
    public override void ControllNegative(Action callBack)
    {
        light.enabled = false;
    }

    public override void ControllPositive(Action callBack)
    {
        light.enabled = true;
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
