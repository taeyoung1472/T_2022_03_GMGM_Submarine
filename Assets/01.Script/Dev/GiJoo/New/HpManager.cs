using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HpManager : MonoBehaviour
{
    [SerializeField] private PlayerPartHpStruct parts;
    [SerializeField] private PhysicalHpManager physicalHpManager;
    [Header("테스트용")]
    [SerializeField] private PlayerPartType targetType;
    [SerializeField] private float dmg;
    public PlayerPartHpStruct Parts { get { return parts; } }
    [ContextMenu("Test")]
    public void GetDMG()
    {
        Damaged(dmg, targetType);
    }
    public void Damaged(float physicsDmg, PlayerPartType partType, float mentalDmg = 0)
    {
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
    }
    public void DefaultState(PlayerPartType part)
    {
        print($"{part} 부분의 부상은 없습니다!");
    }
    public void LightInjure(PlayerPartType part)
    {
        print($"{part} 부분에 경상을 입었습니다!");
    }
    public void HeavyInjure(PlayerPartType part)
    {
        print($"{part} 부분에 중상을 입었습니다!");
    }
    public void Blackout(PlayerPartType part)
    {
        print($"{part} 부분이 완전히 손상되었습니다!");
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