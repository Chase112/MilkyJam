using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* concrete way of capturing input
 * Should be the only class that modifies InuputHolder records
     */
public abstract class InputController : MonoBehaviour
{
    [HideInInspector]
    public InputHolder inputHolder;
    protected void Start()
    {
        inputHolder = GetComponentInParent<InputHolder>();
    }
}
