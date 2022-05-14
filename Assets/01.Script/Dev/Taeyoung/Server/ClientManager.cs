using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using System;
public class ClientManager : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"서버로부터 온 메시지 : {_msg}");
        Client.Instance.myId = _myId;
        UIManager_Network.Instance.ConnectSucces();
        ClientSend.WelcomeReceved();

        Client.Instance.udp.Connect(((IPEndPoint)Client.Instance.tcp.socket.Client.LocalEndPoint).Port);
    }
    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();

        Vector3 _pos = _packet.ReadVector3();
        Quaternion _rot = _packet.ReadQuaternion();

        GameObject obj = null;
        if (!GameManager_Network.isPlayersSpawn.ContainsKey(_id) || !GameManager_Network.isPlayersSpawn[_id])
        {
            try
            {
                obj = GameManager_Network.Instance.SpawnPlayer(_id, _username, _pos, _rot);
                GameManager_Network.players[Client.Instance.myId].gameObject.GetComponent<PlayerController_Network>().Set();
                GameManager_Network.isPlayersSpawn.Add(_id, true);
                print(GameManager_Network.players[_id]);
                print("플레이어 생성 성공");
            }
            catch (Exception ex)
            {
                print($"플레이어 생성중 오류 다시생성 : {_id} {ex}");
                Destroy(obj);
                GameManager_Network.players.Remove(_id);
                print(GameManager_Network.players[_id]);
                GameManager_Network.isPlayersSpawn[_id] = false;
                ClientSend.RequestSpawnAgain(_id);
            }
        }
    }
    public static void PlayerPositionAndRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _pos = _packet.ReadVector3();
        Quaternion _rot = _packet.ReadQuaternion();
        Vector2 _dir = _packet.ReadVector2();

        try
        {
            GameManager_Network.players[_id].SetPositionAndRotation(_pos, _rot, _dir);
        }
        catch
        {
            ClientSend.RequestSpawnAgain(_id);
        }
    }
    public static void MapPositionAndRotation(Packet _packet)
    {
        try
        {
            Vector3 _pos = _packet.ReadVector3();
            Quaternion _rot = _packet.ReadQuaternion();

            GameManager_Network.Instance.Map.SetPositionAndRotation(_pos, _rot);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }
    public static void PlayerDisconnected(Packet _packet)
    {
        int _id = _packet.ReadInt();

        Destroy(GameManager_Network.players[_id].gameObject);
        GameManager_Network.players.Remove(_id);
    }
    public static void TextSended(Packet packet)
    {
        int id = packet.ReadInt();
        string text = packet.ReadString();
        bool isServer = packet.ReadBool();

        ChatManager_Network.Instance.SendedText(id, text, isServer);
    }
    public static void AudioSended(Packet packet)
    {
        int id = packet.ReadInt();
        Vector3 pos = packet.ReadVector3();

        AudioPacket.Instance.PlayAudio(id, pos);
    }
    public static void ObjectSended(Packet packet)
    {
        int id = packet.ReadInt();
        int index = packet.ReadInt();
        Vector3 pos = packet.ReadVector3();
        Quaternion rot = packet.ReadQuaternion();
        InstantObjectManager_Network.Instance.InstantObject(id, index, pos, rot);
    }
    public static void NetworkPosition(Packet packet)
    {
        try
        {
            NetworkTransformManager.instance.NetTransforms[packet.ReadInt()].SetPosition(packet.ReadVector3());
        }
        catch { /*아직 초기화(Init)이 되지 않은상황*/}
    }
    public static void NetworkRotation(Packet packet)
    {
        try
        {
            NetworkTransformManager.instance.NetTransforms[packet.ReadInt()].SetRotation(packet.ReadQuaternion());
        }
        catch { /*아직 초기화(Init)이 되지 않은상황*/}
    }
    public static void NetworkTransformInit(Packet packet)
    {
        GameManager_Network.Instance.InitNetworkTransform(packet.ReadInt(), packet.ReadString());
    }
    public static void Raycast(Packet packet)
    {
        RaycastHit hit;
        if (RaycastManager.instance.ShootRaycast(packet.ReadVector3(), packet.ReadVector3(),packet.ReadInt(), out hit, packet.ReadFloat()))
        {
            print(hit.transform.name);
        }
    }
    public static void SpawnEnemy(Packet packet)
    {
        int id = packet.ReadInt();
        Vector3 pos = packet.ReadVector3();
        EnemySpawner.instance.SpawnEnemy(id, pos);
    }
    public static void Sync(Packet packet)
    {
        SyncManager.instance.Sync(packet);
    }
}
