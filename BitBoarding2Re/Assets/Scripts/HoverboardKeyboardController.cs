using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using System;

public class HoverboardKeyboardController : MonoBehaviour
{
    //[SerializeField] private float turnLeftSpeed = .1f;
    //[SerializeField] private float turnRightSpeed = .1f;
    //[SerializeField] private float forwardSpeed = .1f;
    //[SerializeField] private float brakeSpeed = .1f;

    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float maxSpeed = 2f;

    [SerializeField] private string[] leftKey;
    [SerializeField] private string[] rightKey;
    [SerializeField] private string[] downKey;
    [SerializeField] private string[] upKey;
    [SerializeField] private string[] boostKey;

    private bool turnLeft = false;
    private bool turnRight = false;
    private bool lowerNose = false;
    private bool raiseNose = false;
    private bool boosting = false;

    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTurnLeft()
    {
        turnLeft = !turnLeft;
    }

    private void OnTurnRight()
    {
        turnRight = !turnRight;
    }

    private void OnNoseUp()
    {
        raiseNose = !raiseNose;
    }

    private void OnNoseDown()
    {
        lowerNose = !lowerNose;
    }

    private void OnBoost()
    {
        boosting = !boosting;
    }

    private void FixedUpdate()
    {
        // Rotate
        Vector3 amountToRotate = new Vector3(Convert.ToInt32(lowerNose) - Convert.ToInt32(raiseNose), Convert.ToInt32(turnRight) - Convert.ToInt32(turnLeft), 0);
        transform.Rotate(amountToRotate);

        // Slow
        Vector3 vel = _rb.velocity;
        _rb.velocity = vel * 0.9f;

        if (_rb.velocity.magnitude < maxSpeed)
        {
            _rb.AddForce(transform.forward * Convert.ToInt32(boosting) * acceleration, ForceMode.Impulse);
        }
    }
}
