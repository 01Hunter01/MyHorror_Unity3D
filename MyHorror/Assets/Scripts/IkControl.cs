using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkControl : MonoBehaviour
{
    protected Animator animator;


    public bool ikActive = false;
    public Transform rightHandObj = null;
    public Transform leftHandObj = null;
    public Transform headObj = null;
    public Transform lookObj = null;

    // ����� ��� Raycast, ����� ����������, �� ��� �� ����� ������� ����
    public LayerMask rayLayer;

    // ��� �� ����������� IK ��� �� ����� Curves
    public float WeightFoot_L, WeightFoot_R;

    // ��������� Raycast hit ������� ���
    private Vector3 footPosL, footPosR;

    // ������� ���
    public Transform footR, footL;

    // ���������� (�������) ������ � ������������
    public Vector3 footLoffset, footRoffset;

    
    public float radiusView = 2.0f;
    
    public bool _oldTest = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    //���������� ��� ������� IK
    void OnAnimatorIK()
    {
        if (_oldTest)
        {
            if (animator)
            {
                //����, �� �������� IK, ������������� ������� � ��������
                if (ikActive)
                {
                    // ������������� ���� ������� ��� ������
                    if (lookObj != null)
                    {
                        RaycastHit _hitHead;

                        if (Physics.Raycast(headObj.position, lookObj.position, out _hitHead, rayLayer))
                        {
                            if(_hitHead.distance < radiusView)
                            {
                                Debug.DrawRay(headObj.position, _hitHead.point, Color.red);
                                animator.SetLookAtWeight(1);
                                animator.SetLookAtPosition(lookObj.position);
                            }
                            
                        }
                        
                    }
                    // ������������� ���� ��� ������ ���� � ���������� � � �������
                    if (rightHandObj != null)
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                    }
                    if (leftHandObj != null)
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
                    }
                }
                // ���� IK ���������, ������ ������� � �������� ��� � ������ � ����������� ���������
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
        else
        {
            WeightFoot_R = animator.GetFloat("Right_Leg");
            WeightFoot_L = animator.GetFloat("Left_Leg");

            IegsIK();
        }

    }


    void IegsIK()
    {
        // ������������� ��� ��� ������������ IK
        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, WeightFoot_L);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, WeightFoot_L);
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, WeightFoot_R);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, WeightFoot_R);

        RaycastHit hit;

        // �������� ������� ��������� ����� ����
        footPosL = animator.GetIKPosition(AvatarIKGoal.LeftFoot);

        //[RayCast] �� ���� �� �����
        if (Physics.Raycast(footPosL + Vector3.up, Vector3.down, out hit, 2.0f, rayLayer))
        {
            // ������ ��������������� ����� � ���������
            Debug.DrawLine(hit.point, hit.point + hit.normal, Color.yellow);

            // ��������� ����� ������� IK
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + footLoffset);

            // ��������� ������ �������� IK
            animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(Vector3.ProjectOnPlane(footL.forward, hit.normal), hit.normal));

            // ��������� ����� ������������ ��������
            footPosL = hit.point;
        }

        footPosR = animator.GetIKPosition(AvatarIKGoal.RightFoot);

        if (Physics.Raycast(footPosR + Vector3.up, Vector3.down, out hit, 2.0f, rayLayer))
        {
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + footRoffset);
            animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(Vector3.ProjectOnPlane(footR.forward, hit.normal), hit.normal));

            footPosR = hit.point;
        }
    }
}
