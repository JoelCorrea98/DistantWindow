using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

public class WorldState : MonoBehaviour
{
    [SerializeField]
    private List<WorldStateEntry> state = new List<WorldStateEntry>();

    // Agregar o actualizar una entrada
    public void SetState(string key, object value)
    {
        var entry = state.Find(e => e.Key == key);
        if (entry != null)
        {
            entry.SetValue(value);
        }
        else
        {
            var newEntry = new WorldStateEntry { Key = key };
            newEntry.SetValue(value);
            state.Add(newEntry);
        }
    }

    // Obtener un valor del estado
    public object GetState(string key)
    {
        var entry = state.Find(e => e.Key == key);
        return entry != null ? entry.GetValue() : null;
    }

    public Dictionary<string, object> GetAllStates()
    {
        var stateDictionary = new Dictionary<string, object>();
        foreach (var entry in state)
        {
            stateDictionary[entry.Key] = entry.GetValue();
        }
        return stateDictionary;
    }

    // Depuración para mostrar el estado completo
    public void DebugState()
    {
        foreach (var entry in state)
        {
            Debug.Log($"Key: {entry.Key}, Value: {entry.ValueAsString}, Type: {entry.ValueType}");
        }
    }
}
