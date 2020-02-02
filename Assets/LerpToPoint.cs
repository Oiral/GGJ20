using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToPoint : MonoBehaviour
{

    public Transform target;

    public float speed = 10f;

    public AnimationCurve speedCurve;

    private void OnEnable()
    {
        StartCoroutine(LerpToEnd());
    }

    IEnumerator LerpToEnd()
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        float elapsedTime = 0;
        while (elapsedTime < speed)
        {
            transform.position = Vector3.Lerp(pos, target.position, speedCurve.Evaluate(elapsedTime / speed));
            transform.rotation = Quaternion.Lerp(rot, target.rotation, speedCurve.Evaluate(elapsedTime / speed));
            elapsedTime += Time.deltaTime;
            yield return 0;
        }

        transform.position = target.position;
        transform.rotation = target.rotation;

        yield return 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.position);
    }
}
