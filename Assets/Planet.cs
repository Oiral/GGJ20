using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public List<Transform> panelLocations;

    public GameObject panelPrefab;

    private void Awake()
    {
        for (int i = 0; i < panelLocations.Count; i++)
        {
            GameObject spawnedObject = Instantiate(panelPrefab, panelLocations[i].transform.position, Quaternion.identity, transform);
            spawnedObject.transform.LookAt(panelLocations[i].GetComponent<MeshFilter>().mesh.vertices[0] + transform.position, transform.position);
        }
    }
}
