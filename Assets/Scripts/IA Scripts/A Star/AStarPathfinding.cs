using System;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding
{
    public static List<T> FindPath(T startNode, T endNode)
    {
        var openSet = new PriorityQueue<T>();
        var closedSet = new HashSet<T>();
        var cameFrom = new Dictionary<T, T>();
        var gScore = new Dictionary<T, float>();
        var fScore = new Dictionary<T, float>();

        openSet.Put(startNode, 0);
        gScore[startNode] = 0;
        fScore[startNode] = Heuristic(startNode, endNode);

        while (!openSet.IsEmpty())
        {
            var current = openSet.Get();

            if (current.Equals(endNode))
            {
                return ReconstructPath(cameFrom, current);
            }

            closedSet.Add(current);

            foreach (var neighbor in current.Neighbors)
            {
                if (closedSet.Contains(neighbor)) continue;

                // Lazy evaluation: calcular el costo solo cuando es necesario
                if (!gScore.ContainsKey(neighbor))
                {
                    // Posponemos el cálculo del costo hasta que sea necesario
                    float tentativeGScore = LazyCost(current, neighbor);
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = tentativeGScore + Heuristic(neighbor, endNode);
                    openSet.Put(neighbor, fScore[neighbor]);
                    cameFrom[neighbor] = current;
                }
                else
                {
                    // Si ya hemos calculado el costo, verificamos si encontramos un camino mejor
                    float tentativeGScore = gScore[current] + LazyCost(current, neighbor);
                    if (tentativeGScore < gScore[neighbor])
                    {
                        gScore[neighbor] = tentativeGScore;
                        fScore[neighbor] = tentativeGScore + Heuristic(neighbor, endNode);
                        cameFrom[neighbor] = current;

                        // Actualizamos la prioridad del vecino en la cola
                        if (openSet.TryGetValue(neighbor, out float oldPriority))
                        {
                            openSet.Put(neighbor, fScore[neighbor]);
                        }
                    }
                }
            }
        }

        return null; // No se encontró un camino
    }

    private static float Heuristic(T a, T b)
    {
        return Vector3.Distance(a.Position, b.Position);
    }

    private static float LazyCost(T a, T b)
    {
        // Aquí puedes agregar lógica para calcular el costo de manera perezosa
        // Por ejemplo, verificar si el nodo está bloqueado o calcular dinámicamente el costo
        return Vector3.Distance(a.Position, b.Position);
    }

    private static List<T> ReconstructPath(Dictionary<T, T> cameFrom, T current)
    {
        var path = new List<T> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }
        path.Reverse();
        return path;
    }
}

