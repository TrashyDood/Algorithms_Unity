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
    [SerializeField]
    List<Bodypart> bodyparts = new();
    [SerializeField]
    Vector3Int maxSize;

    BodyCube _center;

    public Vector3Int MaxSize => maxSize;
    public Vector3Int Center => new(Mathf.CeilToInt(maxSize.x * 0.5f), Mathf.CeilToInt(maxSize.y * 0.5f), Mathf.CeilToInt(maxSize.z * 0.5f));

    public void Attach(int index, Orientation orientation, Bodypart toAttach)
    {
        bodyparts.Add(toAttach);
    }
}

[Serializable]
public class Bodypart
{
    public Vector3Int position;
    public float mass;

    public Bodypart(Vector3Int position, float mass)
    {
        this.position = position;
        this.mass = mass;
    }
}

[Serializable]
public class BodyCube : Bodypart
{
    public Bodypart[] slots = new Bodypart[6];

    public BodyCube(Vector3Int position, float mass) : base(position, mass) { }
}