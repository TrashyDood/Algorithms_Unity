using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public static class UnityUtils
{
    #region coroutines
    public static IEnumerator CRTimer(float duration, Action callback)
    {
        float endTime = Time.time + duration;

        while (Time.time < endTime)
            yield return new WaitForFixedUpdate();

        callback.Invoke();
    }

    public static IEnumerator CRFixedUpdate(uint durationFrames, Action<float> callback)
    {
        uint frameCount = 0;

        while (frameCount < durationFrames)
        {
            callback.Invoke(frameCount);
            yield return new WaitForFixedUpdate();
            frameCount++;
        }
    }

    public static IEnumerator CROscillateDamped(Action<float> action, uint frequency,
        float durationSeconds, float epsilon = 0.0001f)
    {
        int durationFrames = (int)MathF.Round(durationSeconds / Time.fixedDeltaTime);
        float previousValue = 0;

        for (int i = 1; i <= durationFrames; i++)
        {
            float currentValue = MathUtils.FloorToNearest(MathUtils.OscillateDamped(frequency, i / (float)durationFrames), epsilon);
            float delta = currentValue - previousValue;

            action(delta);

            yield return new WaitForFixedUpdate();
            previousValue = currentValue;
        }
    }
    #endregion
}