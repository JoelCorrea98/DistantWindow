using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObs : MonoBehaviour
{
    public string obsName;
    public bool ignoreLayer;
    public int layer = -1;
    public bool Obstruction;
    public GameObject Object;
    private Collider colnow;
    void OnTriggerStay(Collider col)
    {
        if (!Obstruction)
        {
            if (ignoreLayer || layer == col.gameObject.layer)
            {
                Obstruction = true;
                Object = col.gameObject;
                colnow = col;
            }
        }
    }

    private void Update()
    {
        
        if(Object == null || !colnow.enabled)
        {
            Obstruction = false;
        }
        if (Object != null)
        {
            if (!Object.activeInHierarchy)
            {
                Obstruction = false;
            }
        }
    }







    void OnTriggerExit(Collider col)
    {
        if (col == colnow)
        {
            Obstruction = false;
        }

    }

}
