using UnityEngine;

public static class VectorUtils
{
    #region random
    public static Vector2 RandomVector2(float xMax, float yMax,float xMin = 0, float yMin = 0) =>
        new Vector2(UnityEngine.Random.Range(xMin, xMax), UnityEngine.Random.Range(yMin, yMax));

    public static Vector2 RandomVector2(Vector2 max, Vector3 min = default) =>
        RandomVector2(max.x, max.y, min.x, min.y);

    public static Vector2 RandomVector2(Rect bounds) =>
        RandomVector2(bounds.max.x, bounds.max.y, bounds.min.x, bounds.min.y);

    public static Vector3 RandomVector3(float xMax, float yMax, float zMax = 0,
        float xMin = 0, float yMin = 0, float zMin = 0) =>
        new Vector3(UnityEngine.Random.value * xMax + xMin,
            UnityEngine.Random.value * yMax + yMin,
            UnityEngine.Random.value * zMax + zMin);

    public static Vector3 RandomVector3(Vector3 max, Vector3 min = default) =>
        RandomVector3(max.x, max.y, max.z, min.x, min.y, min.z);

    public static Vector3 RandomVector3(Bounds bounds) =>
        RandomVector3(bounds.max.x, bounds.max.y, bounds.max.z,
            bounds.min.x, bounds.min.y, bounds.min.z);

    public static Vector3 RandomVector3(Rect bounds) =>
        RandomVector3(bounds.max.x, bounds.max.y, 0,
            bounds.min.x, bounds.min.y, 0);

    public static Vector3 RandomCircleEdge(float radius)
    {
        float angle = UnityEngine.Random.Range(0, Mathf.PI * 2);
        return new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
    }
    #endregion

    #region bounds
    public static bool OutOfBounds(float x, float y, float xMin, float xMax, float yMin, float yMax) =>
        !(x == Mathf.Clamp(x, xMin, xMax) &&
            y == Mathf.Clamp(y, yMin, yMax));

    public static bool OutOfBounds(Vector2 position, Vector2 min, Vector2 max) =>
        OutOfBounds(position.x, position.y, min.x, max.x, min.y, max.y);

    public static bool OutOfBounds(Vector2 position, Rect bounds) =>
        OutOfBounds(position.x, position.y, bounds.min.x, bounds.max.x, bounds.min.y, bounds.min.y);

    public static bool OutOfBounds(float x, float y, float z, float xMin, float xMax, float yMin, float yMax, float zMin, float zMax) =>
        !(x == Mathf.Clamp(x, xMin, xMax) &&
            y == Mathf.Clamp(y, yMin, yMax) &&
            z == Mathf.Clamp(z, zMin, zMax));

    public static bool OutOfBounds(Vector3 position, Vector3 min, Vector3 max) =>
        OutOfBounds(position.x, position.y, position.z, min.x, max.x, min.y, max.y, min.z, max.z);

    public static bool OutOfBounds(Vector3 position, Bounds bounds) =>
        OutOfBounds(position.x, position.y, position.z, bounds.min.x,
            bounds.max.x, bounds.min.y, bounds.max.y, bounds.min.z, bounds.max.z);

    public static Vector2 OutsideBounds(float x, float y, float xMin,
        float xMax, float yMin, float yMax)
    {
        // Check if both x and y axis are inside bounds.
        if (!OutOfBounds(x, y, xMin, xMax, yMin, yMax))
        {
            float distRight = Mathf.Abs(x - xMax),
                distLeft = Mathf.Abs(x - yMin),
                distUp = Mathf.Abs(y - yMax),
                distDown = Mathf.Abs(y - yMin),
                minDist = Mathf.Min(distRight, distLeft, distUp, distDown);

            x = (minDist == distRight) ? xMax : (minDist == distLeft) ? xMin : x;
            y = (minDist == distUp) ? yMax : (minDist == distDown) ? yMin : y;
        }

        return new Vector2(x, y);
    }

    public static Vector2 OutsideBounds(Vector2 position, Vector2 min, Vector2 max) =>
        OutsideBounds(position.x, position.y, min.x, max.x, min.y, max.y);

    public static Vector2 OutsideBounds(Vector2 position, Rect bounds) =>
        OutsideBounds(position.x, position.y, bounds.min.x,
           bounds.max.x, bounds.min.y, bounds.max.y);

    public static Vector3 OutsideBounds(float x, float y, float z, float xMin,
        float xMax, float yMin, float yMax, float zMin, float zMax)
    {
        // Check if both x and y axis are inside bounds.
        if (!OutOfBounds(x, y, z, xMin, xMax, yMin, yMax, zMin, zMax))
        {
            float distRight = Mathf.Abs(x - xMax),
                distLeft = Mathf.Abs(x - xMin),
                distUp = Mathf.Abs(y - yMax),
                distDown = Mathf.Abs(y - yMin),
                distForward = Mathf.Abs(z - zMax),
                distBack = Mathf.Abs(z - zMin),
                minDist = Mathf.Min(distRight, distLeft, distUp, distDown);

            x = (minDist == distRight) ? xMax : (minDist == distLeft) ? xMin : x;
            y = (minDist == distUp) ? yMax : (minDist == distDown) ? yMin : y;
            z = (minDist == distForward) ? zMax : (minDist == distBack) ? zMin : z;
        }

        return new Vector3(x, y, z);
    }

    public static Vector3 OutsideBounds(Vector3 position, Vector3 min, Vector3 max) =>
        OutsideBounds(position.x, position.y, position.z, min.x, max.x, min.y, max.y, min.z, max.z);

    public static Vector2 OutsideBounds(Vector3 position, Bounds bounds) =>
    OutsideBounds(position.x, position.y, position.z, bounds.min.x, bounds.max.x,
        bounds.min.y, bounds.max.y, bounds.min.z, bounds.max.z);
    #endregion

    #region extensions
    public static Vector2 GetOrthoSize(this Camera camera)
    {
        float height = 2 * camera.orthographicSize,
            width = height * camera.aspect;

        return new Vector2(width, height);
    }

    public static Vector3 ClampVector(this Vector3 vector, Vector3 min, Vector3 max) =>
        new Vector3(Mathf.Clamp(vector.x, min.x, max.x),
        Mathf.Clamp(vector.y, min.y, max.y),
        Mathf.Clamp(vector.z, min.z, max.z));

    public static Vector3 SmoothFollow(this Vector3 vector, Vector3 target, float smoothing, float delta) =>
        Vector3.Lerp(vector, target, (1 / smoothing) * delta);

    public static Vector3 RoundToNearest(this Vector3 vector, float increment) =>
        new Vector3(MathUtils.RoundToNearest(vector.x, increment),
        MathUtils.RoundToNearest(vector.y, increment),
        MathUtils.RoundToNearest(vector.z, increment));

    public static Vector3 FloorToNearest(this Vector3 vector, float increment) =>
        new Vector3(MathUtils.FloorToNearest(vector.x, increment),
        MathUtils.FloorToNearest(vector.y, increment),
        MathUtils.FloorToNearest(vector.z, increment));
    #endregion
}
