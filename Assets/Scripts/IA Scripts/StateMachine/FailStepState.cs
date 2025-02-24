using UnityEngine;

public class FailStepState : IAState
{
    public FailStepState(IAController controller) : base(controller, "FailStep")
    {
    }

    protected override void OnStateEnter(ActionEntity trigger)
    {
        Debug.Log("failStep enter");
        // Aqu� puedes reiniciar o definir un nuevo plan
        _controller.DefineNewPlan();
    }

    protected override void OnStateUpdate()
    {
        Debug.Log("failStep update");
        // Podr�as poner l�gica adicional si quieres
    }

    protected override void OnStateExit(ActionEntity trigger)
    {
        Debug.Log("failStep exit");
    }
}

