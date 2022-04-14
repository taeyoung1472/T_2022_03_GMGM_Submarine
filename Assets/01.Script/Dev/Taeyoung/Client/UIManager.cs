using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject panel;
    [SerializeField] private InputField nameInputField;
    [SerializeField] private Text rayInfoName;
    [SerializeField] private Text rayInfoDesc;
    public Text RayInfoName { get { return rayInfoName; } }
    public Text RayInfoDesc { get { return rayInfoDesc; } }
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else if(Instance != this)
        {
            Debug.LogError("Instance �� �̹� ����");
            Destroy(this);
        }
    }
    public void AAA()
    {
        print("��ư ����");
    }
    public void Connect()
    {
        print("A");
        panel.SetActive(false);
        Client.Instance.ConnectToServer();
    }
}
