using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject particle;
    

    [ContextMenu("Generate Particles")]
    public void Generate()
    {
        foreach (Transform child in GetComponentInChildren<Transform>())
        {
            Debug.DrawRay(transform.position, child.transform.position * 2, Color.green, 2f);
        }
    }
}
