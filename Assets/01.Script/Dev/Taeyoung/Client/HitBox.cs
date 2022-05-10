using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private float damageValue;
    public void Damaged(float damage)
    {
        enemy.Damaged(damage * damageValue);
    }
}
