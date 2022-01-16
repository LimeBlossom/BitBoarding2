using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverboardVRController : MonoBehaviour
{
    [SerializeField] private GameObject playerHead;
    [SerializeField] private float playerHeight = 1.5f;

    [SerializeField] private float turnSpeed = 1f;
    [SerializeField] private float brakeSpeed = .9f;

    [SerializeField] private float acceleration = 3f;
    [SerializeField] private float maxSpeed = 2f;

    private float maxVerticleAngle = 85;

    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Rotate
        Vector3 pos = playerHead.transform.localPosition;
        float leftLean = 0;
        if(pos.x < 0)
        {
            leftLean = Mathf.Pow(pos.x * turnSpeed, 2);
        }
        float rightLean = 0;
        if(pos.x > 0)
        {
            rightLean = Mathf.Pow(pos.x * turnSpeed, 2);
        }

        Vector3 amountToRotate = new Vector3(0, rightLean - leftLean, 0);
        transform.rotation *= Quaternion.Euler(amountToRotate);

        float downLean = 0;
        if (pos.z > 0)
        {
            downLean = Mathf.Pow(pos.z * turnSpeed * 4, 2);
            if(downLean > maxVerticleAngle)
            {
                downLean = maxVerticleAngle;
            }
        }
        float upLean = 0;
        if (pos.z < 0)
        {
            upLean = Mathf.Pow(pos.z * turnSpeed * 4, 2);
            if(upLean > maxVerticleAngle)
            {
                upLean = maxVerticleAngle;
            }
        }

        transform.eulerAngles = new Vector3(downLean - upLean, transform.eulerAngles.y, 0);

        // Slow
        Vector3 vel = _rb.velocity;
        _rb.velocity = vel * brakeSpeed;

        float crouchDistance = playerHeight - pos.y;
        if (_rb.velocity.magnitude < maxSpeed && crouchDistance > 0)
        {
            _rb.AddForce(transform.forward * crouchDistance * acceleration, ForceMode.Impulse);
        }
    }
}
