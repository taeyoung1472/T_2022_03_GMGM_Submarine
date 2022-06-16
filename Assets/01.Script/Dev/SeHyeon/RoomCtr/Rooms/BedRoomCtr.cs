using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedRoomCtr : Room
{
    BedRoomCtr bedRoomCtr;
      void Start()
    {
        bedRoomCtr = GetComponent<BedRoomCtr>();
    }
    private void Update()
    {
        DamageManager.Instance.PartHole(hp);
        DamageManager.Instance.FloodHole(bedRoomCtr.id, bedRoomCtr.hp);
        Debug.Log($"���� HP : {bedRoomCtr.id} {bedRoomCtr.hp}");
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log($"{bedRoomCtr.id}: �浹��");
            bedRoomCtr.hp -= bedRoomCtr.damageValue;
        }
    }

  
}
