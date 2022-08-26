using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateTimeSystem : MonoBehaviour
{
    [SerializeField] private int realSecToGameSec;
    DateTime dateTime;
    private void Start()
    {
        StartCoroutine(TimeSystem());
    }
    IEnumerator TimeSystem()
    {
        WaitForSeconds ws = new WaitForSeconds(1);
        while (true)
        {
            dateTime.AddSeconds(realSecToGameSec);
            print($"Sec : {dateTime.Second}");
            yield return ws;
        }
    }
}
