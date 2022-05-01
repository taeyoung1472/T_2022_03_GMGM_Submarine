using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public abstract class ControllAbleObject : MonoBehaviour
{
    [SerializeField] protected float time = 0.5f;
    public abstract void ControllPositive(Action callBack);
    public abstract void ControllNegative(Action callBack);
    protected abstract IEnumerator ControllingPositive();
    protected abstract IEnumerator ControllingNegative();
}
