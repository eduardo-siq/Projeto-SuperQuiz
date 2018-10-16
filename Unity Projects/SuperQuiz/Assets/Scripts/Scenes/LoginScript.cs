using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour{

	// UI
	public RectTransform loginRect;
	public bool endScene;
	public InputField userInputField;
	public InputField passwordInputField;
	public int selectedInputField = 1;
	public GameObject acceptWindow;
	public GameObject errorWindow;
	private bool quit;

	// Login variables
	private string user = "12";
	private string user2 = "34";
	private string password = "21";
	private string password2 = "43";
	public string userInput = "";
	public string passwordInput = "";
	public bool block;


	void Start(){
		StartCoroutine(StartScene());
		userInputField = GameObject.Find("Canvas/Scroll View/Viewport/Login/LoginWindow/User").GetComponent<InputField>();
		passwordInputField = GameObject.Find("Canvas/Scroll View/Viewport/Login/LoginWindow/Password").GetComponent<InputField>();
		errorWindow = GameObject.Find("Canvas/Scroll View/Viewport/Login/LoginWindow/ErrorWindow").gameObject;
		userInputField.ActivateInputField();
	}

	IEnumerator StartScene(){
		yield return null;
		loginRect = GameObject.Find("Canvas/Scroll View/Viewport/Login").GetComponent<RectTransform>();
		SessionScript.PlaySong();
	}


	void Update(){
		// if (endScene){
		// 	loginRect.anchoredPosition = new Vector2 (loginRect.anchoredPosition.x, loginRect.anchoredPosition.y - Time.deltaTime * 1200);
		// 	return;
		// }
		if (quit){
			SessionScript.songAudio.volume = SessionScript.songAudio.volume - (Time.deltaTime * 2);
		}
	}

	public void UserInput(string input){
		userInput = input;
	}

	public void PasswordInput(string input){
		passwordInput = input;
	}

	public void LoginButton(){   // DUMMY LOGIN
		if (block){
			SessionScript.ButtonAudio(SessionScript.subtle);
			return;
		}
		if (userInput == user && passwordInput == password){
			SessionScript.ButtonAudio(SessionScript.positive);
			SessionScript.userGroup = 0;
			print("userGroup " + SessionScript.userGroup);
			SessionScript.GetQuestionListFromPreLoad();
			Invoke("ToggleAcceptTerms", 0.5f);
			// Invoke("EndScene", 1.2f);
			// Invoke("NextScene", 0.2f);
			//TransitionScript.EndAnimation();
			return;
		}
		if (userInput == user2 && passwordInput == password2){
			SessionScript.ButtonAudio(SessionScript.positive);
			SessionScript.userGroup = 1;
			print("userGroup " + SessionScript.userGroup);
			SessionScript.GetQuestionListFromPreLoad();
			Invoke("ToggleAcceptTerms", 0.5f);
			// Invoke("EndScene", 1.2f);
			// Invoke("NextScene", 0.2f);
			//TransitionScript.EndAnimation();
			return;
		}
		if (userInput != user || userInput != user2 || passwordInput == password || passwordInput == password2){
			OpenErrorMessage();
		}
		print("userGroup " + SessionScript.userGroup);
	}
	
	void OpenErrorMessage(){
		block = true;
		SessionScript.ButtonAudio(SessionScript.negative);
		errorWindow.SetActive(true);
		Invoke ("CloseErrorMessage", 2.5f);
	}
	
	void CloseErrorMessage(){
		errorWindow.SetActive(false);
		SessionScript.ButtonAudioLow(SessionScript.blop);
		block = false;
	}
	
	public void ToggleAcceptTerms(){
		acceptWindow.SetActive(!acceptWindow.activeSelf);
	}
	
	public void SelectAcceptTerms(){
		SessionScript.ButtonAudio(SessionScript.positive);
		//Invoke("ToggleAcceptTerms", 0.5f);
		Invoke("EndScene", 1.2f);
		Invoke("NextScene", 0.2f);
		TransitionScript.EndAnimation();
	}

	public void SelectDeclineTerms(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		Invoke("ToggleAcceptTerms", 0.5f);
		// Invoke("EndScene", 1.2f);
		// Invoke("NextScene", 0.2f);
		// TransitionScript.EndAnimation();
	}

	public void NextScene(){
		if (SessionScript.firstLogIn){   // Dummy first login
			SceneManager.LoadScene("tutorial", LoadSceneMode.Single);
		}
		if (!SessionScript.firstLogIn){
			SceneManager.LoadScene("menu", LoadSceneMode.Single);
		}
	}

	void EndScene(){
		endScene = true;
	}

	public void SelectQuit(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		quit = true;
		print("QUIT");
		Invoke("EndScene", 0.5f);
		Invoke("Quit", 1f);
	}

	void Quit(){
		Application.Quit();
	}
}