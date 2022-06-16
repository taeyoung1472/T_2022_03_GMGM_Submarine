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
        Debug.Log($"지금 HP : {bedRoomCtr.id} {bedRoomCtr.hp}");
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log($"{bedRoomCtr.id}: 충돌함");
            bedRoomCtr.hp -= bedRoomCtr.damageValue;
        }
    }

  
}
