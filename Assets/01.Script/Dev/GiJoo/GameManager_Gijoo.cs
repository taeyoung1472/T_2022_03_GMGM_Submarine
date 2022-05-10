using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Gijoo : MonoBehaviour
{
    public int dayCount = 1;
    private float dayTimer = 0f;

    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        StartCoroutine(NextDayComing());
    }

    public IEnumerator NextDayComing()
    {
        while(true)
        {
            while(dayTimer <= 600f)
            {
                dayTimer += Time.deltaTime;
                yield return null;
            }
            dayTimer = 0f;
            dayCount++;
            Debug.Log(dayCount + "ÀÏÂ÷.");
            playerHealth.WoundInfectionDayCount();
            playerHealth.ColdDayCount();
            playerHealth.PneumoniaDayCount();
            playerHealth.SleepDayCount();
        }
    }
}
