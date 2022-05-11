using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    float timer = 0;
    public int damageValue;
    public int hp = 1000;
    public int id = 0;
   
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Ãæµ¹ÇÔ");
            hp -= damageValue;
            DamageManager.instance.OnHit(id, hp);
        }
    }

}
