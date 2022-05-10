using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Gijoo : MonoBehaviour
{
    public int dayCount = 1;

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
            yield return new WaitForSeconds(600f);
            dayCount++;
            Debug.Log(dayCount + "ÀÏÂ÷.");
            playerHealth.WoundInfectionDayCount();
            playerHealth.ColdDayCount();
            playerHealth.PneumoniaDayCount();
            playerHealth.SleepDayCount();
        }
    }
}
