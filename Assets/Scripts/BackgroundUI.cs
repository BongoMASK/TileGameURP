using DG.Tweening;
using Photon.Realtime;
using UnityEngine;

public class BackgroundUI : MonoBehaviour
{

    [SerializeField] RectTransform background;

    private void OnEnable() {
        GameController.instance.Ev_OnPlayerTurnStarts.AddListener(ChangeBackground);
    }

    private void OnDisable() {
        GameController.instance.Ev_OnPlayerTurnStarts.RemoveListener(ChangeBackground);
    }

    private void ChangeBackground(Player p, int turn) {
        Team t = (Team)p.GetTeam();

        switch(t ) {
            case Team.Red:
                background.DOAnchorPosY(-540, 1).SetEase(Ease.InOutSine);
                break;
            case Team.Blue:
                background.DOAnchorPosY( 540, 1).SetEase(Ease.InOutSine);
                break;

        }
    }
}
