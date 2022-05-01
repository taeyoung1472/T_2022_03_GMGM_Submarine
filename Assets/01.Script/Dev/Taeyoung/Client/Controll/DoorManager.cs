using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : ControllPannel
{
    public override void ControllPositive()
    {
        OnControllPositiveCallback();
        controller.PositiveControll(ControllPositiveCallback);
    }

    public override void ControllNegative()
    {
        OnControllNegativeCallback();
        controller.NegativeControll(ControllNegativeCallback);
    }

    public override void ControllPositiveCallback()
    {
        postiveAction++;
        if (postiveAction >= controller.PositiveControllAbleObjects.Length)
        {
            print("문열기 끝!");
            postiveAction = 0;
        }
    }

    public override void ControllNegativeCallback()
    {
        negativeAction++;
        if(negativeAction >= controller.NegativeControllAbleObjects.Length)
        {
            print("문닫기 끝!");
            negativeAction = 0;
        }
    }

    public override void OnControllPositiveCallback()
    {
        print("문열기 시작!");
    }

    public override void OnControllNegativeCallback()
    {
        print("문닫기 시작!");
    }
}