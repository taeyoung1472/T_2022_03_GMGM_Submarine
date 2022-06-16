using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageCtr : Room
{
    StorageCtr storageCtr;
     void Start()
    {
        
        storageCtr = GetComponent<StorageCtr>();
    }
    private void Update()
    {
        DamageManager.Instance.PartHole(hp);
        DamageManager.Instance.FloodHole(storageCtr.id, storageCtr.hp);
        Debug.Log($"���� HP : {storageCtr.id} {storageCtr.hp}");
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log($"{storageCtr.id}: �浹��");
            storageCtr.hp -= storageCtr.damageValue;
        }   
    }
  

}
