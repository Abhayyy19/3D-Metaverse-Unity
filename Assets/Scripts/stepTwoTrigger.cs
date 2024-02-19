using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class stepTwoTrigger : MonoBehaviour
{
    public GameObject stepTwoPrompt;
    public GameObject addToBurettePrompt;
    public ParticleSystem stepTwoEffect;
    public Camera mainCamera;
    public Camera cutsceneCamera;
    public GameObject cutsceneTimeline;

    private bool playerInsideCollider = false;
    private bool cutsceneTriggered = false;

    void Start()
    {
        stepTwoPrompt.SetActive(false);
        addToBurettePrompt.SetActive(false);
        cutsceneCamera.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideCollider = true;
            stepTwoPrompt.SetActive(true);
            Debug.Log("working");

            // Stop and destroy the stepOneEffect particles
            if (stepTwoEffect != null)
            {
                stepTwoEffect.Stop();
                StartCoroutine(WaitForSec());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideCollider = false;
            addToBurettePrompt.SetActive(false); // Disable the prompt when player exits collider
        }
    }

    void Update()
    {
        // Check for "F" key press and collider collision
        if (playerInsideCollider && Input.GetKeyDown(KeyCode.F) && !cutsceneTriggered)
        {
            addToBurettePrompt.SetActive(false);
            Debug.Log("Animation Started");
            addToBurettePrompt.SetActive(false);
            StartCoroutine(FinishCutscene());
            cutsceneCamera.enabled = true;
            mainCamera.enabled = false;
            cutsceneTimeline.SetActive(true);
            cutsceneTriggered = true;
        }
        else if (playerInsideCollider) // If player is inside collider, display addToBurettePrompt
        {
            addToBurettePrompt.SetActive(true);
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        Destroy(stepTwoEffect); // Destroy the particle system
    }

    IEnumerator FinishCutscene()
    {
        yield return new WaitForSeconds(22);
        Debug.Log("Animation Finished");
        cutsceneCamera.enabled = false;
        mainCamera.enabled = true;
        Destroy(gameObject);
    }
}