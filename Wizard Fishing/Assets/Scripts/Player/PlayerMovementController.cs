using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float moveForce, maxSpeed, decendSpeed, rotSpeed, maxRotSpeed;
    float moveDir, rotDir;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.AddForceAtPosition(Vector2.right * moveDir, Vector2.up * .15f);
        rb.AddTorque(rotSpeed * rotDir);

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), -decendSpeed);
        rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -maxRotSpeed, maxRotSpeed);
       
    }

    void OnMove(InputValue input)
    {
        moveDir = input.Get<float>();
    }

    void OnRotate(InputValue input)
    {
        rotDir = input.Get<float>();
    }
}
