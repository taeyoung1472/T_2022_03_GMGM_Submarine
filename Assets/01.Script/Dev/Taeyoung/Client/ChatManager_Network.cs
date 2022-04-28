using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class ChatManager_Network : MonoBehaviour
{
    public static ChatManager_Network Instance;
    [SerializeField] private GameObject textObject;
    [SerializeField] private Transform content;
    [SerializeField] private InputField inputField;
    public void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            inputField.interactable = true;
            inputField.ActivateInputField();
        }
    }
    public void SendedText(int id, string sendedText, bool isServer)
    {
        Chat_Network chat = Instantiate(textObject, content).GetComponent<Chat_Network>();
        chat.transform.SetSiblingIndex(0);
        Color color;
        if(id == Client.Instance.myId)
            color = Color.green;
        else
            color = Color.white;
        if(isServer)
            color = Color.yellow;
        chat.Set(isServer ? "System" : GameManager_Network.players[id].Username, sendedText, color);
        chat.gameObject.SetActive(true);
    }
    public void SendChat(string input)
    {
        ClientSend.SendText(input);
        inputField.text = "";
        inputField.interactable = false;
    }
    /*[ContextMenu("Å×½ºÆ®")]
    public void SendChat()
    {
        ClientSend.SendText("Taeyoung", "Hello", 0);
        ClientSend.SendText("SuengHyeon", "Nice to meet you ^^", 1);
        ClientSend.SendText("System", "Game Started In Korea", 2);
    }*/
}
