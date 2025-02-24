using UnityEngine;
using System.Collections;

public class TeleportState : IAState
{
    public TeleportState(IAController controller) : base(controller, "Teleport")
    {
    }

    protected override void OnStateEnter(ActionEntity trigger)
    {
        Debug.Log("teleport enter");
        _controller.energyManager.SpendEnergy(4);

        // Iniciamos teletransporte (corrutina)
        _controller.StartCoroutine(TeleportRoutine());
    }

    protected override void OnStateUpdate()
    {
        // Podr�as poner l�gica adicional aqu� si quisieras
    }

    protected override void OnStateExit(ActionEntity trigger)
    {
        Debug.Log("teleport exit");
    }

    private IEnumerator TeleportRoutine()
    {
        // Animaciones
        _controller.animator.SetBool("Runing", false);
        _controller.animator.SetBool("Teleporting", true);

        // Audio
        if (_controller.teleportAudio != null)
        {
            _controller.audioSource.clip = _controller.teleportAudio;
            _controller.audioSource.Play();
        }

        // Ajustar dimensi�n a la del jugador
        WorldStateManager.instance.SetState("EnemyDimension",
            WorldStateManager.instance.GetState("PlayerDimension"));
        _controller.visionDetector.enabled = true;
        _controller.attackCollider.enabled = true;
        _controller._characterMesh.SetActive(true);

        // Teletransporte en s�
        if (_controller._target != null)
        {
            Vector3 currentPosition = _controller.transform.position;
            Vector3 targetPosition = _controller._target.position;
            Vector3 directionToTarget = targetPosition - currentPosition;

            // Te acercas un 60%
            Vector3 newPosition = currentPosition + directionToTarget * 0.6f;
            _controller.transform.position = newPosition;
            Debug.Log($"Teletransportado a {newPosition}");
        }

        // Esperar `teleportDelay`
        yield return new WaitForSeconds(_controller.teleportDelay);

        // Terminar animaci�n
        _controller.animator.SetBool("Teleporting", false);
        _controller.animator.SetBool("Runing", true);

        // Al terminar, alimentamos el FSM
        _controller.FSM.Feed(ActionEntity.NextStep);
    }
}

