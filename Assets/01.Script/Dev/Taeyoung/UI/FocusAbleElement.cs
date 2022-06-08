using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FocusAbleElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject outLine;
    [ContextMenu("자식설정")]
    public void SetOutLine()
    {
        try
        {
            outLine = transform.Find("OutLine").gameObject;
        }
        catch
        {
            print("자식중에서 OutLine이라는 이름의 오브젝트가 없습니다");
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
