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

            Debug.Log("ġ�� �����մϴ�.");
            Debug.Log($"���� Hp : {partHp.Hp}");
            StopCoroutine("Recovery");
            StartCoroutine(Recovery(item));
        }
    }
    public IEnumerator Recovery(HpItemStateSO item)
    {
        Debug.Log("ġ�� �غ���...");
        yield return new WaitForSeconds(item.CoolDown);


        partHp.Hp += dirHpArea * item.DirectRecovery * 0.01f;
        Debug.Log($"���� ġ��, ���� Hp : {partHp.Hp}");
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
                Debug.Log($"���� ġ��, ���� Hp : {partHp.Hp}");
                partHp.CheckHp();
                indirTimer = 0;
            }
        }
        Debug.Log("ġ�� ����.");
    }

    //��ư�� Ŭ���ؼ� �������� ��Ŀ������ �����ϴ� ���
    //��ư �Ǵ� ��ũ���ͺ� ������Ʈ�� �Ϸù�ȣ�� �ο��ؼ� index���� ��ư�� ������ �� UseItem(item[index])�� ����ǵ��� �Ѵ�.
}
