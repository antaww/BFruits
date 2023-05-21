using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool isShaking;
    public AnimationCurve shakeCurve;
    public float shakeDuration = 1f;

    private void Update()
    {
        if (!isShaking) return;
        isShaking = false;
        StartCoroutine(ShakeCamera());
    }

    private IEnumerator ShakeCamera()
    {
        var originalPosition = transform.localPosition;
        var elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            var strength = shakeCurve.Evaluate(elapsedTime / shakeDuration);
            transform.localPosition = originalPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.localPosition = originalPosition;
    }
}
