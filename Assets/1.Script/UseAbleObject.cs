using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbleObject : MonoBehaviour, IUseAble
{
    protected bool isToggled, isHolding;
    [SerializeField] private UseAbleState state;
    [SerializeField] private string name;
    [SerializeField] private string description;
    public bool IsToggled { get { return isToggled; } }
    public string Name { get { return name; } }
    public string Description { get { return description; } }

    public void Toggle()
    {
        if (state == UseAbleState.Toggle)
        {
            if (isToggled)
            {
                isToggled = false;
                Stop();
                print("Turn Off!");
            }
            else
            {
                isToggled = true;
                Run();
                print("Turn On!");
            }
        }
    }
    public virtual void Run()
    {

    }
    public virtual void Stop()
    {

    }
}
enum UseAbleState
{
    Toggle,
    Hold
}
public interface IUseAble
{
    public void Toggle();
}