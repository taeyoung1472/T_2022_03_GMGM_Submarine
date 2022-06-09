using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockpitCtr : Room
{
    CockpitCtr a;
    private void Start()
    {
        a = GetComponent<CockpitCtr>();
    }
    private void Update()
    {
        DamageManager.Instance.FloodHole(a.id, a.hp);
        Debug.Log($"���� HP : {a.id} {a.hp}");
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log($"{a.id}: �浹��");
            a.hp -= a.damageValue;
        }
    }
}
