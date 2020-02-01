using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float poleStop = 90;

    public float moveSpeed = 0.3f;

    public int player;

    public AnimationCurve horizontalSpeedCurve = new AnimationCurve();

    public GameObject pickUpLocation;

    public GameObject carrying = null;

    private void Update()
    {
        Move();

        if (Input.GetButtonDown("Interact" + player))
        {
            Debug.Log("Repair test");
            //Raycast down
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(pickUpLocation.transform.position, pickUpLocation.transform.TransformDirection(Vector3.up), out hit, 1))
            {
                Debug.DrawLine(pickUpLocation.transform.position, hit.point, Color.green, 10f);
                Debug.DrawRay(pickUpLocation.transform.position, pickUpLocation.transform.TransformDirection(Vector3.up), Color.green, 1);
                Debug.Log(hit.transform.gameObject,gameObject);

                if (carrying != null)
                {
                    //If we are carry
                    //Check if we hit a need to be repaired panel
                    if (hit.transform.GetComponent<Panel>() != null)
                    {
                        if (hit.transform.GetComponent<Panel>().destroyed)
                        {
                            GameManager.instance.RepairPanel(hit.transform.gameObject);
                            carrying.GetComponent<PickUpPanel>().Place();
                            carrying = null;
                        }
                    }
                }
                else
                {
                    //Check if we can pick something up
                    if (hit.transform.GetComponentInParent<PickUpPanel>())
                    {
                        //Pick up the panel
                        carrying = hit.transform.GetComponentInParent<PickUpPanel>().gameObject;
                        carrying.GetComponent<PickUpPanel>().PickUp(pickUpLocation);
                    }
                }

            }
        }
    }

    void Move()
    {
        Vector3 rotation = transform.rotation.eulerAngles;

        Vector2 input = new Vector3(-Input.GetAxis("Horizontal" + player), Input.GetAxis("Vertical" + player));

        input.Normalize();

        input *= moveSpeed;

        input *= Time.deltaTime;

        //input.y = input.y * Mathf.Abs((Mathf.Sin(rotation.x * Mathf.PI / 180) + 1));

        input.x = input.x * horizontalSpeedCurve.Evaluate(rotation.x);

        //Debug.Log(input.x);

        rotation += new Vector3(input.y, input.x);
        rotation.z = 0f;

        //rotation.x = Mathf.Clamp(rotation.x, -80, 80);
        //Debug.Log(rotation.x);

        if (rotation.x > poleStop && rotation.x < 180)
        {
            rotation.x = poleStop;
        }else if (rotation.x < 360 - poleStop && rotation.x > 180)
        {
            rotation.x = 360 - poleStop;
        }

        transform.rotation = Quaternion.Euler(rotation);

    }

}
