using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HpManager : MonoBehaviour
{
    [SerializeField] private PlayerPartHpStruct parts;
    [SerializeField] private PhysicalHpManager physicalHpManager;
    [Header("�׽�Ʈ��")]
    [SerializeField] private PlayerPartType targetType;
    [SerializeField] private float dmg;
    InjureType worstType;
    public InjureType WorstType { get { return worstType; } }
    public PlayerPartHpStruct Parts { get { return parts; } }
    [ContextMenu("Test")]
    public void GetDMG()
    {
        Damaged(dmg, targetType);
    }
    public void Damaged(float physicsDmg, PlayerPartType partType, float mentalDmg = 0)
    {
        print($"Part : {partType} DMG : {physicsDmg}");
        switch (partType)
        {
            case PlayerPartType.RightArm:
                parts.RightArm.Hp -= physicsDmg;
                break;
            case PlayerPartType.LeftArm:
                parts.LeftArm.Hp -= physicsDmg;
                break;
            case PlayerPartType.RightLeg:
                parts.RightLeg.Hp -= physicsDmg;
                break;
            case PlayerPartType.LeftLeg:
                parts.LeftLeg.Hp -= physicsDmg;
                break;
            case PlayerPartType.Thorax:
                parts.Thorax.Hp -= physicsDmg;
                break;
            case PlayerPartType.Head:
                parts.Head.Hp -= physicsDmg;
                break;
        }
        worstType = InjureType.Default;
        foreach (PartHP part in parts.parts)
        {
            if((int)part.InjureType > (int)worstType)
            {
                worstType = part.InjureType;
            }
        }
    }
    public void DefaultState(PlayerPartType part)
    {
        print($"{part} �κ��� �λ��� �����ϴ�!");
    }
    public void LightInjure(PlayerPartType part)
    {
        print($"{part} �κп� ����� �Ծ����ϴ�!");
    }
    public void HeavyInjure(PlayerPartType part)
    {
        print($"{part} �κп� �߻��� �Ծ����ϴ�!");
    }
    public void Blackout(PlayerPartType part)
    {
        print($"{part} �κ��� ������ �ջ�Ǿ����ϴ�!");
    }
}
[Serializable]
public struct PlayerPartHpStruct
{
    public PartHP RightArm;
    public PartHP LeftArm;
    public PartHP RightLeg;
    public PartHP LeftLeg;
    public PartHP Thorax;
    public PartHP Head;
    public PartHP[] parts;
}
public enum PlayerPartType
{
    RightArm, 
    LeftArm,
    RightLeg,
    LeftLeg,
    Thorax,
    Head
}