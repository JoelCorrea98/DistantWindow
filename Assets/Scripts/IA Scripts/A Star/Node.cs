using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 Position; // Posición del nodo en el mundo
    public List<Node> Neighbors; // Lista de nodos conectados

    public Node(Vector3 position)
    {
        Position = position;
        Neighbors = new List<Node>();
    }
}

