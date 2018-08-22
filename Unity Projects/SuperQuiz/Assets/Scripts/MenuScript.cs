using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	// UI
	public RectTransform menuRect;
	public bool endScene;
	public GameObject avatar;
	
	// Menu variables
	private string nextScene = "";

	
	void Start(){
		StartCoroutine (StartScene());
	}
	
	IEnumerator StartScene(){
		yield return null;
		menuRect = GameObject.Find("Canvas/Scroll View/Viewport/Menu").GetComponent<RectTransform>();
		avatar = GameObject.Find("Canvas/Scroll View/Viewport/Menu/Avatar").gameObject;
		avatar.transform.Find("Item1").GetComponent<RawImage>().texture = SessionScript.avatarItem1[SessionScript.selectedItem1];
		avatar.transform.Find("Item2").GetComponent<RawImage>().texture = SessionScript.avatarItem2[SessionScript.selectedItem2];
		avatar.transform.Find("Item3").GetComponent<RawImage>().texture = SessionScript.avatarItem3[SessionScript.selectedItem3];
		
		// Checks if there are avaiable question
		if (SessionScript.questionsAskedList.Count >= SessionScript.numberOfQuestionsDemanded){
			GameObject.Find("Canvas/Scroll View/Viewport/Menu/Gameplay/Text").GetComponent<Text>().text = "não há mais questões!";
			GameObject.Find("Canvas/Scroll View/Viewport/Menu/Gameplay").GetComponent<Button>().enabled = false;
		}
	}
	
	
	void Update(){
		if (endScene){
			menuRect.anchoredPosition = new Vector2 (menuRect.anchoredPosition.x, menuRect.anchoredPosition.y - Time.deltaTime * 1200);
			return;
		}
	}
	
	public void Select(string option){
		nextScene = option;
		SessionScript.ButtonAudio(SessionScript.neutral);
		Invoke ("EndScene", 0.25f);
		Invoke ("NextScene", 0.5f);
		
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
}
