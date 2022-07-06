using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartHP : MonoBehaviour
{
    [SerializeField]PlayerRecovery playerRecovery;

    private float hp;
    private float maxHp = 100f;
    [SerializeField] private HpManager hpManager;
    [SerializeField] private HpDisplay hpDisplay;
    [SerializeField] private PlayerPartType type;
    [SerializeField] private HealthStateDataSO lightInjureData;
    [SerializeField] private HealthStateDataSO heavyInjureData;
    [SerializeField] private HealthStateDataSO blackoutInjureData;
    InjureType myInjureType = InjureType.Default;
    public InjureType InjureType { get { return myInjureType; } }
    bool isTryAccessFirst = true;
    public float MaxHp { get { return maxHp; }set { maxHp = value; } }
    public float Hp { get { if (isTryAccessFirst) { hp = maxHp; isTryAccessFirst = false; } return hp; } set { hp = value; CheckHp(); } }
    public void Start()
    {
        PlayerPartDisplay.Instance.SetPartColor(type, hp);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            hpManager.Damaged(10f, type);
            Debug.Log($"ÇöÀç HP : {hp}");
            playerRecovery.SetHpArea();
        }
    }
    public void CheckHp()
    {
        PlayerPartDisplay.Instance.SetPartColor(type, hp);
        if (hp <= 0)
        {
            hp = 0;
            if(myInjureType != InjureType.Blackout)
            {
                AddInjure(InjureType.Blackout);
            }
        }
        else if (hp <= 50)
        {
            if (myInjureType != InjureType.Heavy)
            {
                AddInjure(InjureType.Heavy);
            }
        }
        else if (hp <= 80)
        {
            if (myInjureType != InjureType.Light)
            {
                AddInjure(InjureType.Light);
            }
        }
        else
        {
            if (myInjureType != InjureType.Default)
            {
                AddInjure(InjureType.Default);
            }
        }
        HpDisplay.Instance.DisplayHp();
    }
    void ResetInjure()
    {
        HealthStateManager.Instance.Remove(type, lightInjureData);
        HealthStateManager.Instance.Remove(type, heavyInjureData);
        HealthStateManager.Instance.Remove(type, blackoutInjureData);
    }
    void AddInjure(InjureType injureType)
    {
        ResetInjure();
        switch (injureType)
        {
            case InjureType.Default:
                myInjureType = InjureType.Default;
                break;
            case InjureType.Light:
                myInjureType = InjureType.Light;
                HealthStateManager.Instance.Add(type, lightInjureData);
                break;
            case InjureType.Heavy:
                myInjureType = InjureType.Heavy;
                HealthStateManager.Instance.Add(type, heavyInjureData);
                break;
            case InjureType.Blackout:
                myInjureType = InjureType.Blackout;
                HealthStateManager.Instance.Add(type, blackoutInjureData);
                break;
        }
    }
}
public enum InjureType
{
    Default = 0,
    Light = 1,
    Heavy = 2,
    Blackout = 3
}