using Photon.Realtime;
using UnityEngine;

public class GameController : MonoBehaviour, INetworkedTurnManagerCallbacks
{

    public static GameController instance;
    [SerializeField] NetworkedTurnManager networkedTurnManager;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        networkedTurnManager.TurnManagerListener = this;
    }

    public void OnPieceCreated(Player owner, int turn, object[] pieceData) {
        Debug.Log(owner + " created piece");

        PlayerPieceCreate p = PlayerPieceCreate.ToPlayerPieceCreate(pieceData);
        PlacementManager.instance.PlaceFactionOnBoard(p);
    }

    public void OnPlayerFinished(Player player, int turn, object[] move) {
        Debug.Log(player.NickName + " finished");

        PlayerMove playerMove = PlayerMove.ToPlayerMove(move);
        FactionSelectionManger.instance.PerformMove(playerMove);
    }

    public void OnPlayerMove(Player player, int turn, object[] move) {
        Debug.Log(player.NickName + " moved");

        PlayerMove playerMove = PlayerMove.ToPlayerMove(move);
        FactionSelectionManger.instance.PerformMove(playerMove);
    }

    public void OnPlayerTurnStarts(Player player, int turn) {
        Debug.Log(player.NickName + " turn started");
    }

    public void OnTurnBegins(int turn) {
        Debug.Log("Turn " + turn);
    }

    public void OnTurnCompleted(int turn) {
        Debug.Log("Turn " + turn + " completed.");
    }

    public void OnTurnTimeEnds(int turn) {
        Debug.Log("Times up! Turn " + turn);
    }
}
