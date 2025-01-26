using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABlockHability : MonoBehaviour
{
    [Header("Configuración")]
    public List<Transform> spawnPoints; // Lista de puntos de spawn
    public GameObject objectPrefab; // Prefab del objeto a spawnear
    public float despawnTime = 5f; // Tiempo antes de que los objetos desaparezcan
    public float spawnDelay = 1f; // Tiempo entre cada spawn
    public float startDelay = 3f; // Tiempo antes de iniciar el spawn
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip clip;


    private List<GameObject> spawnedObjects = new List<GameObject>(); // Lista de objetos actualmente spawneados

    /// <summary>
    /// Función que spawnea 3 objetos en los puntos de spawn más cercanos al jugador.
    /// </summary>
    /// <param name="playerPosition">Posición del jugador.</param>
    public void SpawnClosestObjects(Vector3 playerPosition)
    {
        // Verificar que haya spawn points disponibles
        if (spawnPoints == null || spawnPoints.Count < 3)
        {
            Debug.LogWarning("No hay suficientes spawn points configurados.");
            return;
        }

        // Ordenar los puntos de spawn por distancia al jugador
        spawnPoints.Sort((a, b) =>
            Vector3.Distance(playerPosition, a.position).CompareTo(Vector3.Distance(playerPosition, b.position)));

        // Iniciar el proceso de spawn con retraso inicial
        StartCoroutine(SpawnWithDelays());
    }

    /// <summary>
    /// Corutina para spawnear 3 objetos con delay entre cada spawn.
    /// </summary>
    private IEnumerator SpawnWithDelays()
    {
        animator.SetBool("Runing", false);
        animator.SetBool("Cast", true);
        playSound();
        yield return new WaitForSeconds(startDelay); // Esperar el delay inicial

        for (int i = 0; i < 3; i++)
        {
            Transform spawnPoint = spawnPoints[i];
            GameObject spawnedObject = Instantiate(objectPrefab, spawnPoint.position, spawnPoint.rotation);
            spawnedObjects.Add(spawnedObject);

            yield return new WaitForSeconds(spawnDelay); // Esperar antes de spawnear el siguiente objeto
        }

        // Iniciar proceso para eliminar los objetos después de un tiempo
        StartCoroutine(DespawnObjects());
    }

    /// <summary>
    /// Corutina para eliminar los objetos spawneados después de un tiempo.
    /// </summary>
    private IEnumerator DespawnObjects()
    {
        yield return new WaitForSeconds(despawnTime);

        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        // Limpiar la lista de objetos spawneados
        spawnedObjects.Clear();
        animator.SetBool("Cast", false);
        animator.SetBool("Runing", true);
    }

    public void playSound()
    {
        if (clip != null)
        {
            audioSource.clip = clip;
        }

        // Reproducir el audio
        audioSource.Play();
    }
}
