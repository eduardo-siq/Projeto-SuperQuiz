using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.SceneManagement;

[System.Serializable]
public class TextEditorClass : MonoBehaviour {

	//UI Elements
	GameObject textFieldQ;
	GameObject textFieldA;
	GameObject textFieldB;
	GameObject textFieldC;
	GameObject textFieldD;
	GameObject textFieldE;
	GameObject buttonQuestionTG;
	GameObject auxTextFieldI;
	GameObject auxTextFieldT;
	GameObject auxTextFieldQ;
	GameObject auxTextFieldA;
	GameObject auxTextFieldB;
	GameObject auxTextFieldC;
	GameObject auxTextFieldD;
	GameObject auxTextFieldE;
	GameObject questionOptions;
	GameObject questionTGWindows;
	GameObject textFieldI;
	GameObject buttonT;
	GameObject buttonAccept;
	GameObject buttonClear;
	GameObject buttonNew;
	GameObject buttonDelete;
	GameObject questionListWindow;
	GameObject buttonQ;
	GameObject buttonTG;
	GameObject buttonC;
	GameObject clientWindow;
	//GameObject clientWindowReturn;
	GameObject filterWindow;
	//GameObject filterWindowReturn;
	GameObject userGroupList;
	GameObject subjectList;
	GameObject buttonLoadBncQ;
	GameObject buttonSaveBncQ;
	GameObject buttonDeleteBncQ;
	GameObject log;
	//Data
	int currentInputField = 0;
	public List<QuestionButton> questionList;
	int currentQuestionIndex;
	bool currentQuestionMultipleAnswer = true;
	int currentQuestionType = 0; // 0: multiple answer, 1: fill the blanks, 2: point-and-click, 3: longa, 4: imagem
	bool newQuestion = true;
	string path = "C:/";
	string client;
	string fileName;
	public static List <UserGroup> userGroup;
	public static List <Subject> subject;
	//Editor
	bool loadedQuestionFile;
	GameObject button;
	GameObject inputField;
	GameObject checkbox;
	bool questionEditorOpen;
	bool questionTGWindowsOpen;
	bool clientEditorOpen;
	bool filterWindowOpen;
	
	 public List <bool> subjectSelected;	// SELECTED IN EDITOR // REMOVE LATER
	 
	 // Firebase & Database
	 DatabaseScript databaseScript;



