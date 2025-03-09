using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerableZone : MonoBehaviour
{
    public LayerMask targetLayer; // Capa del jugador
    public string detectorName; // Nombre del detector
    public IAController iaController; // Referencia al controlador de la IA

    public bool IsPlayerVulnerable;

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & targetLayer) != 0)
        {
            //Debug.Log($"Player entered {detectorName} detector!");
            //iaController.WorldState.SetState("PlayerDetected", true);
            IsPlayerVulnerable = true;
            WorldStateManager.instance.SetState("PlayerVulnerable", true);
            iaController.NotifyPlayerVulnerableZone();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & targetLayer) != 0)
        {
            IsPlayerVulnerable = false;
            WorldStateManager.instance.SetState("PlayerVulnerable", false);
        }
    }
}
