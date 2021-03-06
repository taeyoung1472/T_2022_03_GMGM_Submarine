using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class Player_Network : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform model;
    [SerializeField] private TextMesh nameTextMesh;
    Vector2 moveDir;
    UseAbleObject useAbleObject;
    RaycastHit hit;
    bool isCanMove = true;
    int id;
    string username;
    public int Id { get { return id; } }
    public bool IsCanMove { get { return isCanMove; } set { isCanMove = value; } }
    public string Username { get { return username; } set { username = value; } }
    public GameObject NameText { get { return nameTextMesh.gameObject; } }
    public void Inintialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        nameTextMesh.text = username;
    }
    public void SetPositionAndRotation(Vector3 pos, Quaternion rot, Vector2 dir)
    {
        dir.Normalize();
        moveDir = Vector2.Lerp(moveDir, dir, Time.deltaTime * 5f);
        Vector2 absMoveDir = new Vector2(Mathf.Abs(moveDir.x), Mathf.Abs(moveDir.y));
        Vector2 calculDir;
        calculDir.x = absMoveDir.x < 0.2f ? 0 : moveDir.x;
        calculDir.y = absMoveDir.y < 0.2f ? 0 : moveDir.y;
        animator.SetFloat("X", calculDir.x);
        animator.SetFloat("Y", calculDir.y);
        transform.SetPositionAndRotation(pos, rot);
        model.localPosition = Vector3.zero;
    }
}
