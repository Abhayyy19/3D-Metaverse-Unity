using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveCompleteTrigger : MonoBehaviour
{
    public GameObject objective1;
    public GameObject finishedObjective1;
    
    private void OnTriggerEnter(Collider other)
    {
        objective1.SetActive(false);
        finishedObjective1.SetActive(true);
    }
}
