using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCheck : MonoBehaviour
{
    public Transform itemContainer;

    // Update is called once per frame
    void Update()
    {
        // Check if the player is holding an object
        if (itemContainer.childCount > 0)
        {
            // Get the first child of the itemContainer
            Transform heldItem = itemContainer.GetChild(0);

            // Check if the held item is the specific object you want to check for
            if (heldItem.CompareTag("FlaskA"))
            {
                //Debug.Log("Player is holding FlaskA.");
            }
            else if(heldItem.CompareTag("FlaskB"))
            {
                //Debug.Log("Player is holding FlaskB.");
            }
            else if(heldItem.CompareTag("FlaskC"))
            {
                //Debug.Log("Player is holding FlaskC.");
            }
        }
        else
        {
            // Player is not holding any object
           // Debug.Log("Player is not holding any object.");
        }
    }
}
