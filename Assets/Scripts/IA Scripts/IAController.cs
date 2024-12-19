using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using IA2;
using Random = UnityEngine.Random;

public enum ActionEntity
{
    Search,
    Chase,
    Attack,
    FailedStep,
    NextStep,
    Teleport,
    Block,
}
public class IAController : MonoBehaviour
{
    private EventFSM<ActionEntity> _fsm;

    //IEnumerable<Tuple<ActionEntity, Item>> _plan;
    private List<List<ActionEntity>> _plans;
    private List<ActionEntity> _Currentplan;
    private List<float> _Costs;

    public Transform _target;
    public GameObject player;
    public AIMovement movement;
    public VisionDetector visionDetector; // Detector de visión adjunto a la IA
    public GlobalDetector globalDetector; // Detector global opcional
    public EnergyManager energyManager;
    public GOAPPlanner planner;

    //Atack
    private bool isAttacking = false; // Para evitar ataques simultáneos
    private bool attackSuccessful = false; // Indica si el ataque fue exitoso
    public float attackDelay = 1.5f; // Tiempo de preparación del ataque
    public int attackDamage = 10; // Daño que inflige el ataque


    public LayerMask playerLayer; // Capa del jugador
    public Collider attackCollider; // Collider que usará el ataque

    //Block
    public IABlockHability blockHability;

    private void PerformSearch()
    {
        if (_target == null) return;

        Movement();

        // Verificar si el jugador ha sido detectado
        if (visionDetector.IsPlayerDetected || globalDetector.IsPlayerDetected)
        {
            Debug.Log("Player detected during search!");
            DefineNewPlan(); // ya hice la accion
        }

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
        Debug.Log("PerformAttack"); //acá podría hacer la logica de ataque
        if (_target == null) return;

        
        if (isAttacking) return; // Si ya está atacando, salir
           
        StartCoroutine(PerformCoRAttack());
        

        if (true) //podríamos chequear si le pegó o no al player
        {
            _fsm.Feed(ActionEntity.NextStep);
        }
        else
            _fsm.Feed(ActionEntity.FailedStep);
    }

    private void PerformTeleport()
    {
        if (_target == null) return;

        TeleportCloserToTarget();

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
        var bridgeStep = new State<ActionEntity>("BridgeStep"); // agregue estos 2, el bridge le permite pasar de un estado a otro como en el ejemplo
        var failStep = new State<ActionEntity>("FailStep");//para que cuando falla que cambie al fail y formule un nuevo plan


        search.OnEnter += a => 
        {
            //logica del search.OnEnter
            energyManager.SpendEnergy(1);
        };
        search.OnUpdate += () =>
        {
            //logica de search.OnUpdate
            PerformSearch();
        };
        search.OnExit += a =>
        {
            //logica del search.OnExit
        };
        /*--------------------------------*/
        chase.OnEnter += a =>
        {
            //logica del search.OnEnter
            energyManager.SpendEnergy(2);
        };
        chase.OnUpdate += () =>
        {
            //logica de search.OnUpdate
            PerformChase();
        };
        chase.OnExit += a =>
        {
            //logica del search.OnExit
        };
        /*--------------------------------*/
        attack.OnEnter += a =>
        {
            //logica del search.OnEnter
            energyManager.SpendEnergy(2);
        };
        attack.OnUpdate += () =>
        {
            //logica de search.OnUpdate
            PerformAttack();
        };
        attack.OnExit += a =>
        {
            //logica del search.OnExit
        };
        /*--------------------------------*/
        block.OnEnter += a =>
        {
            //logica del search.OnEnter
            energyManager.SpendEnergy(4);
            blockHability.SpawnClosestObjects(_target.transform.position);
        };
        block.OnUpdate += () =>
        {
            //logica de search.OnUpdate
        };
        block.OnExit += a =>
        {
            //logica del search.OnExit
        };
        /*--------------------------------*/
        teleport.OnEnter += a =>
        {
            //logica del search.OnEnter
            energyManager.SpendEnergy(4);
            PerformTeleport();
        };
        teleport.OnUpdate += () =>
        {
            //logica de search.OnUpdate
        };
        teleport.OnExit += a =>
        {
            //logica del search.OnExit
        };
        /*--------------------------------*/
        failStep.OnEnter += a => 
        {
            //logica del failStep.OnEnter
        };
        failStep.OnUpdate += () =>
        {
            //logica del failStep.OnUpdate
        };
        failStep.OnExit += a =>
        {
            //logica del failStep.OnExit
        };

       
        bridgeStep.OnEnter += a => {
            var step = _Currentplan.FirstOrDefault();
            if (step != null)
            {

                _Currentplan = _Currentplan.Skip(1).ToList();
                _fsm.Feed(ActionEntity.NextStep);
            }
            else
            {
                // aca podríamos poner un estado de que gano (se pone a baiar o algo) 
            }
        };


        StateConfigurer.Create(any)
            .SetTransition(ActionEntity.NextStep, bridgeStep)
            .SetTransition(ActionEntity.FailedStep, failStep)
            .Done();

        StateConfigurer.Create(bridgeStep)
            .SetTransition(ActionEntity.Search, search)
            .SetTransition(ActionEntity.Chase, chase)
            .SetTransition(ActionEntity.Attack, attack)
            .SetTransition(ActionEntity.Teleport, teleport)
            .SetTransition(ActionEntity.Block, block)
            .Done();

        _fsm = new EventFSM<ActionEntity>(search);
    }

