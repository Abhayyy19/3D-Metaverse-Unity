using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class stepTwoTrigger : MonoBehaviour
{
    public GameObject stepTwoPrompt;
    public GameObject addToBurettePrompt;
    public GameObject phaseTwoTrigger;
    public ParticleSystem stepTwoEffect;
    public Camera mainCamera;
    public Camera cutsceneCamera, cutsceneCamera2;
    public GameObject cutsceneTimeline;

    private bool playerInsideCollider = false;
    private bool cutsceneTriggered = false;

    void Start()
    {
        stepTwoPrompt.SetActive(false);
        addToBurettePrompt.SetActive(false);
        phaseTwoTrigger.SetActive(false);
        cutsceneCamera.enabled = false;
        cutsceneCamera2.enabled = false;
        stepTwoEffect.Play();
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
            Debug.Log("Animation Started");
            StartCutscene();
        }
        else if (playerInsideCollider && !cutsceneTriggered) // If player is inside collider and animation not triggered, display addToBurettePrompt
        {
            addToBurettePrompt.SetActive(true);
        }
    }

    void StartCutscene()
    {
        addToBurettePrompt.SetActive(false); // Disable the prompt when animation starts
        StartCoroutine(FinishCutscene());
        cutsceneCamera.enabled = true;
        mainCamera.enabled = false;
        cutsceneTimeline.SetActive(true);
        cutsceneTriggered = true;
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
        phaseTwoTrigger.SetActive(true);
        Destroy(gameObject); // Destroy this script component
    }
}