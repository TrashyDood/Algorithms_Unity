using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [Range(0f, 2.5f)]
    [SerializeField]
    float smoothing;

    Vector3 offset;

    private void Awake()
    {
        offset =  transform.position - target.position;
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        FirstPersonCamera.FPLook(Input.mousePositionDelta, transform, transform);
    }

    public Coroutine Shake(Vector3 posAmplitude = default, Vector3 rotAmplitude = default,
        uint frequency = 5, float duration = 0.75f, float epsilon = 0.0001f) =>
            StartCoroutine(UnityUtils.CROscillateDamped((f) =>
            {
                if (posAmplitude != Vector3.zero)
                    transform.position = VectorUtils.RoundToNearest(transform.position + posAmplitude * f, epsilon);

                if (rotAmplitude != Vector3.zero)
                    transform.eulerAngles = VectorUtils.RoundToNearest(transform.eulerAngles + rotAmplitude * f, epsilon);

            }, frequency, duration));
}
