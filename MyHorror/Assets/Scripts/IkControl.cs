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

    // Маска для Raycast, чтобы определять, на что мы можем ставить ноги
    public LayerMask rayLayer;

    // Вес на контроллеры IK ног из наших Curves
    public float WeightFoot_L, WeightFoot_R;

    // Сохраняем Raycast hit позиции ног
    private Vector3 footPosL, footPosR;

    // Позиции ног
    public Transform footR, footL;

    // Отклонение (разница) модели и контроллеров
    public Vector3 footLoffset, footRoffset;

    
    public float radiusView = 2.0f;
    
    public bool _oldTest = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    //Вызывается при расчёте IK
    void OnAnimatorIK()
    {
        if (_oldTest)
        {
            if (animator)
            {
                //Если, мы включили IK, устанавливаем позицию и вращение
                if (ikActive)
                {
                    // Устанавливаем цель взгляда для головы
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
                    // Устанавливаем цель для правой руки и выставляем её в позицию
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
                // Если IK неактивен, ставим позицию и вращение рук и головы в изначальное положение
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
        // Устанавливаем вес для контроллеров IK
        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, WeightFoot_L);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, WeightFoot_L);
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, WeightFoot_R);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, WeightFoot_R);

        RaycastHit hit;

        // Получаем текущее положение левой ноги
        footPosL = animator.GetIKPosition(AvatarIKGoal.LeftFoot);

        //[RayCast] От ноги на землю
        if (Physics.Raycast(footPosL + Vector3.up, Vector3.down, out hit, 2.0f, rayLayer))
        {
            // Чертим вспомогательные линии в редакторе
            Debug.DrawLine(hit.point, hit.point + hit.normal, Color.yellow);

            // Установка новой позиции IK
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + footLoffset);

            // Установка нового вращения IK
            animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(Vector3.ProjectOnPlane(footL.forward, hit.normal), hit.normal));

            // Сохраняем точку столкновения рейкаста
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
