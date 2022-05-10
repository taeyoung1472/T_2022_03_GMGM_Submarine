using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
public class UseAbleObject : MonoBehaviour, IUseAble
{
    protected bool isToggled = false, isHolding;
    [SerializeField] private UseAbleState state;
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private UnityEvent runEvent;
    [SerializeField] private UnityEvent stopEvent;
    public bool IsToggled { get { return isToggled; } }
    public string Name { get { return $"{name}"; } }
    public string Description { get {
        switch (state)
        {
        case UseAbleState.Toggle:
            return isToggled ? "| ÄÑÁü | " + description : "| ²¨Áü | " + description;
        case UseAbleState.ClickBtn:
            return "| ÀÛµ¿ |" + description;
        case UseAbleState.Inform:
            return description;
        default:
            return description;
        }
    } }

    public void Click()
    {
        switch (state)
        {
            case UseAbleState.Toggle:
                if (isToggled)
                {
                    isToggled = false;
                    StartCoroutine(Stop());
                }
                else
                {
                    isToggled = true;
                    StartCoroutine(Run());
                }
                break;
            case UseAbleState.ClickBtn:
                StartCoroutine(Run());
                break;
            case UseAbleState.Inform:
                break;
        }
    }
    public virtual IEnumerator Run()
    {
        yield return new WaitForEndOfFrame();
        runEvent?.Invoke();
    }
    public virtual IEnumerator Stop()
    {
        yield return new WaitForEndOfFrame();
        stopEvent?.Invoke();
    }
}
enum UseAbleState
{
    Toggle,
    ClickBtn,
    Inform
}
public interface IUseAble
{
    public void Click();
}