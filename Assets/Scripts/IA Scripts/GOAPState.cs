using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GOAPState : MonoBehaviour
{
    public WorldState worldState;


    public GOAPAction generatingAction = null;
    public int step = 0;

    #region CONSTRUCTOR
    public GOAPState(GOAPAction gen = null)
    {
        generatingAction = gen;
        worldState = new WorldState()
        {
            values = new Dictionary<string, object>() 
        };
    }

    public GOAPState(GOAPState source, GOAPAction gen = null)
    {
        worldState = source.worldState.Clone();
        generatingAction = gen;
    }
    #endregion


    public override bool Equals(object obj)
    {
        var result =
            obj is GOAPState other
            && other.generatingAction == generatingAction
            && other.worldState.values.Count == worldState.values.Count
            && other.worldState.values.All(kv => kv.In(worldState.values));
        return result;
    }

    public override int GetHashCode()
    {
        return worldState.values.Count == 0 ? 0 : 31 * worldState.values.Count + 31 * 31 * worldState.values.First().GetHashCode();
    }

    public override string ToString()
    {
        var str = "";
        foreach (var kv in worldState.values.OrderBy(x => x.Key))
        {
            str += (string.Format("{0:12} : {1}\n", kv.Key, kv.Value));
        }
        return ("--->" + (generatingAction != null ? generatingAction.Name : "NULL") + "\n" + str);
    }
}


public struct WorldState
{
    public int playerHP;
    public Dictionary<string, object> values;

    public WorldState Clone()
    {
        return new WorldState()
        {
            playerHP = this.playerHP,
            values = this.values.ToDictionary(kv => kv.Key, kv => kv.Value)
        };
    }
}
