using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float poleStop = 90;

    private void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 rotation = transform.rotation.eulerAngles;

        Vector2 input = new Vector3(Input.GetAxis("Vertical"), -Input.GetAxis("Horizontal"));

        input.y = input.y * (Mathf.Abs(Mathf.Sin(rotation.x / 360)) + 1);

        Debug.Log(input.y);

        rotation += new Vector3(input.x, input.y);
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
