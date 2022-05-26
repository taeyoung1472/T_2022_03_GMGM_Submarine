using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthStateDisplay : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text descText;
    [SerializeField] private Image backGroundImage;
    [SerializeField] private Image image;
    [SerializeField] private Color weakColor;
    [SerializeField] private Color middleColor;
    [SerializeField] private Color StrongColor;
    [SerializeField] private Color severeColor;
    HealthStateDataSO currentData;
    public HealthStateDataSO CurrentData { get { return currentData; } }
    public void Set(HealthStateDataSO data, PlayerPartType type)
    {
        nameText.text = $"{data.stateName} ({type})";
        descText.text = data.stateDescription;
        image.sprite = data.stateImage;
        if (data.strength >= 8)
            backGroundImage.color = severeColor;
        else if (data.strength >= 6)
            backGroundImage.color = StrongColor;
        else if (data.strength >= 4)
            backGroundImage.color = middleColor;
        else if (data.strength >= 1)
            backGroundImage.color = weakColor;
        currentData = data;
    }
}
