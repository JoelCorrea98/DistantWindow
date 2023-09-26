using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyUp : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 11)
        {
            Destroy(this.gameObject);
        }
    }
}
