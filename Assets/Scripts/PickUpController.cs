using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, itemContainer, fpsCam, dropLocation;
    public GameObject hands;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;


    private void Start()
    {
        //Setup
        if (!equipped)
        {
            
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        //check if the player is in range and e is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        //Drop if Q is pressed while equipped
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();

    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        hands.SetActive(true);

        //make weapon a child of camera and move it to default position
        transform.SetParent(itemContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make RigidBody Kinematic and BOxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //Enable Script
        
    }
    public void Drop()
    {
        equipped = false;
        slotFull = false;

        hands.SetActive(false);

        // Make Rigidbody Kinematic and BoxCollider a trigger
        rb.isKinematic = false;
        coll.isTrigger = false;

        // Calculate the distance between the player and the dropLocation
        float distanceToDropLocation = Vector3.Distance(player.position, dropLocation.position);

        if (distanceToDropLocation <= pickUpRange)
        {
            // If the player is within range of the dropLocation, fit the item to the dropLocation
            transform.SetParent(dropLocation);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            transform.localScale = Vector3.one;
        }
        else
        {
            // If the player is out of range, drop the item where the player is standing
            transform.SetParent(null);
            transform.position = player.position;
        }

        // Item carries momentum of the player
        //rb.velocity = player.GetComponent<Rigidbody>().velocity;

        // Add Force
        //rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        //rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
    }
}
