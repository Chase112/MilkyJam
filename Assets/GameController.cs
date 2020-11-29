using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] turnOn_onDetected;
    public GameObject[] turnOff_onDetected;
    public GameObject[] turnOn_Delayed_onDetected;

    Coroutine coroutine;

    public void LightHit()
    {
        foreach (var it in turnOn_onDetected)
            it.SetActive(true);

        foreach (var it in turnOff_onDetected)
            it.SetActive(false);

        if(coroutine == null)
            coroutine = StartCoroutine(Delay(4.0f));
    }

    IEnumerator Delay(float time)
    {
        yield return new WaitForEndOfFrame();
        
        foreach (var it in turnOn_Delayed_onDetected)
            it.SetActive(true);

        coroutine = null;
    }
}
