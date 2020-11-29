using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LooseScreanController : MonoBehaviour
{
    public void OnRetryClicked()
    {
        SceneManager.LoadScene(0);
    }
}
