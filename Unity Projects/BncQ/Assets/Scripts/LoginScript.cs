using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour {

	//UI Elements
	GameObject welcomeWindow;
	GameObject textFieldUser;
	GameObject textFieldPassword;
	GameObject buttonLogin;
	GameObject waitingWindow;
	
	// Authentication variables
	public static bool startLogin;	// Set true by AuthenticationScript
	public static bool loginSucess;
	public static bool loginFail;
	
	void Start () {
		//UI Elements
		this.gameObject.transform.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);	// Main background
		welcomeWindow = this.gameObject.transform.Find("Welcome Window").gameObject;
		textFieldUser = this.gameObject.transform.Find("Text Field - User").gameObject;
		textFieldPassword = this.gameObject.transform.Find("Text Field - Password").gameObject;
		buttonLogin = this.gameObject.transform.Find("Button - Login").gameObject;
		waitingWindow = this.gameObject.transform.Find("Waiting Window").gameObject;
		welcomeWindow.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/9, Screen.height/2);
		welcomeWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/4.5f, Screen.height/3);
		textFieldUser.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/9, Screen.height/2 - 1 * Screen.height/15);
		textFieldUser.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/4.5f, Screen.height/15);
		textFieldPassword.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/9, Screen.height/2 - 2 * Screen.height/15);
		textFieldPassword.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/4.5f, Screen.height/15);
		buttonLogin.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/9, Screen.height/2 - 3 * Screen.height/15);
		buttonLogin.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/4.5f, Screen.height/15);
		waitingWindow.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.height/9, Screen.height/2 - Screen.height/9);
		waitingWindow.transform.Find("Window").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height/4.5f, Screen.height/4.5f);
		waitingWindow.transform.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height/4.5f, Screen.height/4.5f);
		
	}
	
	void Update () {
		if (startLogin){ StartLogin(); startLogin = false;}
		if (loginSucess){ LoginSuccessful(); loginSucess = false;}
		if (loginFail){ LoginUnsuccessful(); loginFail = false;}
	}
	
	public void StartLogin(){
		// sound
		waitingWindow.SetActive(true);
		waitingWindow.SetActive(false);
		welcomeWindow.SetActive(false);
		textFieldUser.SetActive(false);
		textFieldPassword.SetActive(false);
		buttonLogin.SetActive(false);
	}
	
	public void LoginUnsuccessful(){
		// sound
		waitingWindow.SetActive(false);	
		waitingWindow.SetActive(true);
		welcomeWindow.SetActive(true);
		textFieldUser.SetActive(true);
		textFieldPassword.SetActive(true);
		buttonLogin.SetActive(true);		
	}
	
	public void LoginSuccessful(){
		// sound
		NextScene();
		waitingWindow.SetActive(false);
		welcomeWindow.SetActive(false);
		textFieldUser.SetActive(false);
		textFieldPassword.SetActive(false);
		buttonLogin.SetActive(false);
	}
	
	public void NextScene(){
		SceneManager.LoadScene("BncQ", LoadSceneMode.Single);
	}
	
	public void Quit (){
		Application.Quit();
	}
}