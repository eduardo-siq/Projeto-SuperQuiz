using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LowerMenuScript : MonoBehaviour {
	
	public AboutScript aboutScript;
	public AnswersScript answerScript;
	public AvatarScript avatarScript;
	public GameplayScript gameplayScript;
	public MenuScript menuScript;
	public ResultScript resultScript;
	
	bool quit;
	
	string thisScene;
	string nextScene;
	
	void Start(){
		thisScene = SceneManager.GetActiveScene().name;
		StartCoroutine(GetThisScene());
	}
	
	IEnumerator GetThisScene(){
		yield return null;
		if (thisScene == "about") aboutScript = GameObject.Find("About").GetComponent<AboutScript>();
		if (thisScene == "answers") answerScript = GameObject.Find("Answers").GetComponent<AnswersScript>();
		if (thisScene == "avatar") avatarScript = GameObject.Find("Avatar").GetComponent<AvatarScript>();
		if (thisScene == "gameplay") gameplayScript = GameObject.Find("Gameplay").GetComponent<GameplayScript>();
		if (thisScene == "menu") menuScript = GameObject.Find("Menu").GetComponent<MenuScript>();
		if (thisScene == "result") resultScript = GameObject.Find("Result").GetComponent<ResultScript>();
		print ("this scene: " + thisScene);
	}
	
	void Update(){
		if (quit){
			if(SessionScript.soundOn){
				SessionScript.songAudio.volume = SessionScript.songAudio.volume - (Time.deltaTime * 2);
			}
		}
	}

	public void SelectScene(string option){
		if (thisScene == "gameplay"){
			SessionScript.ButtonAudio(SessionScript.subtle);
			return;
		}
		if (option == thisScene){
			SessionScript.ButtonAudio(SessionScript.subtle);
			return;
		}
		SessionScript.ButtonAudio(SessionScript.neutral);
		nextScene = option;
		Invoke ("EndScene", 0.25f);
		Invoke ("NextScene", 0.5f);
	}
	
	public void SelectQuit(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		quit = true;
		print ("QUIT");
		Invoke ("EndScene", 0.25f);
		Invoke ("Quit", 0.5f);
	}
	
	void Quit(){
		Application.Quit();
	}
	
	public void NextScene(){
		SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
	}
	
	void EndScene(){
		if (thisScene == "about") aboutScript.endScene = true;
		if (thisScene == "answers") answerScript.endScene = true;
		if (thisScene == "avatar") avatarScript.endScene = true;
		if (thisScene == "gameplay") gameplayScript.endScene = true;
		if (thisScene == "menu") menuScript.endScene = true;
		if (thisScene == "result") resultScript.endScene = true;
	}
	
	public void TurnOnOffSound(){
		SessionScript.TurnOnOffSound();
	}
}
