using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class PathFinding
{
    public static Dictionary<T, Node<T>> FindPath<T>(in T origin, in T target, out Node<T> endPoint,
        in Func<T, T, float> getGCost, in Func<T, T, float> getHCost, in Func<T, float> getWeight,
        in int maxIterations = 1000)
        where T : IEnumerable<T>, IEquatable<T>
    {
        Node<T> currentNode;
        Dictionary<T, Node<T>> nodes = new();
        HashSet<T> visited = new(),
            unvisited = new();

        unvisited.Add(origin);
        nodes[origin] = endPoint = new(origin, default, 0, getHCost(origin, target), getWeight(origin));

        for (int i = 0; i < maxIterations && unvisited.Count > 0; i++)
        {
            currentNode = nodes[unvisited.OrderBy(t => nodes[t].FCost).First()]; // Replace with minheap.

            if (endPoint.HCost < currentNode.HCost)
                endPoint = currentNode;

            if (currentNode.Equals(target))
                break;

            unvisited.Remove(currentNode.Value);
            visited.Add(currentNode.Value);

            foreach (var neighbour in currentNode.Value)
            {
                if (visited.Contains(neighbour))
                    continue;

                float newGCost = currentNode.GCost + getGCost(currentNode.Value, neighbour);

                if (!nodes.TryGetValue(neighbour, out Node<T> neighbourNode))
                {
                    neighbourNode = new(neighbour, currentNode, newGCost, getHCost(neighbour, target), getWeight(neighbour));
                    nodes.Add(neighbour, neighbourNode);
                    unvisited.Add(neighbour);
                }
                else if (newGCost < neighbourNode.GCost)
                {
                    neighbourNode.GCost = newGCost;
                    neighbourNode.Previous = currentNode;
                }
            }
        }

        return nodes;
    }

    public static Path<T> GetPath<T>(this Dictionary<T, Node<T>> nodes, in T origin, in T target, in Node<T> endPoint)
        where T : IEnumerable<T>, IEquatable<T> =>
            new Path<T>(nodes, origin, target, endPoint);

    public class Node<T> where T : IEnumerable<T>, IEquatable<T>
    {
        public T Value;
        public Node<T> Previous;
        public float GCost = int.MaxValue,
            HCost = int.MaxValue,
            Weight;

        public float FCost => (GCost + HCost) * Weight;

        public Node(in T value, in Node<T> previous, in float gCost, in float hCost, in float weight)
        {
            Value = value;
            Previous = previous;
            GCost = gCost;
            HCost = hCost;
            Weight = weight;
        }
    }

    public struct Path<T> : IEnumerable<T> where T : IEnumerable<T>, IEquatable<T>
    {
        public Dictionary<T, Node<T>> Nodes;
        public T Origin, Target;
        public Node<T> EndPoint;

        public Path(in Dictionary<T, Node<T>> nodes, in T origin, in T target, in Node<T> endPoint)
        {
            Nodes = nodes;
            Origin = origin;
            Target = target;
            EndPoint = endPoint;
        }

        public IEnumerable<T> Construct() =>
            this.Reverse();

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = EndPoint;

            while (current != null)
            {
                yield return current.Value;
                current = current.Previous;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
