using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox_Network : MonoBehaviour
{
    [SerializeField] private Enemy_Network enemy;
    [SerializeField] private float damageValue;
    public void Hit(float dmg)
    {
        enemy.Damaged(dmg * damageValue);
    }
}
