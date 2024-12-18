using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /*
public class AttackAction : GOAPAction
{
    private WorldState _worldState; // Referencia al estado del mundo

    public AttackAction(WorldState worldState)
    {
        this._worldState = worldState;

        // Precondiciones para ejecutar la acción
        Preconditions.Add("PlayerAlive", true);
        Preconditions.Add("SameDimension", true);
        Preconditions.Add("PlayerInRange", true);
        Preconditions.Add("PlayerDetected", true);
        Preconditions.Add("EnoughEnergy", true);

        // Efectos que produce la acción
        Effects.Add("PlayerHealthReduced", true);

        // Costo de la acción
        Cost = 2f;
    }

    public override bool ArePreconditionsMet(Dictionary<string, object> worldState)
    {
        // Verificar que el jugador está en rango
        if (worldState.ContainsKey("PlayerInRange") && worldState["PlayerInRange"] is bool inRange)
        {
            return inRange;
        }

        return false;
    }

    public override Dictionary<string, object> ApplyEffects(Dictionary<string, object> worldState)
    {
        // Verificar si el estado del mundo contiene la vida del jugador
        if (worldState.ContainsKey("PlayerLife") && worldState["PlayerLife"] is int playerLife)
        {
            // Reducir la vida del jugador
            playerLife -= 1; // Aquí defines cuánto daño quieres causar
            if(playerLife < 0)
            {
                playerLife = 0;
            }
            worldState["PlayerLife"] = playerLife;

            //Debug.Log($"Simulated PlayerLife after damage: {playerLife}");
        }
        else
        {
            Debug.LogWarning("PlayerLife not found in world state or has incorrect type.");
        }

        // Agregar efectos adicionales si es necesario
        worldState["ReduceDistance"] = true;

        // Retornar el estado actualizado
        return base.ApplyEffects(worldState);
    }
public override string GetName()
{
    return "attack";
    }
}
    */

