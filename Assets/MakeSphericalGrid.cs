using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MakeSphericalGrid : MonoBehaviour
{
    public GameObject prefab;

    public float height;
    public float width;
    public float circleRadius;
    public Vector3 origin;
    public Vector3 xAxis;
    public Vector3 yAxis;



    void Start()
    {
        float ySpacing = height * 0.75f;
        float xStagger = width / 2f;
        int xCount = Mathf.CeilToInt(circleRadius / width);
        int yCount = Mathf.CeilToInt(circleRadius / ySpacing);
        float circleRadiusSqr = circleRadius * circleRadius;
        // In cases where x is staggered by y position additional x unit is needed
        // Thus the -1
        for (int x = -xCount - 1; x <= xCount; x++)
        {
            for (int y = -yCount; y <= yCount; y++)
            {
                float yOffset = y * ySpacing;
                float xOffset = x * width + ((y % 2) * xStagger);

                if (xOffset * xOffset + yOffset * yOffset <= circleRadiusSqr)
                {
                    Vector3 position = origin + xAxis * xOffset;
                    position = position + yAxis * yOffset;

                    // Do instantiation using position vector, replace xAxis and yAxis with any vector
                    // Pointing in that axis, standard axis use the axis defined
                    // in Vector3 (such as Vector3.up) or your own. If creating a 2D scene, use Vector2

                    Instantiate(prefab, position, Quaternion.identity, null);
                }
            }
        }

        

    }

}

