using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : GOAPAction
{
    /*
    private AIMovement movement;  // Referencia al componente de movimiento
    private Transform player;     // Referencia al jugador
    private WorldState worldState; // Referencia al estado del mundo
    private EnergyManager energyManager;

    public ChaseAction(WorldState worldState)
    {
      
        this.worldState = worldState;

        // Precondiciones para activar esta acci�n
        Preconditions.Add("PlayerDetected", true);
        Preconditions.Add("EnoughEnergy", true);
        Preconditions.Add("PlayerAlive", true);
        Preconditions.Add("ReduceDistance", false);
        Preconditions.Add("PlayerInRange", false);


        // Efectos que produce esta acci�n
        Effects.Add("ReduceDistance", true);
        Effects.Add("PlayerInRange", true);

        // Costo de la acci�n
        Cost = 2f;
    }

    public override bool ArePreconditionsMet(Dictionary<string, object> currentWorldState)
    {

        if (currentWorldState == null)
        {
            Debug.LogError("currentWorldState is null in ArePreconditionsMet");
            return false;
        }

        if (currentWorldState.ContainsKey("PlayerDetected") && (bool)currentWorldState["PlayerDetected"]
            && currentWorldState.ContainsKey("EnoughEnergy") && (bool)currentWorldState["EnoughEnergy"]
            && currentWorldState.ContainsKey("PlayerAlive") && (bool)currentWorldState["PlayerAlive"]
            && currentWorldState.ContainsKey("ReduceDistance") && !(bool)currentWorldState["ReduceDistance"]
            && currentWorldState.ContainsKey("PlayerInRange") && !(bool)currentWorldState["PlayerInRange"])
        {
            return true;
        }

        return false;
    }

    public override Dictionary<string, object> ApplyEffects(Dictionary<string, object> currentWorldState)
    {
        Debug.Log("Executing ChaseAction: Chasing the player...");

        // Iniciar el movimiento hacia el jugador
        //movement.MoveTo(player.position);

        // Actualizar el estado del mundo si necesario
        //worldState.SetState("ReduceDistance", true); ??????????????????????????????

        // Retornar el estado actualizado
        return base.ApplyEffects(currentWorldState);
    }

    public virtual bool IsDone()
    {
        // Considerar la acci�n completada si la IA est� cerca del jugador
        float distanceToPlayer = Vector3.Distance(movement.transform.position, player.position);
        return distanceToPlayer < 1.5f; // Por ejemplo, cuando est� a menos de 1.5 unidades
    }
    public override string GetName()
    {
        return "chase";
    }
    */
}
