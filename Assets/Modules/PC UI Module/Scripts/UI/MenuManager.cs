using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public static MenuManager instance;

    public Menu[] menus;
    [SerializeField] Image transitionUI;

    private float time = 0.4f;

    public Menu activeMenu { get; private set; }

    private void Awake() {
        instance = this;
    }

    private void Start() {
        transitionUI.gameObject.SetActive(true);
        transitionUI.rectTransform.sizeDelta = Vector2.zero;

        Invoke(nameof(DoTransitionAnimOut), 1);
    }

    /// <summary>
    /// Opens menu using unique string
    /// </summary>
    /// <param name="menuName"></param>
    public void OpenMenu(string menuName) {
        Menu menu = null;
        for (int i = 0; i < menus.Length; i++) {
            if (menus[i].menuName == menuName) {
                menu = menus[i];
                break;
            }
        }

        OpenMenu(menu);
    }

    /// <summary>
    /// Opens menu using Menu object
    /// </summary>
    /// <param name="menu"></param>
    public void OpenMenu(Menu menu) {
        if (menu == null || menu == activeMenu)
            return;

        StartCoroutine(OpenAMenu(menu)); 
    }

    IEnumerator OpenAMenu(Menu menu) {
        DoTransitionAnimIn();

        yield return new WaitForSecondsRealtime(time * 2);

        for (int i = 0; i < menus.Length; i++) {
            if (menus[i].open) {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
        activeMenu = menu;

        DoTransitionAnimOut();
    }

    public void DoTransitionAnimIn() {
        transitionUI.gameObject.SetActive(true);
        transitionUI.rectTransform.DOSizeDelta(Vector2.zero, time).SetUpdate(true);

    }

    public void DoTransitionAnimOut() {
        StartCoroutine(TransitionAnimOut());
    }

    IEnumerator TransitionAnimOut() {
        transitionUI.rectTransform.DOSizeDelta(Vector2.one * 5000, time * 1.5f).SetUpdate(true);

        yield return new WaitForSecondsRealtime(time * 1.5f);
        EventSystem.current.SetSelectedGameObject(null);
        transitionUI.gameObject.SetActive(false);
    }

    /// <summary>
    /// Closes menu using Menu object
    /// </summary>
    /// <param name="menu"></param>
    public void CloseMenu(Menu menu) {
        menu.Close();
    }

    /// <summary>
    /// Closes all menus
    /// </summary>
    public void CloseAllMenus() {
        foreach (Menu menu in menus)
            CloseMenu(menu);
    }
}