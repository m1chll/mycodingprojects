using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;

    public void ActivateAttacking()
    {
        if(maxHealth <= 0)
        {
            return;
        }
        
        if(gameObject.tag == "Enemy")
        {
            GetComponent<EnemyMovement>().StartAttacking();
        }
    }

    public void Update()
    {
        if(maxHealth <= 0)
        {
            maxHealth = 0;
            if(gameObject.tag == "Player")
            {
                SceneManager.LoadScene(0);
            } 
            else if(gameObject.tag == "Enemy")
            {
                GetComponent<EnemyMovement>().Die();
            }
        }
    }
}
