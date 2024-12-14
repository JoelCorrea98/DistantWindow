using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : AIStateBase
{
    public BlockState(AIStateController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("Entering Block State");
    }

    public override void Update()
    {
        Debug.Log("Blocking...");
        controller.EnergyManager.SpendEnergy(1); // Regenera energía
    }

    public override void Exit()
    {
        Debug.Log("Exiting Block State");
    }
}
