using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "��ũ��Ʈ ���̺�/Gijoo/�÷��̾� ����")]
public class PlayerStateData : ScriptableObject
{
    [SerializeField]
    private float playerMaxHp;//�÷��̾��� �ִ� HP
    [SerializeField]
    private float playerMaxMental;//�÷��̾��� �ִ� ���ŷ�
    [SerializeField]
    private float playerSpeed;//�÷��̾��� �̵� �ӵ�
    [SerializeField]
    private float playerRunningSpeed;//�÷��̾��� �޸��� �ӵ�
    [SerializeField]
    private float playerHandlingSpeed;//�÷��̾��� �ൿ �ӵ�

    public float PMHp { get { return playerMaxHp;} set { playerMaxHp = value; } }
    public float PMMp { get { return playerMaxMental; } set { playerMaxMental = value; } }
    public float PSpd { get { return playerSpeed; } set { playerSpeed = value; } }
    public float PRSp { get { return playerRunningSpeed; } set { playerRunningSpeed = value; } }
    public float PMHS { get { return playerHandlingSpeed; } set { playerHandlingSpeed = value; } }
}
