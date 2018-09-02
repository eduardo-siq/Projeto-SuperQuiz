using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScript : MonoBehaviour {

	// UI
	public RectTransform resultRect;
	public bool endScene;
	public GameObject avatar;
	public Text scoreValueText;
	private bool quit;
	
	// Result variables
	private int scoreValue = 0;
	
	// Navigation
	string nextScene = "";

	
	void Start(){
		StartCoroutine (StartScene());
	}
	
	IEnumerator StartScene(){
		yield return null;
		resultRect = GameObject.Find("Canvas/Scroll View/Viewport/Result").GetComponent<RectTransform>();
		avatar = GameObject.Find("Canvas/Scroll View/Viewport/Result/Scroll View/Viewport/Avatar").gameObject;
		avatar.transform.Find("Item1").GetComponent<RawImage>().texture = SessionScript.avatarItem1[SessionScript.selectedItem1];
		avatar.transform.Find("Item2").GetComponent<RawImage>().texture = SessionScript.avatarItem2[SessionScript.selectedItem2];
		avatar.transform.Find("Item3").GetComponent<RawImage>().texture = SessionScript.avatarItem3[SessionScript.selectedItem3];
		scoreValueText = GameObject.Find("Canvas/Scroll View/Viewport/Result/ScoreValue").GetComponent<Text>();
		scoreValue = SessionScript.score;
		scoreValueText.text = scoreValue.ToString();
	}
	
	
	void Update(){
		// if (endScene){
			// resultRect.anchoredPosition = new Vector2 (resultRect.anchoredPosition.x, resultRect.anchoredPosition.y - Time.deltaTime * 1200);
			// return;
		// }
		if (quit){
			SessionScript.songAudio.volume = SessionScript.songAudio.volume - (Time.deltaTime * 2);
		}
	}
	
	public void SelectMenu(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		nextScene = "menu";
		Invoke ("EndScene", 0.25f);
		Invoke ("NextScene", 0.5f);
		
	}
	
	public void SelectAnswers(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		nextScene = "answers";
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
		endScene = true;
	}
}
