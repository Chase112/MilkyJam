using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float fAngle = GameObject.Find("Player").transform.position.x;
        var rot = Quaternion.Euler(-90.0f,fAngle,0.0f);
        transform.SetPositionAndRotation(transform.position,rot);
    }
}
