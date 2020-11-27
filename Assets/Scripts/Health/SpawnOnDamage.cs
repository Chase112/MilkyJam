using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class SpawnOnDamage : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public float particleFileTime;
    public float damageThreshold;
    public Vector3 spawnPointOffset = Vector3.forward;

    float damageAcumulator;

    void Start()
    {
        var healthController = GetComponent<HealthController>();
        healthController.onDamageCallback += (DamageData data) =>
        {
            damageAcumulator += data.damage;

            while(damageAcumulator > damageThreshold)
            {
                damageAcumulator -= damageThreshold;
                Spawn();
            }
        };
        healthController.onDeathCallback += (DamageData data) =>
        {
            damageAcumulator += data.damage*1.5f;

            while (damageAcumulator > damageThreshold)
            {
                damageAcumulator -= damageThreshold;
                Spawn();
            }
        };
    }

    void Spawn()
    {
        int id = Random.Range(0, objectsToSpawn.Length);
        var obj = Instantiate(objectsToSpawn[id], transform.position + spawnPointOffset, Quaternion.Euler(0, 0, Random.value * 360));
        Destroy(obj, particleFileTime);
    }
}
