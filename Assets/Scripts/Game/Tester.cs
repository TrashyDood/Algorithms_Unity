using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tester : MonoBehaviour
{
    [SerializeField]
    CameraController camera;
    [SerializeField]
    float duration = 1;
    [SerializeField]
    uint frequency = 5;
    [SerializeField]
    Vector3 posAmplitude,
        rotAmplitude;

    void OnJump(InputValue inputValue)
    {
        camera.Shake(posAmplitude, rotAmplitude, frequency, duration);
    }

    IEnumerator OscillateCR(Action<float> action, uint frequency, float durationSeconds, float epsilon = 0.0001f)
    {
        int durationFrames = (int)MathF.Round(durationSeconds / Time.fixedDeltaTime);
        float previousValue = 0;

        for (int i = 1; i <= durationFrames; i++)
        {
            float t = i / (float)durationFrames;
            float currentValue = MathUtils.FloorToNearest((float)Math.Sin(2 * Mathf.PI * frequency * t) * (1 - t), epsilon);
            float delta = currentValue - previousValue;

            action(delta);

            yield return new WaitForFixedUpdate();
            previousValue = currentValue;
        }
    }
}
