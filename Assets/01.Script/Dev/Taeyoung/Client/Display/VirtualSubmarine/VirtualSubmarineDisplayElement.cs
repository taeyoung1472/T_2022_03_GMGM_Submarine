using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

[Serializable]
public class VirtualSubmarineDisplayElement/*가상 잠수함에서 파츠 요소*/
{
    #region 복합 자료형들
    [SerializeField] private SubmarinePartType partType;
    [SerializeField] private List<SpriteRenderer> profileImageList = new List<SpriteRenderer>();
    [SerializeField] private TextMeshPro hpTMP;
    [SerializeField] private float hpPercent = 100f;
    #endregion

    #region 프로피터
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
