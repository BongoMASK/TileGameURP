using TMPro;
using UnityEngine;

public class DisplayName : MonoBehaviour
{
    [SerializeField] TMP_Text displayNameText;

    string playerName => PlayerPrefs.GetString("playerName");

    private void OnEnable() {
        ChangeName.OnNameChanged.AddListener(SetName);
    }

    private void OnDisable() {
        ChangeName.OnNameChanged.RemoveListener(SetName);
    }

    private void SetName() {
        displayNameText.text = playerName;
    }
}
