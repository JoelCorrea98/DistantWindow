using UnityEngine;
using IA2; // Ajusta al namespace correcto de tu FSM

public abstract class IAState : State<ActionEntity>
{
    protected IAController _controller;

    protected IAState(IAController controller, string stateName) : base(stateName)
    {
        _controller = controller;

        OnEnter += HandleOnEnter;
        OnUpdate += HandleOnUpdate;
        OnExit += HandleOnExit;
    }

    private void HandleOnEnter(ActionEntity trigger)
    {
        OnStateEnter(trigger);
    }

    private void HandleOnUpdate()
    {
        OnStateUpdate();
    }

    private void HandleOnExit(ActionEntity trigger)
    {
        OnStateExit(trigger);
    }

    protected abstract void OnStateEnter(ActionEntity trigger);
    protected abstract void OnStateUpdate();
    protected abstract void OnStateExit(ActionEntity trigger);
}


