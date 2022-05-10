using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "스크립트 에이블/이태영/Tool")]
public class ToolData : ScriptableObject
{
    public string name;
    public string desc;
    public Sprite image;
    public GameObject toolPrefab;
    public ToolType toolType;
}
public enum ToolType
{
    MeleeWeapon,
    ServeWeapon,
    MainWeapon,
    RepairTool,
    HealthTool,
    ETC,
}