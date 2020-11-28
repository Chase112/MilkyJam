using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BendTheWorld : MonoBehaviour
{
    const int n = 32;
    //List<BendItem> objs = new List<BendItem>();
    List<BendItem> twisted = new List<BendItem>();
    public float fOffset = 180.0f;
     
    // Start is called before the first frame update

    public struct BendItem {
        public Transform transform;
        public int nID;
    }

    void CloneAll()
    {
        float xPlayer = GameObject.Find("Player").transform.position.x;
        int nCenterID = (int)xPlayer;

        twisted.Clear();
        //objs.Clear();

        var orginals = GameObject.Find("Orginal");
        int nOrginalsChilds = orginals.transform.childCount;

        int startID = nCenterID - n/2;
        int endID   = nCenterID + n/2;
        
        //if (startID<0) startID=0;
        //if (endID>nOrginalsChilds-1) endID=nOrginalsChilds-1;
        
        for (int id=startID;id<endID;id++)
        {
            string num = id.ToString();
            if (num.Length<2) num = "0" + num;
            string name = "ground00 (" + num + ")";
            Transform tr = orginals.transform.Find(name);
            if (tr==null) continue;
            
            /*
            BendItem bendObj;
            bendObj.transform = tr;
            bendObj.nID = id;
            objs.Add(bendObj);
            */

            Vector3 posG = tr.position;
            posG.z = 5.0f;

            Transform newTransf = Instantiate(tr,posG,tr.rotation,GameObject.Find("Twisted").transform);
            newTransf.GetComponent<Rigidbody>().isKinematic = true;

            BendItem bendItem;
            bendItem.transform = newTransf;
            bendItem.nID = id;           
            twisted.Add(bendItem);
        }
  /*      
        BendItem bendPla;
        bendPla.transform = orginals.transform.Find("Player").transform;
        bendPla.nID = nCenterID;
        objs.Add(bendPla);
*/
        GameObject others = GameObject.Find("OtherObjects");

        for (int i=0;i<others.transform.childCount;i++)
        {
            Transform child = others.transform.GetChild(i);
            int id = (int)(child.position.x+0.5f);

            if (id<startID || id>=endID) continue;

            Vector3 posG = child.position;
            posG.z = 5.0f;

            Transform newTransf = Instantiate(child,posG,child.rotation,GameObject.Find("Twisted").transform);

            if (newTransf.GetComponent<Rigidbody>()!=null)
            Destroy(newTransf.GetComponent<Rigidbody>());
            //newTransf.GetComponent<Rigidbody>().isKinematic = true;

            BendItem bendItem;
            bendItem.transform = newTransf;
            bendItem.nID = id;
            twisted.Add(bendItem);

        }
        
/*
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

        BendItem bendPlayer;
        bendPlayer.transform = newPlayer;
        bendPlayer.nID = nCenterID;

        twisted.Add(bendPlayer);
*/
    }

    public void Start()
    {
    }

     Vector3 GetCircle(Vector3 center,float radius,float ang)
     {
        while (ang>360.0f) ang-=360.0f;
        Vector3 pos = new Vector3();
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }


    // Update is called once per frame
    public void Update()
    {
        var orginals = GameObject.Find("Orginal");
        float xPlayer = GameObject.Find("Player").transform.position.x;
        //float xPlayer = orginals.Find("Player").transform.position.x;
        int nCenterID = (int)xPlayer;
        
        for (int i=0;i<twisted.Count;i++)
        {
            Debug.Assert(twisted[i].transform is Transform);
            Destroy(twisted[i].transform.gameObject);
        }

        CloneAll();

        //string ss = "";

        if (twisted.Count>0)
        {
            for (int i=0;i<twisted.Count;i++)
            {
                int id = twisted[i].nID;
                Transform g = twisted[i].transform;
//              int id = objs[i].nID;
//              Transform g = objs[i].transform;

                Vector3 center = new Vector3(0,0,-g.position.z);
                float xPlayerDel = xPlayer - (int)xPlayer;
                float fAngleSpan = 360.0f/(float)n;
                float fAngle = fOffset + (id-xPlayer)*fAngleSpan;

                Vector3 circ = GetCircle(center,n/(2.0f*Mathf.PI)-g.position.y,-fAngle);
                Vector3 pos = circ;
                Quaternion rot = Quaternion.LookRotation(new Vector3(0,0,1.0f),center-circ);               
                                
                twisted[i].transform.SetPositionAndRotation(pos,rot);                
            }

            //Twist((BendItem) twisted[twisted.Count-1],xPlayer);
        }
        //Debug.Log(ss);
    }
}
