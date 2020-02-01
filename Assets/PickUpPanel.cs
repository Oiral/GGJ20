using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPanel : MonoBehaviour
{
    public GameObject spawner;

    public bool beingCarried;
    public void PickUp()
    {
        beingCarried = true;
    }

    public void Place()
    {
        beingCarried = false;
    }
}
