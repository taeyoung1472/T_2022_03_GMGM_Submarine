using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecovery : MonoBehaviour
{
    [SerializeField]
    private HpItemStateSO[] hpItems;
    [SerializeField]
    private PartHP PartHP;

    private float dirHpArea = 0;
    private float indirHpArea = 0;

    private float timer = 0f;

    public void UseItem(HpItemStateSO item)
    {
        if (PartHP.Hp < PartHP.maxHp)
        {
            StopCoroutine("Recovery");
            StartCoroutine(Recovery(item));
        }
    }
    public IEnumerator Recovery(HpItemStateSO item)
    {
        yield return new WaitForSeconds(item.CoolDown);
        PartHP.Hp += dirHpArea * item.DirectRecovery * 0.01f;
        while(timer < item.IndirectRecoveryTime)
        {
            timer += Time.deltaTime;
            yield return null;
            if(timer >= item.IndirSecond && timer % item.IndirSecond <= 0.01f)
            {
                PartHP.Hp += indirHpArea * 0.01f;
            }
        }
    }
}
