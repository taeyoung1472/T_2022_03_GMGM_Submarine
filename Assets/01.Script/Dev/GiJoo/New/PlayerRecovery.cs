using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerRecovery : MonoBehaviour
{
    [SerializeField]
    private MentalicHpManager mentalicHp;
    [SerializeField]
    private HpItemStateSO[] hpItems;
    [SerializeField]
    private GameObject partParent;
    //private PartHP partHp;
    private List<HpElement> hpElements = new List<HpElement>();//PartHP[] partHps;

    [SerializeField]
    private HpManager hpManager;
    [SerializeField]
    private PlayerPartType type;

    private float operateBuff = 1f;

    private void Start()
    {
        //partHp = partParent.GetComponentInChildren<PartHP>();
        PartHP[] parts = partParent.GetComponentsInChildren<PartHP>();
        for (int i = 0; i < parts.Length; i++)
        {
            HpElement hpE = new HpElement();
            hpE.part = parts[i];
            hpElements.Add(hpE);
        }
    }

    public void SetHpArea()
    {
        foreach (var element in hpElements)
        {
            element.dirHp = (element.part.MaxHp - element.part.Hp) * 0.5f;
            element.indirHp = (element.part.MaxHp - element.part.Hp) * 0.5f;
        }
    }

    public void UseItem(HpItemStateSO item)
    {
        foreach (var element in hpElements)
        {
            if (element.part.Hp < element.part.MaxHp)
            {
                Debug.Log("치료 시작합니다.");
                StopCoroutine("Recovery");
                StartCoroutine(Recovery(item));
                Debug.Log($"현재 Hp : {element.part.Hp}");
            }
        }
    }
    public IEnumerator Recovery(HpItemStateSO item)
    {
        Debug.Log($"{item.name} 치료 준비중...");
        yield return new WaitForSeconds(item.CoolDown);
        foreach (var element in hpElements)
        {
            element.part.Hp += (float)System.Math.Round(element.dirHp * item.DirectRecovery * 0.01f, 1, System.MidpointRounding.AwayFromZero);
            element.dirHp = (element.part.MaxHp - element.part.Hp) * 0.5f;
        }

        float totalTimer = 0f, indirTimer = 0f;
        while (totalTimer < item.IndirectRecoveryTime)
        {
            totalTimer += Time.deltaTime;
            indirTimer += Time.deltaTime;
            yield return null;
            if (indirTimer * operateBuff >= item.IndirSecond)
            {
                foreach (var element in hpElements)
                {
                    Debug.Log((float)System.Math.Round(element.indirHp * 0.01f, 2, System.MidpointRounding.AwayFromZero));
                    element.part.Hp += (float)System.Math.Round(element.indirHp * 0.01f, 2, System.MidpointRounding.AwayFromZero);
                    Debug.Log($"간접 치료, 현재 Hp : {element.part.Hp}");
                }
                indirTimer = 0;
            }
        }
    }

    public void Operate()
    {
        foreach (var element in hpElements)
        {
            element.part.Hp += element.dirHp;
            operateBuff = 3f;
        }
    }

    public void GandanOper()
    {
        foreach (var element in hpElements)
        {
            element.part.Hp += element.dirHp * 0.7f;
            operateBuff = 2f;
        }
    }
}
public class HpElement
{
    public PartHP part { set; get; }
    public float dirHp { set; get; } = 0;
    public float indirHp { set; get; } = 0;
}