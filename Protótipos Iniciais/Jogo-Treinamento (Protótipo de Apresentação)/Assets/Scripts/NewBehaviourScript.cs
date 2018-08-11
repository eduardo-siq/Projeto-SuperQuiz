using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
public class NewBehaviourScript : MonoBehaviour {

	// UI
	GameObject questionBox;
	GameObject characters;
	GameObject endText;
	GameObject exitWindow;
	GameObject errorWindow;
	GameObject frame;
	Text questionText;
	Text scoreText;
	public List<GameObject> answers;
	public GameObject marker1;
	public GameObject marker2;
	public GameObject marker3;

	// Questions
	public List<Question> questionList;
	public List<int> questionsAsked;
	public Question currentQuestion;
	public int numberOfQuestions;
	public int numberOfQuestionsDemanded = 15;
	public int numberOfQuestionsAsked;
	public int rightAnswer;
	public int pointsByQuestion = 50;
	public bool questionReady;
	
	// Minigame
	public int characterSelected;
	public int characterBest;
	public bool characterReady = false;
	public float char1R;
	public float char1B;
	public float char1G;
	public float char2R;
	public float char2B;
	public float char2G;
	public float char3R;
	public float char3B;
	public float char3G;
	public float targetR;
	public float targetG;
	public float targetB;
	public int pointsByMinigame = 10;
	public GameObject target;
	
	// Score
	public int score;
	
	// Sound
	AudioSource songAudio;
	AudioSource buttonAudio;
	AudioClip song1;
	AudioClip song2;
	AudioClip positive;
	AudioClip negative;
	int currentSong;

	void Start () {
		/*Questions questions = new Questions();
		numberOfQuestions = questions.numberOfQuestions;
		print (numberOfQuestions);
		for (int y = 0; y < numberOfQuestions; y++){
			if (questions.questions[y] == null){
				print ("no more questions");
				break;
			}
			questionList.Add(new Question(questions.questions[y]));
			//questionList.Add(new Question(y,"text","","","","",""));
			questionList[y] = questions.questions[y];
			questionsAsked.Add(-1);
			//print ("question list cout: " + questionList.Count);
			//print ("asked list cout: " + questionsAsked.Count);
		}*/
		// UI
		exitWindow = GameObject.FindWithTag("UI").transform.Find("Exit").gameObject;
		exitWindow.SetActive(false);
		errorWindow = GameObject.FindWithTag("UI").transform.Find("Error").gameObject;
		errorWindow.SetActive(false);
		frame = GameObject.FindWithTag("UI").transform.Find("Frame").gameObject;
		questionBox = GameObject.FindWithTag("UI").transform.Find("Frame/QuestionBox").gameObject;
		questionText = questionBox.transform.Find("QuestionText").GetComponent<Text>();
		endText = GameObject.FindWithTag("UI").transform.Find("Frame/End").gameObject;
		scoreText = GameObject.FindWithTag("UI").transform.Find("Frame/Score").GetComponent<Text>();
		answers = new List<GameObject>();
		answers.Add(questionBox.transform.Find("AnswerA").gameObject);
		answers.Add(questionBox.transform.Find("AnswerB").gameObject);
		answers.Add(questionBox.transform.Find("AnswerC").gameObject);
		answers.Add(questionBox.transform.Find("AnswerD").gameObject);
		answers.Add(questionBox.transform.Find("AnswerE").gameObject);
		questionBox.SetActive(false);
		endText.SetActive(false);
		characters = GameObject.FindWithTag("UI").transform.Find("Frame/Characters").gameObject;
		marker1 = characters.transform.Find("Character1/Marker").gameObject;
		marker2 = characters.transform.Find("Character2/Marker").gameObject;
		marker3 = characters.transform.Find("Character3/Marker").gameObject;
		marker1.SetActive(false);
		marker2.SetActive(false);
		marker3.SetActive(false);
		// Questions
		questionsAsked = new List<int>();
		questionList = new List<Question>();
		LoadFile();
		numberOfQuestions = questionList.Count;
		if (numberOfQuestionsDemanded > numberOfQuestions) numberOfQuestionsDemanded = numberOfQuestions;
		// Minigame
		char1R = Random.Range(0, 1f);
		char1G = Random.Range(0, 1f);
		char1B = Random.Range(0, 1f);
		print (char1R + " " + char1G + " " + char1B);
		characters.transform.Find("Character1/Button").GetComponent<Image>().color = new Color(char1R, char1G, char1B, 1);
		char2R = Random.Range(0, 1f);
		char2G = Random.Range(0, 1f);
		char2B = Random.Range(0, 1f);
		characters.transform.Find("Character2/Button").GetComponent<Image>().color = new Color(char2R, char2G, char2B, 1);
		char3R = Random.Range(0, 1f);
		char3G = Random.Range(0, 1f);
		char3B = Random.Range(0, 1f);
		characters.transform.Find("Character3/Button").GetComponent<Image>().color = new Color(char3R, char3G, char3B, 1);
		target = characters.transform.Find("QuestionButton").gameObject;
		targetR = Random.Range(0, 1f);
		targetG = Random.Range(0, 1f);
		targetB = Random.Range(0, 1f);
		target.GetComponent<Image>().color = new Color(targetR, targetG, targetB, 1);
		SetMinigame();
		Score();
		//Score
		//Sound
		songAudio = this.gameObject.GetComponent<AudioSource> ();
		buttonAudio = this.gameObject.AddComponent<AudioSource> ();
		positive = Resources.Load ("Sound/positive_sound", typeof(AudioClip)) as AudioClip;
		negative = Resources.Load ("Sound/negative_sound", typeof(AudioClip)) as AudioClip;
		song1 = Resources.Load ("Sound/Memories of Green", typeof(AudioClip)) as AudioClip;
		song2 = Resources.Load ("Sound/Damask Rose", typeof(AudioClip)) as AudioClip;
		currentSong = 1;
	}
	
