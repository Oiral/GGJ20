using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Panel : MonoBehaviour
{
    public bool destroyed = false;

    [HideInInspector]
    public VisualEffect destroyParticle;

    private void Start()
    {
        destroyParticle = GetComponentInChildren<VisualEffect>();

        destroyParticle.Stop();
    }
}
