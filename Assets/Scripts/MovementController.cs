using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    /*static*/ public float speedScale = 4;
    /*static*/ public float maxVelocityChange = 4.0f;
    /*static*/ public float tiltValue = 5.0f;

    private Vector3 _velocity = Vector3.zero;

    public Joystick joystick;
    
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // MARK: - Joystick inputs
        float xMovementInput = joystick.Horizontal;
        float zMovementInput = joystick.Vertical;

        // MARK: - Calculate Velocity Vectors
        Vector3 v_x = transform.right * xMovementInput;
        Vector3 v_z = transform.forward * zMovementInput;

        Vector3 v = (v_x + v_z).normalized * speedScale;


        // MARK: - Update Velocity
        _velocity = v;

        transform.rotation = Quaternion.Euler(joystick.Vertical * tiltValue * v.magnitude, 0, -joystick.Horizontal * tiltValue * v.magnitude);
    }


    private void fixedUpdate() {
        if (_velocity != Vector3.zero)
        {

            Vector3 dv = _velocity - rb.velocity;
            // Limit Velocity Change
            dv.x = Mathf.Clamp(dv.x, -maxVelocityChange, maxVelocityChange);
            dv.z = Mathf.Clamp(dv.z, -maxVelocityChange, maxVelocityChange);
            dv.y = 0f;

            rb.AddForce(dv, ForceMode.Acceleration);

        }
    }
}
