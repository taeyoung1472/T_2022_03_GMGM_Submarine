using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine_Network : MonoBehaviour
{
    int controllingId;
    bool isControlling;
    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }
    public void StartControll(int id)
    {
        controllingId = id;
        isControlling = true;
        ClientSend.HandlePlayerMove(false, id);
    }
    public void StopControll(int id)
    {
        isControlling = false;
        ClientSend.HandlePlayerMove(true, id);
    }
    public void Update()
    {
        if (isControlling)
        {
            Controll();
            if (Input.GetKeyDown(KeyCode.E))
            {
                StopControll(controllingId);
            }
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