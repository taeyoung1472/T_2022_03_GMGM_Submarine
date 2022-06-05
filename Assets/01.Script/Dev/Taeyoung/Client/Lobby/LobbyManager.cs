using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;
    private UserData myData;
    private Dictionary<string, UserData> users = new Dictionary<string, UserData>();
    int userCount;
    public void Awake()
    {
        if(instance == null)
            instance = this;
    }
    public void Ready()
    {
        ClientSend.Lobby_Ready(myData.Name, !myData.IsReady);
    }
    public void UpdateUserData(Packet packet)
    {
        int length = packet.ReadInt();
        print($"{length} ���� ���� ������ ����");
        for (int i = 0; i < length; i++)
        {
            string name = packet.ReadString();
            bool isReady = packet.ReadBool();
            bool isLeader = packet.ReadBool();
            if (!users.ContainsKey(name))
            {
                UserData data = new UserData(name);
                data.IsReady = isReady;
                data.IsLeader = isLeader;
                users.Add(name, data);
                print($"User({name}) �� �����");
            }
            else
            {
                users[name].IsReady = isReady;
                users[name].IsLeader = isLeader;
                print($"User({name}) �� ������ ������Ʈ��");
            }
        }
    }
}