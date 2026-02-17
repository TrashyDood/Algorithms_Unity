using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tester : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    float duration = 1;
    [SerializeField]
    uint frequency = 5;
    [SerializeField]
    Vector3 amplitude;

    void OnJump(InputValue inputValue)
    {
        StartCoroutine(Utils.CROscillate((f) =>
        {
            target.eulerAngles = Utils.RoundToNearest(target.eulerAngles + amplitude * f, 0.0001f);
        }, frequency, duration));
    }

    IEnumerator OscillateCR(Action<float> action, uint frequency, float durationSeconds, float epsilon = 0.0001f)
    {
        int durationFrames = (int)MathF.Round(durationSeconds / Time.fixedDeltaTime);
        float previousValue = 0;

        for (int i = 1; i <= durationFrames; i++)
        {
            float t = i / (float)durationFrames;
            float currentValue = Utils.FloorToNearest((float)Math.Sin(2 * Mathf.PI * frequency * t) * (1 - t), epsilon);
            float delta = currentValue - previousValue;

            action(delta);

            yield return new WaitForFixedUpdate();
            previousValue = currentValue;
        }
    }
}
