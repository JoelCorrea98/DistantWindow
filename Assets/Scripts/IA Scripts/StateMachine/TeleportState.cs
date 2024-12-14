using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportState : AIStateBase
{
    public TeleportState(AIStateController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("Entering Teleport State");
    }

    public override void Update()
    {
        Debug.Log("Teleporting...");
        //controller.Movement.TeleportTo(controller.Player.position);
    }

    public override void Exit()
    {
        Debug.Log("Exiting Teleport State");
    }
}
