using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public Node attachedNode;

    public PanelSpawner attachedSpawner;

    public void Explode()
    {
        //Once it explodes
        attachedNode.hasAttached = false;
        attachedNode = null;
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = transform.up * 5;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Explode();
        }
    }

    public void PickUp()
    {
        //If we are attached to something
        if (attachedNode != null)
            return;

        //If we are attached to the spawner
        if (attachedSpawner != null)
            attachedSpawner.CollectPanel();

        //If we can pick this up
        //Maybe parent to the thingy

    }
}
