using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.SceneManagement;

[System.Serializable]
public class UserEditorClass : MonoBehaviour {

	//UI Elements
	GameObject textFieldIndex;
	GameObject textFieldName;
	GameObject textFieldEmail;
	// GameObject textFieldSenha;
	GameObject textFieldTelefone;
	GameObject textFieldCPF;
	// GameObject textFieldDepartamento;
	GameObject textFieldLocalizacao;
	GameObject togglePrimeiro;
	GameObject togglePrimeiroLine;
	GameObject textFieldRespostas;
	GameObject textFieldTempo;
	GameObject textFieldScore;
	GameObject userOptions;
	GameObject userTGWindows;
	GameObject buttonAccept;
	GameObject buttonClear;
	GameObject buttonNew;
	GameObject buttonDelete;
	GameObject userListWindow;
	GameObject buttonQ;
	GameObject clientWindow;
	//GameObject clientWindowReturn;
	//GameObject filterWindowReturn;
	GameObject userGroupList;
	GameObject buttonUploadUsers;
	GameObject waitingWindow;
	GameObject log;
	//Data
	int currentInputField = 0;
	public List<UserButton> userList;
	int currentUserIndex;
	bool currentQuestionMultipleAnswer = true;
	int currentQuestionType = 0; // 0: multiple answer, 1: fill the blanks, 2: point-and-click, 3: longa, 4: imagem
	bool newUser = true;
	string path = "C:/";
	string client;
	string fileName;
	public static List <UserGroup> userGroup;
	//Editor
	bool loadedQuestionFile;
	GameObject button;
	GameObject inputField;
	GameObject checkbox;
	bool userEditorOpen;
	bool userTGWindowsOpen;
	bool clientEditorOpen;
	bool filterWindowOpen;
	
	 public List <bool> subjectSelected;	// SELECTED IN EDITOR // REMOVE LATER
	 
	 // Firebase & Database
	 DatabaseScript databaseScript;
	 
	 // Miscellaneous
	 string nextScene;
	 bool changesToUpload;



