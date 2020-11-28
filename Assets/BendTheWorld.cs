using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BendTheWorld : MonoBehaviour
{
    const int n = 24;
    ArrayList objs = new ArrayList();
    ArrayList twisted = new ArrayList();
     
    // Start is called before the first frame update

    void CloneAll()
    {
        float xPlayer = GameObject.Find("Player").transform.position.x;
        int nCenterID = (int)xPlayer;

        twisted.Clear();

        var orginals = GameObject.Find("Orginal");
        int nOrginalsChilds = orginals.transform.childCount;

        int startID = nCenterID - n/2;
        int endID   = nCenterID + n/2;
        
        if (startID<0) startID=0;
        if (endID>nOrginalsChilds-1) endID=nOrginalsChilds-1;
        
        for (int i=startID;i<endID;i++)
        {
            string name = "ground00 (" + i.ToString() + ")";
            Transform tr = orginals.transform.Find(name);
            objs.Add(tr);

            objs.Add(orginals.transform.Find("Player"));


            Vector3 posG = tr.position;
            posG.z = 5.0f;

            Transform newTransf = Instantiate(tr,posG,tr.rotation,GameObject.Find("Twisted").transform);
            newTransf.GetComponent<Rigidbody>().isKinematic = true;
            
            twisted.Add(newTransf);
        }

        Transform t = orginals.transform.Find("Player");
        
        Vector3 pos = t.position;
        pos.z =5.0f;
        Transform newPlayer = Instantiate(t,pos,t.rotation,GameObject.Find("Twisted").transform);
        newPlayer.GetComponent<Rigidbody>().isKinematic = true;

        Destroy(newPlayer.GetComponent<RigidbodyMovement>());
        Destroy(newPlayer.GetComponent<JumpMovement>());
        Destroy(newPlayer.GetComponent<GroundDetector>());
        Destroy(newPlayer.GetComponent<InputHolder>());
        Destroy(newPlayer.GetComponent<Rigidbody>());
        Destroy(newPlayer.Find("InputControler").gameObject);

        twisted.Add(newPlayer);

    }

    public void Start()
    {
    }

     Vector3 RandomCircle(Vector3 center,float radius,float ang)  {
        // create random angle between 0 to 360 degrees
        
        Vector3 pos = new Vector3();
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    public void Twist(Transform g,float xPlayer,int i)
    {
            
            Vector3 pos = g.position;
            Quaternion rot = g.rotation;
                        
            Vector3 center = new Vector3(0,0,5.0f);
            float xPlayerDel = xPlayer - (int)xPlayer;
            Vector3 circ = RandomCircle(center,n/(2.0f*Mathf.PI),((i+xPlayerDel)*360.0f)/(float)n);
            rot = Quaternion.LookRotation(new Vector3(0,0,1.0f),center-circ);
            
            pos = circ;

            Transform t =  (Transform)twisted[i];
            t.SetPositionAndRotation(pos,rot);
    }

    // Update is called once per frame
    public void Update()
    {
        float xPlayer = GameObject.Find("Player").transform.position.x;
        
        for (int i=0;i<twisted.Count;i++)
        {
            Debug.Assert(twisted[i] is Transform);
            Destroy(((Transform)twisted[i]).gameObject);
        }

        CloneAll();

        if (twisted.Count>0)
        {
            for (int i=0;i<twisted.Count-1;i++)
            {
                Twist((Transform) objs[i],xPlayer,i);
            }

            Twist((Transform) objs[twisted.Count-1],xPlayer,0);
        }

    }
}
