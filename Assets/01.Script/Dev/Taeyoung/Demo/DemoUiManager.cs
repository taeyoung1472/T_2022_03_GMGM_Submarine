using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoUiManager : MonoBehaviour
{
    [SerializeField] private GameObject[] elementPanels;
    [SerializeField] private GameObject panel;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnOff(!panel.activeSelf);
        }
    }
    public void OnOff(bool isOn)
    {
        panel.SetActive(isOn);
        DemoSoundManager.instance.Click(isOn);
        PlayerStat.IsCanControll = !isOn;
    }
    public void ResetActive()
    {
        foreach (var data in elementPanels)
        {
            data.SetActive(false);
        }
    }
}
