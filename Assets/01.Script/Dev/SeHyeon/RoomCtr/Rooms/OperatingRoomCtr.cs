using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatingRoomCtr : Room
{
    OperatingRoomCtr operatingRoomCtr;
     void Start()
    {
    
        operatingRoomCtr = GetComponent<OperatingRoomCtr>();
    }
    private void Update()
    {
        DamageManager.Instance.PartHole(hp);
        DamageManager.Instance.FloodHole(operatingRoomCtr.id, operatingRoomCtr.hp);
        Debug.Log($"���� HP : {operatingRoomCtr.id} {operatingRoomCtr.hp}");
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log($"{operatingRoomCtr.id}: �浹��");
            operatingRoomCtr.hp -= operatingRoomCtr.damageValue;
        }
    }

  
}
