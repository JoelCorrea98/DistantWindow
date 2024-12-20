using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public int MaxLife = 10; // Vida máxima del jugador
    public int currentLife;
    public Image lifeBar;
    public Text energyNum;

    public delegate void OnLifeChanged(int currentLife, int maxLife);
    public event OnLifeChanged LifeChanged;

    public delegate void OnPlayerDeath();
    public event OnPlayerDeath PlayerDied;


    private void Start()
    {
        currentLife = MaxLife; // Inicializar vida al máximo

        // Configurar la vida inicial en el WorldState
        WorldStateManager.instance.SetState("PlayerLife", currentLife);


        // Notificar la vida inicial
        LifeChanged?.Invoke(currentLife, MaxLife);
    }

    public void TakeDamage(int damage)
    {
        currentLife -= damage;
        currentLife = Mathf.Max(0, currentLife); // Evitar valores negativos

        
        WorldStateManager.instance.SetState("PlayerLife", currentLife);


        Debug.Log($"Player took {damage} damage. Current life: {currentLife}");

       

        // Notificar cambio de vida
        LifeChanged?.Invoke(currentLife, MaxLife);

        // Verificar muerte del jugador
        if (currentLife <= 0)
        {
            Debug.Log("Player is dead!");
            WorldStateManager.instance.SetState("PlayerAlive", false);
            PlayerDied?.Invoke();
        }
        canvasRefresh();
    }

    public void Heal(int healAmount)
    {
        currentLife += healAmount;
        currentLife = Mathf.Min(currentLife, MaxLife); // Evitar superar la vida máxima

        Debug.Log($"Player healed {healAmount}. Current life: {currentLife}");

        WorldStateManager.instance.SetState("PlayerLife", currentLife);

        // Notificar cambio de vida
        LifeChanged?.Invoke(currentLife, MaxLife);

        canvasRefresh();
    }

    public int GetCurrentLife()
    {
        return currentLife;
    }

    void canvasRefresh()
    {
        float currentlifeFloat = currentLife;
        float MaxLifeFloat = MaxLife;
        lifeBar.fillAmount = currentlifeFloat / MaxLifeFloat;
    }
}

