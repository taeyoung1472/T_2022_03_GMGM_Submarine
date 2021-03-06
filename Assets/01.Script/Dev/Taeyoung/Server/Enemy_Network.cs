using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Network : MonoBehaviour
{
    Animator animator;
    bool isDead;
    int id;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Initialize(int _id)
    {
        id = _id;
    }
    void Attack()
    {
        animator.SetTrigger("Attack");
    }
    public void Dead()
    {
        isDead = true;
        animator.SetTrigger("Dead");
        Destroy(gameObject, 2f);
    }
    public void Damaged(float damage) 
    {
        if (isDead) return;
        //ClientSend.EnemyHit(id, damage);
        animator.SetTrigger("Hit");
    }
    enum State
    {
        Search,
        Chase,
    }
}
