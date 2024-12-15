using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : AIStateBase
{
    public SearchState(AIStateController controller) : base(controller)  { }

    public override void Enter()
    {
        Debug.Log("Entering Search State");
        // Configurar lógica inicial
    }

    public override void Update()
    {
        //Debug.Log("Searching...");

        Movement();

        // Verificar si el jugador ha sido detectado
        if (controller.VisionDetector.IsPlayerDetected || controller.GlobalDetector.IsPlayerDetected)
        {
            Debug.Log("Player detected during search!");
            controller.iAController.PlanExecute(); // ya hice la accion
        }
    }

    public void Movement()
    {

        if (!controller.Movement.IsMoving)
        {
            Vector3 randomTarget = GetRandomPosition();
            controller.Movement.MoveTo(randomTarget);
            //Debug.Log("Moving to random target: " + randomTarget);
        }

        if (controller.Movement.HasReachedDestination())
        {
            //Debug.Log("Reached target. Generating new patrol target.");
            Vector3 newTarget = GetRandomPosition();
            controller.Movement.MoveTo(newTarget);
        }

        if (controller.Movement.IsMoving)
        {
            Vector3 direction = controller.Movement.CurrentTarget - controller.transform.position;
            direction.y = 0; // Mantener la rotación en el plano horizontal

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, targetRotation, Time.deltaTime * 3);
            }
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Search State");
        // Limpiar lógica si es necesario
        controller.worldState.SetState("PlayerDetected", true); // Notificar al WorldState
        controller.EnergyManager.AddEnergy(2); // Agregar energía si es necesario
    }

    private Vector3 GetRandomPosition()
    {
        // Generar una posición aleatoria dentro de un rango
        return new Vector3(Random.Range(0, 40), 0, Random.Range(0, 40));
    }
}
