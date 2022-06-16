using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoStorageLaunchCtr : Room
{
    TorpedoStorageLaunchCtr torpedoStorageLaunchCtr;
      void Start()
    {
        torpedoStorageLaunchCtr = GetComponent<TorpedoStorageLaunchCtr>();
    }
    private void Update()
    {
        DamageManager.Instance.PartHole(hp);    
        DamageManager.Instance.FloodHole(torpedoStorageLaunchCtr.id, torpedoStorageLaunchCtr.hp);
        Debug.Log($"지금 HP : {torpedoStorageLaunchCtr.id} {torpedoStorageLaunchCtr.hp}");
    }   
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log($"{torpedoStorageLaunchCtr.id}: 충돌함");
            torpedoStorageLaunchCtr.hp -= torpedoStorageLaunchCtr.damageValue;
        }
    }

   
}
