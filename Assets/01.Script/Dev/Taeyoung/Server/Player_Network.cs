using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class Player_Network : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private LayerMask rayLayerMask;
    [SerializeField] private Text rayInnfo_Name, rayOutnfo_Desc;
    UseAbleObject useAbleObject;
    RaycastHit hit;
    bool isCanMove = true;
    int id;
    string username;
    public bool IsCanMove { get { return isCanMove; } set { isCanMove = value; } }
    public void Inintialize(int _id, string _username)
    {
        id = _id;
        username = _username;
    }
    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }
}
