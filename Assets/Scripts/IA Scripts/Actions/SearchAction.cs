using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAction : GOAPAction
{
    private WorldState worldState; // Referencia al estado del mundo

    public SearchAction(WorldState worldState)
    {
        this.worldState = worldState;

        // Configurar precondiciones y efectos
        Preconditions.Add("PlayerDetected", false);

        Effects.Add("PlayerDetected", true);
        Effects.Add("EnoughEnergy", true);

        Cost = 1f;
    }

    public override Dictionary<string, object> ApplyEffects(Dictionary<string, object> currentWorldState)
    {
        Debug.Log("Executing SearchAction...");
        worldState.SetState("PlayerDetected", true);

        return base.ApplyEffects(currentWorldState);
    }

    

    public override bool ArePreconditionsMet(Dictionary<string, object> currentWorldState)
    {
        if (currentWorldState == null)
        {
            Debug.LogError("currentWorldState is null in ArePreconditionsMet");
            return false;
        }

        if (currentWorldState.ContainsKey("PlayerDetected") && currentWorldState["PlayerDetected"] is bool)
        {
            return (bool)currentWorldState["PlayerDetected"];
        }

        Debug.LogWarning("Key 'PlayerDetected' not found or is not a boolean in currentWorldState");
        return false;
    }
}
