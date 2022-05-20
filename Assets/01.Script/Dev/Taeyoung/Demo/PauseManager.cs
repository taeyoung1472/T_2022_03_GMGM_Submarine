using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private bool isServer;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActivePausePanel(!pausePanel.activeSelf);
        }
    }
    public void ActivePausePanel(bool isOn)
    {
        AudioPoolManager.instance.Play(isOn ? openClip : closeClip, transform.position);
        PlayerStat.IsCanControll = !isOn;
        pausePanel.SetActive(isOn);
    }
    public void GoToMain()
    {
        if (isServer)
        {
            Client.Instance.Disconnect();
        }
        PlayerStat.IsCanControll = true;
        SceneManager.LoadScene(0);
    }
    public void Resume()
    {
        ActivePausePanel(false);
    }
}
