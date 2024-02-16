using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionStart : MonoBehaviour
{
    public GameObject missionStartPrompt;
    public GameObject objective1;

    void Start()
    {
        missionStartPrompt.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            missionStartPrompt.SetActive(true);
            objective1.SetActive(true);
            Debug.Log("working");
            StartCoroutine(WaitForSec());
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        Destroy(missionStartPrompt);
        Destroy(gameObject);
    }
}
