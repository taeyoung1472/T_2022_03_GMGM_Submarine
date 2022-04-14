using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Network : MonoBehaviour
{
    [SerializeField] private float sensitivity, camLimit;
    [SerializeField] public Transform cam;
    float rotVertical;
    float rotHorizontal;
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
        rotHorizontal += Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        rotVertical += Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;
        rotVertical = Mathf.Clamp(rotVertical, -camLimit, camLimit);
        cam.localRotation = Quaternion.Euler(-rotVertical, 0, 0);
        transform.rotation = Quaternion.Euler(0, rotHorizontal, 0);
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
