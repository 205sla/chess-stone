using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAniControl : MonoBehaviour
{
    private Animator animator;
    float TimeD=0;
    bool AttackAni1 = false, AttackAni2 = false, AttackAni0 = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (AttackAni1)
        {
            TimeD+=Time.deltaTime;
            if (TimeD > 0.5f)
            {
                AttackAni1 = false;
                animator.SetBool("IsMotion1", false);
                TimeD = 0;
            }
        }
        if (AttackAni2)
        {
            TimeD += Time.deltaTime;
            if (TimeD > 0.5f)
            {
                AttackAni2 = false;
                animator.SetBool("IsMotion2", false);
                TimeD = 0;
            }
        }
        if (AttackAni0)
        {
            TimeD += Time.deltaTime;
            if (TimeD > 0.5f)
            {
                AttackAni0 = false;
                animator.SetBool("IsMotion", false);
                TimeD = 0;
            }
        }
    }

    public void Ani_die(int n)
    {
        if(animator != null)
        {
            if (n == 1)
            {
                animator.SetBool("IsDeath1", true);
            }else if(n == 2)
            {
                animator.SetBool("IsDeath2", true);
            }
            else
            {
                Debug.LogError("애니메이션 오류(파라미터)");
            }
        }
        else
        {
            Debug.LogError("애니메이션 오류(애니메이션컨틀로러)");
        }
    }

    public void Ani_Attack(int n)
    {
        if (animator != null)
        {
            if (n == 1)
            {
                animator.SetBool("IsMotion1", true);
                AttackAni1 = true;
                TimeD = 0;
            }
            else if (n == 2)
            {
                animator.SetBool("IsMotion2", true);
                AttackAni2 = true;
                TimeD = 0;
            }
            else if(n == 0)
            {
                animator.SetBool("IsMotion", true);
                AttackAni0 = true;
                TimeD = 0;
            }
            else
            {
                Debug.LogError("애니메이션 오류(파라미터)");
            }
        }
        else
        {
            Debug.LogError("애니메이션 오류(애니메이션컨틀로러)");
        }
    }

    public void KingSet()
    {

    }
}
