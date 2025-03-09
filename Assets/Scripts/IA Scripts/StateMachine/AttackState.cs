using UnityEngine;
using System.Collections;

public class AttackState : IAState
{
    private bool _isAttacking;
    private bool _attackSuccessful;
    private bool _KeepRunningAttack;

    public AttackState(IAController controller) : base(controller, "Attack")
    {
    }

    protected override void OnStateEnter(ActionEntity trigger)
    {
        Debug.Log("attack enter");
        _KeepRunningAttack = true;

        if (_controller.IsPlayerInAttackRange)
        {
            _controller.energyManager.SpendEnergy(2);

            // Girar hacia el jugador
            var dir = (_controller._target.position - _controller.transform.position).normalized;
            dir.y = 0;
            if (dir != Vector3.zero)
                _controller.transform.forward = dir;

            // Podemos lanzar la corrutina aquí o en OnUpdate
            _controller.StartCoroutine(AttackRoutine());
        }
        else
        {
            // Si no está en rango, fallamos inmediatamente
            _controller.FSM.Feed(ActionEntity.FailedStep);
        }
    }

    protected override void OnStateUpdate()
    {
        // Aquí podrías manejar, si fuera necesario, más lógica durante el ataque
        // Por ej.: chequeos de si el jugador salió de rango en pleno ataque, etc.

        if (_KeepRunningAttack)
        {
            _controller.movement.MoveDirectlyTo(_controller._target.position);
        }
    }

    protected override void OnStateExit(ActionEntity trigger)
    {
        Debug.Log("attack exit");
    }

    private IEnumerator AttackRoutine()
    {
        Debug.Log("Ataque (corrutina) en ejecución");
        _isAttacking = true;
        _attackSuccessful = false;

        // Configuramos animaciones
        _controller.animator.SetBool("Runing", false);
        _controller.animator.SetTrigger("TriggerAttackAnim");
        _controller.animator.SetLayerWeight(1, 1);
        //Sonido
        _controller.audioManager.PlayStateAudio(ActionEntity.Attack);


        /*if (_controller.attacksound != null)
        {
            _controller.audioSource.clip = _controller.attacksound;
            _controller.audioSource.Play();
        }*/


        // Simular preparación del ataque
        yield return new WaitForSeconds(_controller.attackDelay);

        // Activar el collider de ataque
        if (_controller.attackHitCollider != null)
        {
            _controller.attackHitCollider.enabled = true;

            // Comprobar colisiones
            var hitColliders = Physics.OverlapBox(
                _controller.attackHitCollider.bounds.center,
                _controller.attackHitCollider.bounds.extents,
                _controller.attackHitCollider.transform.rotation,
                _controller.playerLayer
            );

            foreach (var hit in hitColliders)
            {
                if (hit.gameObject == _controller.player)
                {
                    _controller.player.GetComponent<PlayerLife>()?.TakeDamage(_controller.attackDamage);
                    _attackSuccessful = true;
                    break;
                }
            }

            // Desactivar el collider
            _controller.attackHitCollider.enabled = false;
            _controller.animator.SetLayerWeight(1, 0);
        }
        _controller.audioManager.StopStateAudio(ActionEntity.Attack);

        // Esperar un pequeño tiempo tras el ataque
        yield return new WaitForSeconds(2.5f);


        // Avisar al FSM
        if (!_attackSuccessful)
            _controller.FSM.Feed(ActionEntity.FailedStep);
        else
            _controller.FSM.Feed(ActionEntity.NextStep);

        _KeepRunningAttack = false;
        yield return new WaitForSeconds(1f);
        _KeepRunningAttack = true;
        _isAttacking = false;

        Debug.Log($"Ataque {(_attackSuccessful ? "exitoso" : "fallido")}");
    }
}

