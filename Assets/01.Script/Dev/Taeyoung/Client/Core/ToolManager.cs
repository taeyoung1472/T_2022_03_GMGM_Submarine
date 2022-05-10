using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ToolManager : MonoBehaviour
{
    [SerializeField] List<ToolCategory> toolCategories = new List<ToolCategory>();
    [SerializeField] int categoryIndex;
    [SerializeField] int selectIndex;
    [SerializeField] private Transform hand;
    [SerializeField] private GameObject obj;
    [SerializeField] private ToolUIManager toolUIManager;
    Dictionary<string, GameObject> toolObject = new Dictionary<string, GameObject>();
    float selectWaitTime;
    bool isSelecting;
    GameObject prevToolObject;

    public static ToolManager instance;
    [SerializeField] private Text magText;
    public Text MagText { get { return magText; } }
    [SerializeField] private RepairUI repairUI;
    public RepairUI RepairUI { get { return repairUI; } }
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void Start()
    {
        toolUIManager.InitializeUI(toolCategories.ToArray());
    }
    public void Update()
    {
        SelectTool();
        if(Time.time < selectWaitTime)
        {
            EnableUI();
        }
        else
        {
            DisableUI();
        }
    }
    void SelectTool()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetKeyInput(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetKeyInput(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GetKeyInput(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GetKeyInput(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            GetKeyInput(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            GetKeyInput(5);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && isSelecting)
        {
            Select();
        }
    }
    void GetKeyInput(int key)
    {
        if (categoryIndex != key)
        {
            categoryIndex = key;
            selectIndex = 0;
            ChangeSelcetType(key);
        }
        else
        {
            ChangingSelect();
        }
        toolUIManager.Select(categoryIndex, selectIndex);
        selectWaitTime = Time.time + 1;
    }
    void EnableUI()
    {
        isSelecting = true;
        obj.SetActive(true);
    }
    void ChangeSelcetType(int value)
    {
        categoryIndex = value;
    }
    void ChangingSelect()
    {
        selectIndex++;
        if(selectIndex >= toolCategories[categoryIndex].Tools.Count)
        {
            selectIndex = 0;
        }
    }
    void Select()
    {
        if(prevToolObject != null)
        {
            prevToolObject.SetActive(false);
        }
        string name = toolCategories[categoryIndex].Tools[selectIndex].name;
        if (toolObject.ContainsKey(name))
        {
            toolObject[name].SetActive(true);
        }
        else
        {
            GameObject obj = Instantiate(toolCategories[categoryIndex].Tools[selectIndex].toolPrefab, hand);
            toolObject[name] = obj;
        }
        prevToolObject = toolObject[name];
        selectWaitTime = 0;
        DisableUI();
    }
    void DisableUI()
    {
        isSelecting = false;
        obj.SetActive(false);
        categoryIndex = -1;
    }
}
[Serializable]
public class ToolCategory
{
    [SerializeField] private List<ToolData> tools = new List<ToolData>();
    public List<ToolData> Tools { get { return tools; } }
}