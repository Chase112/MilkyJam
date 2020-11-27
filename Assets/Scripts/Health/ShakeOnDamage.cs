using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeOnDamage : MonoBehaviour
{

    public float damageBase;
    public float damageRatio;


    public float damageBaseDeath;
    public float damageRatioDeath;

    void Start()
    {
        var health = GetComponentInParent<HealthController>();
        var cam = Camera.main.GetComponent<CameraController>();
        health.onDamageCallback += (DamageData data) =>
        {
            cam.ApplyShake(damageBase + damageRatio * data.damage);
        };

        health.onDeathCallback += (DamageData data) =>
        {
            cam.ApplyShake(damageBaseDeath + damageRatioDeath * data.damage);
        };
    }
}
