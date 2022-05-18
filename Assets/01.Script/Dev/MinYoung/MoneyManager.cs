using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    private int _money = 1000;
    [SerializeField] private Text moneyText;
    private void Awake()
    {
        Money = _money;
        instance = this;
    }
    public bool PurchaseCheck(int cost)
    {
        if(Money - cost >= 0)
        {
            Money -= cost;
            return true;
        }
        else
        {
            return false;
        }
    }
    public int Money
    {
        get => _money;
        set
        {
            _money = value;
            moneyText.text = $"{_money}$";
        }
    }
}
