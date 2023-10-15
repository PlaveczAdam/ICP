using System;
using InfiniteCreativity.DTO.Game;
using InfiniteCreativity.Models.GameNS;

namespace InfiniteCreativity.Services.MapPatherNS;

public class GraphNode : IEquatable<GraphNode>
{
    public readonly HexTileDataObject Data;
    public int TotalCost = 0;
    public GraphNode? Parent = null;

    public GraphNode(HexTileDataObject data)
    {
        Data = data;
    }

    public bool Equals(GraphNode? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || Equals(Data, other.Data);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((GraphNode)obj);
    }

    public override int GetHashCode()
    {
        return Data.GetHashCode();
    }
}