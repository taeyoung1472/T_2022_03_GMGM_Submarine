using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class PlayerMove_GiJoo : MonoBehaviour
{
    [SerializeField] private GetPlayerScript playerStatus;

    [SerializeField] private CharacterController cc;
    [SerializeField] private float sensitivity;
    [SerializeField] private Transform cam;
    [SerializeField] private float camLimit;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask rayLayerMask;
    [SerializeField] private Text rayInnfo_Name, rayOutnfo_Desc;
    UseAbleObject useAbleObject;
    Vector3 moveDir;
    RaycastHit hit;
    bool isCanMove = true;
    public bool IsCanMove { get { return isCanMove; } set { isCanMove = value; } }
    void Update()
    {
        PlayerRotate();
        Move();
        Ray();
    }
    public void PlayerRotate()
    {
        float x = transform.eulerAngles.y + Input.GetAxisRaw("Mouse X") * sensitivity;
        float y = cam.localEulerAngles.x - Input.GetAxisRaw("Mouse Y") * sensitivity;
        if(y < camLimit || y > 360 - camLimit)
        {
            cam.localEulerAngles = new Vector3(y, 0, 0);
        }
        transform.eulerAngles = new Vector3(0, x, 0);
    }
    public void DisableMove(bool isDiable)
    {
        IsCanMove = isDiable;
        cc.enabled = isDiable;
    }
    public void Move()
    {
        if (cc.isGrounded)
        {
            if (isCanMove)
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

        }
        moveDir.y -= 9.8f * Time.deltaTime;
        cc.Move(moveDir * Time.deltaTime * playerStatus.PSpd);
    }
    public void Ray()
    {
        Physics.Raycast(cam.position, cam.forward, out hit, 10, rayLayerMask);
        if (hit.transform)
        {
            try
            {
                useAbleObject = hit.transform.GetComponent<UseAbleObject>();
                rayInnfo_Name.text = useAbleObject.Name;
                rayOutnfo_Desc.text = useAbleObject.Description;
            }
            catch
            {
                rayInnfo_Name.text = "";
                rayOutnfo_Desc.text = "";
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                useAbleObject.Click();
            }
        }
        else
        {
            rayInnfo_Name.text  = "";
            rayOutnfo_Desc.text = "";
        }
    }
}
