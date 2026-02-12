using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    CallbackStateManager stateManager;

    private void Awake()
    {
        stateManager[GameState.Playing].onEnterCallback += () => { Debug.Log("Game Start!"); };
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