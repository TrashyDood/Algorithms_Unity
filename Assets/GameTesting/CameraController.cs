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

    private void FixedUpdate()
    {
        if (target != null)
            transform.position = transform.position.SmoothFollow(target.position + offset, smoothing, Time.fixedDeltaTime);
    }

    public void Shake(Vector3 amplitude, float frequency = 15, uint duration = 50) =>
        StartCoroutine(Oscillate(amplitude, frequency, duration));

    IEnumerator Oscillate(Vector3 amplitude, float frequency, uint duration)
    {
        float previousValue = 0;
        Vector3 offset = Vector3.zero;

        for (int i = 0; i < duration; i++)
        {
            float t = i / (duration - 1f);
            float currentValue = Utils.Oscillate(frequency, t) * (1 - t);
            float delta = currentValue - previousValue;

            previousValue = currentValue;
            offset += amplitude * delta;
            transform.eulerAngles += amplitude * delta;

            yield return new WaitForFixedUpdate();
        }

        transform.position -= offset;
    }
}
