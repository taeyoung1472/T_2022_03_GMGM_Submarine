using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
public class UseAbleObject_Client : MonoBehaviour
{
    protected bool isToggled, isHolding;
    [SerializeField] private UseAbleState state;
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private UnityEvent postiveEvent;
    [SerializeField] private UnityEvent negativeEvent;
    public bool IsToggled { get { return isToggled; } }
    public string Name { get { return name; } }
    public string Description { get { return description; } }
    public void Click()
    {
        switch (state)
        {
            case UseAbleState.Toggle:
                if (isToggled)
                {
                    isToggled = false;
                    negativeEvent?.Invoke();
                }
                else
                {
                    isToggled = true;
                    postiveEvent?.Invoke();
                }
                break;
            case UseAbleState.ClickBtn:
                postiveEvent?.Invoke();
                break;
            case UseAbleState.Inform:
                break;
        }
    }
}