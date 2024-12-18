using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionDetector : MonoBehaviour
{
    public LayerMask targetLayer; // Capa del jugador
    public float detectionAngle = 180f; // Ángulo de visión
    public float detectionRange = 10f; // Rango de visión
    public Transform player; // Referencia al jugador
    public IAController iaController; // Controlador de la IA
    public string detectorName; // Nombre del detector

    public bool IsPlayerDetected;

    private void Start()
    {
        iaController = GetComponentInParent<IAController>();
    }

    private void Update()
    {
        //DetectPlayer();
    }

    /*private void DetectPlayer()
    {
        // Verificar si el jugador está dentro del rango
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Verificar si el jugador está dentro del ángulo de visión
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            if (angle <= detectionAngle / 2f)
            {
                // Detectar si hay línea de visión directa
                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, ~targetLayer))
                {
                    Debug.Log("Player detected in vision!");
                    IsPlayerDetected = true;
                    //iaController.WorldState.SetState("PlayerDetected", true);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Dibujar el cono de visión
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & targetLayer) != 0)
        {
            //iaController.WorldState.SetState("PlayerDetected", true);
            IsPlayerDetected = true;
            //iaController.NotifyPlayerDetected(true);
            WorldStateManager.instance.SetState("PlayerDetected", true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & targetLayer) != 0)
        {
            IsPlayerDetected = false;
            WorldStateManager.instance.SetState("PlayerDetected", false);
            //iaController.NotifyPlayerDetected(false);

            //iaController.WorldState.SetState("PlayerDetected", false);
        }
    }
}
