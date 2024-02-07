using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, itemContainer, fpsCam;

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
    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //set parent to null
        transform.SetParent(null);


        //Make RigidBody Kinematic and BOxCollider a trigger
        rb.isKinematic = false;
        coll.isTrigger = false;

        //item carries momentum of the player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //Add Force
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

    }
}
