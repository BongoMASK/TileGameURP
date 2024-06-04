using System;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    
    public Sounds[] sounds;
    public static AudioManager instance;

    // Effects = 0, Music = 1, Voice = 2, UI = 3
    public float[] volumes = { 1, 1, 1, 1 };

    float effectsVolume { 
        get { return volumes[0]; }
        set { volumes[0] = value; }
    }

    float musicVolume { 
        get { return volumes[1]; }
        set { volumes[1] = value;}
    }

    float voiceVolume {
        get {
            return volumes[3];
        }
        set {
            volumes[3] = value;
        }
    }

    float uiVolume {
        get {
            return volumes[3];
        }
        set {
            volumes[3] = value;
        }
    }

    public float masterVolume = 1f;

    private void Awake() {
        // Singleton
        //if (instance != null)
        //    Destroy(this);
        //else {
        //    instance = this;
        //    DontDestroyOnLoad(this);
        //}

        instance = this;

        // Adding Audio Source component
        foreach (var s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            //s.source.spatialBlend = 1;
        }
    }

    private void Start() {
        Play(SoundNames.backgroundMusic);
    }

    /// <summary>
    /// Plays audio using string as name
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name) {
        // Find sound
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound name: " + name + " not found in list!");
            return;
        }

        // Get random out of the clips
        int index = UnityEngine.Random.Range(0, s.clips.Count);
        s.source.clip = s.clips[index];

        // randomize pitch
        if (s.randomizePitch)
            s.source.pitch = UnityEngine.Random.Range(s.pitchMin, s.pitchMax);

        // set volume
        s.source.volume = s.volume * masterVolume * volumes[(int)s.soundType];

        // Play audio
        s.source.Play();
    }

    /// <summary>
    /// Pauses audio
    /// </summary>
    /// <param name="name"></param>
    public void Pause(string name) {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound name: " + name + " not found in list!");    
            return;
        }
        s.source.Stop();
    }

    /// <summary>
    /// Volume change (Listener Function)
    /// </summary>
    /// <param name="volume"></param>
    /// <param name="soundType"></param>
    public void ChangeVolume(float volume, SoundType soundType) {
        volume /= 100f;
        volumes[(int)soundType] = volume;
    }
}
