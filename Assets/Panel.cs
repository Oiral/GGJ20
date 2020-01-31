using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public Node attachedNode;

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
}
