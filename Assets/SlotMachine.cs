//using UnityEngine;

//public class SlotMachine : MonoBehaviour
//{
//    // Example symbols
//    string[] symbols = { "Cherry", "Lemon", "Orange", "Bell", "Seven" };

//    string slot1;
//    string slot2;
//    string slot3;

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.D))
//        {
//            Debug.Log("Input");
//            Spin();
//        }
//    }

//    public void Spin()
//    {
//        // Randomly pick symbols
//        slot1 = symbols[Random.Range(0, symbols.Length)];
//        slot2 = symbols[Random.Range(0, symbols.Length)];
//        slot3 = symbols[Random.Range(0, symbols.Length)];

//        Debug.Log($"Spin Results: {slot1}, {slot2}, {slot3}");

//        CheckResult();
//    }

//    void CheckResult()
//    {
//        if (slot1 == slot2 && slot2 == slot3)
//        {
//            Debug.Log("Jackpot! All symbols match!");
//        }
//        else
//        {
//            Debug.Log("No match. Try again!");
//        }
//    }
//}

using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding
{
    public class Node
    {
        public Vector2Int position;
        public int gCost;
        public int hCost;
        public Node parent;

        public int FCost => gCost + hCost;

        public Node(Vector2Int pos)
        {
            position = pos;
        }
    }

    public static List<Vector2Int> FindPath(Vector2Int start, Vector2Int goal, bool[,] walkableMap)
    {
        List<Node> openList = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        Node startNode = new Node(start);
        Node goalNode = new Node(goal);

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Node currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].FCost < currentNode.FCost ||
                    (openList[i].FCost == currentNode.FCost && openList[i].hCost < currentNode.hCost))
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode.position == goalNode.position)
            {
                return RetracePath(startNode, currentNode);
            }

            foreach (Node neighbor in GetNeighbors(currentNode, walkableMap))
            {
                if (closedSet.Contains(neighbor))
                    continue;

                int tentativeGCost = currentNode.gCost + GetDistance(currentNode, neighbor);

                bool isInOpen = openList.Exists(n => n.position == neighbor.position);
                if (!isInOpen || tentativeGCost < neighbor.gCost)
                {
                    neighbor.gCost = tentativeGCost;
                    neighbor.hCost = GetDistance(neighbor, goalNode);
                    neighbor.parent = currentNode;

                    if (!isInOpen)
                        openList.Add(neighbor);
                }
            }
        }

        return null; // No path found
    }

    private static List<Vector2Int> RetracePath(Node startNode, Node endNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.position);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    private static List<Node> GetNeighbors(Node node, bool[,] map)
    {
        List<Node> neighbors = new List<Node>();
        Vector2Int[] directions = {
            Vector2Int.up, Vector2Int.down,
            Vector2Int.left, Vector2Int.right
        };

        foreach (var dir in directions)
        {
            Vector2Int newPos = node.position + dir;
            if (newPos.x >= 0 && newPos.x < map.GetLength(0) &&
                newPos.y >= 0 && newPos.y < map.GetLength(1) &&
                map[newPos.x, newPos.y])
            {
                neighbors.Add(new Node(newPos));
            }
        }

        return neighbors;
    }

    private static int GetDistance(Node a, Node b)
    {
        return Mathf.Abs(a.position.x - b.position.x) + Mathf.Abs(a.position.y - b.position.y);
    }
}
