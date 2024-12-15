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

    private Dictionary<GOAPAction, AIState> _aiActions = new Dictionary<GOAPAction, AIState>();
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
        _aiActions.Add(new SearchAction(WorldState), AIState.Search);
        _aiActions.Add(new ChaseAction(WorldState), AIState.Chase);
        _aiActions.Add(new AttackAction(WorldState), AIState.Attack);
        _aiActions.Add(new TeleportAction(WorldState), AIState.Teleport);
        _aiActions.Add(new BlockAction(WorldState), AIState.Block);

        foreach (var aiAction in _aiActions)
        {
            AvailableActions.Add(aiAction.Key);
        }
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
                if (_aiActions.ContainsKey(currentAction))
                {
                    StateController.ChangeState(_aiActions[currentAction]);
                }
                // Ejecutar la acción
                currentPlan.Dequeue();
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
        var plan = planner.GeneratePlan(currentState, goal, AvailableActions);
        if (plan != null)
        {
            currentPlan = new Queue<GOAPAction>(plan);
            Debug.Log("Plan generated successfully, plan count steps: " + currentPlan.Count);
        }
        else
        {
            Debug.LogWarning("Failed to generate a plan.");
        }
    }
}
