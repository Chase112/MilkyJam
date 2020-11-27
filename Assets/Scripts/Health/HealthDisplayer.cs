using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplayer : MonoBehaviour
{
    [Range(0, 1)] public float colorLerpFactor = 0.2f;
    [Range(0, 1)] public float externalVisibility = 1.0f;
    HealthController health;

    SpriteRenderer[] sprites;
    float[] alphas;

    Vector3 initialScale;

    // Start is called before the first frame update
    void Awake()
    {
        health = GetComponentInParent<HealthController>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        alphas = new float[sprites.Length];


        for (int i = 0; i < sprites.Length; ++i)
        {
            alphas[i] = sprites[i].color.a;
        }

        initialScale = transform.localScale;

        health.onDamageCallback += (DamageData data) =>
        {
            float healthPercent = health.currentHealth / health.maxHealth;
            //healthPercent = Mathf.Sqrt(healthPercent);
            //healthPercent *= healthPercent;
            //healthPercent *= healthPercent;

            float minScale = 0.2f;
            healthPercent = healthPercent * (1 - minScale) + minScale;

            transform.localScale = initialScale * healthPercent;

            
        };
    }
    private void Update()
    {
        for (int i = 0; i < sprites.Length; ++i)
        {
            Color cl = sprites[i].color;
            cl.a = Mathf.LerpAngle(cl.a, alphas[i] * externalVisibility, colorLerpFactor);
            sprites[i].color = cl;
        }
    }
}
