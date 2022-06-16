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
        Debug.Log($"지금 HP : {operatingRoomCtr.id} {operatingRoomCtr.hp}");
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log($"{operatingRoomCtr.id}: 충돌함");
            operatingRoomCtr.hp -= operatingRoomCtr.damageValue;
        }
    }

  
}
