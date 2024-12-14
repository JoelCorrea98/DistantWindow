using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public float MaxEnergy = 100f; // Energía máxima
    public float CurrentEnergy; // Energía actual
    public float RegenerationRate = 5f; // Energía regenerada por segundo

    private void Start()
    {
        CurrentEnergy = MaxEnergy; // Inicializamos la energía al máximo
    }

    private void Update()
    {
        RegenerateEnergy();
    }

    // Método para gastar energía
    public bool SpendEnergy(float amount)
    {
        if (CurrentEnergy >= amount)
        {
            CurrentEnergy -= amount;
            return true;
        }
        return false; // No hay suficiente energía
    }

    // Método para regenerar energía
    private void RegenerateEnergy()
    {
        if (CurrentEnergy < MaxEnergy)
        {
            CurrentEnergy += RegenerationRate * Time.deltaTime;
            CurrentEnergy = Mathf.Clamp(CurrentEnergy, 0, MaxEnergy);
        }
    }

    // Verificar si hay suficiente energía
    public bool HasEnoughEnergy(float amount)
    {
        return CurrentEnergy >= amount;
    }

    public void AddEnergy(float amount)
    {
        // Agregar energía y asegurarse de que no supere el máximo
        CurrentEnergy += amount;
        CurrentEnergy = Mathf.Clamp(CurrentEnergy, 0, MaxEnergy);
        Debug.Log($"Energy added: {amount}. Current Energy: {CurrentEnergy}");
    }
}
