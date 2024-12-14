using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding
{
    public static List<Node> FindPath(Node startNode, Node endNode)
    {
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
        Dictionary<Node, float> gScore = new Dictionary<Node, float>();
        Dictionary<Node, float> fScore = new Dictionary<Node, float>();

        openSet.Add(startNode);
        gScore[startNode] = 0;
        fScore[startNode] = Heuristic(startNode, endNode);

        while (openSet.Count > 0)
        {
            Node current = openSet[0];
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

            foreach (Node neighbor in current.Neighbors)
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

    private static float Heuristic(Node a, Node b)
    {
        return Vector3.Distance(a.Position, b.Position);
    }

    private static List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)
    {
        List<Node> path = new List<Node> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }
        path.Reverse();
        return path;
    }
}

