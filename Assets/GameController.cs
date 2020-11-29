using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] turnOn_onDetected;
    public GameObject[] turnOff_onDetected;

    public void LightHit()
    {
        Debug.Log("PlayerHit");

        foreach (var it in turnOn_onDetected)
            it.SetActive(true);

        foreach (var it in turnOff_onDetected)
            it.SetActive(false);
    }
}
