using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int MaxLife = 100; // Vida máxima del jugador
    private int currentLife;

    public delegate void OnLifeChanged(int currentLife, int maxLife);
    public event OnLifeChanged LifeChanged;

    public delegate void OnPlayerDeath();
    public event OnPlayerDeath PlayerDied;

    private WorldState worldState;

    private void Start()
    {
        currentLife = MaxLife; // Inicializar vida al máximo
        worldState = FindObjectOfType<WorldState>();

        if (worldState != null)
        {
            // Configurar la vida inicial en el WorldState
            worldState.SetState("PlayerLife", currentLife);
        }

        // Notificar la vida inicial
        LifeChanged?.Invoke(currentLife, MaxLife);
    }

    public void TakeDamage(int damage)
    {
        currentLife -= damage;
        currentLife = Mathf.Max(0, currentLife); // Evitar valores negativos

        var playerLife = (int)worldState.GetState("PlayerLife");
        worldState.SetState("PlayerLife", playerLife - damage);

        Debug.Log($"Player took {damage} damage. Current life: {currentLife}");

        // Actualizar el estado del mundo
        if (worldState != null)
        {
            worldState.SetState("PlayerLife", currentLife);
        }

        // Notificar cambio de vida
        LifeChanged?.Invoke(currentLife, MaxLife);

        // Verificar muerte del jugador
        if (currentLife <= 0)
        {
            Debug.Log("Player is dead!");
            PlayerDied?.Invoke();
        }
    }

    public void Heal(int healAmount)
    {
        currentLife += healAmount;
        currentLife = Mathf.Min(currentLife, MaxLife); // Evitar superar la vida máxima

        Debug.Log($"Player healed {healAmount}. Current life: {currentLife}");

        // Actualizar el estado del mundo
        if (worldState != null)
        {
            worldState.SetState("PlayerLife", currentLife);
        }

        // Notificar cambio de vida
        LifeChanged?.Invoke(currentLife, MaxLife);
    }

    public int GetCurrentLife()
    {
        return currentLife;
    }
}

