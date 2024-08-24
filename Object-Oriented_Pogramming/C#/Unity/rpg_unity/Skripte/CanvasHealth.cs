using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHealth : MonoBehaviour
{
    public Health health;
    public Text textHealth;

    public void Update()
    {
        textHealth.text = health.maxHealth.ToString("f0");

        transform.rotation = Camera.main.transform.rotation;
    }

}
