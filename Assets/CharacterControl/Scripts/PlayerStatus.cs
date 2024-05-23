using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int HP = 100;
    public bool isDead = false;
    public Animator animator;

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            animator.SetTrigger("death");
            GetComponent<CapsuleCollider>().enabled = false;
            isDead = true;
        }
        else
        {
            animator.SetTrigger("hit");
        }
    }
}
