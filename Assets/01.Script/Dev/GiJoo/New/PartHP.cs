using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartHP : MonoBehaviour
{
    [SerializeField] private float hp = 100;
    [SerializeField] private HpManager hpManager;
    [SerializeField] private PlayerPartType type;
    [SerializeField] private HealthStateDataSO lightInjureData;
    [SerializeField] private HealthStateDataSO heavyInjureData;
    [SerializeField] private HealthStateDataSO blackoutInjureData;
    InjureType injureType = InjureType.Default;
    public InjureType Type { get { return injureType; } }
    public float Hp { get { return hp; } set { hp = value; CheckHp(); } }
    public void Start()
    {
        PartHpDisplay.instance.SetPartColor(type, Hp);
        //PlayerPartDisplay.Instance.SetPartColor(type, hp);
    }
    public void CheckHp()
    {
        PartHpDisplay.instance.SetPartColor(type, Hp);
        //PlayerPartDisplay.Instance.SetPartColor(type, hp);
        if (hp <= 0)
        {
            hp = 0;
            if(injureType != InjureType.Blackout)
            {
                AddInjure(InjureType.Blackout);
            }
        }
        else if (hp <= 50)
        {
            if (injureType != InjureType.Heavy)
            {
                AddInjure(InjureType.Heavy);
            }
        }
        else if (hp <= 80)
        {
            if (injureType != InjureType.Light)
            {
                AddInjure(InjureType.Light);
            }
        }
        else
        {
            if (injureType != InjureType.Default)
            {
                AddInjure(InjureType.Default);
            }
        }
        //HpDisplay.Instance.DisplayHp();
    }
    void ResetInjure()
    {
        HealStateDisplay.instance.MinusState(type);
        //HealthStateManager.Instance.Remove(type, lightInjureData);
        //HealthStateManager.Instance.Remove(type, heavyInjureData);
        //HealthStateManager.Instance.Remove(type, blackoutInjureData);
    }
    void AddInjure(InjureType injureType)
    {
        ResetInjure();
        HealthStateDataSO data = null;
        switch (injureType)
        {
            case InjureType.Default:
                break;
            case InjureType.Light:
                data = lightInjureData;
                //HealthStateManager.Instance.Add(type, lightInjureData);
                break;
            case InjureType.Heavy:
                data = heavyInjureData;
                //HealthStateManager.Instance.Add(type, heavyInjureData);
                break;
            case InjureType.Blackout:
                data = blackoutInjureData;
                //HealthStateManager.Instance.Add(type, blackoutInjureData);
                break;
        }
        HealStateDisplay.instance.AddState(data, type);
    }
}
public enum InjureType
{
    Default,
    Light,
    Heavy,
    Blackout
}