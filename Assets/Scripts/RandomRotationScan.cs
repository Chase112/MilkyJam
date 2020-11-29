using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotationScan : MonoBehaviour, IRigidbodyDepended
{
    public float[] desiredRotation;
    public float maxDelta;


    float lastValue;
    int currentId;
    int side = 1;

    float currentTarget => desiredRotation[currentId];

    private void Start()
    {
        lastValue = currentTarget;
        Vector3 angles = transform.eulerAngles;
        angles.y = lastValue;
        transform.eulerAngles = angles;
    }

    private void Update()
    {
        lastValue = Mathf.MoveTowards(lastValue, currentTarget, maxDelta * Time.deltaTime);
        Vector3 angles = transform.eulerAngles;
        angles.y = lastValue;
        transform.eulerAngles = angles;

        if(Mathf.Approximately(lastValue, currentTarget))
        {
            currentId += side;
            if(currentId < 0 || currentId >= desiredRotation.Length)
            {
                side = side == 1 ? -1 : 1;
                currentId = Mathf.Clamp(currentId, 0, desiredRotation.Length-1);
            }
        }
    }

}
