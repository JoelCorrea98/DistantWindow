using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAction : GOAPAction
{
    /*
    private WorldState worldState; // Referencia al estado del mundo


    public BlockAction(WorldState worldState)
    {
        this.worldState = worldState;


        // Precondiciones para ejecutar la acción
        Preconditions.Add("PlayerAlive", true);
        Preconditions.Add("EnoughEnergy", true);
        Preconditions.Add("PlayerVulnerable", true);
        Preconditions.Add("PlayerLowEnergy", true);

        // Efectos que produce la acción
        Effects.Add("PlayerBlocked", true);

        // Costo de la acción
        Cost = 3f;
    }

    public override bool ArePreconditionsMet(Dictionary<string, object> worldState)
    {
        // Verificar precondiciones específicas si es necesario
        return base.ArePreconditionsMet(worldState);
    }

    public override Dictionary<string, object> ApplyEffects(Dictionary<string, object> worldState)
    {
        Debug.Log("Executing BlockAction...");
        Debug.Log("Blocking the player...");
        return base.ApplyEffects(worldState);
    }
    public override string GetName()
    {
        return "block";
    }
    */
}
