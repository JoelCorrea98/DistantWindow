using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BlockState : IAState
{
    // Lista interna de objetos spawneados
    private List<GameObject> _spawnedObjects = new List<GameObject>();

    public BlockState(IAController controller) : base(controller, "Block")
    {
        // En este constructor, simplemente definimos el nombre del estado.
        // Ya que los datos los sacaremos directamente de _controller.xxx
    }

    protected override void OnStateEnter(ActionEntity trigger)
    {
        Debug.Log("block enter");
        // 1) Gastamos energía
        _controller.energyManager.SpendEnergy(4);

        // 2) Iniciamos la corrutina principal de bloqueo
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
        // Si necesitas limpiar algo justo al salir del estado, lo harías aquí
    }

    /// <summary>
    /// Corrutina que controla el spawn de objetos en los puntos más cercanos al jugador.
    /// </summary>
    private IEnumerator SpawnWithDelays()
    {
        // Podemos usar el MISMO animator general,
        // o uno especial que hayas referenciado como "blockAnimator"
        Animator animator = _controller.animator;

        // Activamos anim de "Cast"
        if (animator != null)
        {
            animator.SetBool("Runing", false);
            animator.SetBool("Cast", true);
        }

        // Reproducimos sonido (si existe)
        PlaySound();

        // Esperamos el tiempo de inicio
        yield return new WaitForSeconds(_controller.blockStartDelay);

        // Validamos que tengamos suficientes spawn points
        List<Transform> spawnPoints = _controller.blockSpawnPoints;
        if (spawnPoints == null || spawnPoints.Count < 3)
        {
            Debug.LogWarning("No hay suficientes spawn points configurados para BlockState.");
            // Salimos sin spawnear
            yield break;
        }

        // Ordenamos los puntos de spawn por distancia al jugador
        Vector3 playerPos = (_controller._target != null)
                            ? _controller._target.position
                            : _controller.transform.position;

        spawnPoints.Sort((a, b) =>
            Vector3.Distance(playerPos, a.position)
                    .CompareTo(Vector3.Distance(playerPos, b.position)));

        // Spawneamos 3 objetos
        for (int i = 0; i < 3; i++)
        {
            Transform spawnPoint = spawnPoints[i];
            GameObject spawnedObject = GameObject.Instantiate(
                _controller.blockObjectPrefab,
                spawnPoint.position,
                spawnPoint.rotation
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
        // _controller.FSM.Feed(ActionEntity.NextStep);
    }

    /// <summary>
    /// Reproduce el sonido asignado al "Block".
    /// </summary>
    private void PlaySound()
    {
        AudioSource audioSource = _controller.audioSource;

        AudioClip clip = _controller.blockAudioClip
                         ? _controller.blockAudioClip
                         : _controller.teleportAudio; // Ejemplo fallback

        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
