using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHolder))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GroundDetector))]
public class JumpMovement : MonoBehaviour
{
    public float jumpForce;
    public float forwardForce;

    InputHolder _inputHolder;
    Rigidbody _body;
    GroundDetector _groundDetector;

    bool grounded => _groundDetector.grounded;

    void Start()
    {
        _inputHolder = GetComponent<InputHolder>();
        _body = GetComponent<Rigidbody>();
        _groundDetector = GetComponent<GroundDetector>();
    }


    bool _jumpQueued = false;
    private void Update()
    {
        _jumpQueued |= _inputHolder.keysPressed[0];
    }



    void FixedUpdate()
    {
        if(grounded && _jumpQueued)
        {
            _body.AddForce(Vector3.up * jumpForce + transform.forward * forwardForce);
            _jumpQueued = false;
        }
    }
}
