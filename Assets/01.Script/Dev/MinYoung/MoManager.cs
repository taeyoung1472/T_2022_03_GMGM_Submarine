using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoManager : MonoBehaviour
{
    public static MoManager instance;

    private int _money = 1000;
    public int Money
    {
        get => _money;
        set
        {
            _money = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }

}
