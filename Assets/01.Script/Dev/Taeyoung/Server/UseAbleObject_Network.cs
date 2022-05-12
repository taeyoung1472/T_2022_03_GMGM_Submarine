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
    [SerializeField] private int packetId;
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
                    ClientSend.Controll(packetId, false);
                }
                else
                {
                    isToggled = true;
                    ClientSend.Controll(packetId, true);
                }
                break;
            case UseAbleState.ClickBtn:
                ClientSend.Controll(packetId, true);
                break;
            case UseAbleState.Inform:
                break;
        }
    }
}
public enum UseAbleType
{
    SubmarineControll,
}
public interface IUseAble_Network
{
    public void Click(Player_Network pn);
}