	void Update(){
		if (!songAudio.isPlaying){
			if (currentSong == 1){
				PlaySong(song2);
				currentSong = 2;
				return;			
			}
			if (currentSong == 2){
				PlaySong(song1);
				currentSong = 1;
				return;				
			}
		}
		if (Input.GetKeyDown (KeyCode.R)){
			print ("Application.LoadLevel");
			SceneManager.LoadScene("gameplay1", LoadSceneMode.Single);
		}
		if (Input.GetKeyDown (KeyCode.Escape)){
			ExitWindow();
		}
	}
	
	public void SetMinigame(){
		bool ready;
		do {
			RefreshQuestionButton();
			float character1Score = 5 - (Mathf.Abs(targetR - char1R) + Mathf.Abs(targetG - char1G) + Mathf.Abs(targetB - char1B));
			float character2Score = 5 - (Mathf.Abs(targetR - char2R) + Mathf.Abs(targetG - char2G) + Mathf.Abs(targetB - char2B));
			float character3Score = 5 - (Mathf.Abs(targetR - char3R) + Mathf.Abs(targetG - char3G) + Mathf.Abs(targetB - char3B));
			if (character1Score > character2Score && character1Score > character3Score){
				characterBest = 1;
			}
			if (character2Score > character1Score && character2Score > character3Score){
				characterBest = 2;
			}
			if (character3Score > character1Score && character3Score > character2Score){
				characterBest = 3;
			}
			ready = true;
			if (character1Score == character2Score || character1Score == character3Score || character2Score == character3Score){
				ready = false;
			}
		} while (!ready);
	}
	
	public void MinigameScore(){
		if (characterSelected == characterBest){
			Score(pointsByMinigame);
			ButtonAudio(positive);
		}
		else ButtonAudio(negative);
	}
	
	public void RefreshQuestionButton(){
		targetR = Random.Range(0, 1f);
		targetG = Random.Range(0, 1f);
		targetB = Random.Range(0, 1f);
		target.GetComponent<Image>().color = new Color(targetR, targetG, targetB, 1);
	}
	
	public void NewQuestion(){
		MinigameScore();
		Invoke ("StartNewQuestion", 1);
	}
	
