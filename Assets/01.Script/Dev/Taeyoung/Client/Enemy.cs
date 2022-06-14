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
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask searchLayerMask;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform eye;
    [SerializeField] private float hp;//Enemy Data ¸¸µé±â
    [SerializeField] private float attackDelay = 1f;
    [SerializeField] private AudioClip[] attackClips;
    [SerializeField] private AudioClip[] hitClips;
    float attackChecker;
    void Start()
    {
        animator = GetComponent<Animator>();
        Search();
        StartCoroutine(AIBrain());
        speed = navMeshAgent.speed; 
        navMeshAgent.stoppingDistance = attackRange * 0.5f;
        attackChecker = Time.time;
    }
    IEnumerator AIBrain()
    {
        while (!isDead)
        {
            Transform nearPlayer = CheckPlayer();
            if(nearPlayer != null)
            {
                float dist = Vector3.Distance(transform.position, nearPlayer.position);
                if (dist < attackRange)
                {
                    if (Time.time > attackChecker)
                    {
                        Attack(nearPlayer.GetComponent<HpManager>());
                    }
                }
                else if (dist < searchRange)
                {
                    Chase(nearPlayer);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator Stun(float time)
    {
        navMeshAgent.speed = 0;
        yield return new WaitForSeconds(time);
        navMeshAgent.speed = speed;
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
    void Attack(HpManager health)
    {
        health.Damaged(25, Define.RandomEnum<PlayerPartType>());
        //AudioPoolManager.instance.Play(attackClips, transform.position);
        animator.Play("Attack");
        
        attackChecker = Time.time + attackDelay;
    }
    public void Damaged(float damage) 
    {
        if (isDead) return;
        hp -= damage;
        AudioPoolManager.instance.Play(hitClips, transform.position, 1f, 1.5f);
        if(hp <= 0)
        {
            isDead = true;
            animator.SetTrigger("Dead");
            navMeshAgent.enabled = false;
            return;
        }
        animator.Play("Hit");
        StartCoroutine(Stun(0.5f));
    }
    enum State
    {
        Search,
        Chase,
    }
}
