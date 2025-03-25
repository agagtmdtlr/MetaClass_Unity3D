using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Study_Animation : MonoBehaviour
{
    // StringToHash 캐시된 프로퍼티 색인에 사용
    private static readonly int TEST = Animator.StringToHash("test");

    public Animator animator;
    
    // static field = none
    // static method = StringToHash
    
    // member field
    /*
        name            type        description
        avatar            Avatar     이 Animator에 연결된 아바타
        enabled            bool     Animator 활성화 여부
        isHuman            bool     Humanoid 타입 여부
        layerCount        int         레이어 수
        parameterCount    int         파라미터 개수
        speed            float     전체 애니메이션 속도 배율
        updateMode    AnimatorUpdateMode    업데이트 방식 설정
        applyRootMotion    bool    Root Motion 적용 여부

        name                            type                    description
        bodyPosition / bodyRotation      Vector3 / Quaternion    루트 본 위치 및 회전
        pivotPosition / pivotWeight     Vector3 / float            중심축 정보
     */
    // layerCount
    
    // member method
    /*
         Control Parameter
            void SetBool(string name, bool value)
            void SetFloat(string name, float value)
            void SetInteger(string name, int value)
            void SetTrigger(string name)
            void ResetTrigger(string name)

            bool GetBool(string name)
            float GetFloat(string name)
            int GetInteger(string name)
            bool IsParameterControlledByCurve(string name)

        Check State
            AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex)
            AnimatorTransitionInfo GetAnimatorTransitionInfo(int layerIndex)
            bool IsInTransition(int layerIndex)
            bool IsParameterControlledByCurve(string name)

        Specify for Humanoid
            Transform GetBoneTransform(HumanBodyBones bone)  // Get Bone Transfom Of Humanoid 본 위치 얻기
        */

    private void StudyAnimator()
    {
        animator.SetTrigger(TEST);
        Avatar avatar = animator.avatar;

        var layerCount = animator.layerCount;
        // editor animator layers에 들어가는 layer count를 의미
        
        var parameterCount = animator.parameterCount;
        // parameters shared by all layers
        for (int i = 0; i < parameterCount; i++)
        {
            var parameter = animator.parameters[i];
        }
        
        animator.speed = 0;

    }
}
