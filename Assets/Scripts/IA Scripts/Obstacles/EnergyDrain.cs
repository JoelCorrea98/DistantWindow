using System.Collections;
using UnityEngine;

public class EnergyDrain : MonoBehaviour
{
    [Header("Configuraci�n")]
    public float initialEnergyLoss = 10f; // Energ�a que pierde al entrar
    public float energyLossPerSecond = 5f; // Energ�a que pierde por segundo
    public float movementSpeedReduction = 0.5f; // Porcentaje de reducci�n de velocidad (0.5 = 50%)
    public LayerMask targetLayer;
    private Coroutine energyDrainCoroutine; // Corutina activa de drenaje de energ�a
    public PlayerEnergy playerEnergy; // Referencia al componente de energ�a del jugador
    public PlayerController playerMovement; // Referencia al componente de movimiento del jugador
    private float originalSpeed; // Velocidad original del jugador
    private bool isPlayerInside = false; // Bandera para verificar si el jugador est� dentro del collider

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
                // Reducir la energ�a inicial
                playerEnergy.subtractAmountOfEnergy(initialEnergyLoss);
            }

            if (playerMovement != null)
            {
                // Guardar la velocidad original y reducir la velocidad
                originalSpeed = playerMovement.straightforce;
                playerMovement.straightforce *= movementSpeedReduction;
            }

            // Iniciar el drenaje de energ�a por segundo
            if (!isPlayerInside) // Asegurarse de que no se inicie varias veces
            {
                isPlayerInside = true; // Marcar que el jugador est� dentro
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

            // Detener la corutina de drenaje de energ�a
            if (isPlayerInside) // Verificar si estaba dentro antes de salir
            {
                isPlayerInside = false; // Marcar que el jugador est� fuera
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
        while (isPlayerInside) // Controlar el drenaje mientras el jugador est� dentro
        {
            yield return new WaitForSeconds(1f); // Esperar un segundo

            if (playerEnergy != null && isPlayerInside) // Drenar energ�a si el jugador sigue dentro
            {
                playerEnergy.subtractAmountOfEnergy(energyLossPerSecond);
            }
        }
    }
}


