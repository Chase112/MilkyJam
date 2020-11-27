using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthController : MonoBehaviour, IDamageable
{
    [Header("Health")]
    public float currentHealth = 100;
    public float maxHealth = 100;

    public bool destroyOnDeath = true;

    public Action<DamageData> onDamageCallback;
    public Action<DamageData> onDeathCallback;
    public Action<DamageData> onStaggerCallback;

    [Header("Stagger")]
    public Timer tResetStagger;
    public float staggerLimit;
    public float staggerLevel;

    bool _destroyed = false;

    public void Resurrect()
    {
        _destroyed = false;
        currentHealth = maxHealth;
    }

    public void DealDamage(DamageData data)
    {
        currentHealth -= data.damage;
        staggerLevel += data.staggerIncrease;


        if (data.staggerIncrease > 0)
        {
            tResetStagger.Restart();
        }

        if(staggerLevel >= staggerLimit)
        {
            staggerLevel -= staggerLimit;
            onStaggerCallback(data);
        }

        if (currentHealth > 0)
            onDamageCallback(data);
        else if (!_destroyed)
        {
            onDeathCallback(data);
            _destroyed = true;
        }
    }

    void Start()
    {
        onStaggerCallback += (DamageData data) => { };
        onDamageCallback += (DamageData data) => { };
        onDeathCallback += (DamageData data) => {
            if(destroyOnDeath)
                Destroy(gameObject);
        };
    }

    void Update()
    {
        if(tResetStagger.IsReadyRestart())
        {
            staggerLevel = Mathf.Clamp(staggerLevel - 1, 0, int.MaxValue);
        }
    }
}
