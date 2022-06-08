using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FocusAbleElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject outLine;
    [ContextMenu("�ڽļ���")]
    public void SetOutLine()
    {
        try
        {
            outLine = transform.Find("OutLine").gameObject;
        }
        catch
        {
            print("�ڽ��߿��� OutLine�̶�� �̸��� ������Ʈ�� �����ϴ�");
        }
    }
    public void Start()
    {
        outLine.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        outLine.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outLine.SetActive(false);
    }
}
