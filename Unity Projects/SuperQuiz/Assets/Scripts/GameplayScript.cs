using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayScript : MonoBehaviour {

	// UI
	public RectTransform gameplayRect;
	public GameObject avatar;
	public bool endScene;
	public string nextScene = "";
	
	// Question UI
	public GameObject questionMultiple;
	public GameObject questionWrite;
	public List <GameObject> answers;
	public InputField writtenAnswer;
	public GameObject questionMultipleText;
	public GameObject questionWriteText;
	public GameObject questionPoint;
	public GameObject questionPointText;
	public GameObject questionPointButton;
	public GameObject questionPointConfirm;
	public RawImage questionPointDetail;
	public GameObject questionLong;
	public GameObject questionLongText;
	public GameObject questionLongAnswer;
	public List <GameObject> questionLongAnswers;
	public GameObject questionImage;
	public RawImage questionImageTexture;
	public Image clockImage;
	public Text clockText;
	public GameObject nextQuestion;
	public GameObject menu;
	public Vector2 questionPointOffset;
	public GameObject correctAnswer;
	//public GameObject result;
	
	// Question Variables
	public int rightAnswer;
	public Question currentQuestion;
	public float currentQuestionTime;
	public float lastQuestionTime;
	public bool answerPermitted;
	public Vector3 questionPointAnswer;
	public int clickX;
	public int clickY;
	public bool showAnswer;
	
	void Start(){
		StartCoroutine (StartScene());
	}
	
	IEnumerator StartScene(){
		yield return null;
		// UI
		gameplayRect = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay").GetComponent<RectTransform>();
		avatar = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/Avatar").gameObject;
		avatar.transform.Find("Item1").GetComponent<RawImage>().texture = SessionScript.avatarItem1[SessionScript.selectedItem1];
		avatar.transform.Find("Item2").GetComponent<RawImage>().texture = SessionScript.avatarItem2[SessionScript.selectedItem2];
		
		// Question UI
		questionMultiple = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionMultiple").gameObject;
		questionWrite = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionWrite").gameObject;
		answers = new List <GameObject>();
		answers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionMultiple/ItemA").gameObject);
		answers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionMultiple/ItemB").gameObject);
		answers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionMultiple/ItemC").gameObject);
		answers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionMultiple/ItemD").gameObject);
		answers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionMultiple/ItemE").gameObject);
		questionMultipleText = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionMultiple/Question").gameObject;
		writtenAnswer = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionWrite/WrittenAnswer").GetComponent<InputField>();
		questionWriteText = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionWrite/Question").gameObject;
		questionPoint = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionPoint").gameObject;
		questionPointText = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionPoint/QuestionPointText").gameObject;
		questionPointButton = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionPoint/QuestionPointButton").gameObject;
		questionPointConfirm = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionPoint/QuestionPointConfirm").gameObject;
		questionPointDetail = questionPointConfirm.transform.Find("Text/Detail").GetComponent<RawImage>();
		questionLong = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionLong").gameObject;
		questionLongText = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionLong/QuestionText").gameObject;
		questionLongAnswer = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionLong/QuestionAnswers").gameObject;
		questionLongAnswers = new List <GameObject>();
		questionLongAnswers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionLong/QuestionAnswers/ItemA").gameObject);
		questionLongAnswers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionLong/QuestionAnswers/ItemB").gameObject);
		questionLongAnswers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionLong/QuestionAnswers/ItemC").gameObject);
		questionLongAnswers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionLong/QuestionAnswers/ItemD").gameObject);
		questionLongAnswers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionLong/QuestionAnswers/ItemE").gameObject);
		questionImage = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionImage").gameObject;
		questionImageTexture = questionImage.GetComponent<RawImage>();
		clockImage = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/Clock/Image").gameObject.GetComponent<Image>();
		clockImage.fillAmount = 0;
		clockText = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/Clock/Text").gameObject.GetComponent<Text>();
		clockText.text = "";
		nextQuestion = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/NextQuestion").gameObject;
		menu = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/Menu").gameObject;
		correctAnswer = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/CorrectAnswer").gameObject;
		//result = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/Result").gameObject;
		//result.SetActive(false);
		questionImage.SetActive(false);
		questionMultiple.SetActive(false);
		questionPoint.SetActive(false);
		questionLong.SetActive(false);
		questionWrite.SetActive(false);
		nextQuestion.SetActive(false);
		menu.SetActive(false);
		showAnswer = false;
		
		//Point-and-click
		questionPoint.transform.Find("Background").GetComponent<RawImage>().texture = SessionScript.texturePoint;
		questionPointButton.transform.Find("Button").GetComponent<Image>().sprite = SessionScript.spritePoint;
		RectTransform playabeArea = GameObject.Find("Canvas/Background").GetComponent<RectTransform>();
		float x = Screen.width/2 - playabeArea.sizeDelta.x/2;
		float y = Screen.height/2 - playabeArea.sizeDelta.y/2;
		questionPointOffset = new Vector2 (x,y);
		print ("questionPointOffset " + questionPointOffset);
		
		// TEST TEST TES TEST
		Invoke ("StartNewQuestion", 0.1f);
	}
	
	void Update(){		// CHANGED ANSER QUESTION METHOD -> UPDATE TIMEOUT METHOD
		if (endScene){
			gameplayRect.anchoredPosition = new Vector2 (gameplayRect.anchoredPosition.x, gameplayRect.anchoredPosition.y - Time.deltaTime * 1200);
			return;
		}	
		// Question Timer
		//int index = currentQuestion.index;
		if (answerPermitted){
			currentQuestionTime = currentQuestionTime + Time.deltaTime;
			clockImage.fillAmount = currentQuestionTime/SessionScript.questionTime;
			clockText.text = (SessionScript.questionTime - currentQuestionTime).ToString("0.");
			if (currentQuestionTime > SessionScript.questionTime){
					AnswerTimeout();
					answerPermitted = false;
				if (currentQuestion.questionType == 0){
					answerPermitted = false;
					SessionScript.ButtonAudio(SessionScript.negative);
					//SessionScript.answersList.Add(new Answer(index, false, SessionScript.questionList[index].subject, lastQuestionTime));	// CHANGE THIS PART TO "AcceptMultipleAnswer(wrong)"
					//Invoke ("StartNewQuestion", 1f);
					//ChooseAnswer(-1);
				}
				if (currentQuestion.questionType == 1){
					//AcceptAnswer();
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.Return)){
			if (currentQuestion.questionType == 1 && writtenAnswer.text != ""){	// Fill-the-blank timeout
				//AcceptAnswer();
			}
		}
	}
		
	public void StartNewQuestion(){
		answerPermitted = true;
		showAnswer = false;
		
		bool clear;
		int i = 0;
		do {	// Randomly chooses next question and checks if it's repeated
			clear = true;
			currentQuestion = SessionScript.questionList[Random.Range(0, SessionScript.questionList.Count)];
			if (SessionScript.questionsAskedList.Count != 0){
				for (int y = 0; y < SessionScript.questionsAskedList.Count; y ++){
					if (currentQuestion.index == SessionScript.questionsAskedList[y]){
						clear = false;
					}
				}
				i = i +1;
				if (i > 1000){ clear = true; print ("ENDLESS LOOP!");}	// Escape valve for an unforeseen endless loop
			}
		} while (!clear);
		
		SessionScript.questionsAskedList.Add(currentQuestion.index);
		correctAnswer.SetActive(false);
		nextQuestion.SetActive(false);
		menu.SetActive(false);
		//result.SetActive(false);
		clockText.text = SessionScript.questionTime.ToString();
		
		int a0;
		int a1;
		int a2;
		int a3;
		int a4;
		switch (currentQuestion.questionType){	
		case 1:	// Fill-the-blank
			questionImage.SetActive(false);
			questionWrite.SetActive(true);
			questionPoint.SetActive(false);
			questionMultiple.SetActive(false);
			questionLong.SetActive(false);
			questionWriteText.transform.Find("Text").GetComponent<Text>().text = currentQuestion.text;
			writtenAnswer.GetComponent<Image>().color = new Color(0.975f, 0.975f, 0.975f, 1);
			writtenAnswer.ActivateInputField();
			break;
		case 2:	// Point-and-click
			questionImage.SetActive(false);
			questionWrite.SetActive(false);
			questionPoint.SetActive(true);
			questionMultiple.SetActive(false);
			questionLong.SetActive(false);
			questionPointText.SetActive(true);
			questionPointButton.SetActive(false);
			questionPointConfirm.SetActive(false);
			questionPointText.transform.Find("Question/Text").GetComponent<Text>().text = currentQuestion.text;
			float red = float.Parse(currentQuestion.answer1);
			float blue = float.Parse(currentQuestion.answer2);
			float green = float.Parse(currentQuestion.answer3);
			questionPointAnswer = new Vector3 (red, blue, green);
			print ("point-and-click");
			break;
		case 3:	// Long
			questionImage.SetActive(false);
			questionMultiple.SetActive(false);
			questionPoint.SetActive(false);
			questionLong.SetActive(true);
			questionLongAnswer.SetActive(false);
			questionLongText.SetActive(true);
			questionWrite.SetActive(false);
			questionLongText.transform.Find("Question/Text").GetComponent<Text>().text = currentQuestion.text;
			a0 = Random.Range(0,5);	// Randomly chooses which option would be the correct one
			rightAnswer = a0;
			a1 = Random.Range(0,5);
			while (a1 == a0){
				a1 = Random.Range(0,5);
			}
			a2 = Random.Range(0,5);
			while (a2 == a0 || a2 == a1){
				a2 = Random.Range(0,5);
			}
			a3 = Random.Range(0,5);
			while (a3 == a0 || a3 == a1 || a3 == a2){
				a3 = Random.Range(0,5);
			}
			a4 = Random.Range(0,5);
			while (a4 == a0 || a4 == a1 || a4 == a2 || a4 == a3){
				a4 = Random.Range(0,5);
			}
			
			questionLongAnswers[a0].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer0;
			questionLongAnswers[0].GetComponent<Image>().color = new Color(0.975f, 0.975f, 0.975f, 1);
			questionLongAnswers[a1].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer1;
			questionLongAnswers[1].GetComponent<Image>().color = new Color(0.975f, 0.975f, 0.975f, 1);
			questionLongAnswers[a2].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer2;
			questionLongAnswers[2].GetComponent<Image>().color = new Color(0.975f, 0.975f, 0.975f, 1);
			questionLongAnswers[a3].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer3;
			questionLongAnswers[3].GetComponent<Image>().color = new Color(0.975f, 0.975f, 0.975f, 1);
			questionLongAnswers[a4].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer4;
			questionLongAnswers[4].GetComponent<Image>().color = new Color(0.975f, 0.975f, 0.975f, 1);
			
			questionLongAnswers[0].transform.Find("Text").GetComponent<Text>().text = "A) " + questionLongAnswers[0].transform.Find("Text").GetComponent<Text>().text;
			questionLongAnswers[1].transform.Find("Text").GetComponent<Text>().text = "B) " + questionLongAnswers[1].transform.Find("Text").GetComponent<Text>().text;
			questionLongAnswers[2].transform.Find("Text").GetComponent<Text>().text = "C) " + questionLongAnswers[2].transform.Find("Text").GetComponent<Text>().text;
			questionLongAnswers[3].transform.Find("Text").GetComponent<Text>().text = "D) " + questionLongAnswers[3].transform.Find("Text").GetComponent<Text>().text;
			questionLongAnswers[4].transform.Find("Text").GetComponent<Text>().text = "E) " + questionLongAnswers[4].transform.Find("Text").GetComponent<Text>().text;
			break;
		case 4:	// Image
			questionMultiple.SetActive(true);
			questionWrite.SetActive(false);
			questionPoint.SetActive(false);
			questionLong.SetActive(false);
			questionImage.SetActive(true);
			questionImageTexture.texture = currentQuestion.questionImage;
			questionMultipleText.transform.Find("Text").GetComponent<Text>().text = currentQuestion.text;
			a0 = Random.Range(0,5);	// Randomly chooses which option would be the correct one
			rightAnswer = a0;
			a1 = Random.Range(0,5);
			while (a1 == a0){
				a1 = Random.Range(0,5);
			}
			a2 = Random.Range(0,5);
			while (a2 == a0 || a2 == a1){
				a2 = Random.Range(0,5);
			}
			a3 = Random.Range(0,5);
			while (a3 == a0 || a3 == a1 || a3 == a2){
				a3 = Random.Range(0,5);
			}
			a4 = Random.Range(0,5);
			while (a4 == a0 || a4 == a1 || a4 == a2 || a4 == a3){
				a4 = Random.Range(0,5);
			}
			
			answers[a0].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer0;
			answers[0].GetComponent<Image>().color = new Color(0.975f, 0.975f, 0.975f, 1);
			answers[a1].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer1;
			answers[1].GetComponent<Image>().color = new Color(0.975f, 0.975f, 0.975f, 1);
			answers[a2].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer2;
			answers[2].GetComponent<Image>().color = new Color(0.975f, 0.975f, 0.975f, 1);
			answers[a3].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer3;
			answers[3].GetComponent<Image>().color = new Color(0.975f, 0.975f, 0.975f, 1);
			answers[a4].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer4;
			answers[4].GetComponent<Image>().color = new Color(0.975f, 0.975f, 0.975f, 1);
			
			answers[0].transform.Find("Text").GetComponent<Text>().text = "A) " + answers[0].transform.Find("Text").GetComponent<Text>().text;
			answers[1].transform.Find("Text").GetComponent<Text>().text = "B) " + answers[1].transform.Find("Text").GetComponent<Text>().text;
			answers[2].transform.Find("Text").GetComponent<Text>().text = "C) " + answers[2].transform.Find("Text").GetComponent<Text>().text;
			answers[3].transform.Find("Text").GetComponent<Text>().text = "D) " + answers[3].transform.Find("Text").GetComponent<Text>().text;
			answers[4].transform.Find("Text").GetComponent<Text>().text = "E) " + answers[4].transform.Find("Text").GetComponent<Text>().text;
			break;
		default:	// Multiple answer
			questionMultiple.SetActive(true);
			questionPoint.SetActive(false);
			questionLong.SetActive(false);
			questionWrite.SetActive(false);
			questionImage.SetActive(false);
			questionMultipleText.transform.Find("Text").GetComponent<Text>().text = currentQuestion.text;
			a0 = Random.Range(0,5);	// Randomly chooses which option would be the correct one
			rightAnswer = a0;
			a1 = Random.Range(0,5);
			while (a1 == a0){
				a1 = Random.Range(0,5);
			}
			a2 = Random.Range(0,5);
			while (a2 == a0 || a2 == a1){
				a2 = Random.Range(0,5);
			}
			a3 = Random.Range(0,5);
			while (a3 == a0 || a3 == a1 || a3 == a2){
				a3 = Random.Range(0,5);
			}
			a4 = Random.Range(0,5);
			while (a4 == a0 || a4 == a1 || a4 == a2 || a4 == a3){
				a4 = Random.Range(0,5);
			}
			
			answers[a0].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer0;
			answers[0].GetComponent<Image>().color = new Color(0.975f, 0.975f, 0.975f, 1);
			answers[a1].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer1;
			answers[1].GetComponent<Image>().color = new Color(0.975f, 0.975f, 0.975f, 1);
			answers[a2].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer2;
			answers[2].GetComponent<Image>().color = new Color(0.975f, 0.975f, 0.975f, 1);
			answers[a3].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer3;
			answers[3].GetComponent<Image>().color = new Color(0.975f, 0.975f, 0.975f, 1);
			answers[a4].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer4;
			answers[4].GetComponent<Image>().color = new Color(0.975f, 0.975f, 0.975f, 1);
			
			answers[0].transform.Find("Text").GetComponent<Text>().text = "A) " + answers[0].transform.Find("Text").GetComponent<Text>().text;
			answers[1].transform.Find("Text").GetComponent<Text>().text = "B) " + answers[1].transform.Find("Text").GetComponent<Text>().text;
			answers[2].transform.Find("Text").GetComponent<Text>().text = "C) " + answers[2].transform.Find("Text").GetComponent<Text>().text;
			answers[3].transform.Find("Text").GetComponent<Text>().text = "D) " + answers[3].transform.Find("Text").GetComponent<Text>().text;
			answers[4].transform.Find("Text").GetComponent<Text>().text = "E) " + answers[4].transform.Find("Text").GetComponent<Text>().text;
			break;
		}
	}
	
	public void AnswerQuestion(int answer){		// ADD TIMEOUT FOR FILL THE BLANKS (MAYBE A TimeoutQuestion METHOD INSTEAD OF A "-1" ANSWER FOR TIMEOUT
		if (!answerPermitted){
			return;
		}
		switch (currentQuestion.questionType){
			case 1:		// Fill-the-blank
				if (!answerPermitted) return;
				if (writtenAnswer.text == currentQuestion.answer0){
					writtenAnswer.gameObject.GetComponent<Image>().color = Color.green;
					SessionScript.ButtonAudio(SessionScript.positive);
					SessionScript.answersList.Add(new Answer(currentQuestion.index, true, false, currentQuestion.subject, lastQuestionTime));
					SessionScript.score = SessionScript.score + SessionScript.rightScore;	// PLACEHOLDER SCORE // PLACEHOLDER SCORE // PLACEHOLDER SCORE // PLACEHOLDER SCORE // PLACEHOLDER SCORE 	
				}
				if (writtenAnswer.text != currentQuestion.answer0){
					writtenAnswer.gameObject.GetComponent<Image>().color = Color.red;
					SessionScript.ButtonAudio(SessionScript.negative);
					SessionScript.answersList.Add(new Answer(currentQuestion.index, false, false, currentQuestion.subject, lastQuestionTime));
					SessionScript.score = SessionScript.score + SessionScript.wrongScore;
					showAnswer = true;
				}
				answerPermitted = false;
				Invoke ("EndQuestion", 0.5f);	
				break;
			case 2:		// Point-and-click
				Vector3 click = Input.mousePosition;
				click.x = click.x - questionPointOffset.x;
				click.y = click.y - questionPointOffset.y;
				print ("click " + click.x + ", " + click.y);
				clickX = Mathf.RoundToInt(click.x);
				clickY = Mathf.RoundToInt(click.y);
				bool black = false;
				Color pixelColor = SessionScript.pointAndClickSource.GetPixel(clickX,clickY);
				Vector3 colorInput = new Vector3 (pixelColor.r, pixelColor.b, pixelColor.g);
				if (colorInput.x >= - 0.05f && colorInput.x < 0.05f){
					if (colorInput.y >= - 0.05f && colorInput.y < 0.05f){
						if (colorInput.z >= - 0.05f && colorInput.z < 0.05f){
							black = true;
							print ("black");
							SessionScript.ButtonAudio(SessionScript.subtle);
						}
					}
				}
				if (!black){
					SessionScript.ButtonAudio(SessionScript.neutral);
					Invoke("ToPointConfirm", 0.25f);
					//sound?
				}
				break;
			case 3:		// Long
				if (answer == rightAnswer){
				questionLongAnswers[answer].GetComponent<Image>().color = Color.green;
				SessionScript.ButtonAudio(SessionScript.positive);
				SessionScript.answersList.Add(new Answer(currentQuestion.index, true, false, currentQuestion.subject, lastQuestionTime));
				SessionScript.score = SessionScript.score + SessionScript.rightScore;	// PLACEHOLDER SCORE // PLACEHOLDER SCORE // PLACEHOLDER SCORE // PLACEHOLDER SCORE // PLACEHOLDER SCORE 
				} else{
					questionLongAnswers[answer].GetComponent<Image>().color = Color.red;
					SessionScript.ButtonAudio(SessionScript.negative);
					SessionScript.answersList.Add(new Answer(currentQuestion.index, false, false, currentQuestion.subject, lastQuestionTime));
					SessionScript.score = SessionScript.score + SessionScript.wrongScore;
					showAnswer = true;
				}
				answerPermitted = false;
				Invoke ("EndQuestion", 0.5f);
				break;
			case 4:		// Image
				if (answer == rightAnswer){
				answers[answer].GetComponent<Image>().color = Color.green;
				SessionScript.ButtonAudio(SessionScript.positive);
				SessionScript.answersList.Add(new Answer(currentQuestion.index, true, false, currentQuestion.subject, lastQuestionTime));
				SessionScript.score = SessionScript.score + SessionScript.rightScore;	// PLACEHOLDER SCORE // PLACEHOLDER SCORE // PLACEHOLDER SCORE // PLACEHOLDER SCORE // PLACEHOLDER SCORE 
				} else{
					answers[answer].GetComponent<Image>().color = Color.red;
					SessionScript.ButtonAudio(SessionScript.negative);
					SessionScript.answersList.Add(new Answer(currentQuestion.index, false, false, currentQuestion.subject, lastQuestionTime));
					SessionScript.score = SessionScript.score + SessionScript.wrongScore;
					showAnswer = true;
				}
				answerPermitted = false;
				Invoke ("EndQuestion",  0.5f);
				break;
			default:	// Multiple answer
				if (answer == rightAnswer){
				answers[answer].GetComponent<Image>().color = Color.green;
				SessionScript.ButtonAudio(SessionScript.positive);
				SessionScript.answersList.Add(new Answer(currentQuestion.index, true, false, currentQuestion.subject, lastQuestionTime));
				SessionScript.score = SessionScript.score + SessionScript.rightScore;	// PLACEHOLDER SCORE // PLACEHOLDER SCORE // PLACEHOLDER SCORE // PLACEHOLDER SCORE // PLACEHOLDER SCORE 
				} else{
					answers[answer].GetComponent<Image>().color = Color.red;
					SessionScript.ButtonAudio(SessionScript.negative);
					SessionScript.answersList.Add(new Answer(currentQuestion.index, false, false, currentQuestion.subject, lastQuestionTime));
					SessionScript.score = SessionScript.score + SessionScript.wrongScore;
					showAnswer = true;
				}
				answerPermitted = false;
				Invoke ("EndQuestion", 0.5f);
				break;
		}
	}
	
	public void AnswerTimeout(){
		if (!answerPermitted){
			return;
		}
		switch (currentQuestion.questionType){
			case 1:		// Fill-the-blank
				if (!answerPermitted) return;
				if (writtenAnswer.text == currentQuestion.answer0){
					writtenAnswer.gameObject.GetComponent<Image>().color = Color.green;
					SessionScript.ButtonAudio(SessionScript.positive);
					SessionScript.answersList.Add(new Answer(currentQuestion.index, true, true, currentQuestion.subject, lastQuestionTime));
					SessionScript.score = SessionScript.score + SessionScript.rightScore;	// PLACEHOLDER SCORE // PLACEHOLDER SCORE // PLACEHOLDER SCORE // PLACEHOLDER SCORE // PLACEHOLDER SCORE 	
				}
				if (writtenAnswer.text != currentQuestion.answer0 && writtenAnswer.text != ""){
					writtenAnswer.gameObject.GetComponent<Image>().color = Color.red;
					SessionScript.ButtonAudio(SessionScript.negative);
					SessionScript.answersList.Add(new Answer(currentQuestion.index, false, true, currentQuestion.subject, lastQuestionTime));
					SessionScript.score = SessionScript.score + SessionScript.wrongScore;
					showAnswer = true;
				}
				if (writtenAnswer.text == ""){
					writtenAnswer.gameObject.GetComponent<Image>().color = Color.red;
					SessionScript.ButtonAudio(SessionScript.negative);
					SessionScript.answersList.Add(new Answer(currentQuestion.index, false, true, currentQuestion.subject, lastQuestionTime));
					SessionScript.score = SessionScript.score + SessionScript.timeoutScore;
					showAnswer = true;
				}
				answerPermitted = false;
				Invoke ("EndQuestion", 0.5f);	
				break;
			case 2:		// Point-and-click
				SessionScript.ButtonAudio(SessionScript.negative);
				SessionScript.answersList.Add(new Answer(currentQuestion.index, false, true, currentQuestion.subject, lastQuestionTime));
				SessionScript.score = SessionScript.score + SessionScript.timeoutScore;
				answerPermitted = false;
				showAnswer = true;
				Invoke ("EndQuestion", 0.5f);
				break;
			case 3:		// Long
				SessionScript.ButtonAudio(SessionScript.negative);
				SessionScript.answersList.Add(new Answer(currentQuestion.index, false, true, currentQuestion.subject, lastQuestionTime));
				SessionScript.score = SessionScript.score + SessionScript.timeoutScore;
				answerPermitted = false;
				showAnswer = true;
				Invoke ("EndQuestion", 0.5f);
				break;
			case 4:		// Image
				SessionScript.ButtonAudio(SessionScript.negative);
				SessionScript.answersList.Add(new Answer(currentQuestion.index, true, true, currentQuestion.subject, lastQuestionTime));
				SessionScript.score = SessionScript.score + SessionScript.timeoutScore;
				answerPermitted = false;
				showAnswer = true;
				Invoke ("EndQuestion",  0.5f);
				break;
			default:	// Multiple answer
				SessionScript.ButtonAudio(SessionScript.negative);
				SessionScript.answersList.Add(new Answer(currentQuestion.index, true, true, currentQuestion.subject, lastQuestionTime));
				SessionScript.score = SessionScript.score + SessionScript.timeoutScore;
				answerPermitted = false;
				showAnswer = true;
				Invoke ("EndQuestion", 0.5f);
				break;
		}
	}
	
	public void QuestionPointCheckPixel(){
		bool wrong = false;
		Color pixelColor = SessionScript.pointAndClickSource.GetPixel(clickX,clickY);
		Vector3 colorInput = new Vector3 (pixelColor.r, pixelColor.b, pixelColor.g);
		print ("pixelColor " + colorInput + "/ answer: " + questionPointAnswer);
		if (questionPointAnswer.x <= colorInput.x - 0.05f || colorInput.x + 0.05f < questionPointAnswer.x){	wrong = true;}
		if (questionPointAnswer.y <= colorInput.y - 0.05f || colorInput.y + 0.05f < questionPointAnswer.y){	wrong = true;}	
		if (questionPointAnswer.z <= colorInput.z - 0.05f || colorInput.z + 0.05f < questionPointAnswer.z){	wrong = true;}
		if (wrong){
			print ("wrong");
			SessionScript.ButtonAudio(SessionScript.negative);
			SessionScript.score = SessionScript.score + SessionScript.wrongScore;
			showAnswer = true;
		}
		if (!wrong){
			print ("right");
			SessionScript.ButtonAudio(SessionScript.positive);
			SessionScript.score = SessionScript.score + SessionScript.rightScore;
		}
		answerPermitted = false;
		Invoke ("EndQuestion", 0.5f);	
	}
	
	public void EndQuestion(){
		if (questionWrite.activeSelf){
			writtenAnswer.text = "";
		}
		questionMultiple.SetActive(false);
		questionWrite.SetActive(false);
		questionPoint.SetActive(false);
		questionLong.SetActive(false);
		questionImage.SetActive(false);
		if(showAnswer){
			correctAnswer.SetActive(true);
			correctAnswer.transform.Find("Frame/Text").GetComponent<Text>().text = "RESPOSTA: " + currentQuestion.answer0;
			if (currentQuestion.questionType != 2){
				correctAnswer.transform.Find("Image").gameObject.SetActive(false);
			}
			if (currentQuestion.questionType == 2){
				float red = float.Parse(currentQuestion.answer1);
				float blue = float.Parse(currentQuestion.answer2);
				float green = float.Parse(currentQuestion.answer3);
				correctAnswer.transform.Find("Image").gameObject.SetActive(true);
				correctAnswer.transform.Find("Image").GetComponent<RawImage>().texture = SessionScript.missingTexture;
				Vector3 colorInput = new Vector3 (red, blue, green);
				questionPointDetail.texture = SessionScript.missingTexture;
				for (int i = 0; i < SessionScript.detail.Count; i++){
					if (colorInput == SessionScript.detail[i].colorCode){
						correctAnswer.transform.Find("Image").GetComponent<RawImage>().texture = SessionScript.detail[i].texture;
						print ("detail found");
						break;	
					}
				}
			}
		}
		if (!SessionScript.singleRun){
			menu.SetActive(true);
		}
		//result.SetActive(true);
		if (SessionScript.questionsAskedList.Count < SessionScript.numberOfQuestionsDemanded){
			nextQuestion.SetActive(true);
		}
		if (SessionScript.questionsAskedList.Count == SessionScript.numberOfQuestionsDemanded){
			menu.SetActive(true);
		}
		currentQuestionTime = 0;
		clockImage.fillAmount = 0;
		clockText.text = "";
	}
	
	public void QuestionPointGetDetail(){
		print ("QuestionPointGetDetail");
		Color pixelColor = SessionScript.pointAndClickSource.GetPixel(clickX,clickY);
		Vector3 colorInput = new Vector3 (pixelColor.r, pixelColor.b, pixelColor.g);
		print ("pixelColor " + pixelColor.r + ", " + pixelColor.b + ", " + pixelColor.g);
		questionPointDetail.texture = SessionScript.missingTexture;
		for (int i = 0; i < SessionScript.detail.Count; i++){
			if (colorInput == SessionScript.detail[i].colorCode){
				questionPointDetail.texture = SessionScript.detail[i].texture;
				print ("detail found");
				break;	
			}
			// bool red = false;	IDENTIFICAR PIXEL COM MARGEL DE ERRO
			// bool blue = false;
			// bool green = false;
			// if (colorInput.x >= SessionScript.detail[i].colorCode.x - 0.05f && SessionScript.detail[i].colorCode.x + 0.05f < colorInput.x){
				// red = true;
				// print ("red ok");
			// }
			// if (colorInput.y >= SessionScript.detail[i].colorCode.y - 0.05f && SessionScript.detail[i].colorCode.y + 0.05f < colorInput.y){
				// blue = true;
				// print ("blue ok");
			// }
			// if (colorInput.z >= SessionScript.detail[i].colorCode.z - 0.05f && SessionScript.detail[i].colorCode.z + 0.05f < colorInput.z){
				// green = true;
				// print ("green ok");
			// }
			// if (red && blue && green){
				// questionPointDetail.texture = SessionScript.detail[i].texture;
				// print ("detail found");
				// break;
			// }
		}
	}
	
	public void ToLongQuestionText(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		questionLongText.SetActive(true);
		questionLongAnswer.SetActive(false);
	}
	
	public void ToLongQuestionAnswer(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		questionLongText.SetActive(false);
		questionLongAnswer.SetActive(true);
	}
	
	public void ToPointText(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		questionPointText.SetActive(true);
		questionPointButton.SetActive(false);
		questionPointConfirm.SetActive(false);
	}
	
	public void ToPointImage(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		questionPointText.SetActive(false);
		questionPointButton.SetActive(true);
		questionPointConfirm.SetActive(false);
	}
	
	public void ToPointConfirm(){
		questionPointText.SetActive(false);
		questionPointButton.SetActive(false);
		questionPointConfirm.SetActive(true);
		QuestionPointGetDetail();
	}
	
	public void SelectNextQuestion(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		Invoke ("StartNewQuestion", 1f);
	}
	
	public void SelectMenu(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		nextScene = "menu";
		Invoke ("EndScene", 0.5f);
		Invoke ("NextScene", 1f);
		
	}
	
	// public void SelectResult(){
		// SessionScript.ButtonAudio(SessionScript.positive);
		// nextScene = "result";
		// Invoke ("EndScene", 0.5f);
		// Invoke ("NextScene", 1f);
		
	// }
	
	public void NextScene(){
		SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
	}
	
	void EndScene(){
		endScene = true;
	}

}