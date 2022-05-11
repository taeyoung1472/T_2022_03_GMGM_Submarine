using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
public class Client : MonoBehaviour
{
    public static Client Instance;
    public static int dataBufferSize = 4096;

    public string ip = "127.0.0.1";
    public int port = 26950;
    public int myId = 0;
    public TCP tcp;
    public UDP udp;

    private bool isConnected = false;
    private delegate void PacketHandler(Packet _packet);
    private static Dictionary<int, PacketHandler> packetHandlers;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.Log("인스턴스는 이미 있습니다, 오브젝트를 삭제합니다!");
            Destroy(this);
        }
    }
    private void Start()
    {
        tcp = new TCP();
        udp = new UDP();
    }
    private void OnApplicationQuit()
    {
        Disconnect();
    }
    public void ConnectToServer()
    {
        InitializeClientData();
        isConnected = true;
        tcp.Connect();
    }
    public class TCP
    {
        public TcpClient socket;

        private NetworkStream stream;
        private Packet receivedData;
        private byte[] receiveBuffer;
        public void Connect()
        {
            socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize,
            };
            receiveBuffer = new byte[dataBufferSize];
            socket.BeginConnect(Instance.ip, Instance.port, ConnectCallback, socket);
        }
        private void ConnectCallback(IAsyncResult _result)
        {
            socket.EndConnect(_result);
            if (!socket.Connected)
            {
                return;
            }

            stream = socket.GetStream();

            receivedData = new Packet();

            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }
        public void SendData(Packet _packet)
        {
            try
            {
                if (socket != null)
                {
                    stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                }
            }
            catch (Exception ex)
            {
                Debug.Log($"VIA TCP 데이터를 서버를 보내는중 오류 발생 : {ex}");
            }
        }
        private void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                int _byteLength = stream.EndRead(_result);
                if (_byteLength <= 0)
                {
                    Instance.Disconnect();
                    return;
                }
                byte[] _data = new byte[_byteLength];
                Array.Copy(receiveBuffer, _data, _byteLength);

                receivedData.Reset(HandledData(_data));
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }
            catch
            {
                Disconnect();
            }
        }
        private bool HandledData(byte[] _data)
        {
            int _packetLength = 0;

            receivedData.SetBytes(_data);

            if (receivedData.UnreadLength() >= 4)
            {
                _packetLength = receivedData.ReadInt();
                if (_packetLength <= 0)
                {
                    return true;
                }
            }
            while (_packetLength > 0 && _packetLength <= receivedData.UnreadLength())
            {
                byte[] _packetBytes = receivedData.ReadBytes(_packetLength);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet _packet = new Packet(_packetBytes))
                    {
                        int _packetId = _packet.ReadInt();
                        packetHandlers[_packetId](_packet);
                    }
                });

                _packetLength = 0;
                if (receivedData.UnreadLength() >= 4)
                {
                    _packetLength = receivedData.ReadInt();
                    if (_packetLength <= 0)
                    {
                        return true;
                    }
                }
            }
            if (_packetLength <= 1)
            {
                return true;
            }
            return false;
        }
        public void Disconnect()
        {
            Instance.Disconnect();

            stream = null;
            receivedData = null;
            receiveBuffer = null;
            socket = null;
        }

    }
    public class UDP
    {
        public UdpClient socket;
        public IPEndPoint endPoint;

        public UDP()
        {
            endPoint = new IPEndPoint(IPAddress.Parse(Instance.ip), Instance.port);
        }
        public void Connect(int _localPort)
        {
            socket = new UdpClient(_localPort);

            socket.Connect(endPoint);
            socket.BeginReceive(ReceiveCallback, null);

            using (Packet _packet = new Packet())
            {
                SendData(_packet);
            }
        }
        public void SendData(Packet _packet)
        {
            try
            {
                _packet.InsertInt(Instance.myId);
                if (socket != null)
                {
                    socket.BeginSend(_packet.ToArray(), _packet.Length(), null, null);
                }
            }
            catch (Exception _ex)
            {
                Debug.Log($"VIA UDP 데이타를 서버로 보내는중 오류발생 : {_ex}");
            }
        }
        private void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                byte[] _data = socket.EndReceive(_result, ref endPoint);
                socket.BeginReceive(ReceiveCallback, null);

                if (_data.Length < 4)
                {
                    Instance.Disconnect();
                    return;
                }
                HandleData(_data);
            }
            catch
            {
                Disconnect();
            }
        }
        private void HandleData(byte[] _data)
        {
            using (Packet _packet = new Packet(_data))
            {
                int _packetLength = _packet.ReadInt();
                _data = _packet.ReadBytes(_packetLength);
            }

            ThreadManager.ExecuteOnMainThread(() =>
            {
                using (Packet _packet = new Packet(_data))
                {
                    int _packetId = _packet.ReadInt();
                    packetHandlers[_packetId](_packet);
                }
            });
        }
        public void Disconnect()
        {
            Instance.Disconnect();

            endPoint = null;
            socket = null;
        }
    }
    private void InitializeClientData()
    {
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
            { (int)ServerPackets.welcome, ClientManager.Welcome},
            { (int)ServerPackets.spawnPlayer, ClientManager.SpawnPlayer},
            { (int)ServerPackets.playerPositionAndRotation, ClientManager.PlayerPositionAndRotation},
            { (int)ServerPackets.submarinePositionAndRotation, ClientManager.SubmarinePositionAndRotation},
            { (int)ServerPackets.playerDisconnected, ClientManager.PlayerDisconnected},
            { (int)ServerPackets.spawnEnemy, ClientManager.SpawnEnemy },
            { (int)ServerPackets.textSended, ClientManager.TextSended},
            { (int)ServerPackets.audioSended, ClientManager.AudioSended},
            { (int)ServerPackets.objectSended, ClientManager.ObjectSended},
            { (int)ServerPackets.networkPosition, ClientManager.NetworkPosition },
            { (int)ServerPackets.networkRotation, ClientManager.NetworkRotation },
            { (int)ServerPackets.networkTransformInit, ClientManager.NetworkTransformInit },
        };
        Debug.Log("패킷 초기설정 완료");
    }

    private void Disconnect()
    {
        if (isConnected)
        {
            isConnected = false;
            tcp.socket.Close();
            udp.socket.Close();

            Debug.Log("서버와 연결이 종료되었습니다");
        }
    }
}
