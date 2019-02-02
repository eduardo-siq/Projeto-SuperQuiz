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
	public RectTransform acceptWindowDocument;
	public GameObject errorWindow;
	private bool quit;

	// Login variables
	private string user = "12";			// OBSOLETE
	private string user2 = "34";		// OBSOLETE
	private string password = "21";		// OBSOLETE
	private string password2 = "43";	// OBSOLETE
	public static string userInput = "";
	public static string passwordInput = "";
	public bool block;
	public static bool loginSucess = false;
	public static bool loginFail = false;
	public static bool userDoesNotExist = false;
	public static bool stopAtLogin = false;
	
	// Auxiliary
	public bool acceptWindowOpen;
	public float acceptWindowTimer;
	
	// Authentication
	AuthenticationScript authenticationScript;

	void Start(){
		StartCoroutine(StartScene());
		userInputField = GameObject.Find("Canvas/Scroll View/Viewport/Login/LoginWindow/User").GetComponent<InputField>();
		passwordInputField = GameObject.Find("Canvas/Scroll View/Viewport/Login/LoginWindow/Password").GetComponent<InputField>();
		acceptWindow = GameObject.Find("Canvas/Scroll View/Viewport/Accept").gameObject;
		acceptWindowDocument = acceptWindow.transform.Find("Scroll View/Viewport/Document").GetComponent<RectTransform>();
		errorWindow = GameObject.Find("Canvas/Scroll View/Viewport/Login/LoginWindow/ErrorWindow").gameObject;
		userInputField.ActivateInputField();
		
		// Authentication
		authenticationScript = GameObject.Find("Session").GetComponent<AuthenticationScript>();
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
		if (loginSucess){	// On Update, LoginScript checks if login was made and, if so, moves on with game. 'loginSucess' is set by AuthenticationScript
			LoginValidated();
			loginSucess = false;
		}
		if (loginFail){	// Same, for failure
			OpenErrorMessage();
			loginFail = false;
		}
		if (userDoesNotExist){	// Same, for failure
			ErrorLogin();
			userDoesNotExist = false;
		}
		if (stopAtLogin){
			acceptWindow.SetActive(false);
		}
		if (quit){
			SessionScript.songAudio.volume = SessionScript.songAudio.volume - (Time.deltaTime * 2);
		}
		if (acceptWindowOpen){
			acceptWindowTimer = acceptWindowTimer + Time.deltaTime;
			if (acceptWindowTimer > 10f){
				Vector2 pos = acceptWindowDocument.position;
				acceptWindowDocument.position = new Vector2 (pos.x, pos.y + Time.deltaTime *10f);
			}
		}
	}

	public void UserInput(string input){
		userInput = input;
	}

	public void PasswordInput(string input){
		passwordInput = input;
	}
	
	public void LoginButton(){	// FUNCTIONAL LOGIN
		SessionScript.ButtonAudio(SessionScript.neutral);
		AuthenticationScript.email = userInput;
		authenticationScript.SigninWithEmailAsync(userInput, passwordInput);
	}
	
	void LoginValidated(){
		//SessionScript.userGroup = 0;					// REMOVE LATER, SHOULD BE HANDLED BY DATABASE
		//print("userGroup " + SessionScript.userGroup);	// REMOVE LATER, SHOULD BE HANDLED BY DATABASE
		//SessionScript.GetQuestionListFromPreLoad();		// REMOVE LATER, SHOULD BE HANDLED BY DATABASE
		Invoke("ToggleAcceptTerms", 0.25f);
	}
	
	public void LoginButtonDummy(){   // DUMMY LOGIN
		if (block){
			SessionScript.ButtonAudio(SessionScript.subtle);
			return;
		}
		if (userInput == user && passwordInput == password){
			SessionScript.ButtonAudio(SessionScript.positive);
			SessionScript.userGroup = 0;
			print("userGroup " + SessionScript.userGroup);
			//SessionScript.GetQuestionListFromPreLoad();
			SessionScript.getQuestionListNow = true;
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
			//SessionScript.GetQuestionListFromPreLoad();
			SessionScript.getQuestionListNow = true;
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
		acceptWindowOpen = !acceptWindowOpen;
		acceptWindowTimer = 0f;
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
		AuthenticationScript.TrackRecord("Declined terms of service");
	}
	
	public void ResetAcceptWindowTime(){
		print ("ResetAcceptWindowTime()");
		acceptWindowTimer = 0;
	}

	public void NextScene(){
		if (SessionScript.firstLogIn){   // Dummy first login
			SceneManager.LoadScene("tutorial", LoadSceneMode.Single);
		}
		if (!SessionScript.firstLogIn){
			SceneManager.LoadScene("menu", LoadSceneMode.Single);
		}
	}
	
	public void ErrorLogin(){
		SessionScript.ButtonAudio(SessionScript.negative);
		SceneManager.LoadScene("errorLogin", LoadSceneMode.Single);
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