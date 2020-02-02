using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Panel : MonoBehaviour
{
    public bool destroyed = false;
    
    public VisualEffect destroyParticle;

    public VisualEffectAsset effect;

    private void Start()
    {
        //destroyParticle = GetComponentInChildren<VisualEffect>();

        //destroyParticle.Stop();
    }

    public void DestroyPanel()
    {
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        destroyParticle.visualEffectAsset = effect;
        destroyParticle.Play();
        GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(3.2f);
        GetComponent<MeshRenderer>().enabled = false;
        destroyed = true;

        yield return new WaitForSeconds(1.8f);
        destroyParticle.Stop();

        yield return new WaitForSeconds(2f);
        destroyParticle.visualEffectAsset = null;
    }
}
