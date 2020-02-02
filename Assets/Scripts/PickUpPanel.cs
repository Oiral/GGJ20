using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPanel : MonoBehaviour
{
    public GameObject spawner;

    public bool beingCarried;

    public Transform pickUpObject;

    public Vector3 targetScale;
    public float scaleSpeed = 1f;

    public void PickUp(GameObject obj)
    {
        pickUpObject = obj.transform;

        beingCarried = true;
    }

    public void Place()
    {
        pickUpObject = spawner.transform;

        transform.localScale = Vector3.zero;

        beingCarried = false;
    }

    private void Update()
    {
        if (beingCarried == true)
        {
            transform.position = pickUpObject.transform.position;
            transform.rotation = pickUpObject.transform.rotation * Quaternion.Euler(180,0,0);
            targetScale = Vector3.one * 0.5f;
        }
        else
        {
            transform.position = pickUpObject.transform.position;
            transform.rotation = pickUpObject.transform.rotation;
            targetScale = Vector3.one;
        }

        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
    }
}
