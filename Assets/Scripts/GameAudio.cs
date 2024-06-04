using Photon.Realtime;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    private void OnEnable() {
        GameController.instance.Ev_OnPlayerFinished.AddListener(OnPlayerFinished);
        GameController.instance.Ev_OnPieceCreated.AddListener(OnPieceCreated);
    }

    private void OnDisable() {
        GameController.instance.Ev_OnPlayerFinished.RemoveListener(OnPlayerFinished);
        GameController.instance.Ev_OnPieceCreated.RemoveListener(OnPieceCreated);
    }

    public void OnPlayerFinished(Player player, int i, object[] move) {
        PlayerMove playerMove = PlayerMove.ToPlayerMove(move);

        Faction faction = Faction.FindFaction(playerMove.factionID);
        PlayAudioOnPush(faction.factionType);
    }

    public void OnPieceCreated(Player player, int i, object[] move) {
        PlayerPieceCreate playerMove = PlayerPieceCreate.ToPlayerPieceCreate(move);

        Faction faction = Faction.FindFaction(playerMove.factionID);
        PlayAudioOnPush(faction.factionType);
    }

    public void PlayAudioOnPush(FactionType factionType) {
        string audioName = factionType.ToString() + "Push";
        Debug.Log(audioName);
        AudioManager.instance.Play(audioName);
    }
}
