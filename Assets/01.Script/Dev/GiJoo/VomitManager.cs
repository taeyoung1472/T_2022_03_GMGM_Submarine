using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VomitManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("abc");
            other.gameObject.GetComponent<PlayerHealth>().Virus();
        }
    }
}
