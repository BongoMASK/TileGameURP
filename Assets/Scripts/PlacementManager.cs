using System.Collections.Generic;
using UnityEngine;

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

        //networkedTurnManager.SendNewPieceToAll();

        selectedFactionType.MoveFaction(tile);
        selectedFactionType.GetComponent<Collider>().enabled = true;
        selectedFactionType = null;
    }
}