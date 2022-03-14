using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private CharacterController cc;
    [SerializeField] private float speed;
    [SerializeField] private Transform cam;
    [SerializeField] private float camLimit;
    Vector3 moveDir;
    void Update()
    {
        PlayerRotate();
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveDir.Normalize();
        moveDir = this.transform.TransformDirection(moveDir);
        cc.Move(moveDir * Time.deltaTime * speed);
    }
    public void PlayerRotate()
    {
        float x = transform.eulerAngles.y + Input.GetAxisRaw("Mouse X");
        float y = cam.localEulerAngles.x - Input.GetAxisRaw("Mouse Y");
        if(y < camLimit || y > 360 - camLimit)
        {
            cam.localEulerAngles = new Vector3(y, 0, 0);
        }
        transform.eulerAngles = new Vector3(0, x, 0);
    }
}
