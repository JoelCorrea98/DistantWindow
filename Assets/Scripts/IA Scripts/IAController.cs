using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : MonoBehaviour
{
    public WorldState WorldState { get; private set; }
    public List<GOAPAction> AvailableActions = new List<GOAPAction>();
    private GOAPPlanner planner = new GOAPPlanner();
    private Queue<GOAPAction> currentPlan = new Queue<GOAPAction>();

    public Transform player;
    public AIMovement movement;
    public VisionDetector visionDetector; // Detector de visión adjunto a la IA
    public GlobalDetector globalDetector; // Detector global opcional
    public EnergyManager energyManager;
    public AIStateController StateController;

    private void Start()
    {
        // Inicializar el WorldState
        WorldState = FindObjectOfType<WorldState>();
        if (WorldState == null)
        {
            Debug.LogError("WorldState not found in the scene!");
            return;
        }

        // Inicializar el controlador de estados
        StateController = GetComponent<AIStateController>();
        if (StateController == null)
        {
            Debug.LogError("AIStateController not found in the GameObject!");
            return;
        }

        // Definir el estado inicial del mundo
        WorldState.SetState("PlayerLife", 10);
        WorldState.SetState("PlayerDimension", "red");
        WorldState.SetState("PlayerDetected", false);
        WorldState.SetState("PlayerEnergy", 10.0f);
        WorldState.SetState("PlayerInRange", false);
        WorldState.SetState("EnoughEnergy", true);
        WorldState.SetState("PlayerVulnerable", false);
        WorldState.SetState("ReduceDistance", false);
        WorldState.DebugState();

        // Registrar las acciones
        AvailableActions.Add(new SearchAction(WorldState));
        AvailableActions.Add(new ChaseAction(WorldState));
        AvailableActions.Add(new AttackAction(WorldState));
        AvailableActions.Add(new TeleportAction(WorldState));
        AvailableActions.Add(new BlockAction(WorldState));

        // Generar un plan inicial
        GeneratePlan();
    }

    private void Update()
    {
        // Detectar si el jugador está cerca
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        WorldState.SetState("PlayerInRange", distanceToPlayer < 2f);
        WorldState.SetState("EnoughEnergy", true); // Por ahora, siempre hay suficiente energía

       
    }

    public void PlanExecute()
    {
        // Ejecutar la acción actual
        if (currentPlan.Count > 0)
        {
            var currentAction = currentPlan.Peek();
            var currentState = WorldState.GetAllStates(); // Obtener el estado actual como un diccionario

            if (currentAction.ArePreconditionsMet(currentState))
            {
                Debug.Log("Entre al ejecute plan y tengo las condiciones de la accion " + currentAction);

                // Cambiar el estado en la máquina de estados
                if (currentAction is SearchAction)
                    StateController.ChangeState(AIState.Search);
                //Debug.Log("GOAP Seteo Search como estado actual");
                else if (currentAction is ChaseAction)
                    StateController.ChangeState(AIState.Chase);
                else if (currentAction is AttackAction)
                    StateController.ChangeState(AIState.Attack);
                else if (currentAction is TeleportAction)
                    StateController.ChangeState(AIState.Teleport);
                else if (currentAction is BlockAction)
                    StateController.ChangeState(AIState.Block);

                // Ejecutar la acción
                currentPlan.Dequeue();
                currentAction.ApplyEffects(currentState);
            }
        }
        else
        {
            GeneratePlan();
        }
    }

    public void GeneratePlan()
    {
        // Definir el objetivo
        var goal = new Dictionary<string, object> { { "PlayerLife", 0 } };

        // Obtener el estado actual como un diccionario
        var currentState = WorldState.GetAllStates();

        // Generar un plan
        var plan = planner.Plan(currentState, goal, AvailableActions);
        if (plan != null)
        {
            currentPlan = new Queue<GOAPAction>(plan);
            Debug.Log("Plan generated successfully.");
        }
        else
        {
            Debug.LogWarning("Failed to generate a plan.");
        }
    }
}
