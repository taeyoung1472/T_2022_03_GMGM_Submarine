using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalHpManager : MonoBehaviour
{
    [SerializeField] private HpManager hpManager;
    float percent = 0;
    public bool IsWouned;
    public void Start()
    {
        StartCoroutine(HurtSystem());
    }
    IEnumerator HurtSystem()
    {
        while (true)
        {
            yield return new WaitUntil(() => hpManager.WorstType != InjureType.Default && !IsWouned);
            float timer = Time.time;
            while (true)
            {
                if(hpManager.WorstType == InjureType.Default || hpManager.WorstType == InjureType.Blackout)
                {
                    break;
                }
                if(Time.time > timer)
                {
                    if(Random.value < percent)
                    {
                        WoundeEffect();
                        break;
                    }
                    percent += (float)(hpManager.WorstType + 1) * 0.05f;
                    timer += 60;
                }
                yield return null;
            }
            percent = 0;
        }
    }
    void WoundeEffect()
    {
        IsWouned = true;
    }
}