using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
public class UseAbleObject_Network : MonoBehaviour, IUseAble_Network
{
    protected bool isToggled, isHolding;
    [SerializeField] private UseAbleState state;
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private UnityEvent<int> runEvent;
    [SerializeField] private UnityEvent<int> stopEvent;
    public bool IsToggled { get { return isToggled; } }
    public string Name { get { return name; } }
    public string Description { get { return description; } }
    public void Click(Player_Network player)
    {
        int id = player.Id;
        switch (state)
        {
            case UseAbleState.Toggle:
                if (isToggled)
                {
                    isToggled = false;
                    StartCoroutine(Stop(id));
                }
                else
                {
                    isToggled = true;
                    StartCoroutine(Run(id));
                }
                break;
            case UseAbleState.ClickBtn:
                StartCoroutine(Run(id));
                break;
            case UseAbleState.Inform:
                break;
        }
    }
    public virtual IEnumerator Run(int id)
    {
        yield return new WaitForEndOfFrame();
        runEvent?.Invoke(id);
    }
    public virtual IEnumerator Stop(int id)
    {
        yield return new WaitForEndOfFrame();
        stopEvent?.Invoke(id);
    }
}
public interface IUseAble_Network
{
    public void Click(Player_Network pn);
}