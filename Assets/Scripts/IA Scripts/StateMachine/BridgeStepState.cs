using UnityEngine;
using System.Linq;

public class BridgeStepState : IAState
{
    public BridgeStepState(IAController controller) : base(controller, "BridgeStep")
    {
    }

    protected override void OnStateEnter(ActionEntity trigger)
    {
        Debug.Log("bridgeStep enter");

        // Tomamos el primer paso del plan
        var step = _controller.CurrentPlan.FirstOrDefault();

        // Quitamos ese step de la lista
        _controller.CurrentPlan = _controller.CurrentPlan.Skip(1).ToList();

        // Alimenteamos la FSM para que vaya al siguiente estado que indica `step`
        _controller.FSM.Feed(step);
    }

    protected override void OnStateUpdate()
    {
        // Lógica si fuera necesaria
    }

    protected override void OnStateExit(ActionEntity trigger)
    {
        Debug.Log("bridgeStep exit");
    }
}

