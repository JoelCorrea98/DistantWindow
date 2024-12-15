using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIStateBase
{
    protected AIStateController controller;
    public Dictionary<string, object> worldState;
    public AIStateBase(AIStateController controller)
    {
        this.controller = controller;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
