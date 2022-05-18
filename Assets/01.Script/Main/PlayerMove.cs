using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private CharacterController cc;
    [SerializeField] private float sensitivity;
    [SerializeField] private Transform cam;
    [SerializeField] private float camLimit;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask rayLayerMask;
    [SerializeField] private Text rayInnfo_Name, rayOutnfo_Desc;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private Vector2 recoilValue = Vector2.zero;
    [SerializeField] private int id;
    [SerializeField] private PlayerStateData stateData;
    [SerializeField] private PlayerStat stat;
    public int ID { get { return id; } }
    public float Speed { get { return stateData.PSpd * stat.MoveSpeedFixValue; } }
    UseAbleObject useAbleObject;
    Vector3 moveDir;
    RaycastHit hit;
    bool isCanMove = true;
    public bool IsCanMove { get { return isCanMove; } set { isCanMove = value; } }
    void Update()
    {
        if (PlayerStat.IsCanControll)
        {
            PlayerRotate();
            Move();
            Ray();
        }
    }
    public void PlayerRotate()
    {
        float x = transform.eulerAngles.y + Input.GetAxisRaw("Mouse X") * sensitivity + recoilValue.x;
        float y = cam.localEulerAngles.x - Input.GetAxisRaw("Mouse Y") * sensitivity + recoilValue.y;
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
        //moveDir.y -= 9.8f * Time.deltaTime;
        cc.Move(moveDir * Time.deltaTime * stateData.PSpd * stat.MoveSpeedFixValue + (Vector3.down * 9.8f * Time.deltaTime));
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
    public void Recoil(Vector2 value, float time)
    {
        StartCoroutine(Recol(time, value));
    }
    IEnumerator Recol(float time, Vector2 value)
    {
        Camera camera = cam.GetComponent<Camera>();
        float defaultFOV = camera.fieldOfView;
        camera.fieldOfView = defaultFOV * 1.035f;
        float fixValue = 1f / time;
        float curTime = 0;
        while(1 >= curTime)
        {
            curTime += Time.deltaTime * fixValue;
            recoilValue.y = curve.Evaluate(curTime) * value.y;
            recoilValue.x = curve.Evaluate(curTime) * value.x;
            yield return null;
        }
        recoilValue = Vector2.zero;
        camera.fieldOfView = defaultFOV;
    }
}
