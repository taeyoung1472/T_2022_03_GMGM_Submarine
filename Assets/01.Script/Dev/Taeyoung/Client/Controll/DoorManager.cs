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
            print("������ ��!");
            postiveAction = 0;
        }
    }

    public override void ControllNegativeCallback()
    {
        negativeAction++;
        if(negativeAction >= controller.NegativeControllAbleObjects.Length)
        {
            print("���ݱ� ��!");
            negativeAction = 0;
        }
    }

    public override void OnControllPositiveCallback()
    {
        print("������ ����!");
    }

    public override void OnControllNegativeCallback()
    {
        print("���ݱ� ����!");
    }
}