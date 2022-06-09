using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecovery : MonoBehaviour
{
    [SerializeField]
    private HpItemStateSO[] hpItems;
    [SerializeField]
    private GameObject partParent;
    private PartHP partHp;

    private float dirHpArea = 0;
    private float indirHpArea = 0;


    private void Start()
    {
        partHp = partParent.GetComponentInChildren<PartHP>();
    }

    public void SetHpArea()
    {
        dirHpArea = (partHp.maxHp - partHp.Hp) * 0.5f;
        indirHpArea = (partHp.maxHp - partHp.Hp) * 0.5f;
    }

    public void UseItem(HpItemStateSO item)
    {
        if (partHp.Hp < partHp.maxHp)
        {

            Debug.Log("치료 시작합니다.");
            Debug.Log($"현재 Hp : {partHp.Hp}");
            StopCoroutine("Recovery");
            StartCoroutine(Recovery(item));
        }
    }
    public IEnumerator Recovery(HpItemStateSO item)
    {
        Debug.Log("치료 준비중...");
        yield return new WaitForSeconds(item.CoolDown);


        partHp.Hp += dirHpArea * item.DirectRecovery * 0.01f;
        Debug.Log($"직접 치료, 현재 Hp : {partHp.Hp}");
        yield return null;

        float totalTimer = 0f, indirTimer = 0f;
        while(totalTimer < item.IndirectRecoveryTime)
        {
            totalTimer += Time.deltaTime;
            indirTimer += Time.deltaTime;
            yield return null;
            if(indirTimer >= item.IndirSecond)
            {
                partHp.Hp += indirHpArea * 0.01f;
                Debug.Log($"간접 치료, 현재 Hp : {partHp.Hp}");
                partHp.CheckHp();
                indirTimer = 0;
            }
        }
        Debug.Log("치료 종료.");
    }

    //버튼을 클릭해서 아이템의 리커버리를 실행하는 방식
    //버튼 또는 스크립터블 오브젝트에 일련번호를 부여해서 index번의 버튼을 눌렀을 때 UseItem(item[index])가 실행되도록 한다.
}