    private void Start()
    {
        DefineNewPlan();
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
        ChosePlan(_plans, _Costs);
    }

    public void DefineNewPlan()
    {
        _plans.Clear();
        _Costs.Clear();
        GOAPState goal = new GOAPState();
        GOAPState goalEnergy = new GOAPState();
        //HACER .ADD SI FALLA!!!
        goal.worldState.values["PlayerAlive"] = false;
        StartCoroutine(planner.GeneratePlan(goal));

        goalEnergy.worldState.values["PlayerLowEnergy"] = true;
        StartCoroutine(planner.GeneratePlan(goalEnergy));

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

    private IEnumerator PerformCoRAttack()
    {
        isAttacking = true;
        attackSuccessful = false; // Reiniciar el estado del ataque

        // Simular preparación del ataque
        yield return new WaitForSeconds(attackDelay);

        // Activar el collider de ataque
        if (attackCollider != null)
        {
            attackCollider.enabled = true;

            // Comprobar si el collider toca al jugador
            Collider[] hitColliders = Physics.OverlapBox(
                attackCollider.bounds.center,
                attackCollider.bounds.extents,
                attackCollider.transform.rotation,
                playerLayer
            );

            foreach (var hit in hitColliders)
            {
                if (hit.gameObject == player)
                {
                    // Si golpea al jugador, aplicar daño
                    player.GetComponent<PlayerLife>()?.TakeDamage(attackDamage);
                    attackSuccessful = true; // El ataque fue exitoso
                    break;
                }
            }

            // Desactivar el collider tras el ataque
            attackCollider.enabled = false;
        }

        // Esperar un pequeño tiempo para finalizar el ataque
        yield return new WaitForSeconds(0.1f);

        isAttacking = false;
        Debug.Log($"Ataque {(attackSuccessful ? "exitoso" : "fallido")}");
    }

    public void TeleportCloserToTarget()
    {
        if (_target == null)
        {
            Debug.LogWarning("No se ha asignado un jugador objetivo.");
            return;
        }

        // Obtener la posición actual de la IA y del jugador
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = _target.position;

        // Calcular la dirección hacia el jugador
        Vector3 directionToTarget = targetPosition - currentPosition;

        // Calcular la nueva posición, acercándose un 60% más al jugador
        Vector3 newPosition = currentPosition + directionToTarget * 0.6f;

        // Teletransportar la IA a la nueva posición
        transform.position = newPosition;

        Debug.Log($"Teletransportado a {newPosition}");
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
