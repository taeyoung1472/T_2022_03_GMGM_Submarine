using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Animator animator;
    bool isDead;
    float speed;
    [SerializeField] private Transform[] players;
    [SerializeField] private float searchRange;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask searchLayerMask;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform eye;
    [SerializeField] private float hp;//Enemy Data 만들기
    void Start()
    {
        animator = GetComponent<Animator>();
        Search();
        StartCoroutine(AIBrain());
        speed = navMeshAgent.speed;
    }
    IEnumerator AIBrain()
    {
        while (!isDead)
        {
            Chase(CheckPlayer());
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator Stun(float time)
    {
        navMeshAgent.speed = 0;
        yield return new WaitForSeconds(time);
        navMeshAgent.speed = speed;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == playerLayerMask)
        {
            Attack(collision.transform.GetComponent<PlayerMove>());
        }
    }
    void Search()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 10000, playerLayerMask);
        players = new Transform[cols.Length];
        for (int i = 0; i < cols.Length; i++)
        {
            players[i] = cols[i].transform;
        }
    }
    Transform CheckPlayer()
    {
        List<Transform> trans = new List<Transform>();
        for (int i = 0; i < players.Length; i++)
        {
            RaycastHit hit;
            Vector3 dir = (players[i].position - eye.position).normalized;
            Debug.DrawRay(eye.position, dir, Color.blue, 1f);
            if(Physics.Raycast(eye.position, dir, out hit, searchRange, searchLayerMask))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    trans.Add(hit.transform);
                }
            }
        }
        int j = 0;
        float nearDistance = 100000;
        Transform nearPlayer = null;
        foreach (Transform data in trans)
        {
            float distance = Vector3.Distance(transform.position, data.position);
            if(distance < nearDistance)
            {
                nearDistance = distance;
                nearPlayer = data;
            }
            j++;
        }
        return nearPlayer;
    }
    void Chase(Transform target)
    {
        if (target == null)
        {
            navMeshAgent.SetDestination(transform.position);
            animator.SetBool("IsMove", false);
            return;
        }
        navMeshAgent.SetDestination(target.position);
        animator.SetBool("IsMove", true);
    }
    void Attack(PlayerMove move)
    {
        print("공격!");
    }
    public void Damaged(float damage) 
    {
        if (isDead) return;
        hp -= damage;
        if(hp <= 0)
        {
            isDead = true;
            animator.SetTrigger("Dead");
            navMeshAgent.enabled = false;
            return;
        }
        animator.SetTrigger("Hit");
        StartCoroutine(Stun(0.5f));
    }
    enum State
    {
        Search,
        Chase,
    }
}
