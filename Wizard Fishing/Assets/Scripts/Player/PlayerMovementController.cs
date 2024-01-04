using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float moveForce, maxSpeed, descendSpeed, rotSpeed, maxRotSpeed;
    float moveDir, rotDir;

    public static PlayerMovementController Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.AddForceAtPosition(Vector2.right * moveDir, transform.position + (transform.up * .15f));
        rb.AddTorque(rotSpeed * rotDir);

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), -descendSpeed);
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

    #region Getters and Setters

    public float GetRotation()
    {
        return rotDir;
    }

    public float GetMovementDirection()
    {
        return moveDir;
    }

    public float GetDescendSpeed()
    {
        return descendSpeed;
    }

    public void SetDescendSpeed(float speed)
    {
        descendSpeed = speed;
    }
    #endregion
}
