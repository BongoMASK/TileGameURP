using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public delegate void GameStateChange();

    [SerializeField] private GameState _gameState;

    private void Awake() {
        instance = this;
    }

    public GameState gameState {
        get { return _gameState; }
        set {
            if (value == _gameState)
                return;

            EndGameStage(_gameState);
            _gameState = value;
            BeginGameState(_gameState);
        }
    }

    void EndGameStage(GameState gameState) {
        string funcName = gameState.ToString() + "StageEnded";
        Invoke(funcName, 0);
    }

    void BeginGameState(GameState gameState) {
        string funcName = gameState.ToString() + "StageBegins";
        Invoke(funcName, 0);
    }


    #region Placing Stage

    public GameStateChange PlacingBegins;
    public GameStateChange PlacingEnded;

    public void PlacingStageBegins() {
        PlacingBegins?.Invoke();
    }

    public void PlacingStageEnded() {
        PlacingEnded?.Invoke();
    }

    #endregion


    #region Pushing Stage

    public GameStateChange PushingBegins;
    public GameStateChange PushingEnded;

    public void PushingStageBegins() {
        PushingBegins?.Invoke();
    }

    public void PushingStageEnded() {
        PushingEnded?.Invoke();
    }

    #endregion


    #region GameOver Stage

    public GameStateChange GameOverBegins;
    public GameStateChange GameOverEnded;

    public void GameOverStageBegins() {
        GameOverBegins?.Invoke();
    }

    public void GameOverStageEnded() {
        GameOverEnded?.Invoke();
    }

    #endregion

}

public enum GameState {
    Placing,
    Pushing,
    GameOver
}