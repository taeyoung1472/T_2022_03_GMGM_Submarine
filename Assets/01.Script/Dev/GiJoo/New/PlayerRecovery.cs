using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecovery : MonoBehaviour
{
    [SerializeField]
    private MentalicHpManager mentalicHp;
    [SerializeField]
    private HpItemStateSO[] hpItems;
    [SerializeField]
    private GameObject partParent;
    private PartHP partHp;
    private PartHP[] partHps;

    [SerializeField]
    private HpManager hpManager;
    [SerializeField]
    private PlayerPartType type;

    private float dirHpArea = 0;
    private float indirHpArea = 0;

    private void Start()
    {
        partHp = partParent.GetComponentInChildren<PartHP>();
        partHps = partParent.GetComponentsInChildren<PartHP>();
    }

    public void SetHpArea()
    {
        dirHpArea = (partHp.MaxHp - partHp.Hp) * 0.5f;
        indirHpArea = (partHp.MaxHp - partHp.Hp) * 0.5f;
    }

    public void UseItem(HpItemStateSO item)
    {
        if (partHp.Hp < partHp.MaxHp)
        {

            Debug.Log("ġ�� �����մϴ�.");
            Debug.Log($"���� Hp : {partHp.Hp}");
            StopCoroutine("Recovery");
            StartCoroutine(Recovery(item));
        }
    }
    public IEnumerator Recovery(HpItemStateSO item)
    {
        Debug.Log($"{item.name} ġ�� �غ���...");
        yield return new WaitForSeconds(item.CoolDown);

        //hpManager.Damaged(-(float)System.Math.Round(dirHpArea * item.DirectRecovery * 0.01f, 1, System.MidpointRounding.AwayFromZero), type);
        partHp.Hp += (float)System.Math.Round(dirHpArea * item.DirectRecovery * 0.01f, 1, System.MidpointRounding.AwayFromZero);
        mentalicHp.GetDamage(mentalicHp.MentalHp * item.NoMachMentalDown * 0.01f);
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
                partHp.Hp += (float)System.Math.Round(indirHpArea * 0.01f,2,System.MidpointRounding.AwayFromZero);
                Debug.Log($"���� ġ��, ���� Hp : {partHp.Hp}");
                partHp.CheckHp();
                indirTimer = 0;
            }
        }
        Debug.Log("ġ�� ����.");
    }
}
