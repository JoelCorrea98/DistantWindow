using UnityEngine;
using System.Collections.Generic;

public class AIMovement : MonoBehaviour
{
    public GridSystem gridSystem;
    private List<T> path;
    private int currentNodeIndex;
    public Vector3 CurrentTarget;

    /// <summary>
    /// Velocidad de movimiento de la IA.
    /// </summary>
    public float RunSpeed = 3f; // Velocidad pública configurable desde el Inspector.

    /// <summary>
    /// Indica si la IA está en movimiento.
    /// </summary>
    public bool IsMoving
    {
        get
        {
            return path != null && currentNodeIndex < path.Count;
        }
    }

    public void MoveTo(Vector3 targetPosition)
    {
        T startNode = gridSystem.GetClosestNode(transform.position);
        T endNode = gridSystem.GetClosestNode(targetPosition);

        if (startNode == null || endNode == null)
        {
            Debug.LogWarning("StartNode or EndNode is null. Cannot calculate path.");
            path = null;
            return;
        }

        path = AStarPathfinding.FindPath(startNode, endNode);

        if (path == null || path.Count == 0)
        {
            Debug.LogWarning("Pathfinding failed. No valid path found.");
            return;
        }

        currentNodeIndex = 0;
        //Debug.Log($"Path found with {path.Count} nodes. Moving to target.");
    }

    private void Update()
    {
        if (path != null && currentNodeIndex < path.Count)
        {
            CurrentTarget = path[currentNodeIndex].Position;

            // Usa RunSpeed para controlar la velocidad del movimiento
            transform.position = Vector3.MoveTowards(transform.position, CurrentTarget, Time.deltaTime * RunSpeed);

            if ((path[currentNodeIndex].Position - transform.position).normalized != Vector3.zero)
                transform.forward = (path[currentNodeIndex].Position - transform.position).normalized;

            // Avanzar al siguiente nodo si está cerca del actual
            if (Vector3.Distance(transform.position, CurrentTarget) < 0.1f)
            {
                currentNodeIndex++;
            }
        }
        else if (path != null && currentNodeIndex >= path.Count)
        {
            Debug.Log("Destination reached.");
            path = null; // Reinicia el camino para permitir nuevas órdenes
        }
    }

    public bool HasReachedDestination()
    {
        return path != null && currentNodeIndex >= path.Count;
    }
}



