using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SmartPhone : MonoBehaviour
{
    [SerializeField] private GameObject phone;
    [SerializeField] private Vector3 usePos;
    [SerializeField] private Vector3 deUsePos;
    bool isUsing = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isUsing = !isUsing;
            UsePhone(isUsing);
        }
    }
    void UsePhone(bool isUse)
    {
        Sequence seq = DOTween.Sequence();
        if (isUse)
        {
            seq.AppendCallback(() => phone.SetActive(true));
            seq.Append(phone.transform.DOLocalMove(usePos, 1f));
        }
        else
        {
            seq.Append(phone.transform.DOLocalMove(deUsePos, 1f));
            seq.AppendCallback(() => phone.SetActive(false));
        }
    }
}
