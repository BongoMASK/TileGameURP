using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager instance;

    [SerializeField] Deck deck;
    [SerializeField] NetworkedTurnManager networkedTurnManager;

    public Faction selectedFactionType { get; private set; }

    [SerializeField] List<Faction> availableFactionPrefabs = new List<Faction>();

    bool canPlace => GameStateManager.instance.gameState == GameState.Placing;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        networkedTurnManager.BeginTurn();
    }

    private void Update() {
        ChangeFactionRotation();
    }

    public void ChangeSelectedFactionType(int i) {
        if (!canPlace)
            return;

        Vector3 pos = Vector3.zero;

        if (selectedFactionType != null) {
            selectedFactionType.transform.position = pos;
            Destroy(selectedFactionType.gameObject);
        }

        selectedFactionType = Instantiate(availableFactionPrefabs[i], pos, Quaternion.identity);
        selectedFactionType.transform.position = pos + selectedFactionType.offset;
        selectedFactionType.GetComponent<Collider>().enabled = false;
    }

    public void ChangeFactionPos(SquareTile tile) {
        if(!canPlace)
            return;

        if (selectedFactionType == null)
            return;

        if (!tile.isEmpty)
            return;

        selectedFactionType.transform.position = tile.transform.position + selectedFactionType.offset;
    }

    public void ChangeFactionRotation() {
        if (selectedFactionType == null)
            return;

        if(Input.GetMouseButtonDown(1)) {
            Vector3 rot = selectedFactionType.transform.rotation.eulerAngles + new Vector3(0, 90, 0);
            selectedFactionType.transform.rotation = Quaternion.Euler(rot);
        }
    }

    public void PlaceFaction(SquareTile tile) {
        if (!canPlace)
            return;

        if (selectedFactionType == null)
            return;
        
        if (!tile.isEmpty)
            return;

        int id = Faction.id + 1;

        PlayerPieceCreate playerPieceCreate = new PlayerPieceCreate(id, tile.tileID, selectedFactionType.factionType);
        networkedTurnManager.SendNewPieceToAll(playerPieceCreate.ToByteArray(), PhotonNetwork.LocalPlayer);
        Destroy(selectedFactionType.gameObject);

        //selectedFactionType.MoveFaction(tile);
        //selectedFactionType.GetComponent<Collider>().enabled = true;
        //selectedFactionType = null;
    }

    public void PlaceFactionOnBoard(PlayerPieceCreate playerPieceCreate) {
        int index = (int)playerPieceCreate.factionType;

        Faction faction = Instantiate(availableFactionPrefabs[index]);
        SquareTile t = SquareTile.FindTile(playerPieceCreate.tileID);

        faction.factionID = playerPieceCreate.factionID;
        Faction.id = faction.factionID;

        faction.MoveFaction(t);
        faction.GetComponent<Collider>().enabled = true;
    }
}