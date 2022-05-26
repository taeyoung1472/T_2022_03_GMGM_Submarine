using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VirtualSubmarineDisplay : MonoBehaviour
{
    #region 디스플레이 요소관련
    [SerializeField] private VirtualSubmarineDisplayElement[] elements;
    Dictionary<SubmarinePartType, VirtualSubmarineDisplayElement> elementDic = new Dictionary<SubmarinePartType, VirtualSubmarineDisplayElement>();
    #endregion

    #region 프로퍼티
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