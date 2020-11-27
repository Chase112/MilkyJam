using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputHolder))]
public class RigidbodyMovement : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float flyingMovementSpeed = 1.0f;
    [Range(0.0f, 1.0f)]
    public float xyDamping = 1.0f;

    [Range(0.0f, 1.0f), Tooltip("learp factor used to rotate body towards given direction")]
    public float rotationSpeed = 0.3f;
    [Space]
    [SerializeField] bool moveToDirection = true;
    [SerializeField] bool rotateToDirection = true;

    [System.NonSerialized] public bool atExternalRotation;

    Rigidbody _body;
    InputHolder _inputHolder;

    GroundDetector _groundDetector;

    public float desiredRotation;
    private void Start()
    {
        _body = GetComponent<Rigidbody>();
        _inputHolder = GetComponent<InputHolder>();
        _groundDetector = GetComponent<GroundDetector>();

        desiredRotation = _body.rotation.eulerAngles.y;
    }

    void FixedUpdate()
    {
        UpdateRotation();
        UpdatePosition();
        atExternalRotation = false;
    }

    public void ApplyExternalRotation(Vector2 externalRotation, float rotationSpeed)
    {
        atExternalRotation = true;
        desiredRotation = -Vector2.SignedAngle(externalRotation, Vector2.up);

        float currentRotation = _body.rotation.eulerAngles.y;
        float newRotation = Mathf.LerpAngle(currentRotation, desiredRotation, rotationSpeed);
        _body.rotation = Quaternion.Euler(0, -newRotation, 0);
    }
    public void ApplyExternalRotationSide(Vector2 externalRotation, float maxDifference)
    {
        atExternalRotation = true;
        desiredRotation = -Vector2.SignedAngle(externalRotation, Vector2.up);

        float currentRotation = _body.rotation.eulerAngles.y;
        float difference = Mathf.Clamp(desiredRotation - currentRotation, -maxDifference, maxDifference);
        float newRotation = currentRotation + difference;
        _body.rotation = Quaternion.Euler(0, -newRotation, 0);
    }


    void UpdateRotation()
    {
        if (atExternalRotation || !rotateToDirection ) //|| (_groundDetector && !_groundDetector.grounded))
            return;

        else if (_inputHolder.atRotation)
            desiredRotation = -Vector2.SignedAngle(_inputHolder.rotationInput, Vector2.up);
        else if (_inputHolder.atMove)
            desiredRotation = -Vector2.SignedAngle(_inputHolder.positionInput, Vector2.up);
        // else;

        float currentRotation = -_body.rotation.eulerAngles.y;
        float newRotation = Mathf.LerpAngle(currentRotation, desiredRotation, rotationSpeed);
        _body.rotation = Quaternion.Euler(0,  -newRotation, 0);
    }
    void UpdatePosition()
    {
        if (!moveToDirection || !_inputHolder.atMove)
            return;


        bool grounded = !_groundDetector || _groundDetector.grounded;
        float speed = grounded ? movementSpeed : flyingMovementSpeed;
        speed *= _body.mass;

        Vector2 force = _inputHolder.positionInput.normalized * speed;
        _body.AddForce(force.To3D());


        // additional damping
        Vector3 velocity = _body.velocity;
        velocity.x *= xyDamping;
        velocity.z *= xyDamping;
        _body.velocity = velocity;

    }
}
