using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phaseThreePrompt : MonoBehaviour
{
    public GameObject phaseThreePrompt2;
    public GameObject startPouringPrompt;
    public GameObject liquidFillSlider;
    public GameObject successPanel;
    public Camera mainCamera;
    public Camera phase3Cam;

    private bool playerInsideCollider = false;
    private bool isPouring = false;

    void Start()
    {
        startPouringPrompt.SetActive(false);
        phaseThreePrompt2.SetActive(false);
        liquidFillSlider.SetActive(false);
        mainCamera.enabled = true; // Enable the main camera by default
        phase3Cam.enabled = false; // Disable the fixed camera by default
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideCollider = true;
            phaseThreePrompt2.SetActive(true);
            startPouringPrompt.SetActive(true);
            Debug.Log("Phase 3 triggered");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideCollider = false;
            if (!isPouring)
            {
                liquidFillSlider.SetActive(false);
                startPouringPrompt.SetActive(false);
                // Switch back to the main camera when the panel is closed
                mainCamera.enabled = true;
                phase3Cam.enabled = false;
            }
            else
            {
                // If pouring, stop pouring and hide slider and prompt
                isPouring = false;
                liquidFillSlider.SetActive(false);
                startPouringPrompt.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (playerInsideCollider)
        {
            // Check if the liquidFillSlider is active
            if (liquidFillSlider.activeSelf)
            {
                // If the liquidFillSlider is active and the player presses the 'C' key
                if (Input.GetKeyDown(KeyCode.C))
                {
                    // Switch back to the main camera
                    //mainCamera.enabled = true;
                    //phase3Cam.enabled = false;
                }
            }
            else
            {
                // If the liquidFillSlider is not active, check for pouring action
                if (Input.GetKeyDown(KeyCode.F))
                {
                    isPouring = !isPouring;
                    liquidFillSlider.SetActive(isPouring);
                    startPouringPrompt.SetActive(!isPouring);

                    // Switch to the fixed camera when the panel is opened
                    mainCamera.enabled = !isPouring;
                    phase3Cam.enabled = isPouring;
                }
            }
        }
    }
    private void OnEnable()
    {
        EventManager.OnSuccessChanged += HandleSuccessChanged;
    }

    private void OnDisable()
    {
        EventManager.OnSuccessChanged -= HandleSuccessChanged;
    }

    private void HandleSuccessChanged(bool isSuccess)
    {
        if (isSuccess)
        {
            Debug.Log("Success state changed to true!");
            successPanel.SetActive(true);
            // Do something when success becomes true
            /*Destroy(startPouringPrompt);
            Destroy(phaseThreePrompt);
            Destroy(phase3Cam);
            Destroy(liquidFillSlider);*/
            Destroy(gameObject);

        }
        else
        {
            Debug.Log("Success state changed to false!");
            // Do something when success becomes false
        }
    }
}