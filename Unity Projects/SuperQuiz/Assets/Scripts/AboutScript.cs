using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AboutScript : MonoBehaviour  {
	
	// UI
	public RectTransform resultRect;
	public bool endScene;
	private bool quit;
	
	void Start(){
		StartCoroutine (StartScene());
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
	
	IEnumerator StartScene(){
		yield return null;
		resultRect = GameObject.Find("Canvas/Scroll View/Viewport/About").GetComponent<RectTransform>();
	}
	
	public void Noise(){
		SessionScript.ButtonAudio(SessionScript.negative);
	}
	
	public void SelectMenu(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		Invoke ("EndScene", 0.25f);
		Invoke ("NextScene", 0.5f);
	}
	
	public void NextScene(){
		SceneManager.LoadScene("menu", LoadSceneMode.Single);
	}
	
	void EndScene(){
		endScene = true;
	}
}

