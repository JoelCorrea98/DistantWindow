using UnityEngine;

public class ChaseState : IAState
{
    private float stopPathfindingRange = 3.0f; // Rango en el que deja de usar pathfinding
    private bool isUsingPathfinding = true; // Indica si está usando pathfinding

    public ChaseState(IAController controller) : base(controller, "Chase")
    {
    }

    protected override void OnStateEnter(ActionEntity trigger)
    {
        Debug.Log("chase enter");
        _controller.energyManager.SpendEnergy(2);

        // Iniciar pathfinding hacia el jugador
        _controller.movement.MoveTo(_controller._target.position);
        isUsingPathfinding = true;

        // Animaciones
        _controller.animator.SetBool("Teleporting", false);
        _controller.animator.SetBool("Runing", true);

        // Sonido
        _controller.audioManager.PlayStateAudio(ActionEntity.Chase);
        _controller.audioManager.PlayStateAudio(ActionEntity.Search);
        if (_controller.chaseSound != null)
        {

            //_controller.audioSource.clip = _controller.chaseSound;
            //_controller.audioSource.Play();
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

        // Calcular la distancia al jugador
        float distanceToPlayer = Vector3.Distance(_controller.transform.position, _controller._target.position);

        // Si está dentro del rango para dejar de usar pathfinding
        if (distanceToPlayer <= stopPathfindingRange)
        {
            if (isUsingPathfinding)
            {
                // Dejar de usar pathfinding y moverse directamente hacia el jugador
                _controller.movement.StopPathfinding();
                isUsingPathfinding = false;
            }

            // Moverse directamente hacia el jugador
            _controller.movement.MoveDirectlyTo(_controller._target.position);
        }
        else
        {
            if (!isUsingPathfinding)
            {
                // Volver a usar pathfinding si está fuera del rango
                _controller.movement.MoveTo(_controller._target.position);
                isUsingPathfinding = true;
            }
        }

        // Si llegamos a destino (solo si estamos usando pathfinding)
        if (isUsingPathfinding && _controller.movement.HasReachedDestination())
        {
            _controller.FSM.Feed(ActionEntity.NextStep);
        }
    }

    protected override void OnStateExit(ActionEntity trigger)
    {
        Debug.Log("chase exit");

        // Detener el movimiento al salir del estado
        _controller.movement.Stop();
        isUsingPathfinding = false;

        //_controller.audioManager.StopAllAudio();
    }
}