	void Start () {
		//UI Elements
		this.gameObject.transform.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
		textFieldQ = this.gameObject.transform.Find("Text Field - Q").gameObject;
		textFieldA = this.gameObject.transform.Find("Text Field - A").gameObject;
		textFieldB = this.gameObject.transform.Find("Text Field - B").gameObject;
		textFieldC = this.gameObject.transform.Find("Text Field - C").gameObject;
		textFieldD = this.gameObject.transform.Find("Text Field - D").gameObject;
		textFieldE = this.gameObject.transform.Find("Text Field - E").gameObject;
		buttonQuestionTG = this.gameObject.transform.Find("Question Buttons/Button - Question TG").gameObject;
		auxTextFieldI = this.gameObject.transform.Find("Auxiliary Text Field/Text Field - I").gameObject;
		auxTextFieldT = this.gameObject.transform.Find("Auxiliary Text Field/Text Field - T").gameObject;
		auxTextFieldQ = this.gameObject.transform.Find("Auxiliary Text Field/Text Field - Q").gameObject;
		auxTextFieldA = this.gameObject.transform.Find("Auxiliary Text Field/Text Field - A").gameObject;
		auxTextFieldB = this.gameObject.transform.Find("Auxiliary Text Field/Text Field - B").gameObject;
		auxTextFieldC = this.gameObject.transform.Find("Auxiliary Text Field/Text Field - C").gameObject;
		auxTextFieldD = this.gameObject.transform.Find("Auxiliary Text Field/Text Field - D").gameObject;
		auxTextFieldE = this.gameObject.transform.Find("Auxiliary Text Field/Text Field - E").gameObject;
		questionOptions = this.gameObject.transform.Find("Question Buttons").gameObject;
		questionTGWindows = this.gameObject.transform.Find("Question Buttons/TG Window").gameObject;
		textFieldI = this.gameObject.transform.Find("Question Buttons/Text Field - I").gameObject;
		buttonT = this.gameObject.transform.Find("Question Buttons/Button - T").gameObject;
		buttonNew = this.gameObject.transform.Find("Question Buttons/Button - New").gameObject;
		buttonDelete = this.gameObject.transform.Find("Question Buttons/Button - Delete").gameObject;
		buttonAccept =  this.gameObject.transform.Find("Question Buttons/Button - Accept").gameObject;
		buttonClear =  this.gameObject.transform.Find("Question Buttons/Button - Clear").gameObject;
		questionListWindow = this.gameObject.transform.Find("Question List").gameObject;
		buttonQ = this.gameObject.transform.Find("Button - Q Window").gameObject;
		buttonTG = this.gameObject.transform.Find("Button - TG Window").gameObject;
		buttonC = this.gameObject.transform.Find("Button - C Window").gameObject;
		clientWindow = this.gameObject.transform.Find("Client Window").gameObject;
		//clientWindowReturn = this.gameObject.transform.Find("Client Window/Return").gameObject;
		filterWindow = this.gameObject.transform.Find("Filter Window").gameObject;
		buttonLoadBncQ = this.gameObject.transform.Find("Button - Load BncQ").gameObject;
		buttonSaveBncQ = this.gameObject.transform.Find("Button - Save BncQ").gameObject;
		buttonDeleteBncQ = this.gameObject.transform.Find("Button - Delete BncQ").gameObject;
		log = this.gameObject.transform.Find("Log").gameObject;
		textFieldI.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 + Screen.height/3 + Screen.height/15);
		textFieldI.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/9, Screen.height/15);
		buttonT.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6 + Screen.width/9, Screen.height/2 + Screen.height/3);
		buttonT.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/9, Screen.height/15);
		textFieldQ.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2);
		textFieldQ.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/3);
		textFieldA.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 - 1 * Screen.height/15);
		textFieldA.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/15);
		textFieldB.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 - 2 * Screen.height/15);
		textFieldB.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/15);
		textFieldC.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 - 3 * Screen.height/15);
		textFieldC.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/15);
		textFieldD.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 - 4 * Screen.height/15);
		textFieldD.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/15);
		textFieldE.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 - 5 * Screen.height/15);
		textFieldE.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/15);
		buttonQuestionTG.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 + Screen.height/3);
		buttonQuestionTG.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/9, Screen.height/15);
		auxTextFieldI.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/6 + Screen.width/30, Screen.height/2 + Screen.height/3);
		auxTextFieldI.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/9, Screen.height/15);
		auxTextFieldT.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/6 + Screen.width/9 + Screen.width/30, Screen.height/2 + Screen.height/3);
		auxTextFieldT.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/9, Screen.height/15);
		auxTextFieldQ.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/6 + Screen.width/30, Screen.height/2);
		auxTextFieldQ.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3 - Screen.width/15, Screen.height/3);
		auxTextFieldA.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/6 + Screen.width/30, Screen.height/2 - 1 * Screen.height/15);
		auxTextFieldA.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3 - Screen.width/15, Screen.height/15);
		auxTextFieldB.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/6 + Screen.width/30, Screen.height/2 - 2 * Screen.height/15);
		auxTextFieldB.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3 - Screen.width/15, Screen.height/15);
		auxTextFieldC.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/6 + Screen.width/30, Screen.height/2 - 3 * Screen.height/15);
		auxTextFieldC.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3 - Screen.width/15, Screen.height/15);
		auxTextFieldD.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/6 + Screen.width/30, Screen.height/2 - 4 * Screen.height/15);
		auxTextFieldD.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3 - Screen.width/15, Screen.height/15);
		auxTextFieldE.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/6 + Screen.width/30, Screen.height/2 - 5 * Screen.height/15);
		auxTextFieldE.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3 - Screen.width/15, Screen.height/15);
		questionTGWindows.transform.Find("Window").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 - Screen.height/3);
		questionTGWindows.transform.Find("Window").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, 2 * Screen.height/3);
		questionTGWindows.transform.Find("User Groups Scroll").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 - Screen.height/3);
		questionTGWindows.transform.Find("User Groups Scroll").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6, 2 * Screen.height/3);
		questionTGWindows.transform.Find("Subjects Scroll").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2, Screen.height/2 - Screen.height/3);
		questionTGWindows.transform.Find("Subjects Scroll").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6, 2 * Screen.height/3);
		buttonAccept.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2, Screen.height/15);
		buttonAccept.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6, Screen.height/15);
		buttonClear.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/15);
		buttonClear.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6, Screen.height/15);
		buttonNew.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/18, Screen.height/2 + Screen.height/3);
		buttonNew.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/18, Screen.width/18);
		buttonDelete.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/9, Screen.height/2 + Screen.height/3);
		buttonDelete.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/18, Screen.width/18);
		questionListWindow.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/30, Screen.height/15);
		questionListWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3 - Screen.width/15, 11.5f * Screen.height/15);
		Vector2 xy = this.gameObject.transform.Find("Question List/Scrollbar Vertical").GetComponent<RectTransform>().sizeDelta;
		this.gameObject.transform.Find("Question List/Scrollbar Vertical").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/30, xy.y);
		buttonQ.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/30, Screen.height/2 + Screen.height/3);
		buttonQ.GetComponent<RectTransform>().sizeDelta = new Vector2((Screen.width/3 - Screen.width/15)/3, Screen.height/15);
		buttonTG.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/30 + (Screen.width/3 - Screen.width/15)/3, Screen.height/2 + Screen.height/3);
		buttonTG.GetComponent<RectTransform>().sizeDelta = new Vector2((Screen.width/3 - Screen.width/15)/3, Screen.height/15);
		buttonC.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/30 + 2*(Screen.width/3 - Screen.width/15)/3, Screen.height/2 + Screen.height/3);
		buttonC.GetComponent<RectTransform>().sizeDelta = new Vector2((Screen.width/3 - Screen.width/15)/3, Screen.height/15);
		clientWindow.transform.Find("Window").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 - Screen.height/3);
		clientWindow.transform.Find("Window").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, 2 * Screen.height/3);
		Vector2 xy2 = clientWindow.gameObject.transform.Find("User Groups Scroll").GetComponent<RectTransform>().sizeDelta;
		clientWindow.gameObject.transform.Find("User Groups Scroll").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/45, xy2.y);
		clientWindow.gameObject.transform.Find("Subjects Scroll").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/45, xy2.y);
		clientWindow.transform.Find("Return").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/15);
		clientWindow.transform.Find("Return").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6, Screen.height/15);
		clientWindow.transform.Find("Clear").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2, Screen.height/15);
		clientWindow.transform.Find("Clear").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6, Screen.height/15);
		clientWindow.transform.Find("Client").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, 5 * Screen.height/6);
		clientWindow.transform.Find("Client").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/15);
		clientWindow.transform.Find("User Groups Text").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 - Screen.height/3 + 2 * Screen.height/3 - Screen.height/15);
		clientWindow.transform.Find("User Groups Text").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/12, Screen.height/15);
		clientWindow.transform.Find("User Groups Less").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/12 - Screen.width/6, Screen.height/2 - Screen.height/3 + 2 * Screen.height/3 - Screen.height/15);
		clientWindow.transform.Find("User Groups Less").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/36, Screen.height/15);
		clientWindow.transform.Find("User Groups Set").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/18, Screen.height/2 - Screen.height/3 + 2 * Screen.height/3 - Screen.height/15);
		clientWindow.transform.Find("User Groups Set").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/36, Screen.height/15);
		clientWindow.transform.Find("User Groups More").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/36, Screen.height/2 - Screen.height/3 + 2 * Screen.height/3 - Screen.height/15);
		clientWindow.transform.Find("User Groups More").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/36, Screen.height/15);
		clientWindow.transform.Find("Subjects Text").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2, Screen.height/2 - Screen.height/3 + 2 * Screen.height/3 - Screen.height/15);
		clientWindow.transform.Find("Subjects Text").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/12, Screen.height/15);
		clientWindow.transform.Find("Subjects Less").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/12, Screen.height/2 - Screen.height/3 + 2 * Screen.height/3 - Screen.height/15);
		clientWindow.transform.Find("Subjects Less").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/36, Screen.height/15);
		clientWindow.transform.Find("Subjects Set").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/9, Screen.height/2 - Screen.height/3 + 2 * Screen.height/3 - Screen.height/15);
		clientWindow.transform.Find("Subjects Set").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/36, Screen.height/15);
		clientWindow.transform.Find("Subjects More").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/9 + Screen.width/36, Screen.height/2 - Screen.height/3 + 2 * Screen.height/3 - Screen.height/15);
		clientWindow.transform.Find("Subjects More").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/36, Screen.height/15);
		clientWindow.transform.Find("User Groups Scroll").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 - Screen.height/3);
		clientWindow.transform.Find("User Groups Scroll").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6, 2 * Screen.height/3 - Screen.height/15);
		clientWindow.transform.Find("Subjects Scroll").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2, Screen.height/2 - Screen.height/3);
		clientWindow.transform.Find("Subjects Scroll").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6, 2 * Screen.height/3 - Screen.height/15);
		filterWindow.transform.Find("Window").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 - Screen.height/3);
		filterWindow.transform.Find("Window").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, 2 * Screen.height/3);
		filterWindow.transform.Find("User Groups Scroll").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 - Screen.height/3);
		filterWindow.transform.Find("User Groups Scroll").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6, 2 * Screen.height/3);
		filterWindow.transform.Find("Subjects Scroll").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2, Screen.height/2 - Screen.height/3);
		filterWindow.transform.Find("Subjects Scroll").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6, 2 * Screen.height/3);	
		filterWindow.transform.Find("Return").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/15);
		filterWindow.transform.Find("Return").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/15);
		buttonSaveBncQ.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/6 + Screen.width/30, Screen.height/15);
		buttonSaveBncQ.GetComponent<RectTransform>().sizeDelta = new Vector2((Screen.width/3 - Screen.width/15)/3, Screen.height/15);
		buttonLoadBncQ.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/6 + Screen.width/30 + (Screen.width/3 - Screen.width/15)/3, Screen.height/15);
		buttonLoadBncQ.GetComponent<RectTransform>().sizeDelta = new Vector2((Screen.width/3 - Screen.width/15)/3, Screen.height/15);
		buttonDeleteBncQ.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/6 + Screen.width/30 + 2 * (Screen.width/3 - Screen.width/15)/3, Screen.height/15);
		buttonDeleteBncQ.GetComponent<RectTransform>().sizeDelta = new Vector2((Screen.width/3 - Screen.width/15)/3, Screen.height/15);
		log.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.height/60, Screen.height/60);
		log.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - Screen.height/30, Screen.height/30);

		//Data
		questionList = new List<QuestionButton>();
		button = Resources.Load("Prefab/Button Prefab") as GameObject;
		inputField = Resources.Load("Prefab/InputField Prefab") as GameObject;
		checkbox = Resources.Load("Prefab/Checkbox Prefab") as GameObject;
		//userGroup = new List<string>();
		
		//Editor
		loadedQuestionFile = false;
		questionEditorOpen = true;
		questionTGWindowsOpen = false;
		clientEditorOpen = false;
		filterWindowOpen = false;
		questionTGWindows.SetActive(false);
		clientWindow.SetActive(false);
		filterWindow.SetActive(false);
		userGroup = new List<UserGroup>();
		subject = new List<Subject>();
		
		//LoadFile();
		ClearQuestion();
		
		// Set-up dynamic interface
		OrderListOfQuestion();
		OrderSubjectEditor();
		OrderUserGroupEditor();
		
		// Firebase & Database
		databaseScript = GameObject.Find("Firebase").GetComponent<DatabaseScript>();
		databaseScript.enabled = true;
		
		
		Log("Welcome to Question DataBase Editor");
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab)){
			if (currentQuestionType == 0){
				if (currentInputField > 6){ currentInputField = 1;}
				switch (currentInputField){
					default:
						textFieldQ.GetComponent<InputField>().ActivateInputField();
						break;
					case 1:
						textFieldQ.GetComponent<InputField>().DeactivateInputField();
						textFieldA.GetComponent<InputField>().ActivateInputField();
						break;
					case 2:
						textFieldA.GetComponent<InputField>().DeactivateInputField();
						textFieldB.GetComponent<InputField>().ActivateInputField();
						break;
					case 3:
						textFieldB.GetComponent<InputField>().DeactivateInputField();
						textFieldC.GetComponent<InputField>().ActivateInputField();
						break;
					case 4:
						textFieldC.GetComponent<InputField>().DeactivateInputField();
						textFieldD.GetComponent<InputField>().ActivateInputField();
						break;
					case 5:
						textFieldD.GetComponent<InputField>().DeactivateInputField();
						textFieldE.GetComponent<InputField>().ActivateInputField();
						break;
					case 6:
						textFieldE.GetComponent<InputField>().DeactivateInputField();
						textFieldQ.GetComponent<InputField>().ActivateInputField();
						break;
				}
			}
			if (currentQuestionType == 1){
				if (currentInputField > 2){ currentInputField = 1;}
				switch (currentInputField){
					default:
						textFieldQ.GetComponent<InputField>().ActivateInputField();
						break;
					case 1:
						textFieldQ.GetComponent<InputField>().DeactivateInputField();
						textFieldA.GetComponent<InputField>().ActivateInputField();
						break;
					case 2:
						textFieldA.GetComponent<InputField>().DeactivateInputField();
						textFieldQ.GetComponent<InputField>().ActivateInputField();
						break;
				}
			}
			currentInputField = currentInputField + 1;
		}
		if (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)){
			if (Input.GetKeyDown(KeyCode.N)){
				NewQuestion();
				return;
			}
			if (Input.GetKeyDown(KeyCode.S)){
				AcceptQuestion();
				return;
			}
			if (Input.GetKeyDown(KeyCode.Z)){
				ClearQuestion();
				return;
			}
		}
	}
	
	public void SelectInputField (string field){
		textFieldQ.GetComponent<InputField>().DeactivateInputField();
		textFieldA.GetComponent<InputField>().DeactivateInputField();
		textFieldB.GetComponent<InputField>().DeactivateInputField();
		textFieldC.GetComponent<InputField>().DeactivateInputField();
		textFieldD.GetComponent<InputField>().DeactivateInputField();
		textFieldE.GetComponent<InputField>().DeactivateInputField();
		if (field == "Text Field - Q"){
			currentInputField = 1;
		}
		if (field == "Text Field - A"){
			currentInputField = 2;
		}
		if (field == "Text Field - B"){
			currentInputField = 3;
		}
		if (field == "Text Field - C"){
			currentInputField = 4;
		}
		if (field == "Text Field - D"){
			currentInputField = 5;
		}
		if (field == "Text Field - E"){
			currentInputField = 6;
		}
	}
	
	public void MouseOverQuestion(GameObject button){
		int index = 0;
		for (int i = 0; i < questionList.Count; i++){
			if (questionList[i].button == button){
				index = i;
				break;
			}
		}
		string questionTypeString = "()";
		if (questionList[index].question.questionType == 0)
			questionTypeString = "(T)";
		if (questionList[index].question.questionType == 1)
			questionTypeString = "(L)";
		if (questionList[index].question.questionType == 2)
			questionTypeString = "(P)";
		if (questionList[index].question.questionType == 3)
			questionTypeString = "(Tl)";
		if (questionList[index].question.questionType == 4)
			questionTypeString = "(I)";
		auxTextFieldI.GetComponent<InputField>().text = "Questão " + questionTypeString + " nº " + questionList[index].question.index.ToString();
		auxTextFieldQ.GetComponent<InputField>().text = questionList[index].question.text;
		auxTextFieldA.GetComponent<InputField>().text = questionList[index].question.answer0;
		auxTextFieldB.GetComponent<InputField>().text = questionList[index].question.answer1;
		auxTextFieldC.GetComponent<InputField>().text = questionList[index].question.answer2;
		auxTextFieldD.GetComponent<InputField>().text = questionList[index].question.answer3;
		auxTextFieldE.GetComponent<InputField>().text = questionList[index].question.answer4;
	}
	
	public void MouseOverQuestion(){
		auxTextFieldI.GetComponent<InputField>().text = "";
		auxTextFieldQ.GetComponent<InputField>().text = "";
		auxTextFieldA.GetComponent<InputField>().text = "";
		auxTextFieldB.GetComponent<InputField>().text = "";
		auxTextFieldC.GetComponent<InputField>().text = "";
		auxTextFieldD.GetComponent<InputField>().text = "";
		auxTextFieldE.GetComponent<InputField>().text = "";
	}
	
	public void ClearInputFieldIndex(){
		currentInputField = 0;
	}
	
	public void AcceptQuestion (){
		ClearInputFieldIndex();
		string q = textFieldQ.GetComponent<InputField>().text;
		string a = textFieldA.GetComponent<InputField>().text;
		string b = textFieldB.GetComponent<InputField>().text;
		string c = textFieldC.GetComponent<InputField>().text;
		string d = textFieldD.GetComponent<InputField>().text;
		string e = textFieldE.GetComponent<InputField>().text;
		if (currentQuestionType == 0){	
			if (q == "" && a == "" && b == "" && c == "" && d == "" && e == ""){
				Log("Question is empty!");
				return;
			}
			if (q == "" || a == "" || b == "" || c == "" || d == "" || e == ""){
				Log("Question is lacking fields!");
				return;
			}
		}
		if (currentQuestionType == 1){	
			if (q == "" && a == ""){
				Log("Question is empty!");
				return;
			}
			if (q == "" || a == ""){
				Log("Question is lacking fields!");
				return;
			}
		}
		if (currentQuestionType == 2){	
			if (q == "" && a == "" && b == "" && c == "" && d == ""){
				Log("Question is empty!");
				return;
			}
			if (q == "" || a == "" || b == "" || c == "" || d == ""){
				Log("Question is lacking fields!");
				return;
			}
		}
		if (currentQuestionType == 3){	
			if (q == "" && a == "" && b == "" && c == "" && d == "" && e == ""){
				Log("Question is empty!");
				return;
			}
			if (q == "" || a == "" || b == "" || c == "" || d == "" || e == ""){
				Log("Question is lacking fields!");
				return;
			}
		}
		if (currentQuestionType == 4){	
			if (q == "" && a == "" && b == "" && c == "" && d == "" && e == ""){
				Log("Question is empty!");
				return;
			}
			if (q == "" || a == "" || b == "" || c == "" || d == "" || e == ""){
				Log("Question is lacking fields!");
				return;
			}
		}
		Log("Saved question #" + currentQuestionIndex.ToString());
		textFieldI.GetComponent<InputField>().text = "Questão nº" + currentQuestionIndex.ToString();
		List <bool> subjectSelected = NewSubjectSelectedInQuestion ();
		List <bool> userGroupSelected = NewUserGroupSelectedInQuestion ();
		if (newQuestion){
			if (currentQuestionType == 0){
				AddQuestion(new Question(currentQuestionIndex, currentQuestionType, q, a, b, c, d, e, subjectSelected, userGroupSelected));
			}
			if (currentQuestionType == 1){
				AddQuestion(new Question(currentQuestionIndex, currentQuestionType, q, a, "", "", "", "", subjectSelected, userGroupSelected));
			}
			if (currentQuestionType == 2){
				AddQuestion(new Question(currentQuestionIndex, currentQuestionType, q, a, b, c, d, "", subjectSelected, userGroupSelected));
			}
			if (currentQuestionType == 3){
				AddQuestion(new Question(currentQuestionIndex, currentQuestionType, q, a, b, c, d, e, subjectSelected, userGroupSelected));
			}
			if (currentQuestionType == 4){
				AddQuestion(new Question(currentQuestionIndex, currentQuestionType, q, a, b, c, d, e, subjectSelected, userGroupSelected));
			}
		}
		if (!newQuestion){
			if (currentQuestionType == 0){
				questionList[currentQuestionIndex].question = new Question(currentQuestionIndex, currentQuestionType, q, a, b, c, d, e, subjectSelected, userGroupSelected);
			}
			if (currentQuestionType == 1){
				questionList[currentQuestionIndex].question.questionType = currentQuestionType;
				questionList[currentQuestionIndex].question.text = q;
				questionList[currentQuestionIndex].question.answer0 = a;
				questionList[currentQuestionIndex].question.ClientSpecification(subjectSelected, userGroupSelected);
			}
			if (currentQuestionType == 2){
				questionList[currentQuestionIndex].question = new Question(currentQuestionIndex, currentQuestionType, q, a, b, c, d, "", subjectSelected, userGroupSelected);
			}
			if (currentQuestionType == 3){
				questionList[currentQuestionIndex].question.questionType = currentQuestionType;
				questionList[currentQuestionIndex].question.text = q;
				questionList[currentQuestionIndex].question.answer0 = a;
				questionList[currentQuestionIndex].question.ClientSpecification(subjectSelected, userGroupSelected);
			}
			if (currentQuestionType == 4){
				questionList[currentQuestionIndex].question = new Question(currentQuestionIndex, currentQuestionType, q, a, b, c, d, e, subjectSelected, userGroupSelected);
			}
			questionList[currentQuestionIndex].button.gameObject.transform.Find("Text").GetComponent<Text>().text = "Questão " + currentQuestionIndex.ToString()  + ": " + questionList[currentQuestionIndex].question.text;
		}
		NextQuestion();
		OrderListOfQuestion();
	}
	
	public List <bool> NewUserGroupSelectedInQuestion (){
		List <bool> newList = new List <bool>();
		for (int i = 0; i < userGroup.Count; i++){
			newList.Add(userGroup[i].selectedInQuestion);
		}
		return newList;
	}
	
	public List <bool> NewSubjectSelectedInQuestion (){
		List <bool> newList = new List <bool>();
		for (int i = 0; i < userGroup.Count; i++){
			newList.Add(subject[i].selectedInQuestion);
		}
		return newList;
	}
	
	public void ChangeType(){
		currentQuestionType = currentQuestionType + 1;
		if (currentQuestionType > 4){ // Loops question types
			currentQuestionType = 0;
		}
		if (questionList.Count != 0){
			if (!newQuestion){
				questionList[currentQuestionIndex].question.questionType = currentQuestionType;
			}
		}
		SetChangeTypeButton();
	}
	
	public void SetChangeTypeButton(){
		if (currentQuestionType == 0){
			textFieldB.GetComponent<InputField>().interactable = true;
			textFieldC.GetComponent<InputField>().interactable = true;
			textFieldD.GetComponent<InputField>().interactable = true;
			textFieldE.GetComponent<InputField>().interactable = true;
			buttonT.transform.Find("Text").GetComponent<Text>().text = "Tipo: Teste";
			textFieldB.GetComponent<InputField>().text = "";
			textFieldC.GetComponent<InputField>().text = "";
			textFieldD.GetComponent<InputField>().text = "";
			textFieldE.GetComponent<InputField>().text = "";
			if (questionList.Count != 0){
				if (!newQuestion){
					textFieldB.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer1;
					textFieldC.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer2;
					textFieldD.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer3;
					textFieldE.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer4;
				}
			}
			textFieldA.transform.Find("Placeholder").GetComponent<Text>().text = "[item A, resposta certa]";
			textFieldB.transform.Find("Placeholder").GetComponent<Text>().text = "[item B]";
			textFieldC.transform.Find("Placeholder").GetComponent<Text>().text = "[item C]";
			textFieldD.transform.Find("Placeholder").GetComponent<Text>().text = "[item D]";
			textFieldE.transform.Find("Placeholder").GetComponent<Text>().text = "[item E]";
		}
		if (currentQuestionType == 1){
			buttonT.transform.Find("Text").GetComponent<Text>().text = "Tipo: Lacuna";
			textFieldB.GetComponent<InputField>().text = "n/a";
			textFieldC.GetComponent<InputField>().text = "n/a";
			textFieldD.GetComponent<InputField>().text = "n/a";
			textFieldE.GetComponent<InputField>().text = "n/a";
			textFieldB.GetComponent<InputField>().interactable = false;
			textFieldC.GetComponent<InputField>().interactable = false;
			textFieldD.GetComponent<InputField>().interactable = false;
			textFieldE.GetComponent<InputField>().interactable = false;
			textFieldA.transform.Find("Placeholder").GetComponent<Text>().text = "[resposta]";
			textFieldB.transform.Find("Placeholder").GetComponent<Text>().text = "n/a";
			textFieldC.transform.Find("Placeholder").GetComponent<Text>().text = "n/a";
			textFieldD.transform.Find("Placeholder").GetComponent<Text>().text = "n/a";
			textFieldE.transform.Find("Placeholder").GetComponent<Text>().text = "n/a";
		}
		if (currentQuestionType == 2){
			textFieldB.GetComponent<InputField>().text = "";
			textFieldC.GetComponent<InputField>().text = "";
			textFieldD.GetComponent<InputField>().text = "";
			if (questionList.Count != 0){
				if (!newQuestion){
					textFieldB.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer1;
					textFieldC.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer2;
					textFieldD.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer3;
				}
			}
			textFieldB.GetComponent<InputField>().interactable = true;
			textFieldC.GetComponent<InputField>().interactable = true;
			textFieldD.GetComponent<InputField>().interactable = true;
			buttonT.transform.Find("Text").GetComponent<Text>().text = "Tipo: Point&Click";
			textFieldE.GetComponent<InputField>().text = "n/a";
			textFieldE.GetComponent<InputField>().interactable = false;
			textFieldA.transform.Find("Placeholder").GetComponent<Text>().text = "[resposta]";
			textFieldB.transform.Find("Placeholder").GetComponent<Text>().text = "[código de cor: vermelho, entre 0 e 1]";
			textFieldC.transform.Find("Placeholder").GetComponent<Text>().text = "[código de cor: azul, entre 0 e 1]";
			textFieldD.transform.Find("Placeholder").GetComponent<Text>().text = "[código de cor: verde, entre 0 e 1]";
			textFieldE.transform.Find("Placeholder").GetComponent<Text>().text = "n/a";
		}
		if (currentQuestionType == 3){
			textFieldB.GetComponent<InputField>().interactable = true;
			textFieldC.GetComponent<InputField>().interactable = true;
			textFieldD.GetComponent<InputField>().interactable = true;
			textFieldE.GetComponent<InputField>().interactable = true;
			buttonT.transform.Find("Text").GetComponent<Text>().text = "Tipo: Longa";
			textFieldB.GetComponent<InputField>().text = "";
			textFieldC.GetComponent<InputField>().text = "";
			textFieldD.GetComponent<InputField>().text = "";
			textFieldE.GetComponent<InputField>().text = "";
			if (questionList.Count != 0){
				if (!newQuestion){
					textFieldB.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer1;
					textFieldC.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer2;
					textFieldD.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer3;
					textFieldE.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer4;
				}
			}
			textFieldA.transform.Find("Placeholder").GetComponent<Text>().text = "[item A, resposta certa]";
			textFieldB.transform.Find("Placeholder").GetComponent<Text>().text = "[item B]";
			textFieldC.transform.Find("Placeholder").GetComponent<Text>().text = "[item C]";
			textFieldD.transform.Find("Placeholder").GetComponent<Text>().text = "[item D]";
			textFieldE.transform.Find("Placeholder").GetComponent<Text>().text = "[item E]";
		}
		if (currentQuestionType == 4){
			textFieldB.GetComponent<InputField>().interactable = true;
			textFieldC.GetComponent<InputField>().interactable = true;
			textFieldD.GetComponent<InputField>().interactable = true;
			textFieldE.GetComponent<InputField>().interactable = true;
			buttonT.transform.Find("Text").GetComponent<Text>().text = "Tipo: Imagem";
			textFieldB.GetComponent<InputField>().text = "";
			textFieldC.GetComponent<InputField>().text = "";
			textFieldD.GetComponent<InputField>().text = "";
			textFieldE.GetComponent<InputField>().text = "";
			if (questionList.Count != 0){
				if (!newQuestion){
					textFieldB.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer1;
					textFieldC.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer2;
					textFieldD.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer3;
					textFieldE.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer4;
				}
			}
			textFieldA.transform.Find("Placeholder").GetComponent<Text>().text = "[item A, resposta certa]";
			textFieldB.transform.Find("Placeholder").GetComponent<Text>().text = "[item B]";
			textFieldC.transform.Find("Placeholder").GetComponent<Text>().text = "[item C]";
			textFieldD.transform.Find("Placeholder").GetComponent<Text>().text = "[item D]";
			textFieldE.transform.Find("Placeholder").GetComponent<Text>().text = "[item E]";
		}
	}
	
	public void UserGroupAndSubjectWindow(){
		if(questionTGWindowsOpen){
			questionTGWindows.SetActive(false);
		}
		if(!questionTGWindowsOpen){
			questionTGWindows.SetActive(true);
		}
		questionTGWindowsOpen = !questionTGWindowsOpen;
	}
	
	public void ClearQuestion(){
		if (newQuestion){
			textFieldI.GetComponent<InputField>().text = "Nova questão, nº " + currentQuestionIndex.ToString();		
			textFieldQ.GetComponent<InputField>().text = "";
			textFieldA.GetComponent<InputField>().text = "";
			textFieldB.GetComponent<InputField>().text = "";
			textFieldC.GetComponent<InputField>().text = "";
			textFieldD.GetComponent<InputField>().text = "";
			textFieldE.GetComponent<InputField>().text = "";
		}
		if (!newQuestion){
			textFieldI.GetComponent<InputField>().text = "Questão nº " + currentQuestionIndex.ToString();
			textFieldQ.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.text;
			textFieldA.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer0;
			textFieldB.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer1;
			textFieldC.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer2;
			textFieldD.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer3;
			textFieldE.GetComponent<InputField>().text = questionList[currentQuestionIndex].question.answer4;
		}
		SetChangeTypeButton();
	}
	
	public void AddQuestion(Question newQuestion){
		GameObject newButton = Instantiate(button) as GameObject;
        newButton.transform.SetParent(this.gameObject.transform.Find("Question List/Viewport/Content"), false);
		newButton.name = currentQuestionIndex.ToString();
		questionList.Add(new QuestionButton(newQuestion, newButton));
		currentQuestionIndex = questionList.Count;
		newButton.GetComponent<Button>().onClick.AddListener(delegate{ChooseQuestion(newQuestion.index);});
		OrderListOfQuestion();
	}
	
	public void OrderListOfQuestion(){
		for (int i = 0; i < questionList.Count; i ++){
			string questionTypeString = "()";
			if (questionList[i].question.questionType == 0)
				questionTypeString = "(T)";
			if (questionList[i].question.questionType == 1)
				questionTypeString = "(L)";
			if (questionList[i].question.questionType == 2)
				questionTypeString = "(P)";
			if (questionList[i].question.questionType == 3)
				questionTypeString = "(Tl)";
			if (questionList[i].question.questionType == 4)
				questionTypeString = "(I)";
			questionList[i].button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * Screen.height/15);
			questionList[i].button.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3 - Screen.width/15 - Screen.width/30, Screen.height/15);
			questionList[i].button.gameObject.transform.Find("Text").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/75, 0);
			questionList[i].button.gameObject.transform.Find("Text").GetComponent<RectTransform>().sizeDelta = new Vector2(- Screen.width/50, 0);
			questionList[i].button.gameObject.transform.Find("Text").GetComponent<Text>().text = "Questão " + questionTypeString + " " + questionList[i].question.index.ToString() + ": " + questionList[i].question.text;
			this.gameObject.transform.Find("Question List/Viewport/Content").GetComponent<RectTransform>().sizeDelta = new Vector2(0, (1 + i) * Screen.height/15);
		}
	}
	
	public void ChooseQuestion(int index){
		ClearInputFieldIndex();
		newQuestion = false;
		currentQuestionIndex = index;
		currentQuestionType = questionList[index].question.questionType;
		SetChangeTypeButton();
		textFieldI.GetComponent<InputField>().text = "Questão nº " + questionList[index].question.index.ToString();
		textFieldQ.GetComponent<InputField>().text = questionList[index].question.text;
		textFieldA.GetComponent<InputField>().text = questionList[index].question.answer0;
		textFieldB.GetComponent<InputField>().text = questionList[index].question.answer1;
		textFieldC.GetComponent<InputField>().text = questionList[index].question.answer2;
		textFieldD.GetComponent<InputField>().text = questionList[index].question.answer3;
		textFieldE.GetComponent<InputField>().text = questionList[index].question.answer4;
		if (userGroup.Count > 0){
			for (int i = 0; i < userGroup.Count; i++){
				userGroup[i].questionButton.GetComponent<Toggle>().isOn = questionList[index].question.userGroups[i];
			}
		}
		if (subject.Count > 0){
			for (int i = 0; i < subject.Count; i++){
				subject[i].questionButton.GetComponent<Toggle>().isOn = questionList[index].question.subjects[i];
			}
		}
		Log("Opened question #" + currentQuestionIndex.ToString());
		QuestionEditorWindow();
		
		//test
		print ("question " + index + ", " + questionList[index].question.subjects.Count + " subjects");
		for (int i = 0; i < questionList[index].question.subjects.Count; i++){
			print (subject[i].name + ": " + questionList[index].question.subjects[i]);
		}
	}
	
	public void NewQuestion(){
		if (newQuestion){
			return;
		}
		if (!newQuestion){
			NextQuestion();
		}
	}
	
	public void NextQuestion(){
		ClearInputFieldIndex();
		currentQuestionIndex = questionList.Count;
		newQuestion = true;
		ClearQuestion ();
	}
	
	public void DeleteQuestion(){
		ClearInputFieldIndex();
		if (newQuestion){
			return;
		}
		Destroy(questionList[currentQuestionIndex].button);
		questionList.Remove(questionList[currentQuestionIndex]);
		for (int i = currentQuestionIndex; i < questionList.Count; i++){
			questionList[i].question.index = i;
		}
		Log("Deleted question #" + currentQuestionIndex.ToString());
		OrderListOfQuestion();
		NewQuestion();
	}
	
	public void SaveFile(){
		ClearInputFieldIndex();
		if (questionList.Count < 1){
			Log("There are no questions to be saved!");
			return;
		}
		BinaryFormatter bf = new BinaryFormatter();
		FileStream fileStream;
		if (!File.Exists(Application.persistentDataPath + "/" + fileName)){
			fileStream = File.Create(Application.persistentDataPath + "/" + fileName);
		} else {
			fileStream = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);
		}
		BncQFile qdbFile = new BncQFile();
		
		// info saved - start
		// question
		qdbFile.t = new List<string>();		// Text
		qdbFile.tp = new List<int>();		// Type
		qdbFile.a = new List<string>();		// Item A
		qdbFile.b = new List<string>();		// Item B
		qdbFile.c = new List<string>();		// Item C
		qdbFile.d = new List<string>();		// Item D
		qdbFile.e = new List<string>();		// Item E
		qdbFile.u = new List<string>();		// User Group
		qdbFile.s = new List<string>();		// Subject
		for (int i = 0; i < questionList.Count; i++){
			qdbFile.t.Add(questionList[i].question.text);
			qdbFile.tp.Add(questionList[i].question.questionType);
			qdbFile.a.Add(questionList[i].question.answer0);
			qdbFile.b.Add(questionList[i].question.answer1);
			qdbFile.c.Add(questionList[i].question.answer2);
			qdbFile.d.Add(questionList[i].question.answer3);
			qdbFile.e.Add(questionList[i].question.answer4);
			string u = "";
			string s = "";
			if (userGroup.Count > 0){
				for (int y = 0; y < questionList[i].question.userGroups.Count; y++){
					if (y != 0){
						u = u + " ";
					}
					if (questionList[i].question.userGroups[y]){
						u = u + "T";
					}
					if (!questionList[i].question.userGroups[y]){
						u = u + "F";
					}
				}
				qdbFile.u.Add(u);
			} if (userGroup.Count == 0){
				qdbFile.u.Add("X");
			}
			if (subject.Count > 0){
				for (int y = 0; y < questionList[i].question.subjects.Count; y++){
					if (y != 0){
						s = s + " ";
					}
					if (questionList[i].question.subjects[y]){
						s = s + "T";
					}
					if (!questionList[i].question.subjects[y]){
						s = s + "F";
					}
				}
				qdbFile.s.Add(s);
			} if (subject.Count == 0){
				qdbFile.s.Add("X");
			}
		}
		// client
		qdbFile.client = client;
		qdbFile.uName = new List<string>();
		qdbFile.sName = new List<string>();
		for (int i = 0; i < userGroup.Count; i++){
			qdbFile.uName.Add(userGroup[i].name);
		}
		for (int i = 0; i < subject.Count; i++){
			qdbFile.sName.Add(subject[i].name);
		}
		// info saved - end	
		
		bf.Serialize(fileStream, qdbFile);
		fileStream.Close();
		Log("Saved BncQ file, at " + Application.persistentDataPath);
	}
	
	public void ClientEditorWindow(){
		if (clientEditorOpen){
			questionEditorOpen = true;
			clientEditorOpen = false;
			filterWindowOpen = false;
			clientWindow.SetActive(false);
			filterWindow.SetActive(false);
			questionOptions.SetActive(true);
			return;
		}
		if (!clientEditorOpen){
			questionEditorOpen = false;
			clientEditorOpen = true;
			filterWindowOpen = false;
			clientWindow.SetActive(true);
			filterWindow.SetActive(false);
			questionOptions.SetActive(false);
			return;
		}
	}
	
	public void SetClientName(string name){
		client = name;
		fileName = "BncQ_" + client + ".dat";
	}
	
	public void AddUserGroup(){
		string newName = "grupo " + userGroup.Count.ToString();
		int nextIndex = userGroup.Count;
		GameObject newInput = Instantiate(inputField) as GameObject;
		newInput.transform.SetParent(this.gameObject.transform.Find("Client Window/User Groups Scroll/Viewport/Content"), false);
		newInput.transform.Find("Placeholder").GetComponent<Text>().text = "grupo " +  userGroup.Count.ToString();
		newInput.GetComponent<InputField>().onEndEdit.AddListener(delegate{SetUserGroup();});
		GameObject newFilterButton = Instantiate(checkbox) as GameObject;
		newFilterButton.transform.SetParent(this.gameObject.transform.Find("Filter Window/User Groups Scroll/Viewport/Content"), false);
		newFilterButton.GetComponent<Toggle>().onValueChanged.AddListener(delegate{SelectUserGroupFilter(nextIndex);});
		GameObject newQuestionButton = Instantiate(checkbox) as GameObject;
		newQuestionButton.transform.SetParent(this.gameObject.transform.Find("Question Buttons/TG Window/User Groups Scroll/Viewport/Content"), false);
		newQuestionButton.GetComponent<Toggle>().onValueChanged.AddListener(delegate{SelectUserGroupInQuestion(nextIndex);});
		userGroup.Add(new UserGroup(newName, newInput, newFilterButton, newQuestionButton));
		for (int i = 0; i < questionList.Count; i++){
			questionList[i].question.userGroups.Add(false);
		}
		OrderUserGroupEditor();
		clientWindow.transform.Find("User Groups Set").GetComponent<InputField>().text = userGroup.Count.ToString();	
	}
	
	public void LoadUserGroup(){	// Same as Add, but does not expand the "questionList[i].question.userGroups" list
		string newName = "grupo " + userGroup.Count.ToString();
		int nextIndex = userGroup.Count;
		GameObject newInput = Instantiate(inputField) as GameObject;
		newInput.transform.SetParent(this.gameObject.transform.Find("Client Window/User Groups Scroll/Viewport/Content"), false);
		newInput.transform.Find("Placeholder").GetComponent<Text>().text = "grupo " +  userGroup.Count.ToString();
		newInput.GetComponent<InputField>().onEndEdit.AddListener(delegate{SetUserGroup();});
		GameObject newFilterButton = Instantiate(checkbox) as GameObject;
		newFilterButton.transform.SetParent(this.gameObject.transform.Find("Filter Window/User Groups Scroll/Viewport/Content"), false);
		newFilterButton.GetComponent<Toggle>().onValueChanged.AddListener(delegate{SelectUserGroupFilter(nextIndex);});
		GameObject newQuestionButton = Instantiate(checkbox) as GameObject;
		newQuestionButton.transform.SetParent(this.gameObject.transform.Find("Question Buttons/TG Window/User Groups Scroll/Viewport/Content"), false);
		newQuestionButton.GetComponent<Toggle>().onValueChanged.AddListener(delegate{SelectUserGroupInQuestion(nextIndex);});
		userGroup.Add(new UserGroup(newName, newInput, newFilterButton, newQuestionButton));
		OrderUserGroupEditor();
		clientWindow.transform.Find("User Groups Set").GetComponent<InputField>().text = userGroup.Count.ToString();
	}
	
	public void SetUserGroup(){
		for (int i = 0; i < userGroup.Count; i++){
			userGroup[i].SetUserGroup();
		}
	}
	
	public void RemoveUserGroup(){
		if (userGroup.Count > 0){
			int index = userGroup.Count -1;
			userGroup[index].DestroyButtons();
			clientWindow.transform.Find("User Groups Set").GetComponent<InputField>().text = userGroup.Count.ToString();
			userGroup.RemoveAt(index);
			for (int i = 0; i < questionList.Count; i++){
				questionList[i].question.userGroups.RemoveAt(index);
			}
		}
	}
	
	public void OrderUserGroupEditor(){
		for (int i = 0; i < userGroup.Count; i++){
			RectTransform rectClient = userGroup[i].clientButton.GetComponent<RectTransform>();
			rectClient.sizeDelta = new Vector2( Screen.width/6, Screen.height/15);
			rectClient.anchoredPosition = new Vector2(0, - Screen.height/15 * (1 + i));
			clientWindow.transform.Find("User Groups Scroll/Viewport/Content").GetComponent<RectTransform>().sizeDelta = new Vector2(0, (1 + i) * Screen.height/15);
			RectTransform rectFilter = userGroup[i].filterButton.GetComponent<RectTransform>();
			userGroup[i].filterButton.transform.Find("Label").GetComponent<Text>().text = userGroup[i].name;
			rectFilter.sizeDelta = new Vector2( Screen.width/6, Screen.height/15);
			rectFilter.anchoredPosition = new Vector2(0, - Screen.height/15 * (i));
			filterWindow.transform.Find("User Groups Scroll/Viewport/Content").GetComponent<RectTransform>().sizeDelta = new Vector2(0, (1 + i) * Screen.height/15);
			RectTransform rectQuestion = userGroup[i].questionButton.GetComponent<RectTransform>();
			userGroup[i].questionButton.transform.Find("Label").GetComponent<Text>().text = userGroup[i].name;
			rectQuestion.sizeDelta = new Vector2( Screen.width/6, Screen.height/15);
			rectQuestion.anchoredPosition = new Vector2(0, - Screen.height/15 * (i));
			questionTGWindows.transform.Find("User Groups Scroll/Viewport/Content").GetComponent<RectTransform>().sizeDelta = new Vector2(0, (1 + i) * Screen.height/15);
		}
	}
	
	public void SelectUserGroupInQuestion (int selected){
		userGroup[selected].ToggleInQuestionWindow();
		if (newQuestion){
			return;
		}
		if (!newQuestion){
			questionList[currentQuestionIndex].question.userGroups[selected] = userGroup[selected].selectedInQuestion;
		}
	}
	
	public void SelectUserGroupFilter(int filter){
		userGroup[filter].selectedFilter = !userGroup[filter].selectedFilter;
		// ADD FILTER HERE!!
	}
	
	public void AddSubject(){
		string newName = "tópico " + subject.Count.ToString();
		int nextIndex = subject.Count;
		GameObject newInput = Instantiate(inputField) as GameObject;
		newInput.transform.SetParent(this.gameObject.transform.Find("Client Window/Subjects Scroll/Viewport/Content"), false);
		newInput.transform.Find("Placeholder").GetComponent<Text>().text = "tópico " +  subject.Count.ToString();
		newInput.GetComponent<InputField>().onEndEdit.AddListener(delegate{SetSubject();});
		GameObject newFilterButton = Instantiate(checkbox) as GameObject;
		newFilterButton.transform.SetParent(this.gameObject.transform.Find("Filter Window/Subjects Scroll/Viewport/Content"), false);
		newFilterButton.GetComponent<Toggle>().onValueChanged.AddListener(delegate{SelectSubjectFilter(nextIndex);});
		GameObject newQuestionButton = Instantiate(checkbox) as GameObject;
		newQuestionButton.transform.SetParent(this.gameObject.transform.Find("Question Buttons/TG Window/Subjects Scroll/Viewport/Content"), false);
		newQuestionButton.GetComponent<Toggle>().onValueChanged.AddListener(delegate{SelectSubjectInQuestion(nextIndex);});
		subject.Add(new Subject(newName, newInput, newFilterButton, newQuestionButton));
		for (int i = 0; i < questionList.Count; i++){
			questionList[i].question.subjects.Add(false);
		}
		OrderSubjectEditor();
		clientWindow.transform.Find("Subjects Set").GetComponent<InputField>().text = subject.Count.ToString();	
	}
	
	public void LoadSubject(){	// Same as Add, but does not expand the "questionList[i].question.userGroups" list
		string newName = "tópico " + subject.Count.ToString();
		int nextIndex = subject.Count;
		GameObject newInput = Instantiate(inputField) as GameObject;
		newInput.transform.SetParent(this.gameObject.transform.Find("Client Window/Subjects Scroll/Viewport/Content"), false);
		newInput.transform.Find("Placeholder").GetComponent<Text>().text = "tópico " +  subject.Count.ToString();
		newInput.GetComponent<InputField>().onEndEdit.AddListener(delegate{SetSubject();});
		GameObject newFilterButton = Instantiate(checkbox) as GameObject;
		newFilterButton.transform.SetParent(this.gameObject.transform.Find("Filter Window/Subjects Scroll/Viewport/Content"), false);
		newFilterButton.GetComponent<Toggle>().onValueChanged.AddListener(delegate{SelectSubjectFilter(nextIndex);});
		GameObject newQuestionButton = Instantiate(checkbox) as GameObject;
		newQuestionButton.transform.SetParent(this.gameObject.transform.Find("Question Buttons/TG Window/Subjects Scroll/Viewport/Content"), false);
		newQuestionButton.GetComponent<Toggle>().onValueChanged.AddListener(delegate{SelectSubjectInQuestion(nextIndex);});
		subject.Add(new Subject(newName, newInput, newFilterButton, newQuestionButton));
		OrderSubjectEditor();
		clientWindow.transform.Find("Subjects Set").GetComponent<InputField>().text = subject.Count.ToString();
	}
	
	public void SetSubject(){
		for (int i = 0; i < subject.Count; i++){
			subject[i].SetSubject();
		}
	}
	
	public void RemoveSubject(){
		if (subject.Count > 0){
			int index = subject.Count -1;
			subject[index].DestroyButtons();
			clientWindow.transform.Find("Subjects Set").GetComponent<InputField>().text = subject.Count.ToString();
			subject.RemoveAt(index);
			for (int i = 0; i < questionList.Count; i++){
				questionList[i].question.subjects.RemoveAt(index);
			}
		}
	}
	
	public void OrderSubjectEditor(){
		for (int i = 0; i < subject.Count; i++){
			RectTransform rectClient = subject[i].clientButton.GetComponent<RectTransform>();
			rectClient.sizeDelta = new Vector2( Screen.width/6, Screen.height/15);
			rectClient.anchoredPosition = new Vector2(0, - Screen.height/15 * (1 + i));
			clientWindow.transform.Find("Subjects Scroll/Viewport/Content").GetComponent<RectTransform>().sizeDelta = new Vector2(0, (1 + i) * Screen.height/15);
			RectTransform rectFilter = subject[i].filterButton.GetComponent<RectTransform>();
			subject[i].filterButton.transform.Find("Label").GetComponent<Text>().text = subject[i].name;
			rectFilter.sizeDelta = new Vector2( Screen.width/6, Screen.height/15);
			rectFilter.anchoredPosition = new Vector2(0, - Screen.height/15 * (i));
			filterWindow.transform.Find("Subjects Scroll/Viewport/Content").GetComponent<RectTransform>().sizeDelta = new Vector2(0, (1 + i) * Screen.height/15);
			RectTransform rectQuestion = subject[i].questionButton.GetComponent<RectTransform>();
			subject[i].questionButton.transform.Find("Label").GetComponent<Text>().text = subject[i].name;
			rectQuestion.sizeDelta = new Vector2( Screen.width/6, Screen.height/15);
			rectQuestion.anchoredPosition = new Vector2(0, - Screen.height/15 * (i));
			questionTGWindows.transform.Find("Subjects Scroll/Viewport/Content").GetComponent<RectTransform>().sizeDelta = new Vector2(0, (1 + i) * Screen.height/15);
		}
	}
	
	public void SelectSubjectInQuestion (int selected){
		subject[selected].ToggleInQuestionWindow();
		if (newQuestion){
			return;
		}
		if (!newQuestion){
			questionList[currentQuestionIndex].question.subjects[selected] = subject[selected].selectedInQuestion;
		}
	}
	
	public void SelectSubjectFilter(int filter){
		subject[filter].selectedFilter = !subject[filter].selectedFilter;
		// ADD FILTER HERE!!
	}
	
	public void ClearClientePreferences(){
		if (userGroup.Count > 0){
			for (int i = 0; i < questionList.Count; i++){
				questionList[i].question.userGroups = new List<bool>();
			}
			for (int i = 0; i < userGroup.Count; i++){
			RemoveUserGroup();
			}
		}
		if (subject.Count > 0){
			for (int i = 0; i < questionList.Count; i++){
				questionList[i].question.subjects = new List<bool>();
			}
			for (int i = 0; i < subject.Count; i++){
			RemoveSubject();
			}
		}
	}
	
	public void FilterWindow(){
		if (filterWindowOpen){
			questionEditorOpen = true;
			clientEditorOpen = false;
			filterWindowOpen = false;
			clientWindow.SetActive(false);
			filterWindow.SetActive(false);
			questionOptions.SetActive(true);
			return;
		}
		if (!filterWindowOpen){
			questionEditorOpen = false;
			clientEditorOpen = false;
			filterWindowOpen = true;
			clientWindow.SetActive(false);
			filterWindow.SetActive(true);
			questionOptions.SetActive(false);
			return;
		}
	}
	
	public void QuestionEditorWindow(){
		// if (questionEditorOpen){
			// questionEditorOpen = false;
			// clientEditorOpen = false;
			// filterWindowOpen = false;
			// clientWindow.SetActive(false);
			// filterWindow.SetActive(false);
			// questionOptions.SetActive(false);
			// return;
		// }
		if (!questionEditorOpen){
			questionEditorOpen = true;
			clientEditorOpen = false;
			filterWindowOpen = false;
			clientWindow.SetActive(false);
			filterWindow.SetActive(false);
			questionOptions.SetActive(true);
			return;
		}
	}
	
	public void LoadFile (){
		if (loadedQuestionFile){
			Restart();
			return;
		}
		buttonLoadBncQ.transform.Find("Text").GetComponent<Text>().text = "LIMPAR BncQ";
		loadedQuestionFile = true;
		ClearClientePreferences();
		if (File.Exists(Application.persistentDataPath + "/" + fileName)){
			Log("Loaded BncQ file");
			BinaryFormatter bf = new BinaryFormatter();
			FileStream fileStream = File.Open (Application.persistentDataPath + "/" + fileName, FileMode.Open);
			BncQFile qdbFile = (BncQFile)bf.Deserialize(fileStream);
			
			// info loaded - start
			// question
			for (int i = 0; i < qdbFile.t.Count; i++){
				AddQuestion(new Question(
				i,
				qdbFile.tp[i],
				qdbFile.t[i],
				qdbFile.a[i],
				qdbFile.b[i],
				qdbFile.c[i],
				qdbFile.d[i],
				qdbFile.e[i]));
				if (qdbFile.u != null){
					if (qdbFile.u.Count > 0){
						if (qdbFile.u[0] != "X"){
							string[] questionUserGroups;
							string[] space = new string[] {" "};
							questionUserGroups = qdbFile.u[i].Split(space, StringSplitOptions.None);
							for (int y = 0; y < questionUserGroups.Length; y++){
								if (questionUserGroups[y] == "T"){
									questionList[i].question.userGroups.Add(true);
								}
								if (questionUserGroups[y] == "F"){
									questionList[i].question.userGroups.Add(false);
								}
							}
						}
					}
				}
				if (qdbFile.s != null){
					if (qdbFile.s.Count > 0){
						if (qdbFile.s[0] != "X"){
							string[] questionSubjects;
							string[] space = new string[] {" "};
							questionSubjects = qdbFile.s[i].Split(space, StringSplitOptions.None);
							for (int y = 0; y < questionSubjects.Length; y++){
								if (questionSubjects[y] == "T"){
									questionList[i].question.subjects.Add(true);
								}
								if (questionSubjects[y] == "F"){
									questionList[i].question.subjects.Add(false);
								}
							}
						}
					}
				}
			}
			// client
			client = qdbFile.client;
			clientWindow.transform.Find("Client").GetComponent<InputField>().text = client;
			if (qdbFile.uName != null){
				if (qdbFile.uName.Count > 0){
					for (int i = 0; i < qdbFile.uName.Count; i++){
						LoadUserGroup();
					}
					for (int i = 0; i < qdbFile.uName.Count; i++){
						userGroup[i].name = qdbFile.uName[i];
						userGroup[i].LoadUserGroup();
					}
				}
			}
			if (qdbFile.sName != null){
				if (qdbFile.sName.Count > 0){
					for (int i = 0; i < qdbFile.sName.Count; i++){
						LoadSubject();
					}
					for (int i = 0; i < qdbFile.sName.Count; i++){
						subject[i].name = qdbFile.sName[i];
						subject[i].LoadSubject();
					}
				}
			}
			// info loaded - end
			
			fileStream.Close();
		} else Log("There was no BncQ file to be loaded");
	}
	
	public void ReloadFile (){	// OBSOLETE
		ClearInputFieldIndex();
		if (File.Exists(Application.persistentDataPath + "/" + fileName)){
			Log("Reloaded BncQ file");
			BinaryFormatter bf = new BinaryFormatter();
			FileStream fileStream = File.Open (Application.persistentDataPath + "/" + fileName, FileMode.Open);
			BncQFile qdbFile = (BncQFile)bf.Deserialize(fileStream);		
			for (int i = 0; i < questionList.Count; i++){
				Destroy(questionList[i].button);
			}
			if (userGroup.Count > 0){
				for (int i = 0; i < userGroup.Count; i++){
					RemoveUserGroup();
				}
			}
			questionList = new List<QuestionButton>();
			
			// info loaded - start
			// question
			for (int i = 0; i < qdbFile.t.Count; i++){
				AddQuestion(new Question(
				i,
				qdbFile.tp[i],
				qdbFile.t[i],
				qdbFile.a[i],
				qdbFile.b[i],
				qdbFile.c[i],
				qdbFile.d[i],
				qdbFile.e[i]));
				if (qdbFile.u != null){
					if (qdbFile.u.Count > 0){
						string[] questionUserGroups;
						string[] space = new string[] {" "};
						questionUserGroups = qdbFile.u[i].Split(space, StringSplitOptions.None);
						for (int y = 0; y < questionUserGroups.Length; y++){
							if (questionUserGroups[y] == "T"){
								questionList[i].question.userGroups.Add(true);
							}
							if (questionUserGroups[y] == "F"){
								questionList[i].question.userGroups.Add(false);
							}
						}
					}
				}
				if (qdbFile.s != null){
					if (qdbFile.s.Count > 0){
						string[] questionSubjects;
						string[] space = new string[] {" "};
						questionSubjects = qdbFile.s[i].Split(space, StringSplitOptions.None);
						for (int y = 0; y < questionSubjects.Length; y++){
							if (questionSubjects[y] == "T"){
								questionList[i].question.subjects.Add(true);
							}
							if (questionSubjects[y] == "F"){
								questionList[i].question.subjects.Add(false);
							}
						}
					}
				}
			}
			// client
			client = qdbFile.client;
			clientWindow.transform.Find("Client").GetComponent<InputField>().text = client;
			if (qdbFile.uName.Count > 0){
				for (int i = 0; i < qdbFile.uName.Count; i++){
					LoadUserGroup();
				}
				for (int i = 0; i < qdbFile.uName.Count; i++){
					userGroup[i].name = qdbFile.uName[i];
					userGroup[i].LoadUserGroup();
				}
				for (int i = 0; i < qdbFile.sName.Count; i++){
					LoadSubject();
				}
				for (int i = 0; i < qdbFile.sName.Count; i++){
					subject[i].name = qdbFile.sName[i];
					subject[i].LoadSubject();
				}
			}
			// info loaded - end
			
			fileStream.Close();
			OrderListOfQuestion();
			OrderUserGroupEditor();
			NewQuestion();
		} else Log("There was no BncQ file to be reloaded");
	}
	
	public void DeleteFile (){
		ClearInputFieldIndex();
		if (File.Exists(Application.persistentDataPath + "/" + fileName)){
			Log ("Deleted BncQ file");
			File.Delete (Application.persistentDataPath + "/" + fileName);
		}
		else Log("There was no BncQ file to be deleted");
	}
	
	void Log (string newString){
		log.GetComponent<InputField>().text = DateTime.Now + " : " + newString;
	}
	
	public void Restart(){
		SceneManager.LoadScene("BncQ", LoadSceneMode.Single);
	}
	
	public void Quit (){
		Log ("Quit!");
		Application.Quit();
	}
	
	public void DeleteAllClientePreferencesAndSaveAndRestart(){
		userGroup = new List<UserGroup>();
		for (int i = 0; i < questionList.Count; i++){
			questionList[i].question.userGroups = new List <bool>();
		}
		SaveFile();
		Restart();
	}
}
