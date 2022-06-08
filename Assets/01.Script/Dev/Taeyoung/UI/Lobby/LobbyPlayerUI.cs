using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LobbyPlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameTMP;
    [SerializeField] private Image profileImage;
    UserData userData = null;
    public TextMeshProUGUI NameTMP { get => nameTMP; }
    public Image ProfileImage { get => profileImage; }
    public UserData UserData { get => userData; set { userData = value; } }
    [ContextMenu("Init")]
    public void Init()
    {
        nameTMP = Transform.FindObjectOfType<TextMeshProUGUI>();
    }
}