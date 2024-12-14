using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : AIStateBase
{
    public AttackState(AIStateController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("Entering Attack State");
    }

    public override void Update()
    {
        Debug.Log("Attacking the player...");
        controller.PlayerLife.TakeDamage(1); // Reduce vida del jugador
    }

    public override void Exit()
    {
        Debug.Log("Exiting Attack State");
    }
}
