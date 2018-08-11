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
	
	// Result variables
	private int scoreValue = 0;
	private bool quit;

	
	void Start(){
		StartCoroutine (StartScene());
	}
	
	IEnumerator StartScene(){
		yield return null;
		resultRect = GameObject.Find("Canvas/Scroll View/Viewport/Result").GetComponent<RectTransform>();
		avatar = GameObject.Find("Canvas/Scroll View/Viewport/Result/Avatar").gameObject;
		avatar.transform.Find("Item1").GetComponent<RawImage>().texture = SessionScript.avatarItem1[SessionScript.selectedItem1];
		avatar.transform.Find("Item2").GetComponent<RawImage>().texture = SessionScript.avatarItem2[SessionScript.selectedItem2];
		scoreValueText = GameObject.Find("Canvas/Scroll View/Viewport/Result/ScoreValue").GetComponent<Text>();
		scoreValue = SessionScript.score;
		scoreValueText.text = scoreValue.ToString();
	}
	
	
	void Update(){
		if (endScene){
			resultRect.anchoredPosition = new Vector2 (resultRect.anchoredPosition.x, resultRect.anchoredPosition.y - Time.deltaTime * 1200);
			return;
		}
		if (quit){
			SessionScript.songAudio.volume = SessionScript.songAudio.volume - (Time.deltaTime * 2);
		}
	}
	
	public void SelectMenu(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		Invoke ("EndScene", 0.25f);
		Invoke ("NextScene", 0.5f);
		
	}
	
	public void SelectQuit(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		quit = false;
		print ("QUIT");
		Invoke ("EndScene", 0.25f);
		Invoke ("Quit", 0.5f);
	}
	
	void Quit(){
		Application.Quit();
	}
	
	public void NextScene(){
		SceneManager.LoadScene("menu", LoadSceneMode.Single);
	}
	
	void EndScene(){
		endScene = true;
	}
}
