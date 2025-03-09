using UnityEngine;
using System.Collections.Generic;

public class IAAudioManager : MonoBehaviour
{
    // Clase para asociar un estado con un clip de audio
    [System.Serializable]
    public class StateAudio
    {
        public ActionEntity state; // Estado asociado al audio
        public AudioClip audioClip; // Clip de audio
        public bool loop = true; // Si el audio debe repetirse en bucle
        [Range(0f, 1f)] public float volume = 1f; // Volumen del audio (rango de 0 a 1)
    }

    // Lista de estados y sus respectivos audios
    public List<StateAudio> stateAudios;

    // Lista de AudioSources para manejar múltiples sonidos
    private List<AudioSource> audioSources;

    // Estados activos actualmente
    private HashSet<ActionEntity> activeStates = new HashSet<ActionEntity>();

    void Awake()
    {
        Debug.Log("Audio Manager Awake llamado");

        // Inicializar la lista de AudioSources
        audioSources = new List<AudioSource>();

        // Crear varios AudioSources (puedes ajustar el número según tus necesidades)
        for (int i = 0; i < 5; i++) // 5 es un número arbitrario, puedes cambiarlo
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            audioSources.Add(newSource);
        }
    }

    // Método para reproducir el audio correspondiente a un estado
    public void PlayStateAudio(ActionEntity state)
    {
        // Si el estado ya está activo, no hagas nada
        if (activeStates.Contains(state))
        {
            return;
        }

        // Buscar el clip de audio correspondiente al estado
        StateAudio stateAudio = stateAudios.Find(s => s.state == state);
        if (stateAudio == null || stateAudio.audioClip == null)
        {
            Debug.LogWarning($"No se encontró un AudioClip para el estado: {state}");
            return;
        }

        // Buscar un AudioSource disponible
        AudioSource availableSource = audioSources.Find(source => !source.isPlaying);
        if (availableSource == null)
        {
            Debug.LogWarning("No hay AudioSources disponibles para reproducir el sonido.");
            return;
        }

        // Configurar y reproducir el audio
        availableSource.clip = stateAudio.audioClip;
        availableSource.loop = stateAudio.loop;
        availableSource.volume = stateAudio.volume; // Configurar el volumen
        availableSource.Play();

        // Agregar el estado a la lista de estados activos
        activeStates.Add(state);
    }

    // Método para detener el audio de un estado específico
    public void StopStateAudio(ActionEntity state)
    {
        // Buscar el clip de audio correspondiente al estado
        StateAudio stateAudio = stateAudios.Find(s => s.state == state);
        if (stateAudio == null || stateAudio.audioClip == null)
        {
            Debug.LogWarning($"No se encontró un AudioClip para el estado: {state}");
            return;
        }

        // Buscar el AudioSource que está reproduciendo este clip
        AudioSource sourceToStop = audioSources.Find(source => source.clip == stateAudio.audioClip && source.isPlaying);
        if (sourceToStop != null)
        {
            sourceToStop.Stop();
        }

        // Remover el estado de la lista de estados activos
        activeStates.Remove(state);
    }

    // Método para detener todos los sonidos
    public void StopAllAudio()
    {
        foreach (var source in audioSources)
        {
            source.Stop();
        }
        activeStates.Clear();
    }
}
