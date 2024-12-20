using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetector : MonoBehaviour
{
    IAController controlller;
    void Start()
    {
        controlller = GetComponentInParent<IAController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 11)
        {
            controlller.PlayerInAttackRange(true);
        }   
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            controlller.PlayerInAttackRange(false);
        }
    }
}
