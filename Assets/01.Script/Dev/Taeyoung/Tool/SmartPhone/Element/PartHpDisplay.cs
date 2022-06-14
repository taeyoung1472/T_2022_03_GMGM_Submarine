using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartHpDisplay : MonoBehaviour
{
    public static PartHpDisplay instance;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color lightColor = Color.white;
    [SerializeField] private Color middleColor = Color.white;
    [SerializeField] private Color heavyColor = Color.white;
    [SerializeField] private Color severeColor = Color.white;
    [SerializeField] private Color blackoutColor = Color.white;
    [SerializeField] private SpriteRenderer head;
    [SerializeField] private SpriteRenderer thorax;
    [SerializeField] private SpriteRenderer rArm;
    [SerializeField] private SpriteRenderer lArm;
    [SerializeField] private SpriteRenderer rLeg;
    [SerializeField] private SpriteRenderer lLeg;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
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
