using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightFootIK : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform target;
    private void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0.5f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0.5f);

        animator.SetIKPosition(AvatarIKGoal.RightFoot, target.position);
        animator.SetIKRotation(AvatarIKGoal.RightFoot, target.rotation);

        animator.SetLookAtWeight(1.0f);
        animator.SetLookAtPosition(target.position);
    }
}
