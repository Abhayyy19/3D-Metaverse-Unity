using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SliderController : MonoBehaviour
{
    public Slider slider;
    public float increaseRate = 1f; // Adjust this to control how fast the slider increases
    public GameObject stopCock;

    public GameObject liquidFillSlider, startPouringPrompt, confirmPrompt;
    public Camera currentCam, mainCamera;

    public float rotationAngle = 90f; // Angle to rotate the stopcock
    public float minRange = 0.3f; // Minimum value of the range
    public float maxRange = 0.7f; // Maximum value of the range

    public GameObject successPanel;
    public GameObject excessivePanel;
    public GameObject lessPanel;

    private bool isIncreasing = false;
    private Quaternion initialRotation; // Store the initial rotation of the stopcock
    private bool success = false;

    void Start()
    {
        confirmPrompt.SetActive(true);
        startPouringPrompt.SetActive(false);
        liquidFillSlider.SetActive(true);
        currentCam.enabled = true;
        mainCamera.enabled = false;
        // Store the initial rotation of the stopcock
        initialRotation = stopCock.transform.rotation;
        // Hide all outcome panels initially
        successPanel.SetActive(false);
        excessivePanel.SetActive(false);
        lessPanel.SetActive(false);
    }

    void Update()
    {
        // Check if the left mouse button is being held down
        if (Input.GetMouseButton(0))
        {
            isIncreasing = true;
        }
        else
        {
            isIncreasing = false;
        }

        // If the key is being held down, increase the slider value and rotate the stopcock
        if (isIncreasing)
        {
            slider.value += increaseRate * Time.deltaTime;
            // Rotate the stopcock
            RotateStopcock(rotationAngle);
        }
        else
        {
            // If the button is released, rotate the stopcock back to its initial rotation
            stopCock.transform.rotation = initialRotation;
        }

        // Check if the player presses the confirm key ("C")
        if (Input.GetKeyDown(KeyCode.C))
        {
            float sliderValue = slider.value;
            if (sliderValue >= minRange && sliderValue <= maxRange)
            {
                // Disable the slider and enable the success panel
                successPanel.SetActive(true);
                StartCoroutine(WaitForSec());
                currentCam.enabled = false;
                mainCamera.enabled = true;
                liquidFillSlider.SetActive(false);
                confirmPrompt.SetActive(false);
                success = true;
                EventManager.TriggerSuccessChanged(success);
            }
            else if (sliderValue > maxRange)
            {
                // Reset the slider and enable the excessive panel
                excessivePanel.SetActive(true);
                slider.value = 0f;
                StartCoroutine(WaitForSec());
            }
            else
            {
                // Reset the slider and enable the less panel
                lessPanel.SetActive(true);
                StartCoroutine(WaitForSec());
            }
        }
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1);
        successPanel.SetActive(false);
        excessivePanel.SetActive(false);
        lessPanel.SetActive(false);
    }

    void RotateStopcock(float angle)
    {
        // Calculate the target rotation based on the angle
        Quaternion targetRotation = Quaternion.Euler(angle, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z);
        // Rotate the stopcock towards the target rotation
        stopCock.transform.rotation = Quaternion.RotateTowards(stopCock.transform.rotation, targetRotation, Time.deltaTime * 100f);
    }
}