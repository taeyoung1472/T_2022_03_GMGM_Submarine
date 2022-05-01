using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
public class Controller : MonoBehaviour
{
    [SerializeField] private UnityEvent<Action>[] positiveControllAbleObjects;
    [SerializeField] private UnityEvent<Action>[] negativeControllAbleObjects;
    public UnityEvent<Action>[] PositiveControllAbleObjects { get { return positiveControllAbleObjects; } }
    public UnityEvent<Action>[] NegativeControllAbleObjects { get { return negativeControllAbleObjects; } }
    Action positiveAction, negativeAction;
    int postiveCallback = 0, negativeCallback = 0;
    public void PositiveControll(Action callBack)
    {
        positiveAction = callBack;
        foreach (var controllEvent in positiveControllAbleObjects)
        {
            controllEvent?.Invoke(CheckPositiveCallback);
        }
    }
    public void NegativeControll(Action callBack)
    {
        negativeAction = callBack;
        foreach (var controllEvent in negativeControllAbleObjects)
        {
            controllEvent?.Invoke(CheckNegativeCallback);
        }
    }
    public void CheckPositiveCallback()
    {
        postiveCallback++;
        if(postiveCallback >= positiveControllAbleObjects.Length)
        {
            postiveCallback = 0;
            positiveAction?.Invoke();
        }
    }
    public void CheckNegativeCallback()
    {
        negativeCallback++;
        if(negativeCallback >= negativeControllAbleObjects.Length)
        {
            negativeCallback = 0;
            negativeAction?.Invoke();
        }
    }
}
