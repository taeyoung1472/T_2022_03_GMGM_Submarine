using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantCtr : Room
{
    RestaurantCtr restaurantCtr;
     void Start()
    {
        restaurantCtr=GetComponent<RestaurantCtr>();
    }

    private void Update()
    {
        DamageManager.Instance.PartHole(hp);
        DamageManager.Instance.FloodHole(restaurantCtr.id, restaurantCtr.hp);
        Debug.Log($"지금 HP : {restaurantCtr.id} {restaurantCtr.hp}");
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log($"{restaurantCtr.id}: 충돌함");
            restaurantCtr.hp -= restaurantCtr.damageValue;
        }
    }

    
}
