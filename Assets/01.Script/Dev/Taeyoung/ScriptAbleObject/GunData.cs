using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "��ũ��Ʈ ���̺�/���¿�/Gun")]
public class GunData : ScriptableObject
{
    public string gunName;
    public int mag;
    public Vector2 recoil;
    public float shootDelay;
    public float reloadDelay;
    public float zoomTime;
    public float damage;
    public Vector3 zoomPos;
    public Vector3 defaultPos;
    public GunMode gunMode;
}
public enum GunMode
{
    Semi,
    Brust,
    Auto
}
