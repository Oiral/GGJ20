using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject particle;

    public Transform testParent;

    [ContextMenu("Generate Particles")]
    public void Generate()
    {
        foreach (Transform child in GetComponentInChildren<Transform>())
        {
            Debug.DrawRay(transform.position, child.transform.position, Color.green, 5f);

            VisualEffect effect = Instantiate(particle, transform.position, Quaternion.LookRotation(child.transform.position), testParent).GetComponent<VisualEffect>();
            effect.Stop();
            child.GetComponent<Panel>().destroyParticle = effect;
        }
    }
}
