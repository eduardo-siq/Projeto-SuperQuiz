using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnswersScript : MonoBehaviour  {
	
	// UI
	public RectTransform resultRect;
	public bool endScene;
	public GameObject avatar;
	public GameObject answerLinesViewport;
	public GameObject answerLinePrefab;
	public Scrollbar answerLineScrollbar;
	public Texture answerLineTexture1;
	public Texture answerLineTexture2;
	public Text scoreValueText;
	private bool quit;
	
	string right = "acertou";
	string wrong = "errou";
	
	void Start(){
		StartCoroutine (StartScene());
		// answerLineScrollbar = GameObject.Find("Canvas/Scroll View/Viewport/Answers/Scroll View/Scrollbar Vertical").GetComponent<Scrollbar>();
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
	
	IEnumerator StartScene(){
		yield return null;
		resultRect = GameObject.Find("Canvas/Scroll View/Viewport/Answers").GetComponent<RectTransform>();
		answerLinesViewport = GameObject.Find("Canvas/Scroll View/Viewport/Answers/Scroll View/Viewport/Content").gameObject;
		answerLinePrefab = Resources.Load("Prefabs/AnswerLine") as GameObject;
		answerLineTexture1 = Resources.Load("Textures/botão_answer_list") as Texture;
		answerLineTexture2 = Resources.Load("Textures/botão_answer_list2") as Texture;

		AnswersList();
	}
	
	void AnswersList(){
		string thisAnswer = "";
		int index = 0;
		bool variation1 = true;
		answerLinesViewport.transform.parent.GetComponent<RectTransform>().anchorMax = new Vector2 (0, 1);
		answerLinesViewport.transform.parent.GetComponent<RectTransform>().anchorMin = new Vector2 (0, 1);
		for (int i = 0; i < SessionScript.answersList.Count ; i++){
			index = i + 1;
			if (SessionScript.answersList[i].right){
				thisAnswer = right;
			}
			if (!SessionScript.answersList[i].right){
				thisAnswer = wrong;
			}
			GameObject newAnswerLine = Instantiate(answerLinePrefab);
			newAnswerLine.transform.SetParent(answerLinesViewport.transform, true);
			newAnswerLine.transform.Find("Text").GetComponent<Text>().text = "Questão " + index.ToString() + ": " + thisAnswer + " (" + SessionScript.answersList[i].time.ToString("0.#") + "s)";
			newAnswerLine.GetComponent<RectTransform>().anchoredPosition = new Vector3 (0f, 0f - 30 * i, 0f);
			if (variation1){
				newAnswerLine.GetComponent<RawImage>().texture = answerLineTexture1;
			} else {
				newAnswerLine.GetComponent<RawImage>().texture = answerLineTexture2;
			}
			variation1 = !variation1;
			float sizeY = 30 * i;
			if (sizeY < 200) sizeY = 200f;
			answerLinesViewport.GetComponent<RectTransform>().sizeDelta = new Vector2 (200f, sizeY);
			newAnswerLine.transform.localScale = new Vector3 (1,1,1);
		}
		answerLinesViewport.transform.parent.transform.parent.Find("Scrollbar Vertical").GetComponent<Scrollbar>().value = 1;
		// answerLinesViewport.transform.parent.transform.parent.Find("Scrollbar Vertical").GetComponent<Scrollbar>().size = 0;
	}
	
	public void SelectResult(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		Invoke ("EndScene", 0.25f);
		Invoke ("NextScene", 0.5f);
	}
	
	public void NextScene(){
		SceneManager.LoadScene("result", LoadSceneMode.Single);
	}
	
	void EndScene(){
		endScene = true;
	}
}

