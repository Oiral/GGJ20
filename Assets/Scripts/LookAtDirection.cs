using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtDirection : MonoBehaviour
{
    Vector3 prevPos = new Vector3();

    public GameObject cam;
    public float lookSpeed = 5;

    private void Update()
    {
        Vector3 dir;

        dir = transform.position - prevPos;

        dir = dir.normalized;

        //dir = transform.position + dir;
        
        if (dir.magnitude > 0.1f)
        {
            Vector3 up;

            up = transform.position - cam.transform.position;


            //Lerp to dir from current rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir, up), lookSpeed * Time.deltaTime);

            prevPos = transform.position;
        }

        

    }
}
