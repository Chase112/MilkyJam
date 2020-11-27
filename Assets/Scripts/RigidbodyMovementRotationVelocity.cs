using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputHolder))]
public class RigidbodyMovementRotationVelocity : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    [Range(0.0f, 1.0f)] 
    public float xyDamping = 1.0f;
    
    public float rotationSpeedMax = 30.0f;
    [Range(0.0f, 1.0f)]
    public float rotationScale = 0.3f;
    [Range(0, 1)]
    public float rotationDamping;
    
    [Space]
    [SerializeField] bool moveToDirection = true;
    [SerializeField] bool rotateToDirection = true;

    [System.NonSerialized] public bool atExternalRotation;

    Rigidbody _body;
    InputHolder _inputHolder;

    float _rotationVelocity;
    void UpdateRotationVelocity()
    {
        _body.rotation = Quaternion.Euler(0, _body.rotation.eulerAngles.y + _rotationVelocity, 0);
        _rotationVelocity *= rotationDamping;
    }

    public float desiredRotation;
    private void Start()
    {
        _body = GetComponent<Rigidbody>();
        _inputHolder = GetComponent<InputHolder>();

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

    void UpdateRotation()
    {
        if (atExternalRotation || !rotateToDirection)
            return;

        else if (_inputHolder.atRotation)
            desiredRotation = -Vector2.SignedAngle(_inputHolder.rotationInput, Vector2.up);
        else if (_inputHolder.atMove)
            desiredRotation = -Vector2.SignedAngle(_inputHolder.positionInput, Vector2.up);
        // else;

        float currentRotation = -_body.rotation.eulerAngles.y;
        float rotationDif = Mathf.DeltaAngle( desiredRotation, currentRotation);

        _rotationVelocity += Mathf.Clamp(rotationDif * rotationScale, -rotationSpeedMax, rotationSpeedMax);
        UpdateRotationVelocity();
    }
    void UpdatePosition()
    {
        if (!moveToDirection || !_inputHolder.atMove)
            return;


        float speed = movementSpeed * _body.mass;
        Vector2 force = _inputHolder.positionInput.normalized * speed;
        _body.AddForce(force.To3D());

        _body.velocity *= xyDamping;
    }


}