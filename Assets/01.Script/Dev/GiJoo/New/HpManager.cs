using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HpManager : MonoBehaviour
{
    [SerializeField] private PartHpStruct parts;
    [SerializeField] private PhysicalHpManager physicalHpManager;
    [Header("�׽�Ʈ��")]
    [SerializeField] private PartType targetType;
    [SerializeField] private float dmg;
    public PartHpStruct Parts { get { return parts; } }
    [ContextMenu("Test")]
    public void GetDMG()
    {
        Damaged(dmg, targetType);
    }
    public void Damaged(float physicsDmg, PartType partType, float mentalDmg = 0)
    {
        switch (partType)
        {
            case PartType.RightArm:
                parts.RightArm.Hp -= physicsDmg;
                break;
            case PartType.LeftArm:
                parts.LeftArm.Hp -= physicsDmg;
                break;
            case PartType.RightLeg:
                parts.RightLeg.Hp -= physicsDmg;
                break;
            case PartType.LeftLeg:
                parts.LeftLeg.Hp -= physicsDmg;
                break;
            case PartType.Thorax:
                parts.Thorax.Hp -= physicsDmg;
                break;
            case PartType.Head:
                parts.Head.Hp -= physicsDmg;
                break;
        }
    }
    public void DefaultState(PartType part)
    {
        print($"{part} �κ��� �λ��� �����ϴ�!");
    }
    public void LightInjure(PartType part)
    {
        print($"{part} �κп� ����� �Ծ����ϴ�!");
    }
    public void HeavyInjure(PartType part)
    {
        print($"{part} �κп� �߻��� �Ծ����ϴ�!");
    }
    public void Blackout(PartType part)
    {
        print($"{part} �κ��� ������ �ջ�Ǿ����ϴ�!");
    }
}
[Serializable]
public struct PartHpStruct
{
    public PartHP RightArm;
    public PartHP LeftArm;
    public PartHP RightLeg;
    public PartHP LeftLeg;
    public PartHP Thorax;
    public PartHP Head;
}
public enum PartType
{
    RightArm, 
    LeftArm,
    RightLeg,
    LeftLeg,
    Thorax,
    Head
}