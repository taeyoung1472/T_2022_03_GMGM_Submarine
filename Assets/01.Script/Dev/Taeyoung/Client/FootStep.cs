using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    [SerializeField] private AudioClip[] footSteps;
    public void PlayFootStep()
    {
        AudioPoolManager.instance.Play(footSteps[Random.Range(0, footSteps.Length)], transform.position);
    }
}