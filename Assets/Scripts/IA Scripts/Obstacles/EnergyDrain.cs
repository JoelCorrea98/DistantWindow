using System.Collections;
using System.Collections.Generic;
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
            energyDrainCoroutine = StartCoroutine(DrainEnergyOverTime());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto que sale es el jugador
        if (other.CompareTag("Player"))
        {
            // Restaurar la velocidad original del jugador
            if (playerMovement != null)
            {
                playerMovement.straightforce = originalSpeed;
            }

            // Detener la corutina de drenaje de energía
            if (energyDrainCoroutine != null)
            {
                StopCoroutine(energyDrainCoroutine);
                energyDrainCoroutine = null;
            }
        }
    }

    private IEnumerator DrainEnergyOverTime()
    {
        while (true)
        {
            // Esperar un segundo
            yield return new WaitForSeconds(1f);

            // Reducir energía del jugador si el componente está presente
            if (playerEnergy != null)
            {
                playerEnergy.subtractAmountOfEnergy(energyLossPerSecond);
            }
        }
    }
}
