using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] Image overlay;
    [SerializeField] RectTransform rectTransform;

    Vector2 pos = new Vector2(0, -1080);

    float duration = 0.4f;

    private void Start() {
        rectTransform.anchoredPosition = pos;
    }

    public void EnterPopUp() {
        overlay.enabled = true;
        overlay.DOFade(0.6f, duration).SetEase(Ease.OutSine);
        rectTransform.DOAnchorPos(Vector2.zero, duration).SetEase(Ease.OutSine);
    }

    public void ExitPopUp() {
        overlay.DOFade(0, duration).SetEase(Ease.OutSine);
        rectTransform.DOAnchorPos(pos, duration).SetEase(Ease.OutSine);
        overlay.enabled = false;
    }
}
