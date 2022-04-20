using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
public class UIManager_Network : MonoBehaviour
{
    public static UIManager_Network Instance;
    [SerializeField] private GameObject panel;
    [SerializeField] private InputField nameInputField;
    [SerializeField] private InputField ipInputField;
    [SerializeField] private Button connecButton;
    [SerializeField] private Text rayInfoName;
    [SerializeField] private Text rayInfoDesc;
    bool isConnectSucces;
    public Text RayInfoName { get { return rayInfoName; } }
    public Text RayInfoDesc { get { return rayInfoDesc; } }
    public string NameText { get { return nameInputField.text == "" ? "Empty" : nameInputField.text; } }
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else if(Instance != this)
        {
            Debug.LogError("Instance 가 이미 있음");
            Destroy(this);
        }
    }
    public void Connect()
    {
        string[] ipAddress = ipInputField.text.Split(':');
        try
        {
            Client.Instance.ip = ipAddress[0].Trim();
            Client.Instance.port = Convert.ToInt32(ipAddress[1].Trim());
            Client.Instance.ConnectToServer();
            connecButton.interactable = false;
            isConnectSucces = false;
            StartCoroutine(CheckConnect());
        }
        catch (Exception ex)
        {
            print($"잘못된 값을 입력 : {ex}");
        }
    }
    IEnumerator CheckConnect()
    {
        yield return new WaitForSeconds(2.5f);
        if (!isConnectSucces)
        {
            print("연결실패!");
            connecButton.interactable = true;
        }
    }
    public void ConnectSucces()
    {
        panel.SetActive(false);
        isConnectSucces = true;
        connecButton.interactable = true;
    }
}
