using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_Improve : MonoBehaviour
{
    [SerializeField] private float sensitivity;
    [SerializeField] private Transform cam;
    [SerializeField] private float camLimit;
    [SerializeField] private float jumpForce;
    [SerializeField] private PlayerStateData playerStateData;
    [SerializeField] private PlayerStat playerStat;

    private CharacterController cc;

    float speed;
    public float nowSpeed = 1;
    public float handlingSpeed = 1;

    Vector3 moveDir;

    bool isCanMove = true;
    public bool isCanRun = true;
    public bool IsCanMove { get { return isCanMove; } set { isCanMove = value; } }

    public void Awake()
    {
        cc = GetComponent<CharacterController>();
    }
    void Update()
    {
        PlayerRotate();
        Move();
    }
    public void PlayerRotate()
    {
        float x = transform.eulerAngles.y + Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        float y = cam.localEulerAngles.x - Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;
        if (y < camLimit || y > 360 - camLimit)
        {
            cam.localEulerAngles = new Vector3(y, 0, 0);
        }
        transform.eulerAngles = new Vector3(0, x, 0);
    }
    public void DisableMove(bool isDisable)
    {
        IsCanMove = isDisable;
        cc.enabled = isDisable;
    }
    public void Move()
    {
        if (cc.isGrounded)
        {
            if (playerStat.IsCanMove)
            {
                moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                moveDir.Normalize();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    moveDir.y = jumpForce;
                }
                moveDir = this.transform.TransformDirection(moveDir);
            }
        }
        else
        {
            moveDir.y -= 9.8f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (isCanRun)
            {
                speed = playerStateData.PRSp * nowSpeed;//GetComponent<PlayerHealth>().playerRunningSpeedNow;
            }
            else
            {
                speed = playerStateData.PSpd * nowSpeed;//GetComponent<PlayerHealth>().playerSpeedNow;
            }
        }
        else
        {
            speed = playerStateData.PSpd * nowSpeed;//GetComponent<PlayerHealth>().playerSpeedNow;
        }
        cc.Move(moveDir * Time.deltaTime * speed * playerStat.MoveSpeedFixValue);
    }

}
