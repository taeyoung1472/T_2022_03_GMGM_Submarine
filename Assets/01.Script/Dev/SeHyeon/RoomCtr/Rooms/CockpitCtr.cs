using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockpitCtr : Room
{
    CockpitCtr cockpitCtr;
   void Start()
    {
      
        cockpitCtr = GetComponent<CockpitCtr>();
    }
    private void Update()
    {
        DamageManager.Instance.PartHole(hp);
        DamageManager.Instance.FloodHole(cockpitCtr.id, cockpitCtr.hp);
        Debug.Log($"���� HP : {cockpitCtr.id} {cockpitCtr.hp}");
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log($"{cockpitCtr.id}: �浹��");
            cockpitCtr.hp -= cockpitCtr.damageValue;
        }
    }

   
}
