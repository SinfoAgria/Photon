using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldWeapon : MonoBehaviour
{
    protected Animator animator;

    public bool ikActive = true;
    public Transform weapon = null;
    public Transform lookObj = null;

    [Range(0f, 1f)] public float weight = 1f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal.
            if (ikActive)
            {

                // Set the look target position, if one has been assigned
                if (lookObj != null)
                {
                    animator.SetLookAtWeight(weight);
                    animator.SetLookAtPosition(lookObj.position);
                }

                // Set the right hand target position and rotation, if one has been assigned
                if (weapon != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, weight);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, weapon.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, weapon.rotation);

                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, weight);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, weight);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, weapon.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, weapon.rotation);
                }

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);

                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }
}
