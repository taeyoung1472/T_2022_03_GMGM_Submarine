using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFootIK : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform target;
    private void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0.5f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0.5f);

        animator.SetIKPosition(AvatarIKGoal.LeftFoot, target.position);
        animator.SetIKRotation(AvatarIKGoal.LeftFoot, target.rotation);

        animator.SetLookAtWeight(1.0f);
        animator.SetLookAtPosition(target.position);
    }
}
