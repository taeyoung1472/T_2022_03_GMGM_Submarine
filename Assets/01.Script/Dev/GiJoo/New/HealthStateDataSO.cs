using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "스크립트 에이블/Gijoo/State")]
public class HealthStateDataSO : ScriptableObject
{
    public Sprite stateImage;
    public string stateName;
    public string stateDescription;
    [Range(0, 10)] public int strength = 1;
    [Range(0f, 1f)] public float moveSpeedFixValue;
    [Range(0f, 1f)] public float workSpeedFixValue;
    [Range(0, 100)] public int infectionChance;
    [Range(0, 100)] public int coughChance;
}
