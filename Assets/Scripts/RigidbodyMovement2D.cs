using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputHolder))]
public class RigidbodyMovement2D : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    [Range(0.0f, 1.0f), Tooltip("learp factor used to rotate body towards given direction")]
    public float rotationSpeed = 0.3f;
    [Space]
    [SerializeField] bool moveToDirection = true;
    [SerializeField] bool rotateToDirection = true;

    [System.NonSerialized] public bool atExternalRotation;

    Rigidbody2D _body;
    InputHolder _inputHolder;

    public float desiredRotation;
    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _inputHolder = GetComponent<InputHolder>();

        desiredRotation = _body.rotation;
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

        float currentRotation = _body.rotation;
        _body.rotation = Mathf.LerpAngle(currentRotation, desiredRotation, rotationSpeed);
    }
    public void ApplyExternalRotationSide(Vector2 externalRotation, float maxDifference)
    {
        atExternalRotation = true;
        desiredRotation = -Vector2.SignedAngle(externalRotation, Vector2.up);

        float currentRotation = _body.rotation;
        float difference = Mathf.Clamp(desiredRotation - currentRotation, -maxDifference, maxDifference);
        _body.rotation = currentRotation + difference; //Mathf.Lerp(currentRotation, desiredRotation, rotationSpeed);
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

        float currentRotation = _body.rotation;
        _body.rotation = Mathf.LerpAngle(currentRotation, desiredRotation, rotationSpeed);
    }
    void UpdatePosition()
    {
        if (!moveToDirection || !_inputHolder.atMove)
            return;

        float speed = movementSpeed * _body.mass;
        _body.AddForce(_inputHolder.positionInput.normalized * speed );
    }
}
