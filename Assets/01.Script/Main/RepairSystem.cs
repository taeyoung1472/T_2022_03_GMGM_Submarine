using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairSystem : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField] private int[] partDamageValue;
    [SerializeField] private bool[] isRepairing;
    public void Start()
    {
        StartCoroutine(RepairCor());
    }
    [ContextMenu("병주고 약주기")]
    public void Repair()
    {
        for (int i = 0; i < 3; i++)
        {
            int rand1 = Random.Range(0, partDamageValue.Length), rand2 = Random.Range(1, 10);
            partDamageValue[rand1] = Random.Range(1, rand2);
            isRepairing[rand1] = true;
        }
    }
    public IEnumerator RepairCor()
    {
        int i = 0;
        while (true)
        {
            while (i == 0)
            {
                foreach (var item in isRepairing)
                {
                    if (item)
                    {
                        i++;
                    }
                }
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitUntil(() => i != 0);
            for (int j = 0; j < partDamageValue.Length; j++)
            {
                if (isRepairing[j])
                {
                    if(partDamageValue[j] > 1)
                    {
                        partDamageValue[j]--;
                    }
                    else
                    {
                        partDamageValue[j]--;
                        i--;
                        isRepairing[j] = false;
                    }
                }
            }
            yield return new WaitForSeconds(0.25f * i);
        }
    }
}
enum PartObject
{
    
}