using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour{

    // UI
    public RectTransform menuRect;
    public bool endScene;
    public GameObject avatar;

    // Menu variables
    private string nextScene = "";


    void Start(){
        StartCoroutine(StartScene());
    }

    IEnumerator StartScene(){
        yield return null;
		if (SessionScript.justStartedSession && SessionScript.firstLogIn) AuthenticationScript.FirstLoginCompleted();
		if (SessionScript.resetAnswersAutomatically){
			AuthenticationScript.ResetAnswers();
			SessionScript.questionsAskedList = new List<int>();
			SessionScript.answersList = new List<Answer>();
		}
		if (SessionScript.getOtherPlayers){
			AuthenticationScript.GetOtherPlayers();
			SessionScript.getOtherPlayers = false;
		}
        menuRect = GameObject.Find("Canvas/Scroll View/Viewport/Menu").GetComponent<RectTransform>();
        avatar = GameObject.Find("Canvas/Scroll View/Viewport/Menu/ToAvatar/Portrait").gameObject;
		if (SessionScript.player.avatar.skin == -1){
			avatar.SetActive(false);
			GameObject.Find("Canvas/Scroll View/Viewport/Menu/ToAvatar/NoPortrait").gameObject.SetActive(true);
		}else{
			avatar.GetComponent<AvatarPortrait>().SpecificAvatar(SessionScript.player.avatar);
		}
        // avatar.transform.Find("Item1").GetComponent<RawImage>().texture = SessionScript.avatarItem1[SessionScript.selectedItem1];
        // avatar.transform.Find("Item2").GetComponent<RawImage>().texture = SessionScript.avatarItem2[SessionScript.selectedItem2];
        // avatar.transform.Find("Item3").GetComponent<RawImage>().texture = SessionScript.avatarItem3[SessionScript.selectedItem3];

        // Checks if there are avaiable question
        if(SessionScript.questionsAskedList.Count >= SessionScript.numberOfQuestionsDemanded){
            GameObject.Find("Canvas/Scroll View/Viewport/Menu/ToGameplay/Text").GetComponent<Text>().text = "";	//"não há mais questões!";
			// GameObject.Find("Canvas/Scroll View/Viewport/Menu/ToGameplay").GetComponent<Button>().enabled = false;
			GameObject.Find("Canvas/Scroll View/Viewport/Menu/ToGameplay").GetComponent<Image>().sprite = Resources.Load("Textures/UI/PlayDisabled", typeof (Sprite)) as Sprite;
        }
		
		// Soundtrack
		if (!SessionScript.songAudio.isPlaying){
			SessionScript.PlaySong();
		}
		Invoke ("CheckSong", 1f);
		
		// Easter Egg
		if (SessionScript.justStartedSession && AuthenticationScript.email == "rocketprogames@gmail.com"){
			PopUpScript.InstantiatePopUp("Bem-vindo de volta, Mestre. Como posso servi-lo?", ">>");
		}
		
		// Just Started Session
		SessionScript.justStartedSession = false;
    }


    void Update(){
        // if (endScene){
        // menuRect.anchoredPosition = new Vector2 (menuRect.anchoredPosition.x, menuRect.anchoredPosition.y - Time.deltaTime * 1200);
        // return;
        // }
    }

    public void Select(string option){
		if (option == "gameplay"){
			if(SessionScript.questionsAskedList.Count >= SessionScript.numberOfQuestionsDemanded){
				SessionScript.ButtonAudio(SessionScript.subtle);
				 PopUpScript.InstantiatePopUp("Todos as questões foram respondidas!", "OK");
				return;
			}
			SessionScript.currentSong = SessionScript.song2;
			SessionScript.fadeOutSong = true;
		}
        nextScene = option;
        SessionScript.ButtonAudio(SessionScript.neutral);
		Invoke("EndScene", 1.2f);
        Invoke("NextScene", 0.2f);
        // TransitionScript.PlayAnimation();
        // TransitionScript.StartAnimation();
        TransitionScript.EndAnimation();

    }

    // public void SelectQuit(){
    // SessionScript.ButtonAudio(SessionScript.neutral);
    // quit = true;
    // print ("QUIT");
    // Invoke ("EndScene", 0.25f);
    // Invoke ("Quit", 0.5f);
    // }

    // void Quit(){
    // Application.Quit();
    // }

    public void NextScene(){
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }

    void EndScene(){
        endScene = true;
    }

    public void TurnOnOffSound(){
        SessionScript.TurnOnOffSound();
    }
	
	// Double Checks
	
	void CheckSong(){
		if (!SessionScript.songAudio.isPlaying){
			SessionScript.PlaySong();
		}
	}
}
