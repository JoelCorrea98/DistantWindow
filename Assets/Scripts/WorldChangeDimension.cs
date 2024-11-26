using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldChangeDimension : MonoBehaviour
{

    public GameObject orangeStructures;
    public GameObject otherStructures;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            ChangeOrangeDimension();
        }
    }

    void ChangeOrangeDimension()
    { 
        orangeStructures.SetActive(true);
        otherStructures.SetActive(false);
    }
}
