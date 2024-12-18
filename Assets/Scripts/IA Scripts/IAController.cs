using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using IA2;
using Random = UnityEngine.Random;

public enum ActionEntity
{
    Kill,
    PickUp,
    NextStep,
    FailedStep,
    Open,
    Success
}
public class IAController : MonoBehaviour
{
    private EventFSM<ActionEntity> _fsm;

    //IEnumerable<Tuple<ActionEntity, Item>> _plan;
    private List<List<ActionEntity>> _plans;
    private List<ActionEntity> _Currentplan;
    private List<float> _Costs;

    public Transform _target;
    public AIMovement movement;
    public VisionDetector visionDetector; // Detector de visión adjunto a la IA
    public GlobalDetector globalDetector; // Detector global opcional
    public EnergyManager energyManager;
    public GOAPPlanner planner;


    private void PerformSearch()
    {
        if (_target == null) return;

        _fsm.Feed(ActionEntity.NextStep);
    }

    private void PerformChase()
    {
        if (_target == null) return;

        Debug.Log("Chasing the player...");
        this.movement.MoveTo(_target.position);

        _fsm.Feed(ActionEntity.NextStep);
    }

    private void PerformAttack()
    {
        Debug.Log("PerformAttack", other.gameObject);
        if (_target == null) return;

        var mace = _ent.items.FirstOrDefault(it => it.type == ItemType.Mace);
        if (mace)
        {
            other.Kill();
            if (other.type == ItemType.Door)
                Destroy(_ent.Removeitem(mace).gameObject);
            _fsm.Feed(ActionEntity.NextStep);
        }
        else
            _fsm.Feed(ActionEntity.FailedStep);
    }

    private void PerformTeleport()
    {
        if (_target == null) return;

        _fsm.Feed(ActionEntity.NextStep);
    }

    private void PerformBlock()
    {
        if (_target == null) return;

        _fsm.Feed(ActionEntity.NextStep);
    }

    private void Awake()
    {
        var any = new State<ActionEntity>("any");

        var search = new State<ActionEntity>("Search");
        var chase = new State<ActionEntity>("Chase");
        var attack = new State<ActionEntity>("Attack");
        var teleport = new State<ActionEntity>("Teleport");
        var block = new State<ActionEntity>("Block");


        kill.OnEnter += a => {
            _ent.GoTo(_target.transform.position);
            _ent.OnHitItem += PerformAttack;
        };

        kill.OnExit += a => _ent.OnHitItem -= PerformAttack;

        failStep.OnEnter += a => { _ent.Stop(); Debug.Log("Plan failed"); };

        pickup.OnEnter += a => { _ent.GoTo(_target.transform.position); _ent.OnHitItem += PerformPickUp; };
        pickup.OnExit += a => _ent.OnHitItem -= PerformPickUp;

        open.OnEnter += a => { _ent.GoTo(_target.transform.position); _ent.OnHitItem += PerformOpen; };
        open.OnExit += a => _ent.OnHitItem -= PerformOpen;

        bridgeStep.OnEnter += a => {
            var step = _plan.FirstOrDefault();
            if (step != null)
            {

                _plan = _plan.Skip(1);
                var oldTarget = _target;
                _target = step.Item2;
                if (!_fsm.Feed(step.Item1))
                    _target = oldTarget;
            }
            else
            {
                _fsm.Feed(ActionEntity.Success);
            }
        };

        success.OnEnter += a => { Debug.Log("Success"); };
        success.OnUpdate += () => { _ent.Jump(); };

        StateConfigurer.Create(any)
            .SetTransition(ActionEntity.NextStep, bridgeStep)
            .SetTransition(ActionEntity.FailedStep, idle)
            .Done();

        StateConfigurer.Create(bridgeStep)
            .SetTransition(ActionEntity.Kill, kill)
            .SetTransition(ActionEntity.PickUp, pickup)
            .SetTransition(ActionEntity.Open, open)
            .SetTransition(ActionEntity.Success, success)
            .Done();

        _fsm = new EventFSM<ActionEntity>(idle, any);
    }

    public void ChosePlan(List<List<ActionEntity>> plans, List <float> costs)
    {
        float lowerCost = Mathf.Infinity;

        for (int i = 0; i < costs.Count; i++)
        {
            if (costs[i] < lowerCost)
            {
                lowerCost = costs[i];
                _Currentplan = plans[i];
            }
        }
        _fsm.Feed(ActionEntity.NextStep);
    }

    public void NotifyNewPlans(List<ActionEntity> plan, float cost)
    {
        _plans.Add(plan);
        _Costs.Add(cost);
    }

    public void DefineNewPlan()
    {
        _plans.Clear();
        _Costs.Clear();
        planner.
        StartCoroutine(planner.GeneratePlan());
    }

    private void Update()
    {
        //Never forget
        _fsm.Update();
    }

    public void Movement()
    {

        if (movement.IsMoving)
        {
            Vector3 randomTarget = GetRandomPosition();
            movement.MoveTo(randomTarget);
            //Debug.Log("Moving to random target: " + randomTarget);
        }

        if (movement.HasReachedDestination())
        {
            //Debug.Log("Reached target. Generating new patrol target.");
            Vector3 newTarget = GetRandomPosition();
            movement.MoveTo(newTarget);
        }

        if (movement.IsMoving)
        {
            Vector3 direction = movement.CurrentTarget - transform.position;
            direction.y = 0; // Mantener la rotación en el plano horizontal

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3);
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        // Generar una posición aleatoria dentro de un rango
        return new Vector3(Random.Range(0, 40), 0, Random.Range(0, 40));
    }
    /// <summary>
    /// /////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    /*
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

            //Estados que simplifican algunos datos de arriba
            WorldState.SetState("SameDimension", true);
            WorldState.SetState("PlayerAlive", true);
            WorldState.SetState("PlayerLowEnergy", false);



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
                int count = 0;
                foreach (var step in currentPlan)
                {

                    Debug.Log("plan:" + count +" "+ step.GetName());
                    count++;
                }

            }
            else
            {
                Debug.LogWarning("Failed to generate a plan.");
            }
        }
        public void NotifyPlayerDetected(bool detected)
        {
            if ((bool)WorldState.GetState("PlayerDetected") != detected)
            {
                WorldState.SetState("PlayerDetected", detected);
            }
        }*/
}
