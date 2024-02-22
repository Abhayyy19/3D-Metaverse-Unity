using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionStart : MonoBehaviour
{
    public GameObject missionStartPrompt;
    public GameObject stepOneTrigger;
    public Camera phase1Cam, phase2Cam, phase3Cam; 
    public ParticleSystem startMissionEffect, stepOneEffect;

    private bool missionStarted = false;

    void Start()
    {
        missionStartPrompt.SetActive(false);
        stepOneTrigger.SetActive(false);
        startMissionEffect.Play();
        stepOneEffect.Stop();
        phase1Cam.enabled = false;
        phase2Cam.enabled = false;
        phase3Cam.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !missionStarted)
        {
            missionStarted = true;
            missionStartPrompt.SetActive(true);
            startMissionEffect.Stop();
            stepOneEffect.Play();
            stepOneTrigger.SetActive(true);
            Debug.Log("Mission started");
            StartCoroutine(WaitForSec());
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
        missionStartPrompt.SetActive(false);
        Destroy(startMissionEffect); // Stop the particle system
        Destroy(gameObject);
    }
}