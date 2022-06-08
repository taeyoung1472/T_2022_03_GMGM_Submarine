using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpDisplay : MonoBehaviour
{
    [SerializeField] private HpManager player;
    [SerializeField] private Text hpText;
    public static HpDisplay Instance;
    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void OnEnable()
    {
        DisplayHp();
    }
    public void DisplayHp()
    {
        float hpTotal = 0;
        hpTotal += player.Parts.RightArm.Hp;
        hpTotal += player.Parts.LeftArm.Hp;
        hpTotal += player.Parts.RightLeg.Hp;
        hpTotal += player.Parts.LeftLeg.Hp;
        hpTotal += player.Parts.Thorax.Hp;
        hpTotal += player.Parts.Head.Hp;
        hpText.text = $"{Mathf.RoundToInt(hpTotal)} / 600";
    }
}
