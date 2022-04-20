using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChatManager_Network : MonoBehaviour
{
    public static ChatManager_Network Instance;
    [SerializeField] private GameObject textObject;
    [SerializeField] private Transform content;
    public void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    public void SendedText(string userName, string sendedText, int mode)
    {
        Chat_Network chat = Instantiate(textObject, content).GetComponent<Chat_Network>();
        Color color;
        switch (mode)
        {
            case 0://내가보냄
                color = Color.green;
                break;
            case 1://다른 플레이어가 보냄
                color = Color.white;
                break;
            case 2://시스템
                color = Color.blue;
                break;
            default:
                color = Color.white;
                break;
        }
        chat.Set(userName, sendedText, color);
        chat.gameObject.SetActive(true);
    }
    [ContextMenu("테스트")]
    public void SendChat()
    {
        ClientSend.SendText("Taeyoung", "Hello", 0);
        ClientSend.SendText("SuengHyeon", "Nice to meet you ^^", 1);
        ClientSend.SendText("System", "Game Started In Korea", 2);
    }
}
