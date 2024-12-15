using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GOAPAction
{
    // Precondiciones para ejecutar esta acción
    public Dictionary<string, object> Preconditions { get; private set; } = new Dictionary<string, object>();

    // Efectos que produce esta acción
    public Dictionary<string, object> Effects { get; private set; } = new Dictionary<string, object>();

    // Costo de ejecutar esta acción
    public float Cost { get; set; } = 1f;

    // Método para comprobar si las precondiciones están cumplidas
    public virtual bool ArePreconditionsMet(Dictionary<string, object> worldState)
    {
        foreach (var precondition in Preconditions)
        {
            if (!worldState.ContainsKey(precondition.Key) || worldState[precondition.Key] != precondition.Value)
            {
                return false;
            }
        }
        return true;
    }

    // Método para aplicar los efectos al estado del mundo etereo 
    public virtual Dictionary<string,object> ApplyEffects(Dictionary<string, object> worldState)
    {
        foreach (var effect in Effects)
        {
            worldState[effect.Key] = effect.Value;
        }

        return worldState;
    }
    public virtual string GetName()
    {
        return "GoapAction";
    }
}
