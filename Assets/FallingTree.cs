using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractible {
    void PerformInteraction();
}

public class FallingTree : MonoBehaviour, IInteractible
{

    bool bHasFallen = false;
    public void PerformInteraction()
    {
        StartCoroutine("FallTree");
    }
    // Start is called before the first frame update

    public float fAngle = -49.0f;
    void Start()
    {
        
    }


    IEnumerator FallTree() 
    {
        if (!bHasFallen)
        {
            bHasFallen = true;

            for (float ft = 0.0f; ft >= fAngle; ft -= 0.2f) 
            {
                transform.Rotate(new Vector3(0,0,-0.2f),Space.World);
                yield return new WaitForSeconds(.0025f);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.F)) {
            Debug.Log("fall");
            StartCoroutine("FallTree");
        }*/
    }
}
