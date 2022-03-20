using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarinControllChair : UseAbleObject
{
    [SerializeField] private SubmarineController submarineController;
    public override void Run()
    {
        submarineController.SitDown();
    }
    public override void Stop()
    {
        submarineController.SitUp();
    }
}