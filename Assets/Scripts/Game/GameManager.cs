using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    CallbackStateManager stateManager;

    private void Awake()
    {
        
    }

    private void Start()
    {
        stateManager.Initialize();
    }
}

public enum GameState
{
    Playing,
    Paused,
    GameOver
}