using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Network : MonoBehaviour
{
    int controllingId;
    bool isControlling;
    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }
    public void StartControll()
    {
        controllingId = Client.Instance.myId;
        isControlling = true;
        ClientSend.HandlePlayerMove(false, controllingId);
    }
    public void StopControll()
    {
        isControlling = false;
        ClientSend.HandlePlayerMove(true, controllingId);
    }
    public void Update()
    {
        if (isControlling)
        {
            Controll();
            //if (Input.GetKeyDown(KeyCode.E))
            //{
            //    StopControll();
            //}
        }
    }
    public void Controll()
    {
        bool[] _inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space),
            Input.GetKey(KeyCode.LeftControl),
        };
        ClientSend.SubmarineMovement(_inputs);
    }
}