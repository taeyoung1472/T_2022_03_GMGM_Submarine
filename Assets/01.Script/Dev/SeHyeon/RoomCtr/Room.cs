using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : MonoBehaviour
{
   protected GameObject suri;
    protected virtual void Start()
    {
        suri = Resources.Load<GameObject>("Prefabs/Suri");
    }
    
      
    

    public int damageValue;
    public int hp = 1000;
    public int id = 0;
    protected abstract void SuriBuwi();
    protected abstract void OnCollisionEnter(Collision collision);
    

}
