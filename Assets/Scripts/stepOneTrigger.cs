using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stepOneTrigger : MonoBehaviour
{
    public GameObject stepOnePrompt;
    public GameObject stepTwoTrigger;
    public ParticleSystem stepOneEffect, stepTwoEffect;

    void Start()
    {
        stepTwoTrigger.SetActive(false);
        stepOnePrompt.SetActive(false);
        stepTwoEffect.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stepOnePrompt.SetActive(true);
            Debug.Log("working");

            // Stop and destroy the stepOneEffect particles
            if (stepOneEffect != null)
            {
                stepOneEffect.Stop();
                stepTwoEffect.Play();
                //Destroy(stepOneEffect.gameObject);
                StartCoroutine(WaitForSec());
            }
        }
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        stepOnePrompt.SetActive(false);
        stepTwoTrigger.SetActive(true);
        Destroy(stepOneEffect); // Stop the particle system
        Destroy(gameObject);
    }

}


