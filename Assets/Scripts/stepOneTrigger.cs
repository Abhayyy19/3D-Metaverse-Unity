using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stepOneTrigger : MonoBehaviour
{
    public GameObject stepOnePrompt;

    void Start()
    {
        stepOnePrompt.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stepOnePrompt.SetActive(true);
            Debug.Log("working");
        }
    }

}

