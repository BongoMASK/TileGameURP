using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public enum AnimationType {
    UpDownSin,
    UpDownSinUI,
    Bounce,
}

public class DoAnimation : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] AnimationType AnimationType;
    [SerializeField] Vector3 endValue;
    [SerializeField] float duration;

    RectTransform rectTransform => GetComponent<RectTransform>();
    Button button => GetComponent<Button>();

    private void OnEnable() {
        transform.DOPlay();
        rectTransform.DOPlay();
    }

    private void OnDisable() {
        transform.DOPause();
        rectTransform.DOPause();
    }

    private void Start() {
        if (button != null) {
            button.onClick.AddListener(PlayAnimation);
            return;
        }

        PlayAnimation();
    }

    public void PlayAnimation() {
        string funcName = AnimationType.ToString() + "Animation";
        Invoke(funcName, 0);
    }


    #region Animations

    void UpDownSinAnimation() {
        transform.DOMove(endValue, duration).SetLoops(-1, LoopType.Yoyo);
    }

    void UpDownSinUIAnimation() {
        Vector3 pos = rectTransform.anchoredPosition;
        rectTransform.DOAnchorPos(pos + endValue, duration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void BounceAnimation() {
        Vector3 scale = transform.localScale;
        transform.localScale = endValue;
        transform.DOScale(Vector3.one, duration).SetEase(Ease.OutBounce);
    }

    #endregion
}
