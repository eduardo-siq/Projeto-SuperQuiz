using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LowerMenuScript : MonoBehaviour
{

    public static LowerMenuScript instance = null;

    // Buttons
    public GameObject menuButton;
    public GameObject soundButton;
    public GameObject infoButton;
    public GameObject exitButton;
    public bool lockScene;

    // Scripts
    public AboutScript aboutScript;
    public AnswersScript answerScript;
    public AvatarScript avatarScript;
    public GameplayScript gameplayScript;
    public MenuScript menuScript;
    public ResultScript resultScript;

    // Textures
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    bool quit;

    string thisScene;
    string nextScene;

    void Start(){
        instance = this;

        print("START LOWER SCRIPT MENU");
        // Buttons
        menuButton = this.gameObject.transform.Find("ToMenu").gameObject;
        soundButton = this.gameObject.transform.Find("Sound").gameObject;
        infoButton = this.gameObject.transform.Find("ToAbout").gameObject;
        instance.lockScene = false;

        // Scripts
        thisScene = SceneManager.GetActiveScene().name;
        if (thisScene == "about"){
            aboutScript = GameObject.FindWithTag("SceneManager").GetComponent<AboutScript>();
            infoButton.GetComponent<Button>().interactable = false;
        }
        if (thisScene == "answers") answerScript = GameObject.FindWithTag("SceneManager").GetComponent<AnswersScript>();
        if (thisScene == "avatar") avatarScript = GameObject.FindWithTag("SceneManager").GetComponent<AvatarScript>();
        if (thisScene == "gameplay"){
            gameplayScript = GameObject.FindWithTag("SceneManager").GetComponent<GameplayScript>();
            instance.lockScene = true;
            menuButton.GetComponent<Button>().interactable = false;
            infoButton.GetComponent<Button>().interactable = false;
        }
        if (thisScene == "menu"){
            menuScript = GameObject.FindWithTag("SceneManager").GetComponent<MenuScript>();
            menuButton.GetComponent<Button>().interactable = false;
        }
        if (thisScene == "result") resultScript = GameObject.FindWithTag("SceneManager").GetComponent<ResultScript>();

        // Textures
        soundOnSprite = Resources.Load("Textures/LowerMenu/MusicaBotao", typeof(Sprite)) as Sprite;
        soundOffSprite = Resources.Load("Textures/LowerMenu/MusicaOffBotao", typeof(Sprite)) as Sprite;

        // Methods
        SetButtonItens();
    }

    void Update(){
        if (quit){
            if (SessionScript.soundOn){
                SessionScript.songAudio.volume = SessionScript.songAudio.volume - (Time.deltaTime * 2);
            }
        }
    }

    public void SetButtonItens(){
        if (SessionScript.soundOn){
            soundButton.GetComponent<Image>().sprite = soundOnSprite;
        }
        if (!SessionScript.soundOn){
            soundButton.GetComponent<Image>().sprite = soundOffSprite;
        }
    }

    public void SelectScene(string option){
        print("SELECT SCENE " + option);
        if (instance.lockScene){
            SessionScript.ButtonAudio(SessionScript.subtle);
            print("LOCKED (instance.lockScene " + instance.lockScene + ")");
            return;
        }
        if (option == thisScene){
            print("SAME SCENE (instance.lockScene " + instance.lockScene + ")");
            SessionScript.ButtonAudio(SessionScript.subtle);
            return;
        }
		if (thisScene == "avatar"){
			AuthenticationScript.SaveAvatar();
		}
        print("CHANGE SCENE");
        instance.lockScene = true;
        SessionScript.ButtonAudio(SessionScript.neutral);
        nextScene = option;
        Invoke("EndScene", 1.2f);   // OBSOLETE?
        Invoke("NextScene", 0.2f);
        // TransitionScript.PlayAnimation();
        // TransitionScript.StartAnimation();
        TransitionScript.EndAnimation();
    }

    public void SelectQuit(){
        SessionScript.ButtonAudio(SessionScript.neutral);
        //quit = true;
        print("QUIT");
        SelectScene ("login");	// Exit button logs out the player and returns to login scene
		AuthenticationScript.SignOut();
    }

    void Quit(){
        Application.Quit();
    }

    public void NextScene(){
        instance.lockScene = true;
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }

    void EndScene(){   // OBSOLETE?
        if (thisScene == "about") aboutScript.endScene = true;
        if (thisScene == "answers") answerScript.endScene = true;
        if (thisScene == "avatar") avatarScript.endScene = true;
        if (thisScene == "gameplay") gameplayScript.endScene = true;
        if (thisScene == "menu") menuScript.endScene = true;
        if (thisScene == "result") resultScript.endScene = true;
    }

    public void TurnOnOffSound(){
        SessionScript.TurnOnOffSound();
        SetButtonItens();
    }
}
