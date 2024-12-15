using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding
{
    public static List<T> FindPath(T startNode, T endNode)
    {
        List<T> openSet = new List<T>();
        HashSet<T> closedSet = new HashSet<T>();
        Dictionary<T, T> cameFrom = new Dictionary<T, T>();
        Dictionary<T, float> gScore = new Dictionary<T, float>();
        Dictionary<T, float> fScore = new Dictionary<T, float>();

        openSet.Add(startNode);
        gScore[startNode] = 0;
        fScore[startNode] = Heuristic(startNode, endNode);

        while (openSet.Count > 0)
        {
            T current = openSet[0];
            foreach (var node in openSet)
            {
                if (fScore[node] < fScore[current])
                {
                    current = node;
                }
            }

            if (current == endNode)
            {
                return ReconstructPath(cameFrom, current);
            }

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (T neighbor in current.Neighbors)
            {
                if (closedSet.Contains(neighbor)) continue;

                float tentativeGScore = gScore[current] + Vector3.Distance(current.Position, neighbor.Position);

                if (!openSet.Contains(neighbor))
                {
                    openSet.Add(neighbor);
                }
                else if (tentativeGScore >= gScore[neighbor])
                {
                    continue;
                }

                cameFrom[neighbor] = current;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, endNode);
            }
        }

        return null; // No se encontró un camino
    }

    private static float Heuristic(T a, T b)
    {
        return Vector3.Distance(a.Position, b.Position);
    }

    private static List<T> ReconstructPath(Dictionary<T, T> cameFrom, T current)
    {
        List<T> path = new List<T> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }
        path.Reverse();
        return path;
    }
}

