using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainRotation : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.active)
        {
            float fAngle = player.transform.position.x;        
            var rot = Quaternion.Euler(-90.0f,fAngle,0.0f);
            transform.SetPositionAndRotation(transform.position,rot);
        }
    }
}
