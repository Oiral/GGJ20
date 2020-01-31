using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float poleStop = 90;

    public float moveSpeed = 0.3f;

    public int player;

    public AnimationCurve horizontalSpeedCurve = new AnimationCurve();

    private void Update()
    {
        Move();
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

        Debug.Log(input.x);

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
