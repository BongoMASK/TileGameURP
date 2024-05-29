using Photon.Pun;
using UnityEngine;

public class FactionSelectionManger : MonoBehaviour
{
    public static FactionSelectionManger instance;

    [SerializeField] NetworkedTurnManager turnManager;


    [SerializeField] private Faction _selectedFaction;
    public Faction selectedFaction {
        get => _selectedFaction;
        set {
            if (!isPushingStage)
                return;

            // Deselect Faction
            if (value == _selectedFaction) {
                if (_selectedFaction != null)
                    _selectedFaction.HighlightFaction(false);

                _selectedFaction = null;
            }

            // Select Faction
            else {
                // Remove outline
                if (_selectedFaction != null)
                    _selectedFaction.HighlightFaction(false);

                _selectedFaction = value;

                // Add outline
                if (_selectedFaction != null)
                    _selectedFaction.HighlightFaction(true);
            }
        }
    }

    bool isPushingStage => GameStateManager.instance.gameState == GameState.Pushing;


    private void Awake() {
        instance = this;
    }

    public void Push(int b) {
        if (selectedFaction == null)
            return;

        Border border = (Border)b;

        PlayerMove playerMove = new PlayerMove(selectedFaction.factionID, border);
        turnManager.SendMove(playerMove.ToByteArray(), true, PhotonNetwork.LocalPlayer);

        //selectedFaction.InitiatePush(border);
        selectedFaction = null;
    }

    public void PerformMove(PlayerMove playerMove) {
        Faction faction = Faction.FindFaction(selectedFaction.factionID);
        playerMove.Print();

        faction.InitiatePush(playerMove.direction);
        selectedFaction = null;
    }

}