	public void StartNewQuestion(){
		if (!characterReady){
			return;
		}
		if (numberOfQuestionsAsked >= numberOfQuestionsDemanded){
			print ("no more questions");
			EndGame();
			return;
		}
		questionReady = true;
		ClearCharacter();
		print ("NEW QUESTION (" + + Time.deltaTime + ")");
		SwitchWindows();
		int a0;
		int a1;
		int a2;
		int a3;
		int a4;
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
		bool clear = true;
		int i = 0;
		do {
			clear = true;
			print ("looping (" + Time.deltaTime + ")");
			currentQuestion = questionList[Random.Range(0, numberOfQuestions)];	
			//print ("random question index : " + currentQuestion.index + " (" + Time.deltaTime + ")");
			//print (questionList.Count);
			for (int y = 0; y < questionList.Count; y ++){
				//print ("y = " + y + " (" + Time.deltaTime + ")");
				if (currentQuestion.index == questionsAsked[y]){
					clear = false;
					print ("Repeated question (" + Time.deltaTime + ")");
				}
			}
			i = i +1;
			if (i > 1000){ clear = true; print ("ENDLESS LOOP!");}	// Escape valve for an unforeseen endless loop
			if (clear){ print ("Question OK (" + Time.deltaTime + ")");}
			print ("loop end (" + Time.deltaTime + ")");
		} while (!clear);
		questionsAsked[numberOfQuestionsAsked] = currentQuestion.index;
		print ("question asked index: " + questionsAsked[numberOfQuestionsAsked] + " (" + Time.deltaTime + ")");
		print ("question index: " + currentQuestion.index + " (" + Time.deltaTime + ")");
		questionText.text = currentQuestion.text;
		
		//print (currentQuestion.text); print (currentQuestion.answer0); print (currentQuestion.answer1); print (currentQuestion.answer2); print (currentQuestion.answer3); print (currentQuestion.answer4);
		//print ("a0: " + a0); print ("a1: " + a1); print ("a2: " + a2); print ("a3: " + a3); print ("a4: " + a4);print (answers.Count);
		
		answers[a0].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer0;
		answers[0].GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 1);
		answers[a1].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer1;
		answers[1].GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 1);
		answers[a2].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer2;
		answers[2].GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 1);
		answers[a3].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer3;
		answers[3].GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 1);
		answers[a4].transform.Find("Text").GetComponent<Text>().text = currentQuestion.answer4;
		answers[4].GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 1);
		
		answers[0].transform.Find("Text").GetComponent<Text>().text = "A) " + answers[0].transform.Find("Text").GetComponent<Text>().text;
		answers[1].transform.Find("Text").GetComponent<Text>().text = "B) " + answers[1].transform.Find("Text").GetComponent<Text>().text;
		answers[2].transform.Find("Text").GetComponent<Text>().text = "C) " + answers[2].transform.Find("Text").GetComponent<Text>().text;
		answers[3].transform.Find("Text").GetComponent<Text>().text = "D) " + answers[3].transform.Find("Text").GetComponent<Text>().text;
		answers[4].transform.Find("Text").GetComponent<Text>().text = "E) " + answers[4].transform.Find("Text").GetComponent<Text>().text;

		numberOfQuestionsAsked = numberOfQuestionsAsked + 1;
		//print ("numberOfQuestionsAsked: " + numberOfQuestionsAsked + " (" + Time.deltaTime + ")");
	}
	
	public void ChooseAnswer(GameObject obj){
		if (!questionReady) return;
		if (obj == answers[0]){
			if (rightAnswer == 0){
				answers[0].GetComponent<Image>().color = Color.green;
				Score(pointsByQuestion);
				ButtonAudio(positive);
			}
			else{
				answers[0].GetComponent<Image>().color = Color.red;
				ButtonAudio(negative);
			}
		}
		if (obj == answers[1]){
			if (rightAnswer == 1){
				answers[1].GetComponent<Image>().color = Color.green;
				Score(pointsByQuestion);
				ButtonAudio(positive);
			}
			else{
				answers[1].GetComponent<Image>().color = Color.red;
				ButtonAudio(negative);
			}
		}
		if (obj == answers[2]){
			if (rightAnswer == 2){
				answers[2].GetComponent<Image>().color = Color.green;
				Score(pointsByQuestion);
				ButtonAudio(positive);
			}
			else{
				answers[2].GetComponent<Image>().color = Color.red;
				ButtonAudio(negative);
			}	
		}
		if (obj == answers[3]){
			if (rightAnswer == 3){
				answers[3].GetComponent<Image>().color = Color.green;
				Score(pointsByQuestion);
				ButtonAudio(positive);
			}
			else{
				answers[3].GetComponent<Image>().color = Color.red;
				ButtonAudio(negative);
			}
		}
		if (obj == answers[4]){
			if (rightAnswer == 4){
				answers[4].GetComponent<Image>().color = Color.green;
				Score(pointsByQuestion);
				ButtonAudio(positive);
			}
			else{
				answers[4].GetComponent<Image>().color = Color.red;
				ButtonAudio(negative);
			}
		}
		questionReady = false;
		SetMinigame();
		Invoke ("SwitchWindows", 1);
		Invoke ("NewBackground", 1);
	}
	
	public void SwitchWindows(){
		questionBox.SetActive(!questionBox.activeSelf);
		characters.SetActive(!characters.activeSelf);
		if (numberOfQuestionsAsked == numberOfQuestionsDemanded){
			EndGame();
		}
	}
	
