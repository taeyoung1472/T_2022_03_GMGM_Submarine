using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VirtualSubmarineDisplay : MonoBehaviour
{
    #region ���÷��� ��Ұ���
    [SerializeField] private VirtualSubmarineDisplayElement[] elements;
    Dictionary<SubmarinePartType, VirtualSubmarineDisplayElement> elementDic = new Dictionary<SubmarinePartType, VirtualSubmarineDisplayElement>();
    #endregion

    #region ������Ƽ
    public Dictionary<SubmarinePartType, VirtualSubmarineDisplayElement> ElementDic { get { return elementDic; } }
    #endregion

    public void Awake()
    {
        foreach (VirtualSubmarineDisplayElement element in elements)
        {
            elementDic.Add(element.PartType, element);
        }
    }
    public void Start()
    {
        foreach (VirtualSubmarineDisplayElement element in elements)
        {
            element.Init();
        }
    }
}
public enum SubmarinePartType
{
    ControllRoom,
    HealthRoom,
    BedRoom,
    EngineRoom,
    TorpedoRoom,
    KitchenRoom,
    StroageRoom,
}