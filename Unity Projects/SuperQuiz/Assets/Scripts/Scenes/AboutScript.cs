using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AboutScript : MonoBehaviour{

	// UI
	public RectTransform aboutRect;
	public bool endScene;
	private bool quit;
	public GameObject faqWindow;
	public GameObject rulesWindow;
	public string nextScene;

	void Start(){
		// StartCoroutine(StartScene());
		aboutRect = GameObject.Find("Canvas/Scroll View/Viewport/About").GetComponent<RectTransform>();
		faqWindow = GameObject.Find("Canvas/Scroll View/Viewport/About/FAQWindow").gameObject;
		rulesWindow = GameObject.Find("Canvas/Scroll View/Viewport/About/RulesWindow").gameObject;
		// faqWindow.SetActive(false);
		// rulesWindow.SetActive(false);
		// faqWindow.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0f, 0f);	// Objeto começa fora de cena para que seu Start() ocorra, mas fora da visão do player
		// rulesWindow.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0f, 0f);	// Objeto começa fora de cena para que seu Start() ocorra, mas fora da visão do player
	}

	void Update(){
		// if (endScene){
		// aboutRect.anchoredPosition = new Vector2 (aboutRect.anchoredPosition.x, aboutRect.anchoredPosition.y - Time.deltaTime * 1200);
		// return;
		// }
		if (quit){
			SessionScript.songAudio.volume = SessionScript.songAudio.volume - (Time.deltaTime * 2);
		}
	}

	IEnumerator StartScene(){
		yield return null;
		aboutRect = GameObject.Find("Canvas/Scroll View/Viewport/About").GetComponent<RectTransform>();
		faqWindow = GameObject.Find("Canvas/Scroll View/Viewport/About/FAQWindow").gameObject;
		rulesWindow = GameObject.Find("Canvas/Scroll View/Viewport/About/RulesWindow").gameObject;
		// faqWindow.SetActive(false);
		// rulesWindow.SetActive(false);
		// faqWindow.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0f, 0f);	// Objeto começa fora de cena para que seu Start() ocorra, mas fora da visão do player
		// rulesWindow.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0f, 0f);	// Objeto começa fora de cena para que seu Start() ocorra, mas fora da visão do player
	}
	
	public void SelectReturn(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		Invoke("CloseWindow", 0.5f);
	}
	
	void CloseWindow(){
		faqWindow.SetActive(false);
		rulesWindow.SetActive(false);
	}
	
	public void SelectFaq(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		Invoke("OpenFaq", 0.5f);
	}
	
	void OpenFaq(){
		faqWindow.SetActive(true);
	}
	
	public void SelectRules(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		Invoke("OpenRules", 0.5f);
	}
	
	void OpenRules(){
		rulesWindow.SetActive(true);
	}
	
	public void SelectTutorial(){
		nextScene = "tutorial";
		SessionScript.showAvararPictureMessage = true;
		SessionScript.ButtonAudio(SessionScript.neutral);
		Invoke("EndScene", 1.2f);
		Invoke("NextScene", 0.2f);
		// TransitionScript.PlayAnimation();
		// TransitionScript.StartAnimation();
		TransitionScript.EndAnimation();
	}

	public void Noise(){
		SessionScript.ButtonAudio(SessionScript.negative);
	}

	public void SelectMenu(){
		nextScene = "menu";
		SessionScript.ButtonAudio(SessionScript.neutral);
		Invoke("EndScene", 1.2f);
		Invoke("NextScene", 0.2f);
		// TransitionScript.PlayAnimation();
		// TransitionScript.StartAnimation();
		TransitionScript.EndAnimation();
	}

	public void NextScene(){
		SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
	}

	void EndScene(){	// OBSOLETE?
		endScene = true;
	}
	
//		DESAFIO QUIZ, version alpha 0.7
//		developed by ROCKET PRO GAMES, rocketprogames@gmail.com
//		script by Eduardo Siqueira
//		São Paulo, Brasil, 2019
}

