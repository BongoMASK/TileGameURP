using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class FactionButton : MonoBehaviour
{
    [SerializeField] Deck deck;
    [SerializeField] FactionType factionType;

    [SerializeField] Button button;
    [SerializeField] TMP_Text factionCountText;

    string factionCountString => "x" + deckFaction.count;

    DeckFaction deckFaction;

    private void Awake() {
        this.enabled = false;
        Invoke(nameof(StupidFunc), 0.2f);
    }

    private void Start() {
        deckFaction = deck.GetDeckFaction(factionType);
        SetUpButton(null, 0, null);
    }

    private void OnEnable() {
        GameController.instance.Ev_OnPieceCreated.AddListener(SetUpButton);
    }

    private void OnDisable() {
        GameController.instance.Ev_OnPieceCreated.RemoveListener(SetUpButton);
    }

    void SetUpButton(Player p, int i, object[] move) {
        factionCountText.text = factionCountString;

        if (deckFaction.isEmpty) {
            button.interactable = false;
        }
    }

    void StupidFunc() {
        // this is done, because the singleton used in onenable hasnt been assigned yet
        // So we are delaying it to make it work
        this.enabled = true;
    }
}
