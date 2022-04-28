using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{
    void Update()
    {
        foreach (var item in GameManager_Network.players.Keys)
        {
            print($"GameManager_Network.players : {item}");
        }   
    }
}
