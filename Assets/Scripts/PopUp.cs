using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] Image overlay;
    [SerializeField] RectTransform rectTransform;

    Vector2 pos = new Vector2(0, -1080);

    float duration = 0.7f;

    private void Start() {
        rectTransform.anchoredPosition = pos;
    }

    public void EnterPopUp() {
        overlay.enabled = true;
        overlay.DOFade(0.6f, duration).SetEase(Ease.InOutSine);
        rectTransform.DOAnchorPos(Vector2.zero, duration).SetEase(Ease.InOutSine);
    }

    public void ExitPopUp() {
        overlay.DOFade(0, duration).SetEase(Ease.InOutSine);
        rectTransform.DOAnchorPos(pos, duration).SetEase(Ease.InOutSine).
            OnComplete(() => overlay.enabled = false);
        
    }
}
