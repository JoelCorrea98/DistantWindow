using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public float MaxEnergy = 100f; // Energ�a m�xima
    public float CurrentEnergy; // Energ�a actual
    public float RegenerationRate = 5f; // Energ�a regenerada por segundo

    private void Start()
    {
        CurrentEnergy = MaxEnergy; // Inicializamos la energ�a al m�ximo
    }

    private void Update()
    {
        RegenerateEnergy();
    }

    // M�todo para gastar energ�a
    public bool SpendEnergy(float amount)
    {
        if (CurrentEnergy >= amount)
        {
            CurrentEnergy -= amount;
            return true;
        }
        return false; // No hay suficiente energ�a
    }

    // M�todo para regenerar energ�a
    private void RegenerateEnergy()
    {
        if (CurrentEnergy < MaxEnergy)
        {
            CurrentEnergy += RegenerationRate * Time.deltaTime;
            CurrentEnergy = Mathf.Clamp(CurrentEnergy, 0, MaxEnergy);
        }
    }

    // Verificar si hay suficiente energ�a
    public bool HasEnoughEnergy(float amount)
    {
        return CurrentEnergy >= amount;
    }

    public void AddEnergy(float amount)
    {
        // Agregar energ�a y asegurarse de que no supere el m�ximo
        CurrentEnergy += amount;
        CurrentEnergy = Mathf.Clamp(CurrentEnergy, 0, MaxEnergy);
        Debug.Log($"Energy added: {amount}. Current Energy: {CurrentEnergy}");
    }
}
