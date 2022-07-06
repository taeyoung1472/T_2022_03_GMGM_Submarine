using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : MonoBehaviour
{
    [Header("规狼 备己 夸家")]
    public RandomPosGetter[] walls;
    public int damageValue;
    public int hp = 1000;
    public int id = 0;

   protected virtual Vector3[] RandomWall()
    {
        Vector3[] a = {};
       for(int i = 0; i < walls.Length; i++)
        {
            a[i] = walls[i].GetRandomPos();
        }
       return a;
    }
    protected abstract void OnCollisionEnter(Collision collision);
    
    

}
