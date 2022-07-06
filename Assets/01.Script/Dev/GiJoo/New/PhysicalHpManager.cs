using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalHpManager : MonoBehaviour
{
    [SerializeField] private HpManager hpManager;
    [SerializeField] private GameObject player;
    
    private PlayerStateData playerSO;

    int woundInfectionCount = 0;
    float percent = 0;

    public bool IsWound;

    public void Start()
    {
        StartCoroutine(HurtSystem());
    }
    private IEnumerator HurtSystem()
    {
        while (true)
        {
            yield return new WaitUntil(() => hpManager.WorstType != InjureType.Default && !IsWound);
            float timer = Time.time + 60;
            while (true)
            {
                if(hpManager.WorstType == InjureType.Default || hpManager.WorstType == InjureType.Blackout)
                {
                    break;
                }
                if(hpManager.WorstType == InjureType.Heavy && timer >= 900)
                { 

                    //사망 넣어야함
                    break;
                }
                if(Time.time > timer)
                {
                    if(Random.value < percent)
                    {
                        WoundInfection();
                        break;
                    }
                    percent += (float)hpManager.WorstType * 0.05f;
                    timer += 60;
                }
                yield return null;
            }
            percent = 0;
        }
    }
    void WoundInfection()
    {
        IsWound = true;
        woundInfectionCount++;
        StartCoroutine(WoundInfectionStep());
    }
    private IEnumerator WoundInfectionStep()
    {
        playerSO.PSpd -= 0.05f;
        playerSO.PRSp -= 0.05f;
        playerSO.PMHS -= 0.05f;
        while(woundInfectionCount == 2)
        {
            yield return new WaitForSeconds(10f);
            player.GetComponent<MentalicHpManager>().GetDamage(1);
        }
    }
}