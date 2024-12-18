using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDetector : MonoBehaviour
{
    public LayerMask targetLayer; // Capa del jugador
    public string detectorName; // Nombre del detector
    public IAController iaController; // Referencia al controlador de la IA

    public bool IsPlayerDetected;

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & targetLayer) != 0)
        {
            //Debug.Log($"Player entered {detectorName} detector!");
            //iaController.WorldState.SetState("PlayerDetected", true);
            IsPlayerDetected = true;
            WorldStateManager.instance.SetState("PlayerDetected", true);
            //iaController.NotifyPlayerDetected(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & targetLayer) != 0)
        {
            //Debug.Log($"Player left {detectorName} detector!");
            IsPlayerDetected = false;
            WorldStateManager.instance.SetState("PlayerDetected", false);
            //iaController.NotifyPlayerDetected(false);
            //iaController.WorldState.SetState("PlayerDetected", false);
        }
    }
}

