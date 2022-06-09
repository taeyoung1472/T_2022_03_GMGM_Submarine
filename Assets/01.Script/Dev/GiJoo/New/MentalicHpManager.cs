using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentalicHpManager : MonoBehaviour
{
    [SerializeField] private float mentalHp = 100;
    public float MentalHp { get { return mentalHp; }set { mentalHp = value; } }
    public void GetDamage(float dmg)
    {
        mentalHp -= dmg;
        print($"������ ���� ���� ���ŷ� : {mentalHp}");
    }
}
