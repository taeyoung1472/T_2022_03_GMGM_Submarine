using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "스크립트 에이블/Gijoo/플레이어 스탯")]
public class PlayerStateData : ScriptableObject
{
    [SerializeField]
    private float playerMaxHp;//플레이어의 최대 HP
    [SerializeField]
    private float playerMaxMental;//플레이어의 최대 정신력
    [SerializeField]
    private float playerSpeed;//플레이어의 이동 속도
    [SerializeField]
    private float playerRunningSpeed;//플레이어의 달리기 속도
    [SerializeField]
    private float playerHandlingSpeed;//플레이어의 행동 속도

    public float PMHp { get { return playerMaxHp;} set { playerMaxHp = value; } }
    public float PMMp { get { return playerMaxMental; } set { playerMaxMental = value; } }
    public float PSpd { get { return playerSpeed; } set { playerSpeed = value; } }
    public float PRSp { get { return playerRunningSpeed; } set { playerRunningSpeed = value; } }
    public float PMHS { get { return playerHandlingSpeed; } set { playerHandlingSpeed = value; } }
}
