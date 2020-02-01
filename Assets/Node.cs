using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool hasAttached;

    public int rowNum;

    public float radius;

    public GameObject panel;

    public GameObject panelAttachPoint;

    private void Start()
    {
        GameObject spawnedPanel = Instantiate(panel, panelAttachPoint.transform.position, Quaternion.identity, transform);

        spawnedPanel.transform.localScale = Vector3.one * (radius / 2);

        spawnedPanel.transform.localRotation = Quaternion.identity;
    }
}
