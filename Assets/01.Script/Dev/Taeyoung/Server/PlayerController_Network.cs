using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContorller_Network : MonoBehaviour
{
    [SerializeField] private float sensitivity, camLimit;
    [SerializeField] public Transform cam;
    private void Update()
    {
        PlayerRotate();
    }
    private void FixedUpdate()
    {
        SendInputToServer();
    }
    public void PlayerRotate()
    {
        float x = transform.eulerAngles.y + Input.GetAxisRaw("Mouse X") * sensitivity;
        float y = cam.localEulerAngles.x - Input.GetAxisRaw("Mouse Y") * sensitivity;
        if (y < camLimit || y > 360 - camLimit)
        {
            cam.localEulerAngles = new Vector3(y, 0, 0);
        }
        transform.eulerAngles = new Vector3(0, x, 0);
    }
    private void SendInputToServer()
    {
        bool[] _inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space),
        };
        ClientSend.PlayerMovement(_inputs);
    }
}
