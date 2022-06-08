using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LobbyPlayerListManager : MonoBehaviour
{
    [SerializeField] private Sprite playerSprite, nonePlayerSprite;
    [SerializeField] private List<LobbyPlayerUI> playerUis = new List<LobbyPlayerUI>();
    int playerIdx = 0;
    public void AddPlayer(UserData data)
    {
        playerUis[playerIdx].NameTMP.text = data.Name;
        playerUis[playerIdx].ProfileImage.sprite = playerSprite;
        playerUis[playerIdx].UserData = data;
        playerIdx++;
    }
    public void DisconnectPlayer(UserData data)
    {
        playerIdx--;
        int idx = 0;
        foreach (LobbyPlayerUI ui in playerUis)
        {
            if(ui.name == data.Name)
            {
                //playerUis[idx].NameTMP.text = "...";
                //playerUis[idx].ProfileImage.sprite = nonePlayerSprite;
                for (int i = idx; i < playerUis.Count - 1; i++)
                {
                    if(playerUis[i + 1].UserData != null)
                    {
                        playerUis[i].NameTMP.text = playerUis[i + 1].NameTMP.text;
                        playerUis[i].ProfileImage.sprite = playerSprite;
                        playerUis[i + 1].NameTMP.text = "...";
                        playerUis[i + 1].ProfileImage.sprite = nonePlayerSprite;
                        playerUis[i + 1].UserData = null;
                    }
                }
                return;
            }
            idx++;
        }
    }
}