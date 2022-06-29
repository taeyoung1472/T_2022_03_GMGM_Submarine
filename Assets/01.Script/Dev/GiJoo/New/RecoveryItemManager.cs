using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryItemManager : MonoBehaviour
{
    private HpItemStateSO nowHpItemSO = null;
    public HpItemStateSO NowHpItemSO { get { return nowHpItemSO; } set { nowHpItemSO = value; } }
    public bool IsItemOn { private set; get; } = false;

    public void SetNowItem(HpItemStateSO item)
    {
        nowHpItemSO = item;
        IsItemOn = !IsItemOn;
    }
}
