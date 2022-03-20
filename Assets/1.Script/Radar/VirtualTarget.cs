using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualTarget : MonoBehaviour
{
    Animator animator;
    bool isCanSearch = true;
    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Find(Vector3 pos)
    {
        if (isCanSearch)
        {
            print("FIND");
            transform.localPosition = pos;
            animator.Play("Search");
            StartCoroutine(WaitForSearch());
        }
    }
    IEnumerator WaitForSearch()
    {
        isCanSearch = false;
        yield return new WaitForSeconds(1);
        isCanSearch = true;
    }
}