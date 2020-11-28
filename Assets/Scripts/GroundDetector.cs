using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GroundDetector : MonoBehaviour, IRigidbodyDepended
{
    public List<Collider> ignoreColliders;
    public Vector3 halfExtends;
    public Vector3 centerOffset;

    public bool grounded;// { get; private set; }
    bool _b;

    bool IsHittingGround()
    {
        var hits = Physics.BoxCastAll(transform.position + centerOffset, halfExtends, transform.forward);
        foreach (var it in hits)
            if (!ignoreColliders.Contains(it.collider))
            {
                Debug.Log(it.collider.name);
                return true;
            }
        Debug.Log("No collision");
        return false;
    }

    private void FixedUpdate()
    {
        grounded = _b;// IsHittingGround();
        _b = false;
    }


    private void OnTriggerStay(Collider other)
    {
        if (!ignoreColliders.Contains(other))
            _b = true;
    }
}
