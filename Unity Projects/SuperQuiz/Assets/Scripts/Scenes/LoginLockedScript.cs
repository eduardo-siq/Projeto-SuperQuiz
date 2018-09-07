using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginLockedScript : MonoBehaviour
{

    // UI
    public RectTransform loginRect;
    public bool endScene;
    public bool quit;
    public InputField userInputField;
    public InputField passwordInputField;
    public GameObject errorWindow;


    // Sound
    public static AudioSource songAudio;
    public static AudioSource buttonAudio;
    public static AudioClip song1;
    public static AudioClip song2;
    public static AudioClip positive;
    public static AudioClip negative;
    public static AudioClip neutral;
    public static AudioClip subtle;
    public static int currentSong;
    public static bool soundOn;
    public static float soundVolume;

    void Start()
    {
        // UI
        loginRect = GameObject.Find("Canvas/Scroll View/Viewport/LoginLocked").GetComponent<RectTransform>();
        userInputField = GameObject.Find("Canvas/Scroll View/Viewport/LoginLocked/User").GetComponent<InputField>();
        passwordInputField = GameObject.Find("Canvas/Scroll View/Viewport/LoginLocked/Password").GetComponent<InputField>();
        errorWindow = GameObject.Find("Canvas/Scroll View/Viewport/LoginLocked/ErrorWindow").gameObject;
        errorWindow.SetActive(false);

        //Sound
        songAudio = this.gameObject.AddComponent<AudioSource>();
        buttonAudio = this.gameObject.AddComponent<AudioSource>();
        positive = Resources.Load("Sound/positive_sound", typeof(AudioClip)) as AudioClip;
        negative = Resources.Load("Sound/negative_sound", typeof(AudioClip)) as AudioClip;
        neutral = Resources.Load("Sound/neutral_sound", typeof(AudioClip)) as AudioClip;
        subtle = Resources.Load("Sound/subtle_sound", typeof(AudioClip)) as AudioClip;
        song1 = Resources.Load("Sound/trilhaSuperQuiz", typeof(AudioClip)) as AudioClip;
        song2 = Resources.Load("Sound/trilhaSuperQuiz", typeof(AudioClip)) as AudioClip;
        currentSong = 1;
        soundOn = true;
        soundVolume = 0.5f;
    }

    void Update()
    {
        if (quit)
        {
            songAudio.volume = songAudio.volume - (Time.deltaTime * 2);
        }
        if (endScene)
        {
            loginRect.anchoredPosition = new Vector2(loginRect.anchoredPosition.x, loginRect.anchoredPosition.y - Time.deltaTime * 1200);
            return;
        }
        if (!songAudio.isPlaying)
        {
            if (currentSong == 1)
            {
                PlaySong(song2);
            }
            if (currentSong == 2)
            {
                PlaySong(song1);
            }
            currentSong = currentSong + 1;
            if (currentSong > 2) { currentSong = 1; }
        }
    }


    public void LoginButton()
    {
        PlaySong(negative);
        userInputField.DeactivateInputField();
        passwordInputField.DeactivateInputField();
        Invoke("Warning", 0.5f);
    }

    public void Warning()
    {
        errorWindow.SetActive(true);
    }

    public void SelectQuit()
    {
        ButtonAudio(SessionScript.neutral);
        quit = true;
        print("QUIT");
        Invoke("EndScene", 0.5f);
        Invoke("Quit", 1f);
    }

    void EndScene()
    {
        endScene = true;
    }

    void Quit()
    {
        Application.Quit();
    }

    public void PlaySong(AudioClip audio)
    {
        songAudio.PlayOneShot(audio, soundVolume);
    }

    public static void ButtonAudio(AudioClip audio)
    {
        buttonAudio.PlayOneShot(audio, soundVolume * 2);
    }
}
