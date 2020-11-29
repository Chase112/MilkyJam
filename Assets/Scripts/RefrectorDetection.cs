using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

interface IDetectionMarker
{
}

public class RefrectorDetection : MonoBehaviour, IRigidbodyDepended
{
    public UnityEvent onDetect;
    public Transform objectToPlaceOnDetection;
    public float testRadius;

    public void TestGround()
    {

        Debug.DrawRay(transform.position, transform.forward * 100, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo))
        {
            objectToPlaceOnDetection.gameObject.SetActive(true);
            objectToPlaceOnDetection.transform.position = hitInfo.point;


            var detected = Physics.OverlapSphere(hitInfo.point, testRadius);
            foreach(var it in detected)
            {
                var detectionMarker = it.GetComponent<IDetectionMarker>();
                if(detectionMarker != null)
                {
                    onDetect.Invoke();
                    break;
                }
            }
        }
        else
        {
            objectToPlaceOnDetection.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        TestGround();
    }
}
