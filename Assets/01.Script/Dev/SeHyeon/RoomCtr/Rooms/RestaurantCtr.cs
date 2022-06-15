using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantCtr : Room
{
    RestaurantCtr a;
    private void Start()
    {
        a=GetComponent<RestaurantCtr>();
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

    protected override void SuriBuwi()
    {
        throw new System.NotImplementedException();
    }
}
