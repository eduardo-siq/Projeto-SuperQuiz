﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayScript : MonoBehaviour{

    // UI
    public RectTransform gameplayRect;
    public GameObject avatar;
    public Text scoreText;
	public GameObject scoreAssembly;
	public RectTransform scoreAssemblyTransform;
    public bool endScene;
    public string nextScene = "";
	public GameObject animationFeedback;
    public AnimSuccessScript successAnimation;
    public AnimErrorScript errorAnimation;
    public bool lockButton;
    public GameObject lowerMenu;
	public RectTransform lowerMenuTransform;
	public GameObject questionCounterAssembly;
    public Text questionCounter;

	// Question UI
	public GameObject questionMultiple;
	public GameObject questionWrite;
	public List<GameObject> answers;
	public InputField writtenAnswer;
	public GameObject questionMultipleText;
	public GameObject questionWriteText;
	public GameObject questionPoint;
	public GameObject questionPointText;
	public GameObject questionPointButton;
	public GameObject questionPointConfirm;
	public GameObject questionPointMovable;
	public Image questionPointDetail;
	public GameObject questionLong;
	public GameObject questionLongText;
	public GameObject questionLongAnswer;
	public List<GameObject> questionLongAnswers;
	public GameObject questionImage;
	public Image questionImageTexture;
	public GameObject clockAssembly;
	public RectTransform clockAssemblyTransform;
	public Image clockImage;
	public Text clockText;
	public GameObject nextQuestion;
	public GameObject toMenuText;
	public Vector2 questionPointOffsetMultiplier;
	public Vector2 questionPointOffsetLinear;
	public GameObject correctAnswer;
	public GameObject itemsCounter;
	public Text itemsCounterText;
	public int alternativeA;
	public int alternativeB;
	public int alternativeC;
	public int alternativeD;
	public int alternativeE;
	public GameObject pointAndClickTurn;
	public RectTransform turnSign;
	public bool turnSignOn;
	public float turnSignTimer;
	//public GameObject result;
	
	// Rotate UI
	public bool uiVertical;
	public Vector2 questionPointMovableOriginalPos;
	public Vector2 animationFeedbackOriginalPos;
	public Vector2 correctAnswerOriginalPos;
	public Vector2 clockAssemblyOriginalPos;
	public Vector2 lowerMenuOriginalPos;
	
    // Question Variables
    public int rightAnswer;
    public Question currentQuestion;
    public float currentQuestionTime;
    public float currentQuestionTimeSpent;
    public bool answerPermitted;
	public int currentPointQuestionIndex;
	public List <int> pointQuestionAnswerIndex;
	public int clickedItemIndex;
    public int clickX;
    public int clickY;
	public Vector3 clickedColor; // R G B
    public bool showAnswer;
	public bool currentIsWrong;
	public bool currentIsTimeout;
	// public int numberOfPointItems;
	// public int numberOfPointItemsAnswered;
	
	// Score animation
	public bool playScoreAnim;
	public float scoreAnimTimer;
	public bool playScoreAnimOnTimeOut;
	public bool playScoreAnimOnWrong;
	public float baseScore;
	public float pointsPerSecond;
	
	// Debug
	Text textPosX;
	Text textPosY;
	Text textName;
	public bool debugPointPosition;

    void Start(){
        StartCoroutine(StartScene());
    }

    IEnumerator StartScene(){
        yield return null;
        // UI
        gameplayRect = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay").GetComponent<RectTransform>();
        avatar = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/Avatar").gameObject;
        // avatar.transform.Find("Item1").GetComponent<RawImage>().texture = SessionScript.avatarItem1[SessionScript.selectedItem1];
        // avatar.transform.Find("Item2").GetComponent<RawImage>().texture = SessionScript.avatarItem2[SessionScript.selectedItem2];
        // avatar.transform.Find("Item3").GetComponent<RawImage>().texture = SessionScript.avatarItem3[SessionScript.selectedItem3];
		scoreAssembly = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/Score").gameObject;
		scoreAssemblyTransform = scoreAssembly.GetComponent<RectTransform>();
		scoreText = scoreAssembly.GetComponent<Text>();
        scoreText.text = SessionScript.player.score.ToString() + " pontos!";
        if (SessionScript.player.score <= 1 && SessionScript.player.score >= -1){
            scoreText.text = SessionScript.player.score.ToString() + " ponto!";
        }
		animationFeedback = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/AnimationFeedback").gameObject;
        successAnimation = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/AnimationFeedback").GetComponent<AnimSuccessScript>();
        errorAnimation = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/AnimationFeedback").GetComponent<AnimErrorScript>();
        lowerMenu = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/LowerMenu").gameObject;
		lowerMenuTransform = lowerMenu.GetComponent<RectTransform>();
        questionCounterAssembly = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionCounter");
		questionCounter = questionCounterAssembly.transform.Find("Counter").GetComponent<Text>();
		
        // Question UI
		questionMultiple = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionMultiple").gameObject;
		questionWrite = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionWrite").gameObject;
		answers = new List<GameObject>();
		answers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionMultiple/ItemA").gameObject);
		answers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionMultiple/ItemB").gameObject);
		answers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionMultiple/ItemC").gameObject);
		answers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionMultiple/ItemD").gameObject);
		answers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionMultiple/ItemE").gameObject);
		questionMultipleText = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionMultiple/Question").gameObject;
		writtenAnswer = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionWrite/WrittenAnswer").GetComponent<InputField>();
		questionWriteText = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionWrite/Question").gameObject;
		questionPoint = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionPoint").gameObject;
		questionPointMovable = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionPoint/Movable").gameObject;
		questionPointText = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionPoint/Movable/QuestionPointText").gameObject;
		questionPointButton = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionPoint/QuestionPointButton").gameObject;
		questionPointConfirm = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionPoint/Movable/QuestionPointConfirm").gameObject;
		questionPointDetail = questionPointConfirm.transform.Find("Detail").GetComponent<Image>();
		questionLong = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionLong").gameObject;
		questionLongText = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionLong/QuestionText").gameObject;
		questionLongAnswer = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionLong/QuestionAnswers").gameObject;
		questionLongAnswers = new List<GameObject>();
		questionLongAnswers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionLong/QuestionAnswers/ItemA").gameObject);
		questionLongAnswers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionLong/QuestionAnswers/ItemB").gameObject);
		questionLongAnswers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionLong/QuestionAnswers/ItemC").gameObject);
		questionLongAnswers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionLong/QuestionAnswers/ItemD").gameObject);
		questionLongAnswers.Add(GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionLong/QuestionAnswers/ItemE").gameObject);
		questionImage = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/QuestionImage").gameObject;
		questionImageTexture = questionImage.GetComponent<Image>();
		clockAssembly = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/Clock").gameObject;
		clockAssemblyTransform = clockAssembly.GetComponent<RectTransform>();
		clockImage = clockAssembly.transform.Find("Image").GetComponent<Image>();
		clockImage.fillAmount = 0;
		clockText = clockAssembly.transform.Find("Text").GetComponent<Text>();
		clockText.text = "";
		nextQuestion = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/NextQuestion").gameObject;
		toMenuText = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/ToMenu").gameObject;
		correctAnswer = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/CorrectAnswer").gameObject;
		itemsCounter = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/ItemsCounter").gameObject;
		itemsCounterText = itemsCounter.GetComponent<Text>();
		//result = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/Result").gameObject;
		//result.SetActive(false);
		questionImage.SetActive(false);
		questionMultiple.SetActive(false);
		questionPoint.SetActive(false);
		questionLong.SetActive(false);
		questionWrite.SetActive(false);
		nextQuestion.SetActive(false);
		toMenuText.SetActive(false);
		itemsCounter.SetActive(false);
		showAnswer = false;
		pointAndClickTurn = GameObject.Find("Canvas/Scroll View/Viewport/Gameplay/TurnWarning").gameObject;
		turnSign = pointAndClickTurn.transform.Find("Sign").GetComponent<RectTransform>();
		turnSignOn = false;
		turnSignTimer = 0f;
		currentIsWrong = false;
		currentIsTimeout = false;
		
		// Rotate UI
		uiVertical = true;
		// questionPointMovableOriginalPos = questionPointMovable.transform.position;
		// animationFeedbackOriginalPos = animationFeedback.transform.position;
		correctAnswerOriginalPos = correctAnswer.transform.position;
		clockAssemblyOriginalPos = clockAssembly.transform.position;
		lowerMenuOriginalPos = lowerMenu.transform.position;
		
		// Score animation
		playScoreAnim = false;
		scoreAnimTimer = 0f;
		if(SessionScript.timeoutScore < 0) playScoreAnimOnTimeOut = true; else playScoreAnimOnTimeOut = false;
		if(SessionScript.wrongScore < 0) playScoreAnimOnWrong = true; else playScoreAnimOnWrong = false;
		baseScore = SessionScript.player.score;
		
		//Debug
		textPosX = questionPoint.transform.Find("Movable/DebugWindow/Window/TextX").GetComponent<Text>();
		textPosY = questionPoint.transform.Find("Movable/DebugWindow/Window/TextY").GetComponent<Text>();
		textName = questionPoint.transform.Find("Movable/DebugWindow/Window/TextName").GetComponent<Text>();
		if (debugPointPosition) questionPoint.transform.Find("DebugWindow").gameObject.SetActive(true);
       
		if (SessionScript.useQuestionPointOffset){   // source: 256 X 512 pixels
			// hardcoded resolution: 800x480 (5/3) or (0.6f)		// for iPad, change to: 800 x600 (3/4) or (0.75f)
			float playableXaxis = Screen.height * 0.6f;
			float questionPointOffsetLinearX = Mathf.Max((Screen.width - playableXaxis) / 2,0f);
			questionPointOffsetLinear = new Vector2(questionPointOffsetLinearX, 0);
			float x = 256f / playableXaxis;
			float y = 512f / Screen.height;
			questionPointOffsetMultiplier = new Vector2(x, y);
			print("Screen.width" + Screen.width);
			print("Screen.height" + Screen.height);
			print("playableXaxis: " + playableXaxis);
			print("questionPointOffsetLinearX : " + questionPointOffsetLinearX);
			print("questionPointOffsetLinear : " + questionPointOffsetLinear);
			print("questionPointOffsetMultiplier : " + questionPointOffsetMultiplier);
		}
		else{
			questionPointOffsetMultiplier = new Vector2(1, 1);
			questionPointOffsetLinear = new Vector2(0, 0);
		}
		
		// Soundtrack
		SessionScript.PlaySong();
		

        // TEST TEST TES TEST
        Invoke("StartNewQuestion", 0.1f);
		
		// Track Record
		AuthenticationScript.TrackRecord("Started gameplay");
    }

	void Update(){       // CHANGED ANSER QUESTION METHOD -> UPDATE TIMEOUT METHOD
		// if (endScene){
		// gameplayRect.anchoredPosition = new Vector2 (gameplayRect.anchoredPosition.x, gameplayRect.anchoredPosition.y - Time.deltaTime * 1200);
		// return;
		// }	
		// Question Timer
		//int index = currentQuestion.index;
		if (answerPermitted){
				currentQuestionTime = currentQuestionTime + Time.deltaTime;
				clockImage.fillAmount = currentQuestionTime / SessionScript.questionTime;
				clockText.text = (SessionScript.questionTime - currentQuestionTime).ToString("0.");
			if (currentQuestionTime > SessionScript.questionTime){
				AnswerTimeout();
				answerPermitted = false;
				if (currentQuestion.questionType == 0){
					answerPermitted = false;
					SessionScript.QuestionAudio(SessionScript.error);
					//SessionScript.answersList.Add(new Answer(index, false, SessionScript.questionList[index].subject, currentQuestionTimeSpent));	// CHANGE THIS PART TO "AcceptMultipleAnswer(wrong)"
					//Invoke ("StartNewQuestion", 1f);
					//ChooseAnswer(-1);
				}
				if (currentQuestion.questionType == 1){
					//AcceptAnswer();
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.Return)){
			if (currentQuestion.questionType == 1 && writtenAnswer.text != ""){   // Fill-the-blank timeout
				//AcceptAnswer();
			}
		}
		if (turnSignOn){
			turnSignTimer = turnSignTimer + Time.deltaTime;
			if (turnSignTimer > 0.5f){
				if (turnSignTimer < 1.5f){
					float spin = Mathf.Cos(turnSignTimer);
					turnSign.Rotate(Vector3.forward * spin * -5);
				}
			}
			if (turnSignTimer > 2.5f){
				EndTurnSign();
			}
		}
		if (playScoreAnim){
			scoreAnimTimer = scoreAnimTimer + Time.deltaTime;
			if (scoreAnimTimer > 0.5f){
				if (scoreAnimTimer <= 2.5f){
					baseScore = baseScore + Time.deltaTime * pointsPerSecond;
					scoreText.text = baseScore.ToString("0.0") + " pontos!";
				}
				if (scoreAnimTimer >= 2.5f){
					playScoreAnim = false;
					baseScore = SessionScript.player.score;
					scoreText.text = baseScore.ToString() + " pontos!";
					if (baseScore <= 1 && baseScore >= -1){
						scoreText.text = baseScore.ToString() + " ponto!";
					}
					playScoreAnim = false;
				}
			}
		}
	}

	public void StartNewQuestion(){
		lowerMenu.SetActive(false);
		lockButton = false;
		answerPermitted = true;
		showAnswer = false;
		currentIsWrong = false;
		currentIsTimeout = false;
		currentQuestionTimeSpent = 0f;
		bool clear;
		int i = 0;
		int questionRoll = 0;
		int questionListIndex = 0;
		bool questionRollError = false;
		do{   // Randomly chooses next question and checks if it's repeated
			print ("question roll!");
			clear = true;
			questionRoll = Random.Range(SessionScript.questionMinIndex, SessionScript.questionMaxIndex + 1);
			bool indexExists = false;
			print ("roll: " + questionRoll);
			for (int y = 0; y < SessionScript.questionList.Count; y++){
				if (SessionScript.questionList[y].index == questionRoll){
					indexExists = true;
					questionListIndex = y;
					print ("question exists! (" + y + ")");
					break;
				}
			}
			if (indexExists){
				if (SessionScript.questionsAskedList.Count != 0){
					for (int y = 0; y < SessionScript.questionsAskedList.Count; y++){
						if (questionRoll == SessionScript.questionsAskedList[y]){
							clear = false;
							print ("repeated question!");
						}
					}
				}
			}
			i = i + 1;
			if (i > 1000) { clear = true; print("ENDLESS LOOP!"); questionRollError = true;} // Escape valve for an unforeseen endless loop
		} while (!clear);
		if (questionRollError){
			correctAnswer.SetActive(false);
			nextQuestion.SetActive(false);
			toMenuText.SetActive(false);
			FinishedQuestions();
			PopUpScript.InstantiatePopUpChangeScene("Parece que houve um erro. Retorne ao menu e tente reiniciar as questões!", "Ok", "menu");
			return;
		}
		currentQuestion = SessionScript.questionList[questionListIndex];
		SessionScript.questionsAskedList.Add(currentQuestion.index);
		correctAnswer.SetActive(false);
		nextQuestion.SetActive(false);
		toMenuText.SetActive(false);
		//result.SetActive(false);
		clockText.text = SessionScript.questionTime.ToString();
		QuestionCounter();

		int a0;
		int a1;
		int a2;
		int a3;
		int a4;
		switch (currentQuestion.questionType){
			case 1: // Fill-the-blank
				questionImage.SetActive(false);
				questionWrite.SetActive(true);
				questionPoint.SetActive(false);
				questionMultiple.SetActive(false);
				questionLong.SetActive(false);
				itemsCounter.SetActive(false);
				questionWriteText.transform.Find("Text").GetComponent<Text>().text = currentQuestion.text;
				writtenAnswer.GetComponent<Image>().color = new Color(0.975f, 0.975f, 0.975f, 1);
				writtenAnswer.ActivateInputField();
				if (!uiVertical){
					RotateUI();
				}
				SessionScript.questionTime = SessionScript.questionTimeShort;
				break;
			case 2: // Point-and-click
				questionImage.SetActive(false);
				questionWrite.SetActive(false);
				questionPoint.SetActive(true);
				questionMultiple.SetActive(false);
				questionLong.SetActive(false);
				questionPointText.SetActive(true);
				questionPointButton.SetActive(false);
				questionPointConfirm.SetActive(false);
				itemsCounter.SetActive(false);
				questionPointText.transform.Find("Question/Text").GetComponent<Text>().text = currentQuestion.text;
				for (int y = 0; y < SessionScript.pointAndClickQuestion.Count; y ++){
					if (SessionScript.pointAndClickQuestion[y].index == currentQuestion.index){
						currentPointQuestionIndex = y;
						print("currentPointQuestionIndex = " +  y);
						break;
					}
				} 
				questionPoint.transform.Find("Background").GetComponent<Image>().sprite = SessionScript.pointAndClickQuestion[currentPointQuestionIndex].sprite;
				questionPointButton.transform.Find("Button").GetComponent<Image>().sprite = SessionScript.pointAndClickQuestion[currentPointQuestionIndex].sprite;
				if (uiVertical){
					RotateUI();
				}
				print("StartNewQuestion() point-and-click");
				print ("currentPointQuestionIndex: " + currentPointQuestionIndex);
				SessionScript.questionTime = SessionScript.questionTimeLong;
				break;
			case 3: // Long
				questionImage.SetActive(false);
				questionMultiple.SetActive(false);
				questionPoint.SetActive(false);
				questionLong.SetActive(true);
				questionLongAnswer.SetActive(false);
				questionLongText.SetActive(true);
				questionWrite.SetActive(false);
				itemsCounter.SetActive(false);
				questionLongText.transform.Find("Question/Text").GetComponent<Text>().text = currentQuestion.text;
				a0 = Random.Range(0, 5);    // Randomly chooses which option would be the correct one
				rightAnswer = a0;
				alternativeA = a0;
				a1 = Random.Range(0, 5);
				while (a1 == a0){
					a1 = Random.Range(0, 5);
				}
				alternativeB = a1;
				a2 = Random.Range(0, 5);
				while (a2 == a0 || a2 == a1){
					a2 = Random.Range(0, 5);
				}
				alternativeC = a2;
				a3 = Random.Range(0, 5);
				while (a3 == a0 || a3 == a1 || a3 == a2){
					a3 = Random.Range(0, 5);
				}
				alternativeD = a3;
				a4 = Random.Range(0, 5);
				while (a4 == a0 || a4 == a1 || a4 == a2 || a4 == a3){
					a4 = Random.Range(0, 5);
				}
				alternativeE = a4;
				

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
				if (!uiVertical){
					RotateUI();
				}
				SessionScript.questionTime = SessionScript.questionTimeShort;
				break;
			case 4: // Image
				questionMultiple.SetActive(true);
				questionWrite.SetActive(false);
				questionPoint.SetActive(false);
				questionLong.SetActive(false);
				questionImage.SetActive(true);
				itemsCounter.SetActive(false);
				questionImageTexture.sprite = currentQuestion.questionImage;
				questionMultipleText.transform.Find("Text").GetComponent<Text>().text = currentQuestion.text;
				questionMultipleText.transform.Find("Text").GetComponent<Text>().alignment = TextAnchor.UpperCenter;
				a0 = Random.Range(0, 5);    // Randomly chooses which option would be the correct one
				rightAnswer = a0;
				alternativeA = a0;
				a1 = Random.Range(0, 5);
				while (a1 == a0){
					a1 = Random.Range(0, 5);
				}
				alternativeB = a1;
				a2 = Random.Range(0, 5);
				while (a2 == a0 || a2 == a1){
					a2 = Random.Range(0, 5);
				}
				alternativeC = a2;
				a3 = Random.Range(0, 5);
				while (a3 == a0 || a3 == a1 || a3 == a2){
					a3 = Random.Range(0, 5);
				}
				alternativeD = a3;
				a4 = Random.Range(0, 5);
				while (a4 == a0 || a4 == a1 || a4 == a2 || a4 == a3){
					a4 = Random.Range(0, 5);
				}
				alternativeE = a4;
				
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
				StartCoroutine("DoubleCheckQuestionImage");
				if (!uiVertical){
					RotateUI();
				}
				SessionScript.questionTime = SessionScript.questionTimeShort;
				break;
			case 5:	// Point-and-click multiple items
				questionImage.SetActive(false);
				questionWrite.SetActive(false);
				questionPoint.SetActive(true);
				questionMultiple.SetActive(false);
				questionLong.SetActive(false);
				questionPointText.SetActive(true);
				questionPointButton.SetActive(false);
				questionPointConfirm.SetActive(false);
				itemsCounter.SetActive(true);
				pointQuestionAnswerIndex = new List<int>();
				questionPointText.transform.Find("Question/Text").GetComponent<Text>().text = currentQuestion.text;
				for (int y = 0; y < SessionScript.pointAndClickQuestion.Count; y ++){
					print ("loop: " + y);
					if (SessionScript.pointAndClickQuestion[y].index == currentQuestion.index){
						currentPointQuestionIndex = y;
						print("currentPointQuestionIndex = " +  y);
						break;
					}
				}
				itemsCounterText.text = 0 + " / " + SessionScript.pointAndClickQuestion[currentPointQuestionIndex].rightItemIndex.Count;
				questionPoint.transform.Find("Background").GetComponent<Image>().sprite = SessionScript.pointAndClickQuestion[currentPointQuestionIndex].sprite;
				questionPointButton.transform.Find("Button").GetComponent<Image>().sprite = SessionScript.pointAndClickQuestion[currentPointQuestionIndex].sprite;
				print("StartNewQuestion() point-and-click multiple items");
				print ("currentPointQuestionIndex : " + currentPointQuestionIndex);
				if (uiVertical){
					RotateUI();
				}
				SessionScript.questionTime = SessionScript.questionTimeLong;
				break;
			default:    // Multiple answer
				questionMultiple.SetActive(true);
				questionPoint.SetActive(false);
				questionLong.SetActive(false);
				questionWrite.SetActive(false);
				questionImage.SetActive(false);
				questionMultipleText.transform.Find("Text").GetComponent<Text>().text = currentQuestion.text;
				questionMultipleText.transform.Find("Text").GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
				a0 = Random.Range(0, 5);    // Randomly chooses which option would be the correct one
				rightAnswer = a0;
				alternativeA = a0;
				a1 = Random.Range(0, 5);
				while (a1 == a0){
					a1 = Random.Range(0, 5);
				}
				alternativeB = a1;
				a2 = Random.Range(0, 5);
				while (a2 == a0 || a2 == a1){
					a2 = Random.Range(0, 5);
				}
				alternativeC = a2;
				a3 = Random.Range(0, 5);
				while (a3 == a0 || a3 == a1 || a3 == a2){
					a3 = Random.Range(0, 5);
				}
				alternativeD = a3;
				a4 = Random.Range(0, 5);
				while (a4 == a0 || a4 == a1 || a4 == a2 || a4 == a3){
					a4 = Random.Range(0, 5);
				}
				alternativeE = a4;

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
				if (!uiVertical){
					RotateUI();
				}
				SessionScript.questionTime = SessionScript.questionTimeShort;
				break;
		}
	}
	
	int GetActualAnswer(int answerGiven){
		int actualAnswer = 0;
		if (answerGiven == alternativeA) actualAnswer = 0;
		if (answerGiven == alternativeB) actualAnswer = 1;
		if (answerGiven == alternativeC) actualAnswer = 2;
		if (answerGiven == alternativeD) actualAnswer = 3;
		if (answerGiven == alternativeE) actualAnswer = 4;
		return actualAnswer;
	}

	public void AnswerQuestion(int answer){
		print ("AnswerQuestion()");
		if (!answerPermitted){
			return;
		}
		currentQuestionTimeSpent = currentQuestionTime;
		switch (currentQuestion.questionType){
			case 1:     // Fill-the-blank
				if (!answerPermitted) return;
					string writtenAnswerSimple = Simple.SimpleText(writtenAnswer.text);
					string rightAnswerSimple = Simple.SimpleText(currentQuestion.answer0);
					if (writtenAnswerSimple == rightAnswerSimple){
					writtenAnswer.gameObject.GetComponent<Image>().color = Color.green;
					successAnimation.PlayAnimation();
					SessionScript.QuestionAudio(SessionScript.success);
					SessionScript.answersList.Add(new Answer(currentQuestion.index, 1, true, false, currentQuestion.subject, currentQuestionTimeSpent));
					SessionScript.player.score = SessionScript.player.score + SessionScript.rightScore;   // PLACEHOLDER player.score // PLACEHOLDER player.score // PLACEHOLDER player.score // PLACEHOLDER player.score // PLACEHOLDER player.score 	
					AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": " + writtenAnswer.text + " (wrong)"); 
				}
				if (writtenAnswerSimple != rightAnswerSimple){
					errorAnimation.PlayAnimation();
					writtenAnswer.gameObject.GetComponent<Image>().color = Color.red;
					SessionScript.QuestionAudio(SessionScript.error);
					SessionScript.answersList.Add(new Answer(currentQuestion.index, 0, false, false, currentQuestion.subject, currentQuestionTimeSpent));
					SessionScript.player.score = SessionScript.player.score + SessionScript.wrongScore;
					showAnswer = true;
					AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": " + writtenAnswer.text + " (wrong)"); 
				}
				AuthenticationScript.SaveAnswers();
				answerPermitted = false;
				Invoke("EndQuestion", 2.25f);
				break;
			case 2:     // Point-and-click
				Vector3 mouseInput = Input.mousePosition;
				clickX = Mathf.RoundToInt((mouseInput.x - questionPointOffsetLinear.x) * questionPointOffsetMultiplier.x);
                clickY = Mathf.RoundToInt((mouseInput.y - questionPointOffsetLinear.y) * questionPointOffsetMultiplier.y);
				/* Debug */ textPosX.text = "pos X: " + clickX.ToString();
				/* Debug */ textPosY.text = "pos Y: " + clickY.ToString();
				bool black = false;
				Color pixelColor = SessionScript.pointAndClickQuestion[currentPointQuestionIndex].source.GetPixel(clickX, clickY);
				clickedColor = new Vector3(pixelColor.r, pixelColor.g, pixelColor.b);
				if (clickedColor.x <= 0.05f){
					if (clickedColor.y <= 0.05f){
						if (clickedColor.z <= 0.05f){
							black = true;
							SessionScript.ButtonAudioLow(SessionScript.subtle);
						}
					}
				}
                if (!black){
                    SessionScript.ButtonAudio(SessionScript.neutral);
                    Invoke("ToPointConfirm", 0.25f);
                    SessionScript.ButtonAudio(SessionScript.subtle);
                }
                break;
			case 3:     // Long
				int actualAnswer = GetActualAnswer(answer);
				if (answer == rightAnswer){
					questionLongAnswers[answer].GetComponent<Image>().color = Color.green;
					successAnimation.PlayAnimation();
					SessionScript.QuestionAudio(SessionScript.success);
					SessionScript.answersList.Add(new Answer(currentQuestion.index, actualAnswer, true, false, currentQuestion.subject, currentQuestionTimeSpent));
					SessionScript.player.score = SessionScript.player.score + SessionScript.rightScore;   // PLACEHOLDER player.score // PLACEHOLDER player.score // PLACEHOLDER player.score // PLACEHOLDER player.score // PLACEHOLDER player.score 
					AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": " + actualAnswer.ToString() + " (right)");
				}
				else{
					errorAnimation.PlayAnimation();
					questionLongAnswers[answer].GetComponent<Image>().color = Color.red;
					SessionScript.QuestionAudio(SessionScript.error);
					SessionScript.answersList.Add(new Answer(currentQuestion.index, actualAnswer, false, false, currentQuestion.subject, currentQuestionTimeSpent));
					SessionScript.player.score = SessionScript.player.score + SessionScript.wrongScore;
					showAnswer = true;
					AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": " + actualAnswer.ToString() + " (wrong)");
				}
				AuthenticationScript.SaveAnswers();
                answerPermitted = false;
                Invoke("EndQuestion", 2.25f);
                break;
            case 4:     // Image
				int actualAnswerB = GetActualAnswer(answer);
					if (answer == rightAnswer){
					answers[answer].GetComponent<Image>().color = Color.green;
					successAnimation.PlayAnimation();
					SessionScript.QuestionAudio(SessionScript.success);
					SessionScript.answersList.Add(new Answer(currentQuestion.index, actualAnswerB, true, false, currentQuestion.subject, currentQuestionTimeSpent));
					SessionScript.player.score = SessionScript.player.score + SessionScript.rightScore;   // PLACEHOLDER player.score // PLACEHOLDER player.score // PLACEHOLDER player.score // PLACEHOLDER player.score // PLACEHOLDER player.score 
					AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": " + actualAnswerB.ToString() + " (right)");
				}
				else{
					answers[answer].GetComponent<Image>().color = Color.red;
					errorAnimation.PlayAnimation();
					SessionScript.QuestionAudio(SessionScript.error);
					SessionScript.answersList.Add(new Answer(currentQuestion.index, actualAnswerB, false, false, currentQuestion.subject, currentQuestionTimeSpent));
					SessionScript.player.score = SessionScript.player.score + SessionScript.wrongScore;
					showAnswer = true;
					AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": " + actualAnswerB.ToString() + " (wrong)");
				}
				AuthenticationScript.SaveAnswers();
                answerPermitted = false;
                Invoke("EndQuestion", 2.25f);
                break;
			case  5:
				if (pointQuestionAnswerIndex.Count < SessionScript.pointAndClickQuestion[currentPointQuestionIndex].rightItemIndex.Count){
					clickedItemIndex = -1;	// resets last clicked item
					mouseInput = Input.mousePosition;
					clickX = Mathf.RoundToInt((mouseInput.x - questionPointOffsetLinear.x) * questionPointOffsetMultiplier.x);
					clickY = Mathf.RoundToInt((mouseInput.y - questionPointOffsetLinear.y) * questionPointOffsetMultiplier.y);
					/* Debug */ textPosX.text = "pos X: " + clickX.ToString();
					/* Debug */ textPosY.text = "pos Y: " + clickY.ToString();
					black = false;
					pixelColor = SessionScript.pointAndClickQuestion[currentPointQuestionIndex].source.GetPixel(clickX, clickY);
					clickedColor = new Vector3(pixelColor.r, pixelColor.g, pixelColor.b);
					if (clickedColor.x <= 0.05f){
						if (clickedColor.y <= 0.05f){
							if (clickedColor.z <= 0.05f){
								black = true;
								SessionScript.ButtonAudioLow(SessionScript.subtle);
								/* Debug */ textName.text = "obj: none";
							}
						}
					}
					if (!black){
						for (int i = 0; i < SessionScript.pointAndClickQuestion[currentPointQuestionIndex].itemColor.Count; i++){
							print ("itemColor[" + i + "]: " + SessionScript.pointAndClickQuestion[currentPointQuestionIndex].itemColor[i] + "(" + SessionScript.pointAndClickQuestion[currentPointQuestionIndex].itemName[i] + ")/ clickedColor: " + clickedColor);
							string clickedColorString = clickedColor.ToString();
							string itemColorString = SessionScript.pointAndClickQuestion[currentPointQuestionIndex].itemColor[i].ToString();
							if (clickedColorString == itemColorString){
								clickedItemIndex = i;	// compares colors to find which item was clicked
								print ("Found item clicked: " + i + "(clickedItemIndex = " + clickedItemIndex + ") " + SessionScript.pointAndClickQuestion[currentPointQuestionIndex].itemName[i]);
								/* Debug */ textName.text = "obj: " + SessionScript.pointAndClickQuestion[currentPointQuestionIndex].itemName[i];
								break;
							}
							/*if(SessionScript.pointAndClickQuestion[currentPointQuestionIndex].itemColor[i].x == clickedColor.x){
								if(SessionScript.pointAndClickQuestion[currentPointQuestionIndex].itemColor[i].y == clickedColor.y){
									if(SessionScript.pointAndClickQuestion[currentPointQuestionIndex].itemColor[i].z == clickedColor.z){
										clickedItemIndex = i;	// compares colors to find which item was clicked
										print ("Found item clicked: " + i + "(clickedItemIndex = " + clickedItemIndex + ")");
									}
								}
							}*/
						}
						if (clickedItemIndex == -1){	// clickedItemIndex == -1 => item não identificado
							print ("QuestionPointCheckPixel() clickedItemIndex == -1");
							SessionScript.ButtonAudioLow(SessionScript.subtle);
							/* Debug */ textName.text = "obj: unknown";
							return;
						}
						if (clickedItemIndex != -1){
							bool repeatedItem = false;
							if (pointQuestionAnswerIndex.Count > 0){
								for (int i = 0; i < pointQuestionAnswerIndex.Count; i++){
									if (clickedItemIndex == pointQuestionAnswerIndex[i]){
										print ("QuestionPointCheckPixel() repeatedItem = true");
										SessionScript.ButtonAudioLow(SessionScript.subtle);
										repeatedItem = true;
										break;
									}
								}
								/* Debug */	print ("List of items clicked so far " + (pointQuestionAnswerIndex.Count).ToString() + " items");
								/* Debug */	for (int i = 0; i < pointQuestionAnswerIndex.Count; i++){
								/* Debug */		print (i + "º: " + SessionScript.pointAndClickQuestion[currentPointQuestionIndex].itemName[pointQuestionAnswerIndex[i]]);
								/* Debug */	}
							}
							if (!repeatedItem){
								SessionScript.ButtonAudio(SessionScript.subtle);
								Invoke("ToPointConfirm", 0.25f);
							}
						}
					}
				}
				break;
            default:    // Multiple answer
				int actualAnswerC = GetActualAnswer(answer);
				if (answer == rightAnswer){
					answers[answer].GetComponent<Image>().color = Color.green;
					successAnimation.PlayAnimation();
					SessionScript.QuestionAudio(SessionScript.success);
					SessionScript.answersList.Add(new Answer(currentQuestion.index, actualAnswerC, true, false, currentQuestion.subject, currentQuestionTimeSpent));
					SessionScript.player.score = SessionScript.player.score + SessionScript.rightScore;   // PLACEHOLDER player.score // PLACEHOLDER player.score // PLACEHOLDER player.score // PLACEHOLDER player.score // PLACEHOLDER player.score 
					AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": " + actualAnswerC.ToString() + " (right)");
				}
				else{
					answers[answer].GetComponent<Image>().color = Color.red;
					errorAnimation.PlayAnimation();
					SessionScript.QuestionAudio(SessionScript.error);
					SessionScript.answersList.Add(new Answer(currentQuestion.index, actualAnswerC, false, false, currentQuestion.subject, currentQuestionTimeSpent));
					SessionScript.player.score = SessionScript.player.score + SessionScript.wrongScore;
					showAnswer = true;
					AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": " + actualAnswerC.ToString() + " (wrong)");
				}
				AuthenticationScript.SaveAnswers();
				answerPermitted = false;
				Invoke("EndQuestion", 2.25f);
				break;
        }
    }

	public void AnswerTimeout(){
		 if (!answerPermitted){
            return;
        }
		switch (currentQuestion.questionType){
			case 1:     // Fill-the-blank
				if (!answerPermitted) return;
				string writtenAnswerSimple = Simple.SimpleText(writtenAnswer.text);
				string rightAnswerSimple = Simple.SimpleText(currentQuestion.answer0);
				if (writtenAnswerSimple == rightAnswerSimple){
					writtenAnswer.gameObject.GetComponent<Image>().color = Color.green;
					successAnimation.PlayAnimation();
					SessionScript.QuestionAudio(SessionScript.success);
					SessionScript.answersList.Add(new Answer(currentQuestion.index, 1, true, true, currentQuestion.subject, currentQuestionTimeSpent));
					SessionScript.player.score = SessionScript.player.score + SessionScript.rightScore;   // PLACEHOLDER player.score // PLACEHOLDER player.score // PLACEHOLDER player.score // PLACEHOLDER player.score // PLACEHOLDER player.score 	
					AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": " + writtenAnswer.text + " (right,  timeout)");
				}
				if (writtenAnswerSimple != rightAnswerSimple && writtenAnswer.text != ""){
					errorAnimation.PlayAnimation();
					writtenAnswer.gameObject.GetComponent<Image>().color = Color.red;
					SessionScript.QuestionAudio(SessionScript.error);
					SessionScript.answersList.Add(new Answer(currentQuestion.index, 5, false, true, currentQuestion.subject, currentQuestionTimeSpent));
					SessionScript.player.score = SessionScript.player.score + SessionScript.wrongScore;
					showAnswer = true;
					AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": " + writtenAnswer.text + " (wrong, timeout)");
				}
				if (writtenAnswer.text == ""){
					writtenAnswer.gameObject.GetComponent<Image>().color = Color.red;
					SessionScript.QuestionAudio(SessionScript.error);
					SessionScript.answersList.Add(new Answer(currentQuestion.index, 5, false, true, currentQuestion.subject, currentQuestionTimeSpent));
					SessionScript.player.score = SessionScript.player.score + SessionScript.timeoutScore;
					showAnswer = true;
					AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": TIMEOUT (wrong)");
				}
				AuthenticationScript.SaveAnswers();
				answerPermitted = false;
				Invoke("EndQuestion", 2.25f);
				break;
			case 2:     // Point-and-click
				errorAnimation.PlayAnimation();
				SessionScript.QuestionAudio(SessionScript.error);
				SessionScript.answersList.Add(new Answer(currentQuestion.index, 5, false, true, currentQuestion.subject, currentQuestionTimeSpent));
				SessionScript.player.score = SessionScript.player.score + SessionScript.timeoutScore;
				answerPermitted = false;
				showAnswer = true;
				AuthenticationScript.SaveAnswers();
				Invoke("EndQuestion", 2.25f);
				AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": TIMEOUT (wrong)");
				break;
			case 3:     // Long
				errorAnimation.PlayAnimation();
				SessionScript.QuestionAudio(SessionScript.error);
				SessionScript.answersList.Add(new Answer(currentQuestion.index, 5, false, true, currentQuestion.subject, currentQuestionTimeSpent));
				SessionScript.player.score = SessionScript.player.score + SessionScript.timeoutScore;
				answerPermitted = false;
				showAnswer = true;
				AuthenticationScript.SaveAnswers();
				AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": TIMEOUT (wrong)");
				Invoke("EndQuestion", 2.25f);
				break;
			case 4:     // Image
				errorAnimation.PlayAnimation();
				SessionScript.QuestionAudio(SessionScript.error);
				SessionScript.answersList.Add(new Answer(currentQuestion.index, 5, false, true, currentQuestion.subject, currentQuestionTimeSpent));
				SessionScript.player.score = SessionScript.player.score + SessionScript.timeoutScore;
				answerPermitted = false;
				showAnswer = true;
				AuthenticationScript.SaveAnswers();
				AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": TIMEOUT (wrong)");
				Invoke("EndQuestion", 2.25f);
				break;
			case 5:	// Point-and-click multiple items
				errorAnimation.PlayAnimation();
				SessionScript.QuestionAudio(SessionScript.error);
				SessionScript.answersList.Add(new Answer(currentQuestion.index, 5, false, true, currentQuestion.subject, currentQuestionTimeSpent));
				SessionScript.player.score = SessionScript.player.score + SessionScript.timeoutScore;
				answerPermitted = false;
				showAnswer = true;
				AuthenticationScript.SaveAnswers();
				AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": TIMEOUT (wrong)");
				Invoke("EndQuestion", 2.25f);
				break;
			default:    // Multiple answer
				errorAnimation.PlayAnimation();
				SessionScript.QuestionAudio(SessionScript.error);
				SessionScript.answersList.Add(new Answer(currentQuestion.index, 5, false, true, currentQuestion.subject, currentQuestionTimeSpent));
				SessionScript.player.score = SessionScript.player.score + SessionScript.timeoutScore;
				answerPermitted = false;
				showAnswer = true;
				AuthenticationScript.SaveAnswers();
				AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": TIMEOUT (wrong)");
				Invoke("EndQuestion", 2.25f);
				break;
		}
	}

	public void QuestionPointCheckPixel(){
		print ("QuestionPointCheckPixel()");
		if (currentQuestion.questionType == 2){
			bool rightItem = false;
			string itemName = "";
			for (int i = 0; i < SessionScript.pointAndClickQuestion[currentPointQuestionIndex].itemColor.Count; i++){
				if (clickedColor == SessionScript.pointAndClickQuestion[currentPointQuestionIndex].itemColor[i]){
					itemName = SessionScript.pointAndClickQuestion[currentPointQuestionIndex].itemName[i];
					if (i == SessionScript.pointAndClickQuestion[currentPointQuestionIndex].rightItemIndex[0]){
						rightItem = true;
					}
					break;
				}
			}
			print("QuestionPointCheckPixel() pixelColor " + clickedColor.ToString() + "/ answer: " + SessionScript.pointAndClickQuestion[currentPointQuestionIndex].itemColor[0]);
		/*	if (questionPointAnswer.x <= colorInput.x - 0.1f || colorInput.x + 0.1f < questionPointAnswer.x) { colorInput.x = questionPointAnswer.x; wrong = true; }
			if (questionPointAnswer.y <= colorInput.y - 0.1f || colorInput.y + 0.1f < questionPointAnswer.y) { colorInput.y = questionPointAnswer.y; wrong = true; }
			if (questionPointAnswer.z <= colorInput.z - 0.1f || colorInput.z + 0.1f < questionPointAnswer.z) { colorInput.z = questionPointAnswer.z; wrong = true; }
			print ("QuestionPointCheckPixel() colorInput (corrected): " + colorInput);	*/
			if (rightItem){
				print("QuestionPointCheckPixel() right");
				successAnimation.PlayAnimation();
				SessionScript.QuestionAudio(SessionScript.success);
				SessionScript.player.score = SessionScript.player.score + SessionScript.rightScore;
				SessionScript.answersList.Add(new Answer(currentQuestion.index, 0, true, false, currentQuestion.subject, currentQuestionTimeSpent));
				AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": " + itemName + " (right)");
			}
			if (!rightItem){
				print("QuestionPointCheckPixel() wrong");
				errorAnimation.PlayAnimation();
				SessionScript.QuestionAudio(SessionScript.error);
				SessionScript.player.score = SessionScript.player.score + SessionScript.wrongScore;
				SessionScript.answersList.Add(new Answer(currentQuestion.index, 1, false, false, currentQuestion.subject, currentQuestionTimeSpent));
				showAnswer = true;
				AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": " + itemName + " (right)");
			}
			AuthenticationScript.SaveAnswers();
			answerPermitted = false;
			Invoke("EndQuestion", 2.25f);
		}
		if (currentQuestion.questionType == 5){
			pointQuestionAnswerIndex.Add(clickedItemIndex);
			print ((pointQuestionAnswerIndex.Count -1).ToString() + "º answer given: " +  clickedItemIndex);
			itemsCounterText.text = pointQuestionAnswerIndex.Count + " / " + SessionScript.pointAndClickQuestion[currentPointQuestionIndex].rightItemIndex.Count;
			if (pointQuestionAnswerIndex.Count >= SessionScript.pointAndClickQuestion[currentPointQuestionIndex].rightItemIndex.Count){	// Compares list of given answers to list of correct answers
				bool rightAllItems = true;
				for (int i = 0; i < SessionScript.pointAndClickQuestion[currentPointQuestionIndex].rightItemIndex.Count; i++){
					bool wrongItem = true;
					for (int y = 0; y < pointQuestionAnswerIndex.Count; y ++){
						if (pointQuestionAnswerIndex[y] == SessionScript.pointAndClickQuestion[currentPointQuestionIndex].rightItemIndex[i]){	// Assumes answer is wrong, if it is still wrong at the end of any loop, breaks and ends question
							wrongItem = false;
							print ("answer " + y + "/" + i + " is right!");
							break;
						} else {print ("answer " + y + "/" + i + " is wrong! given " + pointQuestionAnswerIndex[y] + "correct is " + SessionScript.pointAndClickQuestion[currentPointQuestionIndex].rightItemIndex[i]);}
					}
					if (wrongItem){
						rightAllItems = false;
						print ("wrong, loop: " + i);
						break;
					}
				}
				if (rightAllItems){
					print("QuestionPointCheckPixel() rightAllItems = true");
					successAnimation.PlayAnimation();
					SessionScript.QuestionAudio(SessionScript.success);
					SessionScript.player.score = SessionScript.player.score + SessionScript.rightScore;
					SessionScript.answersList.Add(new Answer(currentQuestion.index, 0, true, false, currentQuestion.subject, currentQuestionTimeSpent));
					AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": right");
				}
				if (!rightAllItems){
					print("QuestionPointCheckPixel() rightAllItems = false");
					errorAnimation.PlayAnimation();
					SessionScript.QuestionAudio(SessionScript.error);
					SessionScript.player.score = SessionScript.player.score + SessionScript.wrongScore;
					SessionScript.answersList.Add(new Answer(currentQuestion.index, 1, false, false, currentQuestion.subject, currentQuestionTimeSpent));
					AuthenticationScript.TrackRecord("answered question " + currentQuestion.index.ToString() + ": wrong");
					showAnswer = true;
				}
				AuthenticationScript.SaveAnswers();
				answerPermitted = false;
				Invoke("EndQuestion", 2.25f);
			} else{	// Not all items have been selected
				SessionScript.ButtonAudio(SessionScript.neutral);
				questionPointText.SetActive(false);
				questionPointButton.SetActive(true);
				questionPointConfirm.SetActive(false);
				answerPermitted = true;
			}
		}
	}

	public void EndQuestion(){
		lockButton = false;
		lowerMenu.SetActive(true);
		if (questionWrite.activeSelf){
			writtenAnswer.text = "";
		}
		questionMultiple.SetActive(false);
		questionWrite.SetActive(false);
		questionPoint.SetActive(false);
		questionLong.SetActive(false);
		questionImage.SetActive(false);
		itemsCounter.SetActive(false);
		if (showAnswer){
			correctAnswer.SetActive(true);
			// Invoke ("CloseCorrection", 2.5f);	// Now delay is defined by playAnimNow
			correctAnswer.transform.Find("Frame/Text").GetComponent<Text>().text = "Resposta certa: " + currentQuestion.answer0;
			if (currentQuestion.questionType != 2){
				correctAnswer.transform.Find("Image").gameObject.SetActive(false);
				correctAnswer.transform.Find("Frame/Text").GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
			}
			if (currentQuestion.questionType == 2){
				correctAnswer.transform.Find("Image").gameObject.SetActive(true);
				correctAnswer.transform.Find("Frame/Text").GetComponent<Text>().alignment = TextAnchor.UpperCenter;
				correctAnswer.transform.Find("Image").GetComponent<Image>().sprite = SessionScript.missingTexture;	// MISSING TEXTURE INSERIDA PRIMEIRO
				correctAnswer.transform.Find("Image").GetComponent<Image>().sprite = SessionScript.pointAndClickQuestion[currentPointQuestionIndex].itemSprite[0];	// Resposta certa, SE ENCONTRADA, SUBSTITUI MISSING TEXTURE
				//StartCoroutine("DoubleCheckCorrectImage");
			}
			if (currentQuestion.questionType == 5){
				string answerText = "";
				for (int i = 0; i < SessionScript.pointAndClickQuestion[currentPointQuestionIndex].rightItemIndex.Count; i++){
					if (i != 0){
						answerText = answerText + ", ";
					}
					answerText = answerText + SessionScript.pointAndClickQuestion[currentPointQuestionIndex].itemName[SessionScript.pointAndClickQuestion[currentPointQuestionIndex].rightItemIndex[i]];
				}
				answerText = answerText + ".";
				correctAnswer.transform.Find("Frame/Text").GetComponent<Text>().text = "Resposta certa: " + answerText;
				//StartCoroutine("DoubleCheckCorrectImage");
			}
		}
		// if (!SessionScript.singleRun){
			// menu.SetActive(true);
		// }
		bool playsAnimNow = false;
		if (!currentIsTimeout && ! currentIsWrong){
			playsAnimNow = true;
		}
		if(currentIsWrong && playScoreAnimOnWrong){
			playsAnimNow = true;
		}
		if (currentIsTimeout && playScoreAnimOnTimeOut){
			playsAnimNow = true;
		}
		if (playsAnimNow){
			StartScoreAnimation();
			if (SessionScript.answersList.Count < SessionScript.numberOfQuestionsDemanded){
				if (showAnswer){
					Invoke ("CloseCorrection", 5.5f);
					Invoke ("InterQuestion", 5.5f);
				} if (!showAnswer){
					Invoke ("InterQuestion", 2.5f);
				}
			}
			if (SessionScript.answersList.Count >= SessionScript.numberOfQuestionsDemanded){
				if(!uiVertical){
					if (showAnswer){
						Invoke ("FinishedQuestions", 7.5f);
						Invoke ("CloseCorrection", 7f);
					}if (!showAnswer){
						Invoke ("FinishedQuestions", 5.5f);
					}
				}
				if(uiVertical){
					if (showAnswer){
						Invoke ("FinishedQuestions", 5.5f);
						Invoke ("CloseCorrection", 5f);
					}if (!showAnswer){
						Invoke ("FinishedQuestions", 3.5f);
					}
				}
			}
		}
		if (!playsAnimNow){
			StartScoreAnimation();
			if (SessionScript.answersList.Count < SessionScript.numberOfQuestionsDemanded){
				if (showAnswer){
					Invoke ("CloseCorrection", 2.5f);
					Invoke ("InterQuestion", 2.5f);
				} if (!showAnswer){
					Invoke ("InterQuestion", 0.5f);
				}
			}
			if (SessionScript.answersList.Count >= SessionScript.numberOfQuestionsDemanded){
				if(!uiVertical){
					if (showAnswer){
						Invoke ("FinishedQuestions", 5.5f);
						Invoke ("CloseCorrection", 5f);
					}if (!showAnswer){
						Invoke ("FinishedQuestions", 2.5f);
					}
				}
				if(uiVertical){
					if (showAnswer){
						Invoke ("FinishedQuestions", 2.5f);
						Invoke ("CloseCorrection", 2f);
					}if (!showAnswer){
						Invoke ("FinishedQuestions", 0.5f);
					}
				}
			}
		}
		currentQuestionTime = 0;
		clockImage.fillAmount = 0;
		clockText.text = "";
		// scoreText.text = SessionScript.player.score.ToString() + " pontos!";
		// if (SessionScript.player.score <= 1 && SessionScript.player.score >= -1){
			// scoreText.text = SessionScript.player.score.ToString() + " ponto!";
		// }
		if (SessionScript.player.score >= SessionScript.thresholdTier1){
			SessionScript.currentTier = 1;
		}
		if (SessionScript.player.score >= SessionScript.thresholdTier2){
			SessionScript.currentTier = 2;
		}
	}
	
	void InterQuestion(){
		nextQuestion.SetActive(true);
	}
	
	void FinishedQuestions(){
		if (!uiVertical){
				RotateUI();
		}
		nextScene = "menu";
		toMenuText.SetActive(true);
		questionCounterAssembly.SetActive(false);
		scoreAssembly.SetActive(false);
		clockAssembly.SetActive(false);
		AuthenticationScript.TrackRecord("Finished gameplay");
		Invoke ("EndScene", 2f);
		Invoke ("NextScene", 2f);
		Invoke("StartEndTransition", 0.8f);	// mesma diferença de timming das outras cenas	// OBSOLETO
		SessionScript.currentSong = SessionScript.song1;
		SessionScript.fadeOutSong = true;
	}
	
	void StartEndTransition(){
		TransitionScript.EndAnimation();
	}
	
	public void CloseCorrection(){
		correctAnswer.SetActive(false);
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
		print ("ToPointImage()");
		SessionScript.ButtonAudio(SessionScript.neutral);
		questionPointText.SetActive(false);
		questionPointButton.SetActive(true);
		questionPointConfirm.SetActive(false);
	}

	public void ToPointConfirm(){
		//if (currentQuestion.questionType == 2) answerPermitted = false;
		questionPointText.SetActive(false);
		questionPointButton.SetActive(false);
		questionPointConfirm.SetActive(true);
		questionPointDetail.sprite = SessionScript.missingTexture;	// MISSING TEXTURE COLOCADA PRIMEIRA
		questionPointDetail.sprite = SessionScript.pointAndClickQuestion[currentPointQuestionIndex].itemSprite[clickedItemIndex];	// Resposta certa, SE ENCONTRADA, SUBSTITUI MISSING TEXTURE
	}

    // public string SimpleText(string text){
        // string character = "";
        // for (int i = 0; i < text.Length; i++){
            // print("index " + i);
            // character = text.Substring(i, 1);
			// // Diacritcs
            // if (character == "à"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "a");
            // }
            // if (character == "Á"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "a");
            // }
            // if (character == "á"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "a");
            // }
            // if (character == "À"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "a");
            // }
            // if (character == "ä"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "a");
            // }
            // if (character == "Ä"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "a");
            // }
            // if (character == "ã"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "a");
            // }
            // if (character == "Ã"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "a");
            // }
            // if (character == "â"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "a");
            // }
            // if (character == "Â"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "a");
            // }
            // if (character == "è"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "a");
            // }
            // if (character == "È"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "e");
            // }
            // if (character == "é"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "e");
            // }
            // if (character == "É"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "e");
            // }
            // if (character == "ë"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "e");
            // }
            // if (character == "Ë"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "e");
            // }
            // if (character == "ê"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "e");
            // }
            // if (character == "Ê"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "e");
            // }
            // if (character == "ì"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "i");
            // }
            // if (character == "Ì"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "i");
            // }
            // if (character == "í"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "i");
            // }
            // if (character == "Í"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "i");
            // }
            // if (character == "ï"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "i");
            // }
            // if (character == "Ï"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "i");
            // }
            // if (character == "î"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "i");
            // }
            // if (character == "Î"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "i");
            // }
            // if (character == "ò"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "o");
            // }
            // if (character == "Ò"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "o");
            // }
            // if (character == "ó"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "o");
            // }
            // if (character == "Ó"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "o");
            // }
            // if (character == "õ"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "o");
            // }
            // if (character == "Õ"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "o");
            // }
            // if (character == "ô"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "o");
            // }
            // if (character == "Ô"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "o");
            // }
            // if (character == "ö"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "o");
            // }
            // if (character == "Ö"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "o");
            // }
            // if (character == "ù"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "u");
            // }
            // if (character == "Ù"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "u");
            // }
            // if (character == "ú"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "u");
            // }
            // if (character == "Ú"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "u");
            // }
            // if (character == "û"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "u");
            // }
            // if (character == "Û"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "u");
            // }
            // if (character == "ü"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "u");
            // }
            // if (character == "Û"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "u");
            // }
            // // Capital letters
            // if (character == "A"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "a");
            // }
            // if (character == "B"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "b");
            // }
            // if (character == "C"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "c");
            // }
            // if (character == "Ç"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "ç");
            // }
            // if (character == "D"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "d");
            // }
            // if (character == "E"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "e");
            // }
            // if (character == "F"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "f");
            // }
            // if (character == "G"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "g");
            // }
            // if (character == "H"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "h");
            // }
            // if (character == "I"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "i");
            // }
            // if (character == "J"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "j");
            // }
            // if (character == "K"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "");
            // }
            // if (character == "L"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "l");
            // }
            // if (character == "M"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "m");
            // }
            // if (character == "N"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "n");
            // }
            // if (character == "O"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "o");
            // }
            // if (character == "P"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "p");
            // }
            // if (character == "Q"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "q");
            // }
            // if (character == "R"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "r");
            // }
            // if (character == "S"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "s");
            // }
            // if (character == "T"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "t");
            // }
            // if (character == "U"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "u");
            // }
            // if (character == "V"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "v");
            // }
            // if (character == "X"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "x");
            // }
            // if (character == "Z"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "z");
            // }
            // if (character == "W"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "w");
            // }
            // if (character == "Y"){
                // text = text.Remove(i, 1);
                // text = text.Insert(i, "y");
            // }
        // }
        // print(text);
        // return text;
    // }

    public void SelectNextQuestion(){
        if (lockButton){
            SessionScript.ButtonAudio(SessionScript.subtle);
            return;
        }
        lockButton = true;
        SessionScript.ButtonAudio(SessionScript.neutral);
        Invoke("StartNewQuestion", 1f);
    }

    public void SelectMenu(){
        SessionScript.ButtonAudio(SessionScript.neutral);
        nextScene = "menu";
        Invoke("EndScene", 0.5f);
        Invoke("NextScene", 1f);
		SessionScript.currentSong = SessionScript.song1;
		SessionScript.fadeOutSong = true;
		AuthenticationScript.TrackRecord("Finished gameplay");
    }

    // public void SelectResult(){
    // SessionScript.QuestionAudio(SessionScript.success);
    // nextScene = "result";
    // Invoke ("EndScene", 0.5f);
    // Invoke ("NextScene", 1f);

    // }

    public void QuestionCounter(){
		int currentQuestion = SessionScript.answersList.Count + 1;
        questionCounter.text = currentQuestion.ToString() + " / " + SessionScript.numberOfQuestionsDemanded.ToString();
    }
	
	void RotateUI(){
		turnSignOn = true;
		turnSignTimer = 0;
		Invoke ("TurnSignAudio", 0.66f);
		pointAndClickTurn.SetActive(true);
		currentQuestionTime = - 2.5f;
		if (uiVertical){	// Turns to horizontal
			animationFeedback.GetComponent<RectTransform>().Rotate(Vector3.forward * 90);
			correctAnswer.GetComponent<RectTransform>().Rotate(Vector3.forward * 90);
			correctAnswer.transform.position = new Vector2 (Screen.width/2 + 60f, Screen.height/2);
			clockAssembly.transform.position = new Vector2 (clockAssemblyTransform.sizeDelta.y * 1.5f, clockAssemblyTransform.sizeDelta.y * 1.5f);
			clockAssemblyTransform.Rotate(Vector3.forward * 90);
			clockAssembly.SetActive(false);
			nextQuestion.GetComponent<RectTransform>().Rotate(Vector3.forward * 90);
			lowerMenu.transform.position = new Vector2 (Screen.width - lowerMenuTransform.sizeDelta.y * 2f, Screen.height/2);
			lowerMenuTransform.Rotate(Vector3.forward * 90);
			turnSign.Rotate(Vector3.forward * 90);
			questionCounterAssembly.SetActive(false);
			scoreAssemblyTransform.Rotate(Vector3.forward * 90);
			scoreAssembly.SetActive(false);
		}
		if (!uiVertical){	// Turns to vertical
			animationFeedback.GetComponent<RectTransform>().Rotate(Vector3.forward * 90 * -1);
			correctAnswer.GetComponent<RectTransform>().Rotate(Vector3.forward * 90 * -1);
			correctAnswer.transform.position = correctAnswerOriginalPos;
			clockAssembly.transform.position = clockAssemblyOriginalPos;
			clockAssemblyTransform.Rotate(Vector3.forward * 90 * -1);
			clockAssembly.SetActive(false);
			nextQuestion.GetComponent<RectTransform>().Rotate(Vector3.forward * 90 * -1);
			lowerMenuTransform.Rotate(Vector3.forward * 90 * -1);
			lowerMenu.transform.position = lowerMenuOriginalPos;
			turnSign.Rotate(Vector3.forward * 90 * -1);
			scoreAssemblyTransform.Rotate(Vector3.forward * 90 * -1);
			scoreAssembly.SetActive(false);
		}
	}
	
	void EndTurnSign(){
		turnSignTimer = 0;
		turnSignOn = false;
		pointAndClickTurn.SetActive(false);
		clockAssembly.SetActive(true);
		scoreAssembly.SetActive(true);
		// if (uiVertical){	// Turns to horizontal
			
		// }
		if (!uiVertical){	// Turns to vertical
			questionCounterAssembly.SetActive(true);
		}
		uiVertical = !uiVertical;
	}
	
	void StartScoreAnimation(){
		print ("StartScoreAnimation()");
		pointsPerSecond = (SessionScript.player.score - baseScore) / 2;	// Anim takes 2 seconds
		scoreAnimTimer = 0f;
		playScoreAnim = true;
		Invoke("StartScoreSound", 0.5f);
	}
	
	void StartScoreSound(){
		SessionScript.ButtonAudioLoud(SessionScript.scoreCounter);
	}
	
	void TurnSignAudio(){
		SessionScript.ButtonAudioLoud(SessionScript.swipe);
	}

    public void NextScene(){
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }

    void EndScene(){
        endScene = true;
    }
	
	// Double Checks
	
	IEnumerator DoubleCheckQuestionImage(){
		yield return null;
		questionImageTexture.sprite = currentQuestion.questionImage;
	}
	
	IEnumerator DoubleCheckCorrectImage(){
		yield return new WaitForSeconds(1f);
		if (currentQuestion.questionType == 2){
			float red = float.Parse(currentQuestion.answer1);
			float green = float.Parse(currentQuestion.answer2);
			float blue = float.Parse(currentQuestion.answer3);
			correctAnswer.transform.Find("Image").gameObject.SetActive(true);
			Vector3 colorInput = new Vector3(red, green, blue);
			correctAnswer.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load("Textures/PointAndClick/detail_" + 100 * colorInput.x + "_" + 100 * colorInput.y + "_" + 100 * colorInput.z, typeof(Sprite)) as Sprite;
		}
		if (currentQuestion.questionType == 5){
			print ("DoubleCheckCorrectImage() Images not available for this question");
		}
	}

//		DESAFIO QUIZ, version alpha 0.7
//		developed by ROCKET PRO GAMES, rocketprogames@gmail.com
//		script by Eduardo Siqueira
//		São Paulo, Brasil, 2019	
}