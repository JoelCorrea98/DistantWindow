using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class Goap : MonoBehaviour
{
    //El satisfies y la heuristica ahora son Funciones externas
	public static IEnumerable<GOAPAction> Execute(GOAPState from, GOAPState to, Func<GOAPState, bool> satisfies, Func<GOAPState, float> h, IEnumerable<GOAPAction> actions)
    {
        int watchdog = 200;

        IEnumerable<GOAPState> seq = AStarNormal<GOAPState>.Run(
            from,
            to,
            (curr,goal)  => h (curr),
            satisfies,
            curr =>
            {
                if (watchdog == 0)
                    return Enumerable.Empty<AStarNormal<GOAPState>.Arc>();
                else
                    watchdog--;

                //en este Where se evaluan las precondiciones, al ser un diccionario de <string,bool> solo se chequea que todas las variables concuerdes
                //En caso de ser un Func<...,bool> se utilizaria ese func de cada estado para saber si cumple o no
                return actions.Where(action => action.preconditions.All(kv => kv.In(curr.worldState.values)))
                              .Where(a => (bool)a.Preconditions(curr))//nota camilo, acá lo castee a bool, pero puede que haya que tocar algo mas
                              //nota ajena: Agregue esto para chequear las precondiuciones puestas  en el Func, Al final deberia quedar solo esta
                              .Aggregate(new FList<AStarNormal<GOAPState>.Arc>(), (possibleList, action) =>
                              {
                                  var newState = new GOAPState(curr);
                                  newState = action.Effects(newState); // se aplican lso effectos del Func
                                  newState.generatingAction = action;
                                  newState.step = curr.step+1;
                                  return possibleList + new AStarNormal<GOAPState>.Arc(newState, action.Cost);
                              });
            });

        if (seq == null)
        {
            Debug.Log("Imposible planear");
            return null;
        }

        foreach (var act in seq.Skip(1))
        {
			Debug.Log(act);
        }

		Debug.Log("WATCHDOG " + watchdog);
		
		return seq.Skip(1).Select(x => x.generatingAction);
	}
}
