using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : AIStateBase
{
    public ChaseState(AIStateController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("Entering Chase Stateeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
    }

    public override void Update()
    {
        Debug.Log("Chasing the player...");
        controller.Movement.MoveTo(controller.Player.position);
    }

    public override void Exit()
    {
        Debug.Log("Exiting Chase State");
    }
}
