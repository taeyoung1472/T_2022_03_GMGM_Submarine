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
        Debug.Log($"지금 HP : {cockpitCtr.id} {cockpitCtr.hp}");
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log($"{cockpitCtr.id}: 충돌함");
            cockpitCtr.hp -= cockpitCtr.damageValue;
        }
    }

   
}
