using UnityEditor;
using UnityEngine;

public class GridRenderer : MonoBehaviour
{
    [SerializeField]
    Color cellColor = Color.green;

    Grid<bool> grid = new(25, 25);

    private void FixedUpdate()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = cellColor;

        for (int x = 0; x < grid.Width; x++)
            for (int y = 0; y < grid.Height; y++)
                Gizmos.DrawCube(transform.position + new Vector3(x, y), Vector3.one * 0.75f);
    }

    [EditorUtils.Button]
    void UpdateGrid()
    {
        Debug.Log("button");
    }
}


[InitializeOnLoad]
public class SceneViewCursorTracker
{
    static SceneViewCursorTracker()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    static void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;
        Vector2 mousePos = e.mousePosition;

        // Convert to world position
        Ray ray = HandleUtility.GUIPointToWorldRay(mousePos);

        // You can now use this ray for raycasting, etc.
    }
}