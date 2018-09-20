using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScript : MonoBehaviour{

	// UI
	public RectTransform resultRect;
	public bool endScene;
	public GameObject avatar;
	public GameObject resultLinesViewport;
	public GameObject resultLinePrefab;
	public Sprite resultLineTexture1;
	public Sprite resultLineTexture2;
	public Text scoreValueText;
	private bool quit;

	// Result variables
	private int scoreValue = 0;

	// Navigation
	string nextScene = "";


	void Start(){
		StartCoroutine(StartScene());
	}

	IEnumerator StartScene(){
		yield return null;
		resultRect = GameObject.Find("Canvas/Scroll View/Viewport/Result").GetComponent<RectTransform>();
		avatar = GameObject.Find("Canvas/Scroll View/Viewport/Result/Scroll View/Viewport/Avatar").gameObject;
		// avatar.transform.Find("Item1").GetComponent<RawImage>().texture = SessionScript.avatarItem1[SessionScript.selectedItem1];
		// avatar.transform.Find("Item2").GetComponent<RawImage>().texture = SessionScript.avatarItem2[SessionScript.selectedItem2];
		// avatar.transform.Find("Item3").GetComponent<RawImage>().texture = SessionScript.avatarItem3[SessionScript.selectedItem3];
		scoreValueText = GameObject.Find("Canvas/Scroll View/Viewport/Result/ScoreValue").GetComponent<Text>();
		scoreValue = SessionScript.score;
		scoreValueText.text = scoreValue.ToString();
		resultLinesViewport = GameObject.Find("Canvas/Scroll View/Viewport/Result/Scroll View/Viewport/Content").gameObject;
		resultLinePrefab = Resources.Load("Prefabs/ResultLine") as GameObject;
		resultLineTexture1 = Resources.Load("Textures/UI/button_result_list", typeof(Sprite)) as Sprite;
		resultLineTexture2 = Resources.Load("Textures/UI/button_result_list2", typeof(Sprite)) as Sprite;
		
		ResultList();
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

	public void ResultList(){
		string thisAnswer = "";
		int index = 0;
		bool variation1 = true;
		resultLinesViewport.transform.parent.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
		resultLinesViewport.transform.parent.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
		for (int i = 0; i < SessionScript.playerList.Count; i++){
			index = i + 1;

			GameObject newResultLine = Instantiate(resultLinePrefab);
			newResultLine.transform.SetParent(resultLinesViewport.transform, true);
			newResultLine.transform.Find("Text").GetComponent<Text>().text = SessionScript.playerList[i].name;
			newResultLine.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 0f - 30 * i, 0f);
			if (variation1){
				newResultLine.GetComponent<Image>().sprite = resultLineTexture1;
			}
			else{
				newResultLine.GetComponent<Image>().sprite = resultLineTexture2;
			}
			variation1 = !variation1;
			float sizeY = 30 * (i + 1);
			if (sizeY < 200) sizeY = 200f;
			resultLinesViewport.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, sizeY);
			newResultLine.transform.localScale = new Vector3(1, 1, 1);
		}
		resultLinesViewport.transform.parent.transform.parent.Find("Scrollbar Vertical").GetComponent<Scrollbar>().value = 1;
		// answerLinesViewport.transform.parent.transform.parent.Find("Scrollbar Vertical").GetComponent<Scrollbar>().size = 0;
	}

	public void SelectMenu(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		nextScene = "menu";
		Invoke("EndScene", 0.25f);
		Invoke("NextScene", 0.5f);

	}

	public void SelectAnswers(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		nextScene = "answers";
		Invoke("EndScene", 1.2f);
		Invoke("NextScene", 1.2f);
		// TransitionScript.PlayAnimation();
		// TransitionScript.StartAnimation();
		TransitionScript.EndAnimation();
	}

	public void SelectQuit(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		quit = true;
		print("QUIT");
		Invoke("EndScene", 0.25f);
		Invoke("Quit", 0.5f);
	}

	void Quit(){
		Application.Quit();
	}

	public void NextScene(){
		SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
	}

	void EndScene(){	// OBSOLETE?
		endScene = true;
	}
}
