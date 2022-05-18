using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private float moveSpeedFixValue = 1f;
    [SerializeField] private float workSpeedFixValue = 1f;
    [SerializeField] private int coughChance = 0;
    [SerializeField] private int infectionChance = 0;
    [SerializeField] private static bool isCanControll = true;
    private bool isCanMove = true;
    public float MoveSpeedFixValue { get { return moveSpeedFixValue; } set { moveSpeedFixValue = value; } }
    public float WorkSpeedFixValue { get { return workSpeedFixValue; } set { workSpeedFixValue = value; } }
    public int CoughChance { get { return coughChance; } set { coughChance = value; } }
    public int InfectionChance { get { return infectionChance; } set { infectionChance = value; } }
    public static bool IsCanControll { get { return isCanControll; } set { isCanControll = value; } }
    public bool IsCanMove { get { return isCanMove; } }
}
