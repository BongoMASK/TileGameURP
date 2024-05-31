using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour, INetworkedTurnManagerCallbacks
{

    public static GameController instance;
    [SerializeField] NetworkedTurnManager networkedTurnManager;

    [HideInInspector] public UnityEvent<Player, int> Ev_OnPlayerTurnStarts;
    [HideInInspector] public UnityEvent<Player, int, object[]> Ev_OnPlayerFinished;
    [HideInInspector] public UnityEvent<Player, int, object[]> Ev_OnPlayerMove;
    [HideInInspector] public UnityEvent<Player, int, object[]> Ev_OnPieceCreated;
    [HideInInspector] public UnityEvent<int> Ev_OnTurnBegins;
    [HideInInspector] public UnityEvent<int> Ev_OnTurnCompleted;
    [HideInInspector] public UnityEvent<int> Ev_OnTurnTimeEnds;


    private void Awake() {
        instance = this;
    }

    private void Start() {
        networkedTurnManager.TurnManagerListener = this;
    }

    public void OnPieceCreated(Player owner, int turn, object[] pieceData) {
        Debug.Log(owner.NickName + " created piece");

        PlayerPieceCreate p = PlayerPieceCreate.ToPlayerPieceCreate(pieceData);
        PlacementManager.instance.PlaceFactionOnBoard(p, owner);

        Ev_OnPieceCreated?.Invoke(owner, turn, pieceData);
    }

    public void OnPlayerFinished(Player player, int turn, object[] move) {
        Debug.Log(player.NickName + " finished");

        PlayerMove playerMove = PlayerMove.ToPlayerMove(move);
        FactionSelectionManger.instance.PerformMove(playerMove);

        Ev_OnPlayerFinished?.Invoke(player, turn, move);
    }

    public void OnPlayerMove(Player player, int turn, object[] move) {
        Debug.Log(player.NickName + " moved");

        PlayerMove playerMove = PlayerMove.ToPlayerMove(move);
        FactionSelectionManger.instance.PerformMove(playerMove);

        Ev_OnPlayerMove?.Invoke(player, turn, move);
    }

    public void OnPlayerTurnStarts(Player player, int turn) {
        Debug.Log(player.NickName + " turn started");

        Ev_OnPlayerTurnStarts?.Invoke(player,turn);
    }

    public void OnTurnBegins(int turn) { 
        Debug.Log("Turn " + turn);

        Ev_OnTurnBegins?.Invoke(turn);
    }

    public void OnTurnCompleted(int turn) {
        Debug.Log("Turn " + turn + " completed.");

        Ev_OnTurnCompleted?.Invoke(turn);
    }

    public void OnTurnTimeEnds(int turn) {
        Debug.Log("Times up! Turn " + turn);
        Debug.Log("deck " + PhotonNetwork.LocalPlayer.IsDeckEmpty());
        Ev_OnTurnTimeEnds?.Invoke(turn);
    }
}
