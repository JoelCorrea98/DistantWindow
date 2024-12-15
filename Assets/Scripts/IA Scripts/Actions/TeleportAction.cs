using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAction : GOAPAction
{
    private WorldState worldState; // Referencia al estado del mundo

    public TeleportAction(WorldState worldState)
    {
        this.worldState = worldState;

        // Precondiciones para ejecutar la acción
        Preconditions.Add("PlayerAlive", true);
        Preconditions.Add("PlayerDetected", true);
        Preconditions.Add("SameDimension", false);
        Preconditions.Add("EnoughEnergy", true);


        // Efectos que produce la acción
        Effects.Add("SameDimension", true);
        Effects.Add("ReduceDistance", true);

        // Costo de la acción
        Cost = 4f;
    }

    public override bool ArePreconditionsMet(Dictionary<string, object> worldState)
    {
        // Verifica que haya suficiente energía antes de ejecutar
        return base.ArePreconditionsMet(worldState);
    }

    public override Dictionary<string, object> ApplyEffects(Dictionary<string, object> worldState)
    {
        Debug.Log("Executing TeleportAction: Teleporting to player's dimension...");

        // Gasta energía para ejecutar la acción
        return base.ApplyEffects(worldState);

        Debug.LogWarning("Not enough energy to execute TeleportAction!");
        return worldState; // No se aplican efectos si no hay energía
    }
}
