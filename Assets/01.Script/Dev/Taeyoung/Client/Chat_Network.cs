using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Chat_Network : MonoBehaviour
{
    [SerializeField] private Text text;
    public void Set(string userName, string sendedText, Color color)
    {
        text.color = color;
        text.text = $"{userName} {DateTime.Now.Hour}:{DateTime.Now.Minute} : {sendedText}";
    }
}
