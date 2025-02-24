using UnityEngine;

public class ChaseState : IAState
{
    public ChaseState(IAController controller) : base(controller, "Chase")
    {
    }

    protected override void OnStateEnter(ActionEntity trigger)
    {
        Debug.Log("chase enter");
        _controller.energyManager.SpendEnergy(2);
        _controller.movement.MoveTo(_controller._target.position);

        // Animaciones
        _controller.animator.SetBool("Teleporting", false);
        _controller.animator.SetBool("Runing", true);

        //Sonido
        if (_controller.chaseSound != null)
        {
            _controller.audioSource.clip = _controller.chaseSound;
            _controller.audioSource.Play();
        }
    }

    protected override void OnStateUpdate()
    {
        Debug.Log("Chase update");

        // Si el jugador está en rango de ataque
        if (_controller.IsPlayerInAttackRange)
        {
            _controller.FSM.Feed(ActionEntity.NextStep);
            return;
        }

        // Si perdimos de vista al jugador
        if (!_controller.visionDetector.IsPlayerDetected &&
            !_controller.globalDetector.IsPlayerDetected)
        {
            _controller.FSM.Feed(ActionEntity.FailedStep);
            return;
        }

        // Si llegamos a destino
        if (_controller.movement.HasReachedDestination())
        {
            _controller.FSM.Feed(ActionEntity.NextStep);
        }
    }

    protected override void OnStateExit(ActionEntity trigger)
    {
        Debug.Log("chase exit");
    }
}

