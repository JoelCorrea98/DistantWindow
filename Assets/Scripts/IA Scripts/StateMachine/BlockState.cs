using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BlockState : IAState
{
    // Lista interna de objetos spawneados
    private List<GameObject> _spawnedObjects = new List<GameObject>();
    private Animator animator;

    public BlockState(IAController controller) : base(controller, "Block")
    {
        // En este constructor, simplemente definimos el nombre del estado.
        // Ya que los datos los sacaremos directamente de _controller.xxx
    }

    protected override void OnStateEnter(ActionEntity trigger)
    {
        Debug.Log("block enter");
        animator = _controller.animator;
        // 1) Gastamos energía
        _controller.energyManager.SpendEnergy(4);
        //_controller.audioManager.StopAllAudio();
        _controller.audioManager.PlayStateAudio(ActionEntity.Block);


        // 2) Iniciamos la corrutina principal de bloqueo
        //_controller.blockHability.SpawnClosestObjects(_controller.player.transform.position);
        _controller.StartCoroutine(SpawnWithDelays());
    }

    protected override void OnStateUpdate()
    {
        // Si quisieras añadir lógica de "mientras está bloqueando", lo harías aquí
        Debug.Log("block update");
    }

    protected override void OnStateExit(ActionEntity trigger)
    {
        Debug.Log("block exit");
        animator.SetBool("Cast", false);
        _controller.audioManager.StopStateAudio(ActionEntity.Block);

        // Si necesitas limpiar algo justo al salir del estado, lo harías aquí
    }

    /// <summary>
    /// Corrutina que controla el spawn de objetos en los puntos más cercanos al jugador.
    /// </summary>
    private IEnumerator SpawnWithDelays()
    {
        // Activamos anim de "Cast"
        if (animator != null)
        {
            animator.SetBool("Runing", false);
            animator.SetBool("Cast", true);
        }

        yield return new WaitForSeconds(_controller.blockStartDelay);

        // Obtenemos la posición y dirección del jugador
        Vector3 playerPos = (_controller._target != null)
                            ? _controller._target.position
                            : _controller.transform.position;

        Vector3 playerForward = (_controller._target != null)
                                ? _controller._target.forward
                                : _controller.transform.forward;

        playerForward = -playerForward;

        // Definimos un rango de spawn y separación entre objetos
        float spawnDistance = 5f; // Distancia frente al jugador
        float spawnRange = 2f; // Rango aleatorio en el eje lateral
        float separation = 5f; // Separación entre objetos

        // Spawneamos 3 objetos
        for (int i = 0; i < 3; i++)
        {
            // Calculamos la posición de spawn
            float offsetX = Random.Range(-spawnRange, spawnRange);
            float offsetZ = spawnDistance + i * separation; // Añadimos separación en el eje forward

            Vector3 spawnOffset = new Vector3(offsetX, 0, offsetZ);
            Vector3 spawnPosition = playerPos + playerForward * spawnOffset.z + _controller.transform.right * spawnOffset.x;

            // Instanciamos el objeto
            GameObject spawnedObject = GameObject.Instantiate(
                _controller.blockObjectPrefab,
                spawnPosition,
                Quaternion.identity // Rotación por defecto
            );

            _spawnedObjects.Add(spawnedObject);

            // Esperamos el "spawnDelay" antes de spawnear el siguiente
            yield return new WaitForSeconds(_controller.blockSpawnDelay);
        }

        // Después de spawnear, iniciamos la destrucción programada
        _controller.StartCoroutine(DespawnObjects());
    }

    /// <summary>
    /// Destruir los objetos spawneados después de "despawnTime" segundos
    /// </summary>
    private IEnumerator DespawnObjects()
    {
        yield return new WaitForSeconds(_controller.blockDespawnTime);

        // Destruimos cada objeto
        foreach (var obj in _spawnedObjects)
        {
            if (obj != null)
                GameObject.Destroy(obj);
        }
        _spawnedObjects.Clear();

        // Reset de animaciones (si corresponde)
        Animator animator = _controller.animator;

        if (animator != null)
        {
            animator.SetBool("Cast", false);
            animator.SetBool("Runing", true);
        }

        // Si deseas, puedes forzar al FSM a pasar al siguiente estado cuando termina la habilidad
        _controller.FSM.Feed(ActionEntity.NextStep);
    }
}
