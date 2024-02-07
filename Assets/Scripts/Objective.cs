using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Objective : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string collisionMessage = "You collided!";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mission0"))
        {
            textDisplay.text = collisionMessage;
        }
    }
}
