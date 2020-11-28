using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        IInteractible[] inter = other.GetComponentsInChildren<IInteractible>();

        if (inter.Length==0 && other.transform.parent!=null)
        {
            inter = other.transform.parent.GetComponentsInParent<IInteractible>();
        }

        for (int i=0;i<inter.Length;i++)
            inter[i].PerformInteraction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
