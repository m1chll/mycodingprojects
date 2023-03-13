using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{

    public NavMeshAgent agent;
    public Animator anim;
    public string sceneName;

    public float damage = 10;

    public float speed = 1.0f;

    private void Awake()
    {
        LoadGame();
    }
    
    private void Update()
    {
        // Move
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (movement != Vector3.zero)
        {
            agent.Move(movement * speed * Time.deltaTime);

            transform.rotation = Quaternion.LookRotation(movement);

            bool isRunning = true;
            anim.SetBool("isRunning", isRunning);
        }
        else if (movement == Vector3.zero)
        {
            bool isRunning = false;
            anim.SetBool("isRunning", isRunning);
        }
        

        // Attack
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isAttacking", true);
        }


        //Save
        if(Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }

        // Go to Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "NPC")
        {
            string newQuestText = other.gameObject.GetComponent<NPCDialog>().content;
            FindObjectOfType<UIManager>().SetQuestText(newQuestText);
            FindObjectOfType<UIManager>().ToggleTextWindow(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "NPC")
        {
            FindObjectOfType<UIManager>().ToggleTextWindow(false);
        }
    }

    public void Attack()
    {
        anim.SetBool("isAttacking", false);

        Health[] healthFound = FindObjectsOfType<Health>();

        // [0] ->

        foreach(Health health in healthFound)
        {
            if(health.gameObject.tag == "Enemy")
            {
                float distance = Vector3.Distance(transform.position, health.transform.position);
                
                if(distance < 2)
                {
                    float randomDamage = Random.Range(damage -5, damage + 5);
                    health.maxHealth -= randomDamage;
                    health.ActivateAttacking();
                }
            }
        }
    }

    public void SaveGame()
    {
        Vector3 posToSave = transform.position;

        PlayerPrefs.SetFloat("xPos", posToSave.x);
        PlayerPrefs.SetFloat("yPos", posToSave.y);
        PlayerPrefs.SetFloat("zPos", posToSave.z);

        Debug.Log("Save Game");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("xPos"))
        {
        Vector3 loadedPos = new Vector3();

        loadedPos.x = PlayerPrefs.GetFloat("xPos");
        loadedPos.y = PlayerPrefs.GetFloat("yPos");
        loadedPos.z = PlayerPrefs.GetFloat("zPos");

        transform.position = loadedPos;

        Debug.Log("Load Position");
        }
    }
}