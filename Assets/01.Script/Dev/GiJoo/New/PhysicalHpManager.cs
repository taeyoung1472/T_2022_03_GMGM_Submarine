using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalHpManager : MonoBehaviour
{
    [SerializeField] private HpManager hpManager;
    [SerializeField] private GameObject player;
    
    private PlayerStateData playerSO;
    private PlayerMove_Improve playerMove;

    int woundInfectionCount = 0;
    float percent = 0;

    public bool IsWound;

    public void Start()
    {
        playerMove = player.GetComponent<PlayerMove_Improve>();
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
    void WoundInfection(HealthStateDataSO data)
    {
        IsWound = true;
        woundInfectionCount++;
        StartCoroutine(WoundInfectionStep(data));
    }
    private IEnumerator WoundInfectionStep(HealthStateDataSO data)
    {
        playerMove.nowSpeed -= 0.05f;
        playerMove.handlingSpeed -= 0.05f;
        while(woundInfectionCount == 2)
        {
            yield return new WaitForSeconds(10f);
            player.GetComponent<MentalicHpManager>().GetDamage(1);
        }
        if(woundInfectionCount == 3)
        {
            playerSO.PRSp *= 0.9f;
            playerSO.PSpd *= 0.9f;
            playerSO.PMHS *= 0.9f;
        }
    }
    public void SimpleOperation()
    {
        woundInfectionCount = 0;

    }
}