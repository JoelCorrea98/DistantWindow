using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AIState
{
    Search,
    Chase,
    Attack,
    Teleport,
    Block
}
public class AIStateController : MonoBehaviour
{
    public Transform Player;
    public AIMovement Movement;
    public PlayerLife PlayerLife;
    public EnergyManager EnergyManager;
    public GlobalDetector GlobalDetector;
    public VisionDetector VisionDetector;
    public WorldState worldState;
    public IAController iAController;

    private AIStateBase currentState;
    private Dictionary<AIState, AIStateBase> states = new Dictionary<AIState, AIStateBase>();

    private void Start()
    {
        // Inicializar estados
        states[AIState.Search] = new SearchState(this);
        states[AIState.Chase] = new ChaseState(this);
        states[AIState.Attack] = new AttackState(this);
        states[AIState.Teleport] = new TeleportState(this);
        states[AIState.Block] = new BlockState(this);

        // Establecer estado inicial
        ChangeState(AIState.Search);
    }

    private void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(AIState newState)
    {
        currentState?.Exit();
        currentState = states[newState];
        currentState?.Enter();
    }
}

