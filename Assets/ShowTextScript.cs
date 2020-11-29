using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowTextScript : MonoBehaviour, IInteractible
{

    public string line1;
    public string line2;
    bool bStartHidden = false;

    
    private GameObject canvas;
    //bool bHasFallen = false;
    public void PerformInteraction()
    {       
        canvas.SetActive(true);
        GameObject.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = line1 + "\n" + line2;
        
        //StartCoroutine("FallTree");
    }    

    public void EndInteraction()
    {
        canvas.SetActive(false);
    }

    void Awake()
    {
        canvas = GameObject.Find("CanvasText");
    }
    // Start is called before the first frame update
    void Start()
    {
       // canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!bStartHidden)
            {
                if (GameObject.Find("ImageStart")!=null)
                  GameObject.Find("ImageStart").SetActive(false);
                  
                bStartHidden = true;
                canvas.SetActive(false);
            }
        }
    }
}
