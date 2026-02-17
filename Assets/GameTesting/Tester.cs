using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tester : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    float frequency = 5;
    [SerializeField]
    uint duration = 200;
    [SerializeField]
    Vector3 amplitude;

    public double value,
        positive,
        negative;

    void OnJump(InputValue inputValue)
    {
        StartCoroutine(Oscillate(frequency, duration));
    }

    IEnumerator Oscillate1(float frequency, float duration)
    {
        Debug.Log("Starting with " + duration + " seconds duration.");

        float previousValue = 0;
        duration /= Time.fixedDeltaTime;
        duration -= duration % 2;

        Debug.Log("Converted to " + duration + " frames.");

        for (int i = 0; i <= duration; i++)
        {
            float t = (int)(i / (duration) * 1000f) / 1000f; // Round to 3rd decimal.
            float currentValue = (float)Math.Sin(2 * Mathf.PI * frequency * t);
            float delta = currentValue - previousValue;



            Debug.Log(t);

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator Oscillate(float frequency, float duration)
    {
        Debug.Log("Starting with " + duration + " seconds duration.");

        duration = 1 / duration;

        Debug.Log("Converted to " + duration + " frames.");

        for (int i = 0; i <= duration; i++)
        {
            float t = MathF.Round(i / (duration) * 1000f) / 1000f; // Round to 3rd decimal.
            Debug.Log(t);

            yield return new WaitForFixedUpdate();
        }
    }
}
