using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAction : GOAPAction
{
    private WorldState worldState; // Referencia al estado del mundo

    public TeleportAction(WorldState worldState)
    {
        this.worldState = worldState;

        // Precondiciones para ejecutar la acci�n
        Preconditions.Add("PlayerAlive", true);
        Preconditions.Add("PlayerDetected", true);
        Preconditions.Add("SameDimension", false);
        Preconditions.Add("EnoughEnergy", true);


        // Efectos que produce la acci�n
        Effects.Add("SameDimension", true);
        Effects.Add("ReduceDistance", true);

        // Costo de la acci�n
        Cost = 4f;
    }

    public override bool ArePreconditionsMet(Dictionary<string, object> worldState)
    {
        // Verifica que haya suficiente energ�a antes de ejecutar
        return base.ArePreconditionsMet(worldState);
    }

    public override Dictionary<string, object> ApplyEffects(Dictionary<string, object> worldState)
    {
        Debug.Log("Executing TeleportAction: Teleporting to player's dimension...");

        // Gasta energ�a para ejecutar la acci�n
        return base.ApplyEffects(worldState);

        Debug.LogWarning("Not enough energy to execute TeleportAction!");
        return worldState; // No se aplican efectos si no hay energ�a
    }
}
