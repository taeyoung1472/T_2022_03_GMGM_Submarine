using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Gijoo : MonoBehaviour
{
    public int dayCount = 0;
    private float dayTimer = 0f;


    public IEnumerator NextDayComing()
    {
        while(true)
        {
            while(dayTimer <= 30f)
            {
                dayTimer += Time.deltaTime;
                yield return null;
            }
            dayCount++;
        }
    }
}
