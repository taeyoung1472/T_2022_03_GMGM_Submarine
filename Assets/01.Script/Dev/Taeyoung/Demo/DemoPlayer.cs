using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPlayer : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float sensevity;
    Animator animator;
    Rigidbody rb;
    Vector2 input;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = transform.TransformDirection(new Vector3(h, 0, v).normalized);
        rb.velocity = moveDir * speed + Vector3.up * rb.velocity.y;
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensevity);
        if(moveDir.magnitude == 0)
        {
            animator.SetBool("IsMove", false);
        }
        else
        {
            animator.SetBool("IsMove", true);
        }
        input = Vector2.Lerp(input, new Vector2(h, v), Time.deltaTime * speed);
        animator.SetFloat("X", input.x);
        animator.SetFloat("Y", input.y);
    }
}
