using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public static Quaternion FPLook(Vector2 inputDelta, Quaternion currentRotation, float yawSensitivity = 1, float pitchSensitivity = 1)
    {
        Quaternion yaw = Quaternion.Euler(Vector3.up * inputDelta.x * yawSensitivity),
            pitch = Quaternion.Euler(Vector3.right * -inputDelta.y * pitchSensitivity);

        return yaw * currentRotation * pitch;
    }
}
