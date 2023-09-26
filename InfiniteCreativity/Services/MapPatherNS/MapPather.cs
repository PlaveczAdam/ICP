using System;
using System.Collections.Generic;
using System.Linq;
using InfiniteCreativity.DTO.Game;
using InfiniteCreativity.Models.GameNS;

namespace InfiniteCreativity.Services.MapPatherNS;

public class MapPather
{
    public GraphPath? FindPathsUsingDijkstra(HexTileDataObject startTile, int maxDistance, HexTileDataObject endTile)
    {
        var startNode = new GraphNode(startTile)
        {
            TotalCost = 0
        };
        var openNodes = new PriorityQueue<GraphNode>();
        openNodes.Enqueue(startNode, 0);
        var closedNodes = new HashSet<GraphNode>();
        while (!openNodes.IsEmpty())
        {
            var currentNode = openNodes.Dequeue();
            if (currentNode.TotalCost > maxDistance)
            {
                return null;
            }
            if (currentNode.Data == endTile)
            {
                return GeneratePath(currentNode);
            }
            closedNodes.Add(currentNode);
            var neighbours = currentNode.Data.GetNeighbours();
            var currentOnWater = currentNode.Data.IsWater();
            foreach (var neighbour in neighbours)
            {
                var neighbourNode = openNodes.FirstOrDefault(x => x.Data == neighbour) ??
                                    new GraphNode(neighbour);
                if (!neighbour.IsWalkable())
                {
                    closedNodes.Add(neighbourNode);
                }

                if (closedNodes.Contains(neighbourNode))
                {
                    continue;
                }

                var newCost = currentNode.TotalCost + 1 + (currentOnWater != neighbour.IsWater() ? maxDistance : 0);

                if (openNodes.Contains(neighbourNode))
                {
                    if (newCost >= neighbourNode.TotalCost)
                    {
                        continue;
                    }

                    openNodes.Remove(neighbourNode);
                }

                neighbourNode.TotalCost = newCost;
                neighbourNode.Parent = currentNode;
                openNodes.Enqueue(neighbourNode, neighbourNode.TotalCost);
            }
        }
        return null;
    }

    private GraphPath? GeneratePath(GraphNode currentNode)
    {
        var path = new GraphPath() { 
            Nodes = new List<GraphNode>() { },
        };
        var curr = currentNode;
        while(curr != null) { 
            path.Nodes.Add(curr);
            curr = curr.Parent;
        }
        path.Nodes.Reverse();
        path.Nodes.RemoveAt(0);
        return path;

    }
}