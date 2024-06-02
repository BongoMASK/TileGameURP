using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ChangeName : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    public static UnityEvent OnNameChanged = new UnityEvent();

    private void Start() {
        if (inputField == null)
            return;

        string randPlayerName = "Player #" + Random.Range(0, 1000).ToString();
        inputField.text = PlayerPrefs.GetString("playerName", randPlayerName);

        ChangeTheName();
    }

    public void ChangeTheName() {
        PlayerPrefs.SetString("playerName", inputField.text);
        OnNameChanged?.Invoke();
    }
}
