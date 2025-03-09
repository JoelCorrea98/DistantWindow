using UnityEngine;
using Random = UnityEngine.Random;

public class SearchState : IAState
{
    public SearchState(IAController controller) : base(controller, "Search")
    {
    }

    protected override void OnStateEnter(ActionEntity trigger)
    {
        Debug.Log("search enter");
        _controller.energyManager.SpendEnergy(1);

        // Al entrar, configuramos animaciones, flags, etc.
        _controller.animator.SetBool("Teleporting", false);
        _controller.animator.SetBool("Runing", true);

        _controller.audioManager.PlayStateAudio(ActionEntity.Search);
        _controller.audioManager.StopStateAudio(ActionEntity.Chase);


        /*if (_controller.searchSound != null)
        {
            _controller.audioSource.clip = _controller.searchSound;
            _controller.audioSource.Play();
        }*/
    }

    protected override void OnStateUpdate()
    {
        if (_controller._target == null)
            return;

        // 1) Patrullamos de forma random
        PatrolMovement();

        // 2) Comprobamos si detectamos al jugador
        if (_controller.visionDetector.IsPlayerDetected ||
            _controller.globalDetector.IsPlayerDetected)
        {
            Debug.Log("Player detected during search!");
            // Actualizamos un estado global si quieres
            WorldStateManager.instance.SetState("PlayerDetected", true);

            // Pasamos al siguiente paso en el plan
            _controller.FSM.Feed(ActionEntity.NextStep);
        }
    }

    protected override void OnStateExit(ActionEntity trigger)
    {
        Debug.Log("search exit");
    }

    /// <summary>
    /// Lógica de patrullaje aleatorio y rotación
    /// </summary>
    private void PatrolMovement()
    {
        // Si no estamos moviendo, elegimos un destino aleatorio
        if (!_controller.movement.IsMoving)
        {
            Vector3 randomTarget = GetRandomPosition();
            _controller.movement.MoveTo(randomTarget);
        }

        // Si se llegó al destino, elegimos uno nuevo
        if (_controller.movement.HasReachedDestination())
        {
            Vector3 newTarget = GetRandomPosition();
            _controller.movement.MoveTo(newTarget);
        }

        // Rotamos hacia la dirección de movimiento
        if (_controller.movement.IsMoving)
        {
            Vector3 direction = _controller.movement.CurrentTarget - _controller.transform.position;
            direction.y = 0;
            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _controller.transform.rotation = Quaternion.Slerp(
                    _controller.transform.rotation,
                    targetRotation,
                    Time.deltaTime * 3
                );
            }
        }
    }

    /// <summary>
    /// Generar posición aleatoria dentro de un rango
    /// </summary>
    private Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(0, 40),
            0,
            Random.Range(0, 40)
        );
    }
}

