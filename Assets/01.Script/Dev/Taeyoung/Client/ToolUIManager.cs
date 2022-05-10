using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToolUIManager : MonoBehaviour
{
    [SerializeField] private ToolUI tamplateObject;
    [SerializeField] private GameObject[] categoryContents;
    [SerializeField] List<CategoriUIElement> categoryList = new List<CategoriUIElement>();
    ToolUI prevSelectedToolUI = null;
    public void InitializeUI(ToolCategory[] toolCategory)
    {
        int category = 0;
        int index = 0;
        foreach (ToolCategory data in toolCategory)
        {
            foreach (ToolData item in data.Tools)
            {
                ToolUI toolUI = Instantiate(tamplateObject) as ToolUI;
                toolUI.transform.SetParent(categoryContents[category].transform);
                toolUI.gameObject.SetActive(true);
                toolUI.InitializeData(item);
                categoryList[category].ToolUI.Add(toolUI);
                //categoryList[category].ToolUI[index].InitializeData(item);
                index++;
            }
            category++;
        }
    }
    public void Select(int category, int index)
    {
        if(prevSelectedToolUI != null) prevSelectedToolUI.OutFocusing();
        categoryList[category].ToolUI[index].InFocusing();
        prevSelectedToolUI = categoryList[category].ToolUI[index];
    }
}
[Serializable]
public class CategoriUIElement
{
    [SerializeField] private List<ToolUI> toolUI;
    public List<ToolUI> ToolUI { get { return toolUI; } }
}