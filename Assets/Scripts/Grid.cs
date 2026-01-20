using System;
using UnityEngine;

[Serializable]
public struct Grid<T>
{
    T[] cells;

    public uint Width, Height;

    public T this[uint x, uint y]
    { 
        get => cells[x + y * Width];
        set => cells[x + y * Width] = value;
    }

    public int Length => cells.Length;

    public T this[Vector2Int coord]
    {
        get => this[(uint)coord.x, (uint)coord.y];
        set => this[(uint)coord.x, (uint)coord.y] = value;
    }

    public Grid(uint width, uint height)
    {
        Width = width;
        Height = height;
        cells = new T[width * height];
    }
}
