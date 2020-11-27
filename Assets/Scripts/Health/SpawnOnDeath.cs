using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDeath : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public Vector3 spawnPointOffset = Vector3.forward;

    void Start()
    {
        var healthController = GetComponentInParent<HealthController>();
        Debug.Assert(healthController);

        healthController.onDeathCallback += (DamageData data) =>
        {
            Spawn();
        };
    }

    void Spawn()
    {
        int id = Random.Range(0, objectsToSpawn.Length);
        var obj = Instantiate(objectsToSpawn[id], transform.position + spawnPointOffset, Quaternion.Euler(0, 0, Random.value * 360));
    }
}
