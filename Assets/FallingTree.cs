using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractible {
    void PerformInteraction();
    void EndInteraction();
}

public class FallingTree : MonoBehaviour, IInteractible
{
    public AudioSource audioSource;

    bool bHasFallen = false;
    public void PerformInteraction()
    {
        StartCoroutine("FallTree");
    }

    public void EndInteraction()
    {        
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
            audioSource.Play();

            for (float ft = 0.0f; ft >= fAngle; ft -= 0.4f) 
            {
                transform.Rotate(new Vector3(0,0,-0.4f),Space.World);
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
