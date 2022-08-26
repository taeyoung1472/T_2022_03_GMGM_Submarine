using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;
    public List<int> joinedId;
    public void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void PlayerConnected(int id)
    {
        if (!joinedId.Contains(id))
        {
            joinedId.Add(id);
        }
    }
}