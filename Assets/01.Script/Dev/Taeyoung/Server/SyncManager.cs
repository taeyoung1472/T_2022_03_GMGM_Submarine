using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncManager : MonoBehaviour
{
    public static SyncManager instance;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void Sync(Packet data)
    {
        int netTransCount = data.ReadInt();
        using (Packet packet = new Packet((int)ClientPackets.sync))
        {
            if (NetworkTransformManager.instance.NetTransforms.Count < netTransCount)
            {
                packet.Write(false);
            }
            else
            {
                packet.Write(true);
            }
            ClientSend.SendTCPData(packet);
        }
    }
}
