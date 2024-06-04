using UnityEngine;
using UnityEngine.UI;

public class AudioUI : MonoBehaviour {
    [SerializeField] Slider[] sliders;
    [SerializeField] Slider masterVolumeSlider;

    AudioManager audioManager => AudioManager.instance;

    float effectsVolume {
        get {
            return audioManager.volumes[0];
        }
        set {
            audioManager.volumes[0] = value;
            PlayerPrefs.SetFloat(nameof(effectsVolume), value);
        }
    }

    float musicVolume {
        get { return audioManager.volumes[1]; }
        set {
            audioManager.volumes[1] = value;
            PlayerPrefs.SetFloat(nameof(musicVolume), value);
        }
    }

    float voiceVolume {
        get {
            return audioManager.volumes[3];
        }
        set {
            audioManager.volumes[3] = value;
            PlayerPrefs.SetFloat(nameof(voiceVolume), value);
        }
    }

    float uiVolume {
        get {
            return audioManager.volumes[2];
        }
        set {
            audioManager.volumes[2] = value;
            PlayerPrefs.SetFloat(nameof(uiVolume), value);
        }
    }

    float masterVolume {
        get {
            return audioManager.masterVolume;
        }
        set {
            audioManager.masterVolume = value;
            PlayerPrefs.SetFloat(nameof(masterVolume), value);
        }
    }

    private void Start() {
        LoadVolumes();

        masterVolumeSlider.maxValue = 100;
        masterVolumeSlider.value = audioManager.masterVolume * 100;

        for (int i = 0; i < sliders.Length; i++) {
            sliders[i].maxValue = 100;
            sliders[i].value = audioManager.volumes[i] * 100;
        }
    }

    /// <summary>
    /// Master Volume change (Listener Function)
    /// </summary>
    /// <param name="volume"></param>
    /// <param name="soundType"></param>
    public void ChangeMasterVolume(float volume) {
        volume /= 100f;
        masterVolume = volume;

        audioManager.Pause(SoundNames.backgroundMusic);
        audioManager.Play(SoundNames.backgroundMusic);
    }

    public void ChangeUIVolume(float volume) {
        volume /= 100f;
        uiVolume = volume;
    }

    public void ChangeEffectsVolume(float volume) {
        volume /= 100f;
        effectsVolume = volume;
    }

    public void ChangeMusicVolume(float volume) {
        volume /= 100f;
        musicVolume = volume;

        audioManager.Pause(SoundNames.backgroundMusic);
        audioManager.Play(SoundNames.backgroundMusic);
    }

    private void LoadVolumes() {
        effectsVolume = PlayerPrefs.GetFloat(nameof(effectsVolume), 1);
        musicVolume = PlayerPrefs.GetFloat(nameof(musicVolume), 1);
        uiVolume = PlayerPrefs.GetFloat(nameof(uiVolume), 1);

        masterVolume = PlayerPrefs.GetFloat(nameof(masterVolume), 0.7f);
    }
}