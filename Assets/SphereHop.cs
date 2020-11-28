using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereHop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (transform.Find("Sphere")!=null)
            {
                transform.Find("Sphere").GetComponent<Rigidbody>().AddForce(new Vector3(0.0f,2.0f,0.0f),ForceMode.Impulse);
            }
        }
        
    }
}
