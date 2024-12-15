using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T
{
    public Vector3 Position; // Posición del nodo en el mundo
    public List<T> Neighbors; // Lista de nodos conectados

    public T(Vector3 position)
    {
        Position = position;
        Neighbors = new List<T>();
    }
}

