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
    bool hasUseAbleObjectNetwork;
    bool hasUseAbleObjectClient;
    UseAbleObject_Network useAbleObjectNetwork;
    UseAbleObject_Client useAbleObjectClient;
    private void Awake()
    {
        rayInnfo_Name = UIManager_Network.Instance.RayInfoName;
        rayInfoDesc = UIManager_Network.Instance.RayInfoDesc;
        player_Network = GetComponent<Player_Network>();
    }
    public void Set()
    {
        foreach (Player_Network player in GameManager_Network.players.Values)
        {
            player.NameText.GetComponent<NickName>().Set(cam);
        }
    }
    private void Update()
    {
        PlayerRotate();
        if (hasUseAbleObjectNetwork)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                useAbleObjectNetwork.Click(player_Network);
            }
        }else if (hasUseAbleObjectClient)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                useAbleObjectClient.Click();
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
                useAbleObjectNetwork = hit.transform.GetComponent<UseAbleObject_Network>();
                rayInnfo_Name.text = useAbleObjectNetwork.Name;
                rayInfoDesc.text = useAbleObjectNetwork.Description;
                hasUseAbleObjectNetwork = true;
            }
            catch
            {
                try
                {
                    useAbleObjectClient = hit.transform.GetComponent<UseAbleObject_Client>();
                    rayInnfo_Name.text = useAbleObjectClient.Name;
                    rayInfoDesc.text = useAbleObjectClient.Description;
                    hasUseAbleObjectClient = true;
                }
                catch
                {
                    Debug.LogError($"Errored : {hit.transform.name} �� UseAbleObject�� �����ϴ�!");
                }
            }
        }
        else
        {
            rayInnfo_Name.text = "";
            rayInfoDesc.text = "";
            useAbleObjectNetwork = null;
            hasUseAbleObjectNetwork = false;
            hasUseAbleObjectClient = false;
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
