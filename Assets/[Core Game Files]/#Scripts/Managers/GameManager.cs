using UnityEngine;

public enum GameState { None, Menu, Paused, Playing, Finished }

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameManager>();

            return _instance;
        }
    }

    // Public
    public GameState currentGameState = GameState.None;

    private void Update()
    {
        if (Input.anyKey)
        {
            currentGameState = GameState.Playing;
        }
    }
}
