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
    [ContextMenu("�ʱ�ȭ")]
    public void InitializeDataTest()
    {
        nameText.text = toolData.name;
        profileImage.sprite = toolData.image;
        rect = GetComponentInChildren<RectTransform>();
    }
    [ContextMenu("����")]
    public void InFocusing()
    {
        rect.sizeDelta = new Vector2(200, 100);
        profileImage.enabled = true;
    }
    [ContextMenu("��������")]
    public void OutFocusing()
    {
        rect.sizeDelta = new Vector2(200, 40);
        profileImage.enabled = false;
    }
}
