using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class PlayerController_Network : MonoBehaviour
{
    [SerializeField] private float sensitivity, camLimit;
    [SerializeField] public Transform cam;
    [SerializeField] private Text rayInnfo_Name, rayInfoDesc;
    [SerializeField] private LayerMask rayLayerMask;
    private Player_Network player_Network;
    float rotVertical;
    float rotHorizontal;
    bool hasUseAbleObject;
    UseAbleObject_Network useAbleObject;

    private void Awake()
    {
        rayInnfo_Name = UIManager_Network.Instance.RayInfoName;
        rayInfoDesc = UIManager_Network.Instance.RayInfoDesc;
        player_Network = GetComponent<Player_Network>();
    }
    private void Update()
    {
        PlayerRotate();
        if (hasUseAbleObject)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                useAbleObject.Click(player_Network);
            }
        }
    }
    private void FixedUpdate()
    {
        SendInputToServer();
        Ray();
    }
    private void Ray()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, 10, rayLayerMask))
        {
            try
            {
                useAbleObject = hit.transform.GetComponent<UseAbleObject_Network>();
                rayInnfo_Name.text = useAbleObject.Name;
                rayInfoDesc.text = useAbleObject.Description;
                hasUseAbleObject = true;
            }
            catch
            {
                Debug.LogError($"Errored : {hit.transform.name} 은 UseAbleObject가 없습니다!");
            }
        }
        else
        {
            rayInnfo_Name.text = "";
            rayInfoDesc.text = "";
            useAbleObject = null;
            hasUseAbleObject = false;
        }
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
