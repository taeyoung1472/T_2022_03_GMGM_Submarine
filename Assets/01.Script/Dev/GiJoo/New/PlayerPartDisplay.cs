using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPartDisplay : MonoBehaviour
{
    public static PlayerPartDisplay Instance;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color lightColor = Color.white;
    [SerializeField] private Color middleColor = Color.white;
    [SerializeField] private Color heavyColor = Color.white;
    [SerializeField] private Color severeColor = Color.white;
    [SerializeField] private Color blackoutColor = Color.white;
    [SerializeField] private Image head;
    [SerializeField] private Image thorax;
    [SerializeField] private Image rArm;
    [SerializeField] private Image lArm;
    [SerializeField] private Image rLeg;
    [SerializeField] private Image lLeg;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void SetPartColor(PlayerPartType type, float hp)
    {
        Color targetColor;
        if (hp <= 0)
            targetColor = blackoutColor;
        else if (hp <= 25)
            targetColor = severeColor;
        else if (hp <= 50)
            targetColor = heavyColor;
        else if (hp <= 75)
            targetColor = middleColor;
        else if (hp < 100)
            targetColor = lightColor;
        else
            targetColor = defaultColor;

        switch (type)
        {
            case PlayerPartType.Head:
                head.color = targetColor;
                break;
            case PlayerPartType.Thorax:
                thorax.color = targetColor;
                break;
            case PlayerPartType.RightArm:
                rArm.color = targetColor;
                break;
            case PlayerPartType.LeftArm:
                lArm.color = targetColor;
                break;
            case PlayerPartType.RightLeg:
                rLeg.color = targetColor;
                break;
            case PlayerPartType.LeftLeg:
                lLeg.color = targetColor;
                break;
        }
    }
}