	void Start () {
		//UI Elements
		this.gameObject.transform.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
		textFieldIndex = this.gameObject.transform.Find("Text Field - Index").gameObject;
		textFieldName = this.gameObject.transform.Find("Text Field - Nome").gameObject;
		textFieldEmail = this.gameObject.transform.Find("Text Field - Email").gameObject;
		// textFieldSenha = this.gameObject.transform.Find("Text Field - Senha").gameObject;
		textFieldTelefone = this.gameObject.transform.Find("Text Field - Telefone").gameObject;
		textFieldCPF = this.gameObject.transform.Find("Text Field - CPF").gameObject;
		// textFieldDepartamento = this.gameObject.transform.Find("Text Field - Departamento").gameObject;
		textFieldLocalizacao = this.gameObject.transform.Find("Text Field - Localização").gameObject;
		togglePrimeiro = this.gameObject.transform.Find("Toogle - Primeiro Login/Toggle").gameObject;
		togglePrimeiroLine = this.gameObject.transform.Find("Toogle - Primeiro Login").gameObject;
		textFieldRespostas = this.gameObject.transform.Find("Text Field - Gabarito Respostas").gameObject;
		textFieldTempo = this.gameObject.transform.Find("Text Field - Gabarito Tempo").gameObject;
		textFieldScore = this.gameObject.transform.Find("Text Field - Pontuação").gameObject;
		userOptions = this.gameObject.transform.Find("User Buttons").gameObject;
		userTGWindows = this.gameObject.transform.Find("User Buttons/TG Window").gameObject;
		buttonNew = this.gameObject.transform.Find("User Buttons/Button - New").gameObject;
		buttonDelete = this.gameObject.transform.Find("User Buttons/Button - Delete").gameObject;
		buttonAccept =  this.gameObject.transform.Find("User Buttons/Button - Accept").gameObject;
		buttonClear =  this.gameObject.transform.Find("User Buttons/Button - Clear").gameObject;
		userListWindow = this.gameObject.transform.Find("User List").gameObject;
		buttonQ = this.gameObject.transform.Find("Button - Q Window").gameObject;
		clientWindow = this.gameObject.transform.Find("Client Window").gameObject;
		//clientWindowReturn = this.gameObject.transform.Find("Client Window/Return").gameObject;
		buttonUploadUsers = this.gameObject.transform.Find("Button - Upload Users").gameObject;
		waitingWindow = this.gameObject.transform.Find("Waiting Window").gameObject;
		log = this.gameObject.transform.Find("Log").gameObject;
		textFieldIndex.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 + Screen.height/3 - 1 * Screen.height/15);
		textFieldIndex.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/15);
		textFieldName.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 + Screen.height/3 - 2 * Screen.height/15);
		textFieldName.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/15);
		textFieldEmail.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 + Screen.height/3 - 3 * Screen.height/15);
		textFieldEmail.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/15);
		// textFieldSenha.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 + Screen.height/3 - 3 * Screen.height/15);
		// textFieldSenha.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/15);
		textFieldTelefone.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 + Screen.height/3 - 4 * Screen.height/15);
		textFieldTelefone.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/15);
		textFieldCPF.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 + Screen.height/3 - 5 * Screen.height/15);
		textFieldCPF.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/15);
		// textFieldDepartamento.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 + Screen.height/3 - 6 * Screen.height/15);
		// textFieldDepartamento.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/15);
		textFieldLocalizacao.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 + Screen.height/3 - 6 * Screen.height/15);
		textFieldLocalizacao.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/15);
		togglePrimeiroLine.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 + Screen.height/3 - 7 * Screen.height/15);
		togglePrimeiroLine.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/15);
		textFieldRespostas.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 + Screen.height/3 - 8 * Screen.height/15);
		textFieldRespostas.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/15);
		textFieldTempo.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 + Screen.height/3 - 9 * Screen.height/15);
		textFieldTempo.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/15);
		textFieldScore.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 + Screen.height/3 - 10 * Screen.height/15);
		textFieldScore.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/15);
		// auxTextFieldQ.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/6 + Screen.width/30, Screen.height/2);
		// auxTextFieldQ.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3 - Screen.width/15, Screen.height/3);
		userTGWindows.transform.Find("Window").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 - Screen.height/3);
		userTGWindows.transform.Find("Window").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, 2 * Screen.height/3);
		userTGWindows.transform.Find("User Groups Scroll").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/2 - Screen.height/3);
		userTGWindows.transform.Find("User Groups Scroll").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6, 2 * Screen.height/3);
		userTGWindows.transform.Find("Subjects Scroll").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2, Screen.height/2 - Screen.height/3);
		userTGWindows.transform.Find("Subjects Scroll").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6, 2 * Screen.height/3);
		buttonAccept.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2, Screen.height/15);
		buttonAccept.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6, Screen.height/15);
		buttonClear.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.width/6, Screen.height/15);
		buttonClear.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6, Screen.height/15);
		buttonNew.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/18, Screen.height/2 + Screen.height/3);
		buttonNew.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/18, Screen.width/18);
		buttonDelete.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/9, Screen.height/2 + Screen.height/3);
		buttonDelete.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/18, Screen.width/18);
		userListWindow.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/30, Screen.height/15);
		userListWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3 - Screen.width/15, 11.5f * Screen.height/15);
		Vector2 xy = this.gameObject.transform.Find("User List/Scrollbar Vertical").GetComponent<RectTransform>().sizeDelta;
		this.gameObject.transform.Find("User List/Scrollbar Vertical").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/30, xy.y);
		buttonQ.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/30, Screen.height/2 + Screen.height/3);
		buttonQ.GetComponent<RectTransform>().sizeDelta = new Vector2((Screen.width/3 - Screen.width/15)/3, Screen.height/15);
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
		buttonUploadUsers.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 + Screen.width/6 + Screen.width/30, Screen.height/15);
		buttonUploadUsers.GetComponent<RectTransform>().sizeDelta = new Vector2((Screen.width/3 - Screen.width/15), Screen.height/15);
		waitingWindow.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.height/9, Screen.height/2 - Screen.height/9);
		waitingWindow.transform.Find("Window").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height/4.5f, Screen.height/4.5f);
		waitingWindow.SetActive(false);
		log.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.height/60, Screen.height/60);
		log.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - Screen.height/30, Screen.height/30);

		//Data
		userList = new List<UserButton>();
		button = Resources.Load("Prefab/Button User Prefab") as GameObject;
		inputField = Resources.Load("Prefab/InputField Prefab") as GameObject;
		checkbox = Resources.Load("Prefab/Checkbox Prefab") as GameObject;
		//userGroup = new List<string>();
		
		//Editor
		loadedQuestionFile = false;
		userEditorOpen = true;
		userTGWindowsOpen = false;
		clientEditorOpen = false;
		filterWindowOpen = false;
		userTGWindows.SetActive(false);
		clientWindow.SetActive(false);
		userGroup = new List<UserGroup>();
		
		//LoadFile();
		ClearUser();
		
		// Set-up dynamic interface
		GetUsersAndSubjects();
		OrderListOfUsers();
		OrderUserGroupEditor();
		
		// Firebase & Database
		DatabaseScript.userEditor = this;
		databaseScript = GameObject.Find("Firebase").GetComponent<DatabaseScript>();
		databaseScript.enabled = true;
		DatabaseScript.GetUsersReference();
		DatabaseScript.GetLocalUserList();
		
		// First User
		if (userList.Count > 0) ChooseUser(0);
		
		
		Log("Welcome to User DataBase Editor");
		
		// Miscellaneous
		nextScene = "";
		changesToUpload = false;
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab)){
			currentInputField = currentInputField + 1;
			if (currentInputField > 9){ currentInputField = 1;}
			switch (currentInputField){
				default:
					textFieldIndex.GetComponent<InputField>().ActivateInputField();
					break;
				case 1:
					textFieldScore.GetComponent<InputField>().DeactivateInputField();
					textFieldIndex.GetComponent<InputField>().ActivateInputField();
					break;
				case 2:	
					textFieldIndex.GetComponent<InputField>().DeactivateInputField();
					textFieldName.GetComponent<InputField>().ActivateInputField();
					break;
				case 3:	
					textFieldName.GetComponent<InputField>().DeactivateInputField();
					textFieldEmail.GetComponent<InputField>().ActivateInputField();
					break;
				case 4:	
					textFieldEmail.GetComponent<InputField>().DeactivateInputField();
					textFieldTelefone.GetComponent<InputField>().DeactivateInputField();
					// textFieldSenha.GetComponent<InputField>().ActivateInputField();
					break;
				// case 5:	
					// textFieldSenha.GetComponent<InputField>().DeactivateInputField();
					// textFieldTelefone.GetComponent<InputField>().ActivateInputField();
					// break;
				case 5:	
					textFieldTelefone.GetComponent<InputField>().DeactivateInputField();
					textFieldCPF.GetComponent<InputField>().ActivateInputField();
					break;
				case 6:	
					textFieldCPF.GetComponent<InputField>().DeactivateInputField();
					textFieldLocalizacao.GetComponent<InputField>().ActivateInputField();
					// textFieldDepartamento.GetComponent<InputField>().ActivateInputField();
					break;
				// case 8:	
					// textFieldDepartamento.GetComponent<InputField>().DeactivateInputField();
					// textFieldLocalizacao.GetComponent<InputField>().ActivateInputField();
					// break;
				case 7:	
					textFieldLocalizacao.GetComponent<InputField>().DeactivateInputField();
					textFieldRespostas.GetComponent<InputField>().ActivateInputField();
					break;
				case 8:	
					textFieldRespostas.GetComponent<InputField>().DeactivateInputField();
					textFieldTempo.GetComponent<InputField>().ActivateInputField();
					break;
				case 9:	
					textFieldTempo.GetComponent<InputField>().DeactivateInputField();
					textFieldScore.GetComponent<InputField>().ActivateInputField();
					break;
			}
		}
		if (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)){
			if (Input.GetKeyDown(KeyCode.N)){
				NewUser();
				return;
			}
			if (Input.GetKeyDown(KeyCode.S)){
				AcceptUser();
				return;
			}
			if (Input.GetKeyDown(KeyCode.Z)){
				ClearUser();
				return;
			}
		}
	}
	
	public void SelectInputField (string field){
		textFieldIndex.GetComponent<InputField>().DeactivateInputField();
		textFieldName.GetComponent<InputField>().DeactivateInputField();
		textFieldEmail.GetComponent<InputField>().DeactivateInputField();
		// textFieldSenha.GetComponent<InputField>().DeactivateInputField();
		textFieldTelefone.GetComponent<InputField>().DeactivateInputField();
		textFieldCPF.GetComponent<InputField>().DeactivateInputField();
		// textFieldDepartamento.GetComponent<InputField>().DeactivateInputField();
		textFieldLocalizacao.GetComponent<InputField>().DeactivateInputField();
		textFieldRespostas.GetComponent<InputField>().DeactivateInputField();
		textFieldTempo.GetComponent<InputField>().DeactivateInputField();
		if (field == "Text Field - Index"){
			currentInputField = 1;
		}
		if (field == "Text Field - Nome"){
			currentInputField = 2;
		}
		if (field == "Text Field - Email"){
			currentInputField = 3;
		}
		// if (field == "Text Field - Senha"){
			// currentInputField = 4;
		// }
		if (field == "Text Field - Telefone"){
			currentInputField = 4;
		}
		if (field == "Text Field - CPF"){
			currentInputField = 5;
		}
		// if (field == "Text Field - Departamento"){
			// currentInputField = 7;
		// }
		if (field == "Text Field - Localização"){
			currentInputField = 6;
		}
		if (field == "Text Field - Primeiro Login"){
			currentInputField = 7;
		}
		if (field == "Text Field - Gabarito Resposta"){
			currentInputField = 8;
		}
		if (field == "Text Field - Gabarito Tempo"){
			currentInputField = 9;
		}
	}
	
	public void ClearInputFieldIndex(){
		currentInputField = 0;
	}
	
	void GetUsersAndSubjects(){
		// string subjectString = DatabaseScript.GetSubjectsFromFirebase();
		string usersString = DatabaseScript.GetUsersFromFirebase();
		// if (subjectString != "X"){
			// string[] userSubjects;
			// string[] space = new string[] {" "};
			// userSubjects = subjectString.Split(space, StringSplitOptions.None);
			// for (int i = 0; i < userSubjects.Length; i++){
				// LoadSubject();;
				// subject[i].name = userSubjects[i];
			// }
		// }
		if (usersString != "X"){
			string[] userUsers;
			string[] space = new string[] {" "};
			userUsers = usersString.Split(space, StringSplitOptions.None);
			for (int i = 0; i < userUsers.Length; i++){
				LoadUserGroup();
				userGroup[i].name = userUsers[i];
			}
		}
	}
	
	public void AcceptUser (){
		ClearInputFieldIndex();
		string a = textFieldIndex.GetComponent<InputField>().text;
		string b = textFieldName.GetComponent<InputField>().text;
		string c = textFieldEmail.GetComponent<InputField>().text;
		// string d = textFieldSenha.GetComponent<InputField>().text;
		string e = textFieldTelefone.GetComponent<InputField>().text;
		string f = textFieldCPF.GetComponent<InputField>().text;
		// string g = textFieldDepartamento.GetComponent<InputField>().text;
		string h = textFieldLocalizacao.GetComponent<InputField>().text;
		bool i = togglePrimeiro.GetComponent<Toggle>().isOn;
		string j = textFieldRespostas.GetComponent<InputField>().text;
		string k = textFieldTempo.GetComponent<InputField>().text;
		string l = textFieldScore.GetComponent<InputField>().text;	
		if (a == "" && b == "" && c == "" /*&& d == "" */&& e == "" && f == "" /*&& g == "" */&& h == "" && j == "" && k == "" && l == ""){
			Log("User is empty!");
			return;
		}
		if (a == "" || b == "" || c == "" || /*d == "" ||*/ e == "" || f == "" || /*g == "" ||*/ h == "" || j == "" || k == "" || l == ""){
			Log("user is lacking fields!");
			return;
		}
		int aInt = int.Parse(a);
		for (int y = 0; y < userList.Count; y++){
			if (aInt == userList[y].user.index){
				Log ("this user index already exists!");
				// audio
				return;
			}
		}
		changesToUpload = true;
		int lInt = int.Parse(l);
		Log("Saved user #" + currentUserIndex.ToString());
		List <bool> userGroupSelected = NewUserGroupSelectedInQuestion ();
		if (newUser){
			AddUser(new User(aInt, b, c, /*d,*/ e, f, /*g,*/ h, i, j, k, lInt, userGroupSelected, true));
		}
		if (!newUser){
			userList[currentUserIndex].user = new User(aInt, b, c, /*d,*/ e, f, /*g,*/ h, i, j, k, lInt, userGroupSelected);
			userList[currentUserIndex].user.edited = true;
			userList[currentUserIndex].button.gameObject.transform.Find("Text").GetComponent<Text>().text = "Usuário(a) " + currentUserIndex.ToString()  + ": " + userList[currentUserIndex].user.name;
		}
		NextUser();
		OrderListOfUsers();
	}
	
	public void UploadUsers(){
		print ("UploadUsers()");
		if (!changesToUpload){
			Log("There is no new information to be uploaded!");
			return;
		}
		for (int i = 0; i < userList.Count; i++){
			print ("i = " + i);
			if (i + 1 == userList.Count){
				DatabaseScript.getNewSnapshot = true;
			}
			if (userList[i].user.edited){
				print ("upload: " + i);
				DatabaseScript.UploadUser(
					DatabaseScript.currentClient,
					userList[i].user.index,
					userList[i].user.name,
					userList[i].user.email,
					// userList[i].user.password,
					userList[i].user.telephone,
					userList[i].user.cpf,
					// userList[i].user.departament,
					userList[i].user.localization,
					userList[i].user.firstLogin,
					userList[i].user.answers,
					userList[i].user.time,
					userList[i].user.score,
					userList[i].user.GetUserGroupString());
					
					userList[i].user.edited = false;
			}
		}
		Log("Users uploaded");
		DatabaseScript.RemoveSpecifiedQuestions();
		changesToUpload = false;
		FinishScene();
	}
	
	public List <bool> NewUserGroupSelectedInQuestion (){
		List <bool> newList = new List <bool>();
		for (int i = 0; i < userGroup.Count; i++){
			newList.Add(userGroup[i].selectedInQuestion);
		}
		return newList;
	}
	
	public void ClearUser(){
		if (newUser){
			textFieldIndex.GetComponent<InputField>().text = "";
			textFieldName.GetComponent<InputField>().text = "";
			textFieldEmail.GetComponent<InputField>().text = "";
			// textFieldSenha.GetComponent<InputField>().text = "";
			textFieldTelefone.GetComponent<InputField>().text = "";
			textFieldCPF.GetComponent<InputField>().text = "";
			// textFieldDepartamento.GetComponent<InputField>().text = "";
			textFieldLocalizacao.GetComponent<InputField>().text = "";
			togglePrimeiro.GetComponent<Toggle>().isOn = true;
			textFieldRespostas.GetComponent<InputField>().text = "";
			textFieldTempo.GetComponent<InputField>().text = "";
			textFieldScore.GetComponent<InputField>().text = "";
		}
		if (!newUser){
			textFieldIndex.GetComponent<InputField>().text = userList[currentUserIndex].user.index.ToString();
			textFieldName.GetComponent<InputField>().text = userList[currentUserIndex].user.name;
			textFieldEmail.GetComponent<InputField>().text = userList[currentUserIndex].user.email;
			// textFieldSenha.GetComponent<InputField>().text = userList[currentUserIndex].user.password;
			textFieldTelefone.GetComponent<InputField>().text = userList[currentUserIndex].user.telephone;
			textFieldCPF.GetComponent<InputField>().text = userList[currentUserIndex].user.cpf;
			// textFieldDepartamento.GetComponent<InputField>().text = userList[currentUserIndex].user.departament;
			textFieldLocalizacao.GetComponent<InputField>().text = userList[currentUserIndex].user.localization;
			togglePrimeiro.GetComponent<Toggle>().isOn = userList[currentUserIndex].user.firstLogin;
			textFieldRespostas.GetComponent<InputField>().text = userList[currentUserIndex].user.answers;
			textFieldTempo.GetComponent<InputField>().text = userList[currentUserIndex].user.time;
			textFieldScore.GetComponent<InputField>().text = userList[currentUserIndex].user.score.ToString();
			userList[currentUserIndex].user.edited = false;
		}
	}
	
	public void AddUser(User newUser){
		print ("AddUser " + newUser);
		GameObject newButton = Instantiate(button) as GameObject;
        newButton.transform.SetParent(this.gameObject.transform.Find("User List/Viewport/Content"), false);
		newButton.name = currentUserIndex.ToString();
		userList.Add(new UserButton(newUser, newButton));
		currentUserIndex = GetNextUserListIndex();
		newButton.GetComponent<Button>().onClick.AddListener(delegate{ChooseUser(newUser.index);});
		OrderListOfUsers();
	}
	
	public void OrderListOfUsers(){
		for (int i = 0; i < userList.Count; i ++){
			string userTypeString = "";
			userList[i].button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * Screen.height/15);
			userList[i].button.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3 - Screen.width/15 - Screen.width/30, Screen.height/15);
			userList[i].button.gameObject.transform.Find("Text").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/75, 0);
			userList[i].button.gameObject.transform.Find("Text").GetComponent<RectTransform>().sizeDelta = new Vector2(- Screen.width/50, 0);
			userList[i].button.gameObject.transform.Find("Text").GetComponent<Text>().text = "Usuário(a) " + userTypeString + " " + userList[i].user.index.ToString() + ": " + userList[i].user.name;
			this.gameObject.transform.Find("User List/Viewport/Content").GetComponent<RectTransform>().sizeDelta = new Vector2(0, (1 + i) * Screen.height/15);
		}
	}
	
	public void ChooseUser(int index){
		print ("ChooseUser " + index);
		ClearInputFieldIndex();
		newUser = false;
		int newIndex = GetUserListIndex(index);
		currentUserIndex = newIndex;
		textFieldIndex.GetComponent<InputField>().text = userList[newIndex].user.index.ToString();
		textFieldName.GetComponent<InputField>().text = userList[newIndex].user.name;
		textFieldEmail.GetComponent<InputField>().text = userList[newIndex].user.email;
		// textFieldSenha.GetComponent<InputField>().text = userList[newIndex].user.password;
		textFieldTelefone.GetComponent<InputField>().text = userList[newIndex].user.telephone;
		textFieldCPF.GetComponent<InputField>().text = userList[newIndex].user.cpf;
		// textFieldDepartamento.GetComponent<InputField>().text = userList[newIndex].user.departament;
		textFieldLocalizacao.GetComponent<InputField>().text = userList[newIndex].user.localization;
		togglePrimeiro.GetComponent<Toggle>().isOn = userList[newIndex].user.firstLogin;
		textFieldRespostas.GetComponent<InputField>().text = userList[newIndex].user.answers;
		textFieldTempo.GetComponent<InputField>().text = userList[newIndex].user.time;
		textFieldScore.GetComponent<InputField>().text = userList[newIndex].user.score.ToString();
		if (userGroup.Count > 0){
			for (int i = 0; i < userGroup.Count; i++){
				userGroup[i].button.GetComponent<Toggle>().isOn = userList[newIndex].user.userGroups[i];
			}
		}
		Log("Opened user #" + currentUserIndex.ToString());
	}
	
	public void NewUser(){
		if (newUser){
			return;
		}
		if (!newUser){
			NextUser();
		}
	}
	
	public void NextUser(){
		ClearInputFieldIndex();
		currentUserIndex = GetNextUserListIndex();
		newUser = true;
		ClearUser();
	}
	
	public void DeleteUser(){
		ClearInputFieldIndex();
		if (newUser){
			return;
		}
		changesToUpload = true;
		Destroy(userList[currentUserIndex].button);
		userList.Remove(userList[currentUserIndex]);
		for (int i = currentUserIndex; i < userList.Count; i++){
			userList[i].user.index = i;
			userList[i].user.edited = true;
		}
		DatabaseScript.FindAndDeleteUser(currentUserIndex);
		Log("Deleted user #" + currentUserIndex.ToString());
		OrderListOfUsers();
		NewUser();
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
		GameObject newUserButton = Instantiate(checkbox) as GameObject;
		newUserButton.transform.SetParent(this.gameObject.transform.Find("User Buttons/TG Window/User Groups Scroll/Viewport/Content"), false);
		newUserButton.GetComponent<Toggle>().onValueChanged.AddListener(delegate{SelectUserGroupInQuestion(nextIndex);});
		userGroup.Add(new UserGroup(newName, newInput, newFilterButton, newUserButton));
		for (int i = 0; i < userList.Count; i++){
			userList[i].user.userGroups.Add(false);
		}
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
			for (int i = 0; i < userList.Count; i++){
				userList[i].user.userGroups.RemoveAt(index);
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
			RectTransform rectQuestion = userGroup[i].button.GetComponent<RectTransform>();
			userGroup[i].button.transform.Find("Label").GetComponent<Text>().text = userGroup[i].name;
			rectQuestion.sizeDelta = new Vector2( Screen.width/6, Screen.height/15);
			rectQuestion.anchoredPosition = new Vector2(0, - Screen.height/15 * (i));
			userTGWindows.transform.Find("User Groups Scroll/Viewport/Content").GetComponent<RectTransform>().sizeDelta = new Vector2(0, (1 + i) * Screen.height/15);
		}
	}
	
	public void SelectUserGroupInQuestion (int selected){
		userGroup[selected].ToggleInQuestionWindow();
		if (newUser){
			return;
		}
		if (!newUser){
			userList[currentUserIndex].user.userGroups[selected] = userGroup[selected].selectedInQuestion;
		}
	}
	
	public void SelectUserGroupFilter(int filter){
		userGroup[filter].selectedFilter = !userGroup[filter].selectedFilter;
		// ADD FILTER HERE!!
	}
	
	public void ClearClientePreferences(){
		if (userGroup.Count > 0){
			for (int i = 0; i < userList.Count; i++){
				userList[i].user.userGroups = new List<bool>();
			}
			for (int i = 0; i < userGroup.Count; i++){
			RemoveUserGroup();
			}
		}
	}
	
	int GetUserListIndex(int index){
		int newIndex = 0;
		for (int i = 0; i < userList.Count; i++){
			if (userList[i].user.index == index){
				newIndex = i;
			}
		}
		return newIndex;
	}
	
	int GetNextUserListIndex(){
		int newIndex = 0;
		if (userList.Count == 1){
			if (userList[0].user.index > 0) newIndex = 0;
			if (userList[0].user.index == 0) newIndex = 1;
		}
		if (userList.Count > 1){
			bool done = false;
			int checkInt = 0;
			List<int> checkedInt = new List<int>();
			int t = 0;						// Infinite loop failsafe
			do{
				for (int i = 0; i < userList.Count; i++){
					if (userList[i].user.index > i){
						if (checkedInt.Count == 0){
							checkInt = i;
							checkedInt.Add(i);
							break;
						} else {	// checkedInt.Count > 0
							bool newIntToCheck = true;
							for (int y = 0 ; y < checkedInt.Count; y++){
								if (checkedInt[y] == i){
									newIntToCheck = false;
								}
							}
							if (newIntToCheck){
								checkInt = i;
								checkedInt.Add(i);
								break;
							}
						}
					}
					if (i == userList.Count - 1 && userList[i].user.index <= i){
						checkInt = i + 1;
						checkedInt.Add(i + 1);
					}
				}
				done = true;
				for (int i = 0; i < userList.Count; i++){
					if (userList[i].user.index == checkInt){
						done = false;
						break;
					}
				}
				if (done) newIndex = checkInt;
				t = t + 1;					// Infinite loop failsafe
				if (t > 250) done = true;	// Infinite loop failsafe
			} while (!done);
		}
		print ("GetNextUserListIndex: " + newIndex);
		return newIndex;
	}
	
	
	
	void Log (string newString){
		log.GetComponent<InputField>().text = DateTime.Now + " : " + newString;
	}
	
	void FinishScene(){
		print ("Finish Scene");
		waitingWindow.SetActive(true);
		databaseScript.StartListener();
		nextScene = "DatabaseSelection";
		Invoke("NextScene", 0.75f);
	}
	
	public void NextScene(){
		SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
	}
	
	public void Restart(){
		SceneManager.LoadScene("BncQ", LoadSceneMode.Single);
	}
	
	public void Quit (){
		Log ("Quit!");
		Application.Quit();
	}
	
	
	
	// Deprecated
	
	public void LoadUserGroup(){	// Same as Add, but does not expand the "userList[i].user.userGroups" list
		string newName = "grupo " + userGroup.Count.ToString();
		int nextIndex = userGroup.Count;
		GameObject newInput = Instantiate(inputField) as GameObject;
		newInput.transform.SetParent(this.gameObject.transform.Find("Client Window/User Groups Scroll/Viewport/Content"), false);
		newInput.transform.Find("Placeholder").GetComponent<Text>().text = "grupo " +  userGroup.Count.ToString();
		newInput.GetComponent<InputField>().onEndEdit.AddListener(delegate{SetUserGroup();});
		GameObject newFilterButton = Instantiate(checkbox) as GameObject;
		newFilterButton.transform.SetParent(this.gameObject.transform.Find("Filter Window/User Groups Scroll/Viewport/Content"), false);
		newFilterButton.GetComponent<Toggle>().onValueChanged.AddListener(delegate{SelectUserGroupFilter(nextIndex);});
		GameObject newUserButton = Instantiate(checkbox) as GameObject;
		newUserButton.transform.SetParent(this.gameObject.transform.Find("User Buttons/TG Window/User Groups Scroll/Viewport/Content"), false);
		newUserButton.GetComponent<Toggle>().onValueChanged.AddListener(delegate{SelectUserGroupInQuestion(nextIndex);});
		userGroup.Add(new UserGroup(newName, newInput, newFilterButton, newUserButton));
		OrderUserGroupEditor();
		clientWindow.transform.Find("User Groups Set").GetComponent<InputField>().text = userGroup.Count.ToString();
	}
	
	public void SetClientName(string name){
		client = name;
		fileName = "BncQ_" + client + ".dat";
	}
	
	public void DeleteFile (){
		ClearInputFieldIndex();
		if (File.Exists(Application.persistentDataPath + "/" + fileName)){
			Log ("Deleted BncQ file");
			File.Delete (Application.persistentDataPath + "/" + fileName);
		}
		else Log("There was no BncQ file to be deleted");
	}
	
	public void DeleteAllClientePreferencesAndSaveAndRestart(){
		userGroup = new List<UserGroup>();
		for (int i = 0; i < userList.Count; i++){
			userList[i].user.userGroups = new List <bool>();
		}
		Restart();
	}
}
