using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public static Popup Instance;
    [SerializeField] private Text nameText;
    [SerializeField] private Text descText;
    [SerializeField] private Text buttonText;
    [SerializeField] private Image image;
    [SerializeField] private Button button;
    [SerializeField] private GameObject popupObject;
    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void DisplayPopup(string name, string desc, Sprite sprite, UnityAction action, string buttonString = "")
    {
        popupObject.SetActive(true);
        nameText.text = name;
        descText.text = desc;
        image.sprite = sprite;
        buttonText.text = buttonString;
        if (buttonString == "")
        {
            button.gameObject.SetActive(false);
            return;
        }
        button.gameObject.SetActive(true);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }
}
