using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject panel;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else if(Instance != this)
        {
            Debug.LogError("Instance �� �̹� ����");
        }
            Destroy(this);
    }
    public void Connect()
    {
        panel.SetActive(false);
        Client.Instance.ConnectToServer();
    }
}
