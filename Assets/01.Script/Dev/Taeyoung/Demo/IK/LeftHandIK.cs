using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandIK : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform target;
    private void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.5f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.5f);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, target.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, target.rotation);

        animator.SetLookAtWeight(1.0f);
        animator.SetLookAtPosition(target.position);
    }
}
