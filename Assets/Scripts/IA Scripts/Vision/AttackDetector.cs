using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetector : MonoBehaviour
{
    public LayerMask targetLayer; // Capa del jugador
    IAController controlller;
    void Start()
    {
        controlller = GetComponentInParent<IAController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == targetLayer)
        {
            controlller.PlayerInAttackRange(true);
        }   
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == targetLayer)
        {
            controlller.PlayerInAttackRange(false);
        }
    }
}
