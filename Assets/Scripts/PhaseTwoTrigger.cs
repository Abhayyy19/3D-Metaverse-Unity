using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseTwoTrigger : MonoBehaviour
{
    public GameObject phaseTwoPrompt;
    public GameObject placeAnalytePrompt;
    //public GameObject liquidFillSlider;
    public GameObject phaseThreeTrigger;

    private bool playerInsideCollider = false;
    private bool cutsceneTriggered = false;
    private bool isPouring = false;

    public Camera mainCamera;
    public Camera cutsceneCamera;
    public GameObject cutsceneTimeline;

    void Start()
    {
        placeAnalytePrompt.SetActive(false);
        //liquidFillSlider.SetActive(false);
        cutsceneCamera.enabled = false;
        phaseThreeTrigger.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideCollider = true;
            phaseTwoPrompt.SetActive(true);
            placeAnalytePrompt.SetActive(true);
            Debug.Log("Phase 2 triggered");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideCollider = false;
            if (!isPouring)
            {
                //liquidFillSlider.SetActive(false);
                placeAnalytePrompt.SetActive(false);
            }
            else
            {
                // If pouring, stop pouring and hide slider and prompt
                isPouring = false;
                //liquidFillSlider.SetActive(false);
                placeAnalytePrompt.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (playerInsideCollider && Input.GetKeyDown(KeyCode.F) && !cutsceneTriggered)
        {
            /*isPouring = !isPouring;
            liquidFillSlider.SetActive(isPouring);
            startPouringPrompt.SetActive(!isPouring);*/
            Debug.Log("Animation 2 Started");
            StartCutscene();
        }
        else if (playerInsideCollider && !cutsceneTriggered) // If player is inside collider and animation not triggered, display addToBurettePrompt
        {
            placeAnalytePrompt.SetActive(true);
        }
    }

    void StartCutscene()
    {
        placeAnalytePrompt.SetActive(false); // Disable the prompt when animation starts
        StartCoroutine(FinishCutscene());
        cutsceneCamera.enabled = true;
        mainCamera.enabled = false;
        cutsceneTimeline.SetActive(true);
        cutsceneTriggered = true;
    }

    IEnumerator FinishCutscene()
    {
        yield return new WaitForSeconds(4);
        Debug.Log("Animation 2 Finished");
        cutsceneCamera.enabled = false;
        mainCamera.enabled = true;
        phaseThreeTrigger.SetActive(true);
        Destroy(gameObject); // Destroy this script component
    }
}