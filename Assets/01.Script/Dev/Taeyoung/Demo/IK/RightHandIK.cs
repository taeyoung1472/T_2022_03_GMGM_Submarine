using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandIK : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform target;
    private void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.5f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.5f);

        animator.SetIKPosition(AvatarIKGoal.RightHand, target.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, target.rotation);

        animator.SetLookAtWeight(1.0f);
        animator.SetLookAtPosition(target.position);
    }
}
