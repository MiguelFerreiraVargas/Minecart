using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    Vector3 originalPos;

    void Awake()
    {
        Instance = this;
        originalPos = transform.localPosition;
    }

    public void Shake(float duration, float strength)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine(duration, strength));
    }

    IEnumerator ShakeRoutine(float duration, float strength)
    {
        float timer = 0f;

        while (timer < duration)
        {
            Vector3 randomPos =
                originalPos + Random.insideUnitSphere * strength;

            transform.localPosition = randomPos;

            timer += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}