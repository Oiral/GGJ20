using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPulse : MonoBehaviour
{
    public float rate;
    public Vector2 minAndMax;

    private void Update()
    {
        float height = minAndMax.y - minAndMax.x;

        GetComponent<Light>().intensity = (height * Mathf.Sin(Time.time)) + minAndMax.x;
    }
}
