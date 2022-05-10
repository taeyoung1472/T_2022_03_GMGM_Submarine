using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllPannelExample : ControllPannel
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

    }

    public override void ControllNegativeCallback()
    {

    }

    public override void OnControllPositiveCallback()
    {

    }

    public override void OnControllNegativeCallback()
    {

    }
}