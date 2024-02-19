using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionStart : MonoBehaviour
{
    public GameObject missionStartPrompt;
    public ParticleSystem startMissionEffect, stepOneEffect;

    private bool missionStarted = false;

    void Start()
    {
        missionStartPrompt.SetActive(false);
        startMissionEffect.Play();
        stepOneEffect.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !missionStarted)
        {
            missionStarted = true;
            missionStartPrompt.SetActive(true);
            startMissionEffect.Stop();
            stepOneEffect.Play();
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