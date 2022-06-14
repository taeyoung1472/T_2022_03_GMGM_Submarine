using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealStateDisplay : MonoBehaviour
{
    public static HealStateDisplay instance;
    [SerializeField] private TextMeshPro healthStateTMP;
    string innerHitText = "외상 : ";
    string outterHitText;
    string mentalicText;
    string fluText;
    string fatigueText;

    Dictionary<string, HealthStateDataSO> stateDatas = new Dictionary<string, HealthStateDataSO>();
    //List<HealthStateDataSO> stateDatas = new List<HealthStateDataSO>();

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void AddState(HealthStateDataSO data, PlayerPartType type)
    {
        string typeString = type.ToString();
        if (stateDatas.ContainsKey(typeString))
        {
            stateDatas[typeString] = data;
        }
        else
        {
            stateDatas.Add(typeString, data);
        }
        DisplayInfo();
    }   
    public void MinusState(PlayerPartType type)
    {
        string typeString = type.ToString();
        if (stateDatas.ContainsKey(typeString))
        {
            stateDatas.Remove(typeString);
        }
        else
        {
            //예외!
        }
        DisplayInfo();
    }
    public void DisplayInfo()
    {
        string result = "";
        result += innerHitText;
        result += "\n<size=75%>";
        foreach (HealthStateDataSO data in stateDatas.Values)
        {
            if (data.strength > 7)
                result += "<color=#FF3333>";
            else if(data.strength > 4)
                result += "<color=#FF8033>";
            else
                result += "<color=#FFFF33>";
            result += $"{data.stateName}({data.name})";
            result += "</color>";
        }
        result += "</size>\n";
        healthStateTMP.text = result;
    }
}
