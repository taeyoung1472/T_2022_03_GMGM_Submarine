using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private CharacterController cc;
    [SerializeField] private float speed, sensitivity;
    [SerializeField] private Transform cam;
    [SerializeField] private float camLimit;
    [SerializeField] private LayerMask rayLayerMask;
    [SerializeField] private Text rayInnfo_Name, rayOutnfo_Desc;
    UseAbleObject useAbleObject;
    Vector3 moveDir;
    RaycastHit hit;
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
    public void Move()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveDir.Normalize();
        moveDir = this.transform.TransformDirection(moveDir);
        cc.Move(moveDir * Time.deltaTime * speed);
    }
    public void Ray()
    {
        Physics.Raycast(cam.position, cam.forward, out hit, 10);
        if (hit.transform.GetComponent<UseAbleObject>())
        {
            useAbleObject = hit.transform.GetComponent<UseAbleObject>();
            rayInnfo_Name.text  = useAbleObject.Name;
            rayOutnfo_Desc.text = useAbleObject.Description;
            if (Input.GetKeyDown(KeyCode.E))
            {
                useAbleObject.Toggle();
            }
        }
        else
        {
            rayInnfo_Name.text  = "";
            rayOutnfo_Desc.text = "";
        }
    }
}
