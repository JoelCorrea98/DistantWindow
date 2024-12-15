using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GOAPAction
{
    public Dictionary<string, bool> preconditions { get; private set; }

    public Func<AIStateBase, bool> Preconditions = delegate { return true; };
    public Dictionary<string, bool> effects { get; private set; }

    public Func<AIStateBase, AIStateBase> Effects;

    public float Cost { get; private set; }

    public string Name { get; private set; }

    public GOAPAction(string name)
    {

        this.Name = name;
        Cost = 1f;
        preconditions = new Dictionary<string, bool>();
        effects = new Dictionary<string, bool>();

        //Para que funcione en la mezcla se hizo esto, pero se le podria settear a cada Action su propia logica de effect
        Effects = (s) =>
        {
            foreach (var item in effects)
            {
                s.worldState.values[item.Key] = item.Value;
            }
            return s;
        };
    }

    public GOAPAction SetCost(float cost)
    {
        if (cost < 1f)
        {
            Debug.Log(string.Format("Warning: Using cost < 1f for '{0}' could yield sub-optimal results", Name));
        }
        this.Cost = cost;
        return this;
    }
    public GOAPAction Pre(string s, bool value)
    {
        preconditions[s] = value;
        return this;
    }

    public GOAPAction Pre(Func<AIStateBase, bool> p)
    {
        Preconditions = p;
        return this;
    }
    public GOAPAction Effect(string s, bool value)
    {
        effects[s] = value;
        return this;
    }

    public GOAPAction Effect(Func<AIStateBase, AIStateBase> e)
    {
        Effects = e;
        return this;
    }
    
}
