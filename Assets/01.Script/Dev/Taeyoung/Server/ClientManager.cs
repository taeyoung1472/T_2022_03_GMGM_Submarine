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

        print("Spawn : " + _id);

        GameManager_Network.Instance.SpawnPlayer(_id, _username, _pos, _rot);
        GameManager_Network.players[Client.Instance.myId].gameObject.GetComponent<PlayerController_Network>().Set();
    }
    public static void PlayerPositionAndRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _pos = _packet.ReadVector3();
        Quaternion _rot = _packet.ReadQuaternion();
        Vector2 _dir = _packet.ReadVector2();

        print(_id);
        GameManager_Network.players[_id].SetPositionAndRotation(_pos, _rot, _dir);
        /*try
        {
            int _id = _packet.ReadInt();
            Vector3 _pos = _packet.ReadVector3();
            Quaternion _rot = _packet.ReadQuaternion();
            Vector2 _dir = _packet.ReadVector2();

            GameManager_Network.players[_id].SetPositionAndRotation(_pos, _rot, _dir);
        }
        catch(Exception ex)
        {
            Debug.Log(ex);
        }*/
    }
    public static void SubmarinePositionAndRotation(Packet _packet)
    {
        try
        {
            Vector3 _pos = _packet.ReadVector3();
            Quaternion _rot = _packet.ReadQuaternion();

            GameManager_Network.Instance.Submarine.SetPositionAndRotation(_pos, _rot);
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

    public static void PlayerHealth(Packet _packet)
    {
        /*int _id = _packet.ReadInt();
        float _health = _packet.ReadFloat();

        GameManager_Network.players[_id].SetHealth(_health);*/
    }
    public static void PlayerRespawn(Packet _packet)
    {
        /*int _id = _packet.ReadInt();

        GameManager_Network.players[_id].Respawn();*/
    }
    public static void CreateItemSpawner(Packet _packet)
    {
        /*int _spawnerId = _packet.ReadInt();
        Vector3 _spawnPosition = _packet.ReadVector3();
        bool _hasItem = _packet.ReadBool();

        GameManager_Network.Instance.CreateItemSpawner(_spawnerId, _spawnPosition, _hasItem);*/
    }
    public static void ItemSpawned(Packet _packet)
    {
        /*int _spawnerId = _packet.ReadInt();

        GameManager_Network.itemSpawners[_spawnerId].ItemSpawned();*/
    }
    public static void ItemPickedUp(Packet _packet)
    {
        /*int _spawnerId = _packet.ReadInt();
        int _byPlayer = _packet.ReadInt();

        GameManager_Network.itemSpawners[_spawnerId].ItemPickedUp();
        GameManager_Network.players[_byPlayer].itemCount++;*/
    }
    public static void SpawnProjectile(Packet _packet)
    {
        /*int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        int _throwByPlayer = _packet.ReadInt();

        GameManager_Network.Instance.SpawnProjectile(_projectileId, _position);
        GameManager_Network.players[_throwByPlayer].itemCount--;*/
    }
    public static void ProjectilePosition(Packet _packet)
    {
        /*int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager_Network.projectiles[_projectileId].transform.position = _position;*/
    }
    public static void ProjectileExploded(Packet _packet)
    {
        /*int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager_Network.projectiles[_projectileId].Explode(_position);*/
    }
    public static void SpawnEnemy(Packet _packet)
    {
        /*int _enemyId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager_Network.Instance.SpawnEnemy(_enemyId, _position);*/
    }

    public static void EnemyPosition(Packet _packet)
    {
        /*int _enemyId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        if (GameManager_Network.enemies.TryGetValue(_enemyId, out EnemyManager _enemy))
        {
            _enemy.transform.position = _position;
        }*/
    }

    public static void EnemyHealth(Packet _packet)
    {
        /*int _enemyId = _packet.ReadInt();
        float _health = _packet.ReadFloat();

        GameManager_Network.enemies[_enemyId].SetHealth(_health);*/
    }
    public static void EnemyThrowItem(Packet _packet)
    {
        /*int _enemyId = _packet.ReadInt();
        Vector3 _shootPosition = _packet.ReadVector3();
        GameManager_Network.Instance.SpawnProjectile_Enemy(_enemyId, _shootPosition);*/
    }
    public void SendedText(Packet packet)
    {
        ChatManager_Network.Instance.SendedText(packet.ReadInt(), packet.ReadString(), packet.ReadBool());
    }
}
