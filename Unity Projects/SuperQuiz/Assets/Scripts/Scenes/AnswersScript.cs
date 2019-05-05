using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnswersScript : MonoBehaviour{

    // UI
    public RectTransform resultRect;
    public bool endScene;
    public GameObject avatar;
    public GameObject answerLinesViewport;
    public GameObject answerLinePrefab;
    public Scrollbar answerLineScrollbar;
    public Sprite answerLineTexture1;
    public Sprite answerLineTexture2;
    public Text scoreValueText;
    private bool quit;

    string right = "acertou";
    string wrong = "errou";

    void Start(){
		// StartCoroutine(StartScene());
		resultRect = GameObject.Find("Canvas/Scroll View/Viewport/Answers").GetComponent<RectTransform>();
		answerLinesViewport = GameObject.Find("Canvas/Scroll View/Viewport/Answers/Scroll View/Viewport/Content").gameObject;
		// answerLinePrefab = Resources.Load("Prefabs/AnswerLine") as GameObject;
		// answerLineTexture1 = Resources.Load("Textures/UI/button_answer_list", typeof(Sprite)) as Sprite;
		// answerLineTexture2 = Resources.Load("Textures/UI/button_answer_list2", typeof(Sprite)) as Sprite;

		AnswersList();
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
        // answerLinePrefab = Resources.Load("Prefabs/AnswerLine") as GameObject;
        // answerLineTexture1 = Resources.Load("Textures/UI/button_answer_list", typeof(Sprite)) as Sprite;
        // answerLineTexture2 = Resources.Load("Textures/UI/button_answer_list2", typeof(Sprite)) as Sprite;

        AnswersList();
    }

	public void AnswersList(){
		if (SessionScript.answersList != null){
			if (SessionScript.answersList.Count > 0){
				string thisAnswer = "";
				int index = 0;
				bool variation1 = true;
				answerLinesViewport.transform.parent.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
				answerLinesViewport.transform.parent.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
				for (int i = 0; i < SessionScript.answersList.Count; i++){
					index = i + 1;
					if (SessionScript.answersList[i].right){
						thisAnswer = right;
					}
					if (!SessionScript.answersList[i].right){
						thisAnswer = wrong;
					}
					GameObject newAnswerLine = Instantiate(answerLinePrefab);
					newAnswerLine.transform.SetParent(answerLinesViewport.transform, true);
					string answerLineText = "";
					if (SessionScript.answersList[i].alternative != -1 && SessionScript.answersList[i].time != -1f){
						answerLineText = "Questão " + index.ToString() + ": " + thisAnswer + " (" + SessionScript.answersList[i].time.ToString("0.#") + "s)";
					}
					if (SessionScript.answersList[i].alternative == -1 && SessionScript.answersList[i].time == -1f){
						answerLineText = "Questão " + index.ToString() + ": ERRO no download da resposta e tempo";
					}
					if (SessionScript.answersList[i].alternative != -1 && SessionScript.answersList[i].time == -1f){
						answerLineText = "Questão " + index.ToString() + ": " + thisAnswer + " (ERRO no download do tempo)";
					}
					if (SessionScript.answersList[i].alternative == -1 && SessionScript.answersList[i].time != -1f){
						answerLineText = "Questão " + index.ToString() + ": ERRO no downlaod da resposta (" + SessionScript.answersList[i].time.ToString("0.#") + "s)";
					}
					newAnswerLine.transform.Find("Text").GetComponent<Text>().text = answerLineText;
					newAnswerLine.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 0f - 30 * i, 0f);
					if (variation1){
						newAnswerLine.GetComponent<Image>().sprite = answerLineTexture1;
					}
					else{
						newAnswerLine.GetComponent<Image>().sprite = answerLineTexture2;
					}
					variation1 = !variation1;
					float sizeY = 30 * (i + 1);
					if (sizeY < 200) sizeY = 200f;
					answerLinesViewport.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, sizeY);
					newAnswerLine.transform.localScale = new Vector3(1, 1, 1);
				}
				answerLinesViewport.transform.parent.transform.parent.Find("Scrollbar Vertical").GetComponent<Scrollbar>().value = 1;
				// answerLinesViewport.transform.parent.transform.parent.Find("Scrollbar Vertical").GetComponent<Scrollbar>().size = 0;
			} else{
				GameObject.Find("Canvas/Scroll View/Viewport/Answers/Warning").gameObject.SetActive(true);
				GameObject.Find("Canvas/Scroll View/Viewport/Answers/Scroll View").gameObject.SetActive(false);
				GameObject.Find("Canvas/Scroll View/Viewport/Answers/Scroll View Background").gameObject.SetActive(false);
			}
		} else{
				GameObject.Find("Canvas/Scroll View/Viewport/Answers/Warning").gameObject.SetActive(true);
				GameObject.Find("Canvas/Scroll View/Viewport/Answers/ScrollView").gameObject.SetActive(false);
				GameObject.Find("Canvas/Scroll View/Viewport/Answers/Scroll View Background").gameObject.SetActive(false);
			}
	}

    public void SelectResult(){
        SessionScript.ButtonAudio(SessionScript.neutral);
        Invoke("EndScene", 1.2f);
        Invoke("NextScene", 0.2f);
        // TransitionScript.PlayAnimation();
        // TransitionScript.StartAnimation();
        TransitionScript.EndAnimation();
    }

    public void NextScene()
    {
        SceneManager.LoadScene("result", LoadSceneMode.Single);
    }

    void EndScene(){	// OBSOLETE?
        endScene = true;
    }
	
//		DESAFIO QUIZ, version alpha 0.7
//		developed by ROCKET PRO GAMES, rocketprogames@gmail.com
//		script by Eduardo Siqueira
//		São Paulo, Brasil, 2019
}

