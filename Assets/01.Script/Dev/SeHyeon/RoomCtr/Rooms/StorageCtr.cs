using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageCtr : Room
{
    
    StorageCtr a;
    private void Start()
    {
      a= GetComponent<StorageCtr>();
    }
    private void Update()
    {
        DamageManager.Instance.FloodHole(a.id, a.hp);
        Debug.Log($"지금 HP : {a.id} {a.hp}");
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
            {
            Debug.Log($"{a.id}: 충돌함");
            a.hp -= a.damageValue;  
        }
    }
}
