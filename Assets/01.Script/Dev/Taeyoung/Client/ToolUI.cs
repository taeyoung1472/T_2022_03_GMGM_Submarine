using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolUI : MonoBehaviour
{
    [SerializeField] private Image profileImage;
    [SerializeField] private Text nameText;
    RectTransform rect;
    [SerializeField] ToolData toolData;
    public void InitializeData(ToolData toolData)
    {
        this.toolData = toolData;
        nameText.text = toolData.name;
        profileImage.sprite = toolData.image;
        rect = GetComponentInChildren<RectTransform>();
    }
    [ContextMenu("초기화")]
    public void InitializeDataTest()
    {
        nameText.text = toolData.name;
        profileImage.sprite = toolData.image;
        rect = GetComponentInChildren<RectTransform>();
    }
    [ContextMenu("선택")]
    public void InFocusing()
    {
        rect.sizeDelta = new Vector2(200, 100);
        profileImage.enabled = true;
    }
    [ContextMenu("선택해제")]
    public void OutFocusing()
    {
        rect.sizeDelta = new Vector2(200, 40);
        profileImage.enabled = false;
    }
}
