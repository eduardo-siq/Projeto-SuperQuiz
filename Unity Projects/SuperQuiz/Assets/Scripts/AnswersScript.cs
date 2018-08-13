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
	public Text scoreValueText;
	private bool quit;
	
	// Answers
	public Text q0;
	public Text q1;
	public Text q2;
	public Text q3;
	public Text q4;
	
	string right = "acertou";
	string wrong = "errou";
	
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
		resultRect = GameObject.Find("Canvas/Scroll View/Viewport/Answers").GetComponent<RectTransform>();
		q0 = GameObject.Find("Canvas/Scroll View/Viewport/Answers/Q0/Text").gameObject.GetComponent<Text>();
		q1 = GameObject.Find("Canvas/Scroll View/Viewport/Answers/Q1/Text").gameObject.GetComponent<Text>();
		q2 = GameObject.Find("Canvas/Scroll View/Viewport/Answers/Q2/Text").gameObject.GetComponent<Text>();
		q3 = GameObject.Find("Canvas/Scroll View/Viewport/Answers/Q3/Text").gameObject.GetComponent<Text>();
		q4 = GameObject.Find("Canvas/Scroll View/Viewport/Answers/Q4/Text").gameObject.GetComponent<Text>();
		
		string thisAnswer = "";
		
		print ("Answers count: " + SessionScript.answersList.Count);
		if (SessionScript.answersList.Count > 0){
			if (SessionScript.answersList[0].right){
				thisAnswer = right;
			}
			if (!SessionScript.answersList[0].right){
				thisAnswer = wrong;
			}
			
			q0.text = "Questão 1: " + thisAnswer + " (" + SessionScript.answersList[0].time.ToString("0.#") + "s)";
		} else {q0.text = "Ainda não respondeu!";}
		if (SessionScript.answersList.Count > 1){
			if (SessionScript.answersList[1].right){
				thisAnswer = right;
			}
			if (!SessionScript.answersList[1].right){
				thisAnswer = wrong;
			}
			
			q1.text = "Questão 1: " + thisAnswer + " (" + SessionScript.answersList[1].time.ToString("0.#") + "s)";
		} else {q1.text = "Ainda não respondeu!";}
		if (SessionScript.answersList.Count > 2){
			if (SessionScript.answersList[2].right){
				thisAnswer = right;
			}
			if (!SessionScript.answersList[2].right){
				thisAnswer = wrong;
			}
			
			q2.text = "Questão 1: " + thisAnswer + " (" + SessionScript.answersList[2].time.ToString("0.#") + "s)";
		} else {q2.text = "Ainda não respondeu!";}
		if (SessionScript.answersList.Count > 3){
			if (SessionScript.answersList[3].right){
				thisAnswer = right;
			}
			if (!SessionScript.answersList[3].right){
				thisAnswer = wrong;
			}
			
			q3.text = "Questão 1: " + thisAnswer + " (" + SessionScript.answersList[3].time.ToString("0.#") + "s)";
		} else {q3.text = "Ainda não respondeu!";}
		if (SessionScript.answersList.Count > 4){
			if (SessionScript.answersList[4].right){
				thisAnswer = right;
			}
			if (!SessionScript.answersList[4].right){
				thisAnswer = wrong;
			}
			
			q4.text = "Questão 1: " + thisAnswer + " (" + SessionScript.answersList[4].time.ToString("0.#") + "s)";
		} else {q4.text = "Ainda não respondeu!";}
		
		if (SessionScript.answersList.Count == 0){
			q0.text = "Ainda não respondeu!";
			q1.text = "Ainda não respondeu!";
			q2.text = "Ainda não respondeu!";
			q3.text = "Ainda não respondeu!";
			q4.text = "Ainda não respondeu!";
		}
	}
	
	public void SelectAnswers(){
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