/*	public void CloseQuestionBox(){
		questionBox.SetActive(false);
		NewBackground();
		if (numberOfQuestionsAsked == numberOfQuestionsDemanded){
			EndGame();
		}	
	}*/
	
	public void ChooseCharacter(int character){
		if (character == 1){
			marker1.SetActive(true);
			marker2.SetActive(false);
			marker3.SetActive(false);	
		}
		if (character == 2){
			marker1.SetActive(false);
			marker2.SetActive(true);
			marker3.SetActive(false);	
		}
		if (character == 3){
			marker1.SetActive(false);
			marker2.SetActive(false);
			marker3.SetActive(true);
		}
		characterSelected = character;
		characterReady = true;
	}
	
	public void ClearCharacter(){
		marker1.SetActive(false);
		marker2.SetActive(false);
		marker3.SetActive(false);
		characterSelected = 0;
		characterReady = false;
	}
	
	public void Score(){
		scoreText.text = "Score: " + score.ToString() + " points";
	}
	
	public void Score(int points){
		score = score + points;
		Invoke ("Score", 0.5f);
	}
	
	public void NewBackground(){
		int b = Random.Range(1,6);
		string background = "background" + b.ToString();
		Texture backgroundImage = Resources.Load ("Images/background" + b.ToString(), typeof(Texture)) as Texture;
		Material mat = Resources.Load ("BackgroundMaterial", typeof(Material)) as Material;
		mat.mainTexture = backgroundImage;
	}
	
	public void PlaySong(AudioClip audio){
		songAudio.PlayOneShot(audio, 0.5f);
	}
	
	public void ButtonAudio(AudioClip audio){
		buttonAudio.PlayOneShot(audio, 1f);
	}
	
	public void EndGame(){
		characters.SetActive(false);
		questionBox.SetActive(false);
		endText.SetActive(true);
		scoreText.text = "";
		endText.transform.Find("EndText").gameObject.GetComponent<Text>().text = "Fim: você fez " + score.ToString() + " pontos";
	}
	
	void LoadFile (){
		//if (File.Exists(Application.persistentDataPath + "/RPG_test_BncQ.dat")){
		if (File.Exists(Application.persistentDataPath + "/RPG_test_BncQ.dat")){
			print ("load saved game");
			BinaryFormatter bf = new BinaryFormatter();
			FileStream fileStream = File.Open (Application.persistentDataPath + "/RPG_test_BncQ.dat", FileMode.Open);
			BncQFile qdbFile = (BncQFile)bf.Deserialize(fileStream);
			
			// info loaded - start
			for (int i = 0; i < qdbFile.t.Count; i++){
				questionList.Add(new Question(
				i,
				qdbFile.t[i],
				qdbFile.a[i],
				qdbFile.b[i],
				qdbFile.c[i],
				qdbFile.d[i],
				qdbFile.e[i]));
				questionsAsked.Add(-1);
			}
			// info loaded - end
			
			fileStream.Close();
		} else MissingBncQ();
	}
	
	void MissingBncQ(){
		print ("there is no file to be loaded");
		errorWindow.SetActive(true);
		Destroy(characters);
		Destroy(questionBox);
		frame.SetActive(false);
		errorWindow.transform.Find("Text").gameObject.GetComponent<Text>().text = "	Não foi possível localizar o banco de questões (" + Application.persistentDataPath + "/RPG_test_BncQ.dat) . Providencie o banco de quesões e reinicie o programa.";
	}
	
	void ExitWindow(){
		exitWindow.SetActive(!exitWindow.activeSelf);
		errorWindow.SetActive(false);
		frame.SetActive(!exitWindow.activeSelf);
	}
	
	public void Quit(){
		print ("Application.Quit");
		Application.Quit();
	}
}
