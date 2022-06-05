using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ClientSend : MonoBehaviour
{
    public static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.Instance.tcp.SendData(_packet);
    }
    public static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.Instance.udp.SendData(_packet);
    }
    #region 패킷들
    public static void WelcomeReceved()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            print("Welcome 받음");
            _packet.Write(Client.Instance.myId);
            _packet.Write(UIManager_Network.Instance.NameText);

            SendTCPData(_packet);
        }
    }
    public static void PlayerMovement(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }
            Quaternion _rot = GameManager_Network.players[Client.Instance.myId].transform.rotation;
            _packet.Write(_rot);
            SendUDPData(_packet);
        }
    }
    public static void SubmarineMovement(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.submarineMovement))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }
            SendUDPData(_packet);
        }
    }
    public static void HandlePlayerMove(bool isMove, int id)
    {
        using (Packet _packet = new Packet((int)ClientPackets.handlePlayerMove))
        {
            _packet.Write(isMove);
            _packet.Write(id);
            SendUDPData(_packet);
        }
    }
    public static void PlayerShoot(Vector3 _facing)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerShoot))
        {
            _packet.Write(_facing);

            SendTCPData(_packet);
        }
    }
    public static void PlayerThrowItem(Vector3 _facing)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerThrowItem))
        {
            _packet.Write(_facing);

            SendTCPData(_packet);
        }
    }
    public static void SendText(string text)
    {
        using (Packet _packet = new Packet((int)ClientPackets.textSend))
        {
            _packet.Write(text);

            SendTCPData(_packet);
        }
    }
    public static void RequestSpawnAgain(int id)
    {
        using (Packet _packet = new Packet((int)ClientPackets.requestSpawnAgain))
        {
            _packet.Write(id);

            SendTCPData(_packet);
        }
    }
    public static void SendAudio(Vector3 pos, AudioEnum audioId)
    {
        using (Packet packet = new Packet((int)ClientPackets.audioSend))
        {
            packet.Write((int)audioId);
            packet.Write(pos);
            AudioPacket.Instance.PlayAudio((int)audioId, pos);

            SendTCPData(packet);
        }
    }
    public static void RequestInstantObject(ObjectEnum index, Vector3 pos, Quaternion rot)
    {
        using (Packet packet = new Packet((int)ClientPackets.requestSpawnObject))
        {
            packet.Write((int)index);
            packet.Write(pos);
            packet.Write(rot);

            SendTCPData(packet);
        }
    }
    public static void Controll(int id, bool isPostive)
    {
        using (Packet packet = new Packet((int)ClientPackets.controll))
        {
            packet.Write(id);
            packet.Write(isPostive);

            SendTCPData(packet);
        }
    }
    public static void EnemyHit(int id, float damage)
    {
        using (Packet packet = new Packet((int)ClientPackets.enemyHit))
        {
            packet.Write(id);
            packet.Write(damage);

            SendTCPData(packet);
        }
    }
    public static void Lobby_Ready(string _name, bool _value)
    {
        using (Packet packet = new Packet((int)ClientPackets.lobby_Ready))
        {
            packet.Write(_name);
            packet.Write(_value);

            SendTCPData(packet);
        }
    }
    /*public static void ReturnInitCheck(int id, string name)
    {
        using (Packet packet = new Packet((int)ClientPackets.returnInitNetworkTransform))
        {
            packet.Write(id);
            packet.Write(bool);

            SendTCPData(packet);
        }
    }*/
    #endregion
}
