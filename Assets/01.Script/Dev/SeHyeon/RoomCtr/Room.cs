using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : MonoBehaviour
{
    public int damageValue;
    public int hp = 1000;
    public int id = 0;
   protected virtual void RandomWall()
    {

    }
    protected abstract void OnCollisionEnter(Collision collision);
    
    

}
