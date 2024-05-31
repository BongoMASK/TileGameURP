using Photon.Pun;
using Photon.Realtime;
using Properties;
using UnityEngine;

public class GameStateManager : MonoBehaviourPunCallbacks
{
    public static GameStateManager instance;

    public delegate void GameStateChange();

    GameState prevGameState;
    public GameState currentGameState {
        get => PhotonNetwork.CurrentRoom.GetGameState();
        private set {
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.CurrentRoom.SetGameState(value);
        }
    }


    private void Awake() {
        instance = this;
    }

    private void Start() {
        currentGameState = GameState.Placing;
    }


    #region Stage Functions


    void EndGameStage(GameState gameState) {
        string funcName = gameState.ToString() + "StageEnded";
        Invoke(funcName, 0);
    }

    void BeginGameState(GameState gameState) {
        string funcName = gameState.ToString() + "StageBegins";
        Invoke(funcName, 0);

        Debug.Log(gameState.ToString() + " Begins");
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

    #endregion

    public void CheckIfDecksEmpty(int turn) {
        if (currentGameState != GameState.Placing)
            return;

        bool allPlayerDecksEmpty = true;
        foreach (var item in PhotonNetwork.PlayerList) {
            if(!item.IsDeckEmpty())
                allPlayerDecksEmpty = false;
        }

        if (allPlayerDecksEmpty)
            currentGameState = GameState.Pushing;
    }


    #region Monobehaviour PUN Callbacks

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged) {
        base.OnRoomPropertiesUpdate(propertiesThatChanged);

        if (propertiesThatChanged.ContainsKey(RoomProps.CurrentGameState)) {
            EndGameStage(prevGameState);
            BeginGameState(currentGameState);
            prevGameState = currentGameState;
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps) {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);

        if(changedProps.ContainsKey(PlayerProps.EmptyDeck)) {
            CheckIfDecksEmpty(1);
        }
    }

    #endregion

}

public enum GameState {
    Placing,
    Pushing,
    GameOver
}