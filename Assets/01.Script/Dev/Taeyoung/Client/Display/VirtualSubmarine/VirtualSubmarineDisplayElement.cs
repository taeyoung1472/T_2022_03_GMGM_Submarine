using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

[Serializable]
public class VirtualSubmarineDisplayElement/*���� ����Կ��� ���� ���*/
{
    #region ���� �ڷ�����
    [SerializeField] private SubmarinePartType partType;
    [SerializeField] private List<SpriteRenderer> profileImageList = new List<SpriteRenderer>();
    [SerializeField] private TextMeshPro hpTMP;
    [SerializeField] private float hpPercent = 100f;
    #endregion

    #region ��������
    public SubmarinePartType PartType { get { return partType; } }
    public List<SpriteRenderer> ProfileImageList { get { return profileImageList; } }
    public TextMeshPro HpTMP { get { return hpTMP; } }
    public float HpPercent { get { return hpPercent; } set { hpPercent = value; } }
    #endregion

    public void Init()
    {
        foreach (var obj in profileImageList)
        {
            obj.gameObject.SetActive(false);
        }
    }
}
