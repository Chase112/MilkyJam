using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            Vector3 pos = transform.position;
            pos.x+=5.0f*Time.deltaTime;
            transform.SetPositionAndRotation(pos,transform.rotation);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            Vector3 pos = transform.position;
            pos.x-=5.0f*Time.deltaTime;
            transform.SetPositionAndRotation(pos,transform.rotation);
        }

    }
}
