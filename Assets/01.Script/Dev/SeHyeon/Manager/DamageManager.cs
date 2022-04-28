using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    static DamageManager instance = null;

    void Start()
    {
        if(null == instance)
        {
            instance = this;
        }    
    }
  public static DamageManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    void Update()
    {
        
    }
   
}
