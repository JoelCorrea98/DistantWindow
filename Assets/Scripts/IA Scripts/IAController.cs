using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    // FSM
    private EventFSM<ActionEntity> _fsm;
    public EventFSM<ActionEntity> FSM => _fsm;

    // Plan
    private List<List<ActionEntity>> _plans = new List<List<ActionEntity>>();
    private List<float> _Costs = new List<float>();
    [SerializeField] private List<ActionEntity> _Currentplan;
    public List<ActionEntity> CurrentPlan
    {
        get => _Currentplan;
        set => _Currentplan = value;
    }

    // Referencias a componentes / datos
    public Transform _target;
    public GameObject player;
    public AIMovement movement;
    public VisionDetector visionDetector;
    public GlobalDetector globalDetector;
    public EnergyManager energyManager;
    public GOAPPlanner planner;
    public Animator animator;
    public GameObject _characterMesh;
    public AudioSource audioSource;
    public AudioClip teleportAudio;
    public AudioClip chaseSound;
    public AudioClip searchSound;
    public AudioClip blockAudioClip;
    public AudioClip attacksound;
    public Collider attackCollider;
    public LayerMask playerLayer;

    public List<Transform> blockSpawnPoints;
    public GameObject blockObjectPrefab;
    public float blockSpawnDelay;
    public float blockStartDelay;
    public float blockDespawnTime;

    // Datos de ataque
    public float attackDelay = 1.5f;
    public int attackDamage = 10;

    // Datos de teleport
    public float teleportDelay = 1.5f;

    // Bandera para saber si el jugador está en rango de ataque
    [SerializeField] private bool _playerInAttackRange = false;
    public bool IsPlayerInAttackRange => _playerInAttackRange;

    // Manejo de dimensión
    private Dimension currentDimension;

    private void Awake()
    {
        var any = new State<ActionEntity>("any");
        var search = new SearchState(this);
        var chase = new ChaseState(this);
        var attack = new AttackState(this);
        var teleport = new TeleportState(this);
        var block = new BlockState(this);
        var failStep = new FailStepState(this);
        var bridgeStep = new BridgeStepState(this);

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

        _fsm = new EventFSM<ActionEntity>(search, any);
    }

    private void Start()
    {
        // Definir plan inicial
        DefineNewPlan();

        // Ajustamos dimensión inicial
        currentDimension = (Dimension)WorldStateManager.instance.GetState("PlayerDimension");
        WorldStateManager.instance.SetState("EnemyDimension", currentDimension);

        // Suscribimos el evento de cambio de dimensión
        var dimensionManager = FindObjectOfType<CambioDimension>();
        if (dimensionManager != null)
            dimensionManager.changingDimension += ChangeDimension;
    }

    private void Update()
    {
        // Actualizamos la FSM
        _fsm.Update();
    }

    private void ChangeDimension()
    {
        if (currentDimension != (Dimension)WorldStateManager.instance.GetState("PlayerDimension"))
        {
            visionDetector.enabled = false;
            attackCollider.enabled = false;
            _characterMesh.SetActive(false);
        }
        else
        {
            visionDetector.enabled = true;
            attackCollider.enabled = true;
            _characterMesh.SetActive(true);
        }

        DefineNewPlan();
    }

    // Métodos de Plan
    public void DefineNewPlan()
    {
        _plans.Clear();
        _Costs.Clear();

        // Ejemplo con 2 objetivos (player muerto y player con poca energía)
        GOAPState goal1 = new GOAPState
        {
            worldState = new WorldState()
            {
                values = new Dictionary<string, object>()
                {
                    { "PlayerAlive", false }
                }
            }
        };
        StartCoroutine(planner.GeneratePlan(goal1));

        GOAPState goal2 = new GOAPState
        {
            worldState = new WorldState()
            {
                values = new Dictionary<string, object>()
                {
                    { "PlayerLowEnergy", true }
                }
            }
        };
        StartCoroutine(planner.GeneratePlan(goal2));
    }

    public void NotifyNewPlan(List<ActionEntity> plan, float cost)
    {
        _plans.Add(plan);
        _Costs.Add(cost);
        ChosePlan(_plans, _Costs);
    }

    public void ChosePlan(List<List<ActionEntity>> plans, List<float> costs)
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
        if (_Currentplan.Count > 0)
            _fsm.Feed(ActionEntity.NextStep);
    }

    // Se llama desde triggers o colliders para indicar si el jugador está en rango.
    public void PlayerInAttackRange(bool value)
    {
        _playerInAttackRange = value;
    }
}

