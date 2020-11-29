using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }
    GameObject player;

    void Update()
    {
        if (!player)
            return;
        float fAngle = player.transform.position.x;
        //transform.Rotate(new Vector3(0,0.4f,0),Space.World);
        //ransform.RotateAround(new Vector3(1.0f,0.0f,0),new Vector3)
        var rot = Quaternion.Euler(-90.0f*Mathf.Deg2Rad,fAngle*Mathf.Deg2Rad,0.0f);
        //transform.rotation;
        
        //rot.x = -90.0f;
        //rot.y = fAngle;
        //var rot = Quaternion.AngleAxis(fAngle, Vector3.up)*Quaternion.AngleAxis(90.0f, Vector3.right);
        transform.SetPositionAndRotation(transform.position,rot);
    }
}
