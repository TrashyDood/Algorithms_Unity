using System;
using System.Collections.Generic;
using UnityEngine;

public enum Orientation
{
    NORTH,
    EAST,
    UP,
    SOUTH,
    WEST,
    DOWN
}

public class CreatureBuilder : MonoBehaviour
{
    
}

[Serializable]
public class Body
{
    BodyCube[,,] _bodyCubes = new BodyCube[7, 7, 7];
    
    public Vector3 MaxSize => new(_bodyCubes.GetLength(0),
        _bodyCubes.GetLength(1),
        _bodyCubes.GetLength(2));
    
    public Vector3Int Center => new(
        Mathf.CeilToInt(_bodyCubes.GetLength(0) * 0.5f),
        Mathf.CeilToInt(_bodyCubes.GetLength(1) * 0.5f),
        Mathf.CeilToInt(_bodyCubes.GetLength(2) * 0.5f));
    
    public BodyCube this[int x, int y, int z] => _bodyCubes[x, y, z];
    public BodyCube this[Vector3Int position] => _bodyCubes[position.x, position.y, position.z];

    float CalcMass()
    {
        return 0;
    }
}

[Serializable]
public class Bodypart
{
    
}

[Serializable]
public class BodyCube : Bodypart
{
    
}