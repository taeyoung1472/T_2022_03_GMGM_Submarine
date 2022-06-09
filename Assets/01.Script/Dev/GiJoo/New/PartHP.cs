using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartHP : MonoBehaviour
{
    [SerializeField]PlayerRecovery playerRecovery;

    private float hp;
    public float maxHp = 100;
    [SerializeField] private HpManager hpManager;
    [SerializeField] private HpDisplay hpDisplay;
    [SerializeField] private PlayerPartType type;
    [SerializeField] private HealthStateDataSO lightInjureData;
    [SerializeField] private HealthStateDataSO heavyInjureData;
    [SerializeField] private HealthStateDataSO blackoutInjureData;
    InjureType injureType = InjureType.Default;
    public InjureType Type { get { return injureType; } }
    bool isTryAccessFirst = true;
    public float Hp { get { if (isTryAccessFirst) { hp = maxHp; isTryAccessFirst = false; } return hp; } set { hp = value; CheckHp(); } }
    public void Start()
    {
        PlayerPartDisplay.Instance.SetPartColor(type, hp);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            hp-=10f;
            Debug.Log($"ÇöÀç HP : {hp}");
            playerRecovery.SetHpArea();
            CheckHp();
        }
    }
    public void CheckHp()
    {
        PlayerPartDisplay.Instance.SetPartColor(type, hp);
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
                break;
            case InjureType.Light:
                HealthStateManager.Instance.Add(type, lightInjureData);
                break;
            case InjureType.Heavy:
                HealthStateManager.Instance.Add(type, heavyInjureData);
                break;
            case InjureType.Blackout:
                HealthStateManager.Instance.Add(type, blackoutInjureData);
                break;
        }
    }
}
public enum InjureType
{
    Default,
    Light,
    Heavy,
    Blackout
}