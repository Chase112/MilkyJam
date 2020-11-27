using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GroundDetector : MonoBehaviour
{
    public List<Collider> ignoreColliders;
    public Vector3 halfExtends;
    public Vector3 centerOffset;

    public bool grounded { get; private set; }

    bool IsHittingGround()
    {
        var hits = Physics.BoxCastAll(transform.position + centerOffset, halfExtends, transform.forward);
        foreach (var it in hits)
            if (!ignoreColliders.Contains(it.collider))
            {
                return true;
            }
        return false;
    }

    private void FixedUpdate()
    {
        grounded = IsHittingGround();
    }
}
