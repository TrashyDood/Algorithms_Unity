using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidBody;
    [SerializeField]
    float speed,
        acceleration,
        deceleration;

    Vector2 input;
    Vector3 currentVelocity,
        targetVelocity;
    
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        rigidBody.Move(currentVelocity * Time.fixedDeltaTime, Quaternion.LookRotation(currentVelocity));
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public float Acceleration
    {
        get => acceleration;
        set => acceleration = value;
    }

    public float Deceleration
    {
        get => deceleration;
        set => deceleration = value;
    }

    public void OnMove(InputValue value)
    {
        Debug.Log("OnMove");
        input = value.Get<Vector2>();
        targetVelocity = new Vector3(input.x, 0, input.y) * speed;
    }
}
