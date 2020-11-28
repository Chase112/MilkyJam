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
            if (pos.x<10.57f) pos.x = 10.57f;
            transform.SetPositionAndRotation(pos,transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        if (GetComponent<Rigidbody>().velocity.y<0.01f)
        {
            //transform.Find("Capsule").
            GetComponent<Rigidbody>().velocity = new Vector3(0.0f,300.0f*Time.deltaTime,0.0f);
           // Debug.Log("jump");
        }

        //RenderSettings.skybox.SetFloat("_Rotation", 16.0f*Time.fixedTime);
        
    }

     void FixedUpdate()
     {
     }
}
