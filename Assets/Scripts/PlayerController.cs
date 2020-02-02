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

    public Transform externalPlayer;

    public GameObject carrying = null;

    public Animator animator;


    [Header("Death")]
    public bool alive = true;

    public float deathScaleTime = 0.5f;

    public float deathMinScale = 0.5f;

    public float respawnTime = 1f;

    public AnimationCurve deathFall;

    private void Update()
    {
        Move();

        Interaction();
        
        CheckForFall();
        
    }

    void Move()
    {
        if (alive == false)
            return;

        Vector3 rotation = transform.rotation.eulerAngles;

        Vector2 input = new Vector3(-Input.GetAxis("Horizontal" + player), Input.GetAxis("Vertical" + player));

        if (input.magnitude < .2f)
        {

            animator.SetBool("Walking", false);
            return;
        }

        if (animator.GetBool("Walking") == false)
        {
            animator.SetBool("Walking", true);
            animator.SetTrigger("Start Walking");
        }
        

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

    void Interaction()
    {
        if (alive == false)
            return;

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
                Debug.Log(hit.transform.gameObject, gameObject);

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

    void CheckForFall()
    {
        if (alive == false)
            return;

        //Raycast down
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(externalPlayer.position, externalPlayer.TransformDirection(Vector3.up), out hit, 1))
        {
            Debug.DrawLine(externalPlayer.position, hit.point, Color.green, 3f);
            //Debug.Log(hit.transform.gameObject, gameObject);
            if (hit.transform.gameObject.GetComponent<Panel>() != null && hit.transform.gameObject.GetComponent<Panel>().destroyed)
            {
                //Fall
                Debug.Log("Player died");
                StartCoroutine(Fall());
            }
        }
    }

    IEnumerator Fall()
    {
        alive = false;

        if (carrying != null)
        {
            carrying.GetComponent<PickUpPanel>().Place();
            carrying = null;
        }

        yield return new WaitForSeconds(0);
        //Fall down
        float elapsedTime = 0;
        while (elapsedTime < deathScaleTime)
        {
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * deathMinScale, deathFall.Evaluate(elapsedTime / deathScaleTime));
            elapsedTime += Time.deltaTime;
            yield return 0;
        }

        transform.localScale = Vector3.one * deathMinScale;

        yield return new WaitForSeconds(respawnTime);

        transform.localScale = Vector3.one;

        float temp;

        if (player == 0)
        {
            temp = -90;
        }
        else
        {
            temp = 90;
        }

        transform.rotation = Quaternion.Euler(temp, 0, 0);

        //Set rotation
        alive = true;
    }

}
