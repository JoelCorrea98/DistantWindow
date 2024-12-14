using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public int GridSizeX;
    public int GridSizeZ;
    public float NodeSize;
    public LayerMask ObstacleLayer;
    public Transform ReferencePoint; // Punto de referencia para la grilla

    private Node[,] grid;

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        if (ReferencePoint == null)
        {
            Debug.LogError("ReferencePoint no asignado. Por favor, arrastra un GameObject al campo ReferencePoint.");
            return;
        }

        grid = new Node[GridSizeX, GridSizeZ];

        Vector3 origin = ReferencePoint.position; // Usamos la posición del punto de referencia

        for (int x = 0; x < GridSizeX; x++)
        {
            for (int z = 0; z < GridSizeZ; z++)
            {
                // Calcular la posición del nodo basado en el punto de referencia
                Vector3 worldPosition = new Vector3(
                    origin.x + (x * NodeSize),
                    origin.y,
                    origin.z + (z * NodeSize)
                );

                // Comprobar si el nodo es caminable
                bool isWalkable = !Physics.CheckSphere(worldPosition, NodeSize / 2, ObstacleLayer);

                if (isWalkable)
                {
                    grid[x, z] = new Node(worldPosition);
                }
                else
                {
                    grid[x, z] = null; // Nodo no caminable
                }
            }
        }

        ConnectNeighbors();
    }

    private void ConnectNeighbors()
    {
        for (int x = 0; x < GridSizeX; x++)
        {
            for (int z = 0; z < GridSizeZ; z++)
            {
                Node node = grid[x, z];
                if (node == null) continue;

                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dz = -1; dz <= 1; dz++)
                    {
                        if (dx == 0 && dz == 0) continue;

                        int neighborX = x + dx;
                        int neighborZ = z + dz;

                        if (neighborX >= 0 && neighborX < GridSizeX && neighborZ >= 0 && neighborZ < GridSizeZ)
                        {
                            Node neighbor = grid[neighborX, neighborZ];
                            if (neighbor != null)
                            {
                                node.Neighbors.Add(neighbor);
                            }
                        }
                    }
                }
            }
        }
    }

    public Node GetClosestNode(Vector3 position)
    {
        int x = Mathf.RoundToInt((position.x - ReferencePoint.position.x) / NodeSize);
        int z = Mathf.RoundToInt((position.z - ReferencePoint.position.z) / NodeSize);

        if (x >= 0 && x < GridSizeX && z >= 0 && z < GridSizeZ)
        {
            return grid[x, z];
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        if (grid == null) return;

        for (int x = 0; x < GridSizeX; x++)
        {
            for (int z = 0; z < GridSizeZ; z++)
            {
                Node node = grid[x, z];
                if (node != null)
                {
                    // Color para nodos caminables
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(node.Position, Vector3.one * NodeSize);

                    // Si quieres resaltar conexiones entre nodos
                    foreach (var neighbor in node.Neighbors)
                    {
                        Gizmos.color = Color.cyan;
                        Gizmos.DrawLine(node.Position, neighbor.Position);
                    }
                }
                else
                {
                    // Color para nodos no caminables
                    Gizmos.color = Color.red;
                    Vector3 worldPosition = new Vector3(x * NodeSize, 0, z * NodeSize);
                    Gizmos.DrawWireCube(worldPosition, Vector3.one * NodeSize);
                }
            }
        }
    }
}


