using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Animator anim;
    public float damage = 10;

    public void StartAttacking()
    {
        anim.SetBool("isAttacking", true);
    }

    public void Attack()
    {
        anim.SetBool("isAttacking", false);
        
        CharacterMovement player = FindObjectOfType<CharacterMovement>();

        float randomDamage = Random.Range(damage -5, damage + 5);

        player.GetComponent<Health>().maxHealth -= randomDamage;
    }

    public void Die()
    {
        anim.SetBool("isDead", true);
    }
}
