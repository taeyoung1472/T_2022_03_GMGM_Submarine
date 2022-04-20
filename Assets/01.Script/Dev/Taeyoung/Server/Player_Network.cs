using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class Player_Network : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform model;
    [SerializeField] private TextMesh nameTextMesh;
    public Vector3 moveDir;
    UseAbleObject useAbleObject;
    RaycastHit hit;
    bool isCanMove = true;
    int id;
    string username;
    public int Id { get { return id; } }
    public bool IsCanMove { get { return isCanMove; } set { isCanMove = value; } }
    public string Username { get { return username; } set { username = value; } }
    public void Inintialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        nameTextMesh.text = username;
    }
    public void SetPositionAndRotation(Vector3 pos, Quaternion rot, Vector2 dir)
    {
        dir.Normalize();
        moveDir = dir;
        animator.SetFloat("Horizontal", dir.x);
        animator.SetFloat("Vertical", dir.y);
        transform.SetPositionAndRotation(pos, rot);
        model.localPosition = Vector3.zero;
    }
}
