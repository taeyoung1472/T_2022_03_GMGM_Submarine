using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineRoomCtr : Room
{
    EngineRoomCtr engineRoomCtr;
    void Start()
    {
        engineRoomCtr = GetComponent<EngineRoomCtr>();
       
    }
    private void Update()
    {
        DamageManager.Instance.PartHole(hp);
        DamageManager.Instance.FloodHole(engineRoomCtr.id, engineRoomCtr.hp);
        Debug.Log($"���� HP : {engineRoomCtr.id} {engineRoomCtr.hp}");
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log($"{engineRoomCtr.id}: �浹��");
            engineRoomCtr.hp -= engineRoomCtr.damageValue;
        }
    }

  
}
