using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ResultSceneManager : MonoBehaviour
{
    [SerializeField] private Analysis anal;
    [SerializeField] private Text dayText;
    [SerializeField] private DescriptionItemSO[] datas;
    private int day;
    public static ResultSceneManager Instance;
    public void Awake()
    {
        Instance = this;
        dayText.text = $"Day : {day}";
    }
    public void GoNextDay()
    {
        anal.Set(datas);
        MoneyManager.instance.Money += 1000;
        day++;
        dayText.text = $"Day : {day}";
    }
}
