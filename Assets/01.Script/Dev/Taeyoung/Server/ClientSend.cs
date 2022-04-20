using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.Instance.tcp.SendData(_packet);
    }
    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.Instance.udp.SendData(_packet);
    }
    #region ÆÐÅ¶µé
    public static void WelcomeReceved()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
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
    #endregion
}
