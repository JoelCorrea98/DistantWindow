using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEngine.Rendering.DebugUI;
using System.Runtime.CompilerServices;
/*
[System.Serializable]
public class WorldStateEntry
{
    public string Key; // La clave del estado
    public string ValueType; // El tipo de valor (para referencia)
    public string ValueAsString; // Representación en formato de texto

    // Convertir el valor almacenado al tipo real
    public object GetValue()
    {
        switch (ValueType)
        {
            case "System.Int32":
                return int.Parse(ValueAsString);
            case "System.Single":
                return float.Parse(ValueAsString);
            case "System.Boolean":
                return bool.Parse(ValueAsString);
            default:
                return ValueAsString; // Asume que es string
        }
    }

    // Asignar el valor con el tipo correcto
    public void SetValue(object value)
    {
        ValueType = value.GetType().ToString();
        ValueAsString = value.ToString();
    }
}
*/
public class WorldStateManager : MonoBehaviour
{
    /*
    [SerializeField]
    private List<WorldStateEntry> state = new List<WorldStateEntry>();
    */
    public static WorldStateManager instance;
    private GOAPState _worldState;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _worldState = new GOAPState();
            _worldState.worldState.values.Add("PlayerAlive", true); //y acá le inicializamos todas las cosas que no se actualicen automaticamente
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Agregar o actualizar una entrada
    public void SetState(string key, object value)
    {
        if (!_worldState.worldState.values.ContainsKey(key))
        {
            _worldState.worldState.values.Add(key, value);
        }
        else
        {
            _worldState.worldState.values[key] = value;
        }
    }

    // Obtener un valor del estado
    public object GetState(string key)
    {
        if (_worldState.worldState.values.ContainsKey(key))
        {
           return  _worldState.worldState.values[key];
        }
        else
        {
            return null;
        }
    }

    public GOAPState GetAllStates()
    {
        GOAPState currentWorldState = _worldState;
        return currentWorldState;
    }

    // Depuración para mostrar el estado completo
    public void DebugState()
    {
        foreach (var entry in _worldState.worldState.values)
        {
            Debug.Log($"Key: {entry.Key}, Value: {entry.Value.ToString()}, Type: {entry.Value.GetType()}");
        }
    }
}
