using System.Collections;
using UnityEngine;

public class EnergyDrain : MonoBehaviour
{
    [Header("Configuración")]
    public float initialEnergyLoss = 10f; // Energía que pierde al entrar
    public float energyLossPerSecond = 5f; // Energía que pierde por segundo
    public float movementSpeedReduction = 0.5f; // Porcentaje de reducción de velocidad (0.5 = 50%)
    public LayerMask targetLayer;
    private Coroutine energyDrainCoroutine; // Corutina activa de drenaje de energía
    public PlayerEnergy playerEnergy; // Referencia al componente de energía del jugador
    public PlayerController playerMovement; // Referencia al componente de movimiento del jugador
    private float originalSpeed; // Velocidad original del jugador
    private bool isPlayerInside = false; // Bandera para verificar si el jugador está dentro del collider

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra es el jugador
        if ((1 << other.gameObject.layer & targetLayer) != 0)
        {
            // Obtener referencias a los componentes del jugador
            playerEnergy = other.GetComponent<PlayerEnergy>();
            playerMovement = other.GetComponent<PlayerController>();

            if (playerEnergy != null)
            {
                // Reducir la energía inicial
                playerEnergy.subtractAmountOfEnergy(initialEnergyLoss);
            }

            if (playerMovement != null)
            {
                // Guardar la velocidad original y reducir la velocidad
                originalSpeed = playerMovement.straightforce;
                playerMovement.straightforce *= movementSpeedReduction;
            }

            // Iniciar el drenaje de energía por segundo
            if (!isPlayerInside) // Asegurarse de que no se inicie varias veces
            {
                isPlayerInside = true; // Marcar que el jugador está dentro
                energyDrainCoroutine = StartCoroutine(DrainEnergyOverTime());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto que sale es el jugador
        if ((1 << other.gameObject.layer & targetLayer) != 0)
        {
            // Restaurar la velocidad original del jugador
            if (playerMovement != null)
            {
                playerMovement.straightforce = originalSpeed;
            }

            // Detener la corutina de drenaje de energía
            if (isPlayerInside) // Verificar si estaba dentro antes de salir
            {
                isPlayerInside = false; // Marcar que el jugador está fuera
                if (energyDrainCoroutine != null)
                {
                    StopCoroutine(energyDrainCoroutine);
                    energyDrainCoroutine = null;
                }
            }
        }
    }

    private IEnumerator DrainEnergyOverTime()
    {
        while (isPlayerInside) // Controlar el drenaje mientras el jugador esté dentro
        {
            yield return new WaitForSeconds(1f); // Esperar un segundo

            if (playerEnergy != null && isPlayerInside) // Drenar energía si el jugador sigue dentro
            {
                playerEnergy.subtractAmountOfEnergy(energyLossPerSecond);
            }
        }
    }
}


