using UnityEngine;

public static class QuaternionUtils
{
    public static Quaternion SmoothMove(this Quaternion quaternion, Quaternion target, float smoothing, float delta) =>
        Quaternion.Lerp(quaternion, target, (1 / smoothing) * delta);
}
