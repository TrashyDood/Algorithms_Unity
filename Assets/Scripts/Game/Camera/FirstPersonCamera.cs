using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField]
    Transform yawTransform,
        pitchTransform;

    public static void FPLook(Vector2 inputDelta, Transform yawTransform, Transform pitchTransform,
        float yawSmoothing = 0.1f, float pitchSmoothing = 0.1f, float yawSensitivity = 1,
        float pitchSensitivity = 1, float yawMax = 361, float pitchMax = 90)
    {
        //yawTransform.localEulerAngles += Vector3.up * inputDelta.x;
        //pitchTransform.localEulerAngles -= Vector3.right * inputDelta.y;
        Vector3 newEuler = new(
            yawTransform.localEulerAngles.x - inputDelta.y,
            yawTransform.localEulerAngles.y + inputDelta.x);


    }
}
