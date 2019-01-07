using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class DatabaseSelectionScript : MonoBehaviour {

	//UI Elements
	GameObject selectionWindow;
	GameObject infoTextBox;	// remove?
	GameObject createButton;
	GameObject deleteButton;
	GameObject customMaintenanceButton;
	GameObject selectQuestionsButton;
	GameObject selectUsersButton;
	GameObject waitingWindow;
	GameObject exitButton;
	bool selectionButtonsVisible;
	GameObject buttonPrefab;
	GameObject setNameWindow;
	InputField setNameWindowInput;
	GameObject log;
	
	
	// Database Entry Selection
	string selectedEntry;		//path: client_ + selectedEntryIndex
	int selectedEntryIndex;
	List <DatabaseEntryButton> databaseEntryButtonList;
	string newEntryCreated;
	
	// Authentication variables
	
	
	// Miscellaneous
	public static bool refereshList;
	string nextScene;
	
	void Start () {
		//UI Elements
		this.gameObject.transform.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);	// Main background
		selectionWindow = this.gameObject.transform.Find("Database List Window").gameObject;
		selectionWindow.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2f - Screen.width/6f + Screen.width/30f, Screen.height/3f);
		selectionWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3f - Screen.width/15f, Screen.height/3f);
		infoTextBox = this.gameObject.transform.Find("Info Text Box").gameObject;
		infoTextBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2f - Screen.width/6f + Screen.width/30f, 2 * Screen.height/3f);
		infoTextBox.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3f - Screen.width/15f, Screen.height/9f);
		createButton = this.gameObject.transform.Find("Create Database").gameObject;
		createButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2f - Screen.width/6f + Screen.width/30f, Screen.height/3f - Screen.height/9f);
		createButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3f - Screen.width/15f, Screen.height/9f);
		deleteButton = this.gameObject.transform.Find("Delete Database").gameObject;
		deleteButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2f + Screen.width/6f + Screen.width/20f, Screen.height/3f + Screen.height/18f);
		deleteButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6f, Screen.height/9f);
		selectQuestionsButton = this.gameObject.transform.Find("Select BncQ").gameObject;
		selectQuestionsButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2f + Screen.width/6f + Screen.width/20f, Screen.height/3f + Screen.height/9f + Screen.height/18f);
		selectQuestionsButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6f, Screen.height/9f);
		selectUsersButton = this.gameObject.transform.Find("Select Users").gameObject;
		selectUsersButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2f + Screen.width/6f + Screen.width/20f, Screen.height/3f + Screen.height/4.5f + Screen.height/18f);
		selectUsersButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6f, Screen.height/9f);
		selectionButtonsVisible = false;
		waitingWindow = this.gameObject.transform.Find("Waiting Window").gameObject;
		waitingWindow.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.height/9, Screen.height/2 - Screen.height/9);
		waitingWindow.transform.Find("Window").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height/4.5f, Screen.height/4.5f);
		waitingWindow.SetActive(false);
		setNameWindow = this.gameObject.transform.Find("Set Name Window").gameObject;
		setNameWindow.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2f, Screen.height/2f);
		setNameWindow.transform.Find("Window").GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, - Screen.height/12f);
		setNameWindow.transform.Find("Window").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/2f, Screen.height/3f);
		setNameWindow.transform.Find("Text").GetComponent<RectTransform>().anchoredPosition = new Vector2(- Screen.width/9f, 0f);
		setNameWindow.transform.Find("Text").GetComponent<RectTransform>().sizeDelta = new Vector2(2 * Screen.width/9f, 2 * Screen.height/12f);
		setNameWindow.transform.Find("Input Field").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/9f, 0f);
		setNameWindow.transform.Find("Input Field").GetComponent<RectTransform>().sizeDelta = new Vector2(2 * Screen.width/9f, Screen.height/12f);
		setNameWindow.transform.Find("Confirm Create Database").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/9f, - Screen.height/6f);
		setNameWindow.transform.Find("Confirm Create Database").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6f, Screen.height/9f);
		setNameWindow.transform.Find("Cancel Create Database").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/18f - Screen.width/6f, - Screen.height/6f);
		setNameWindow.transform.Find("Cancel Create Database").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6f, Screen.height/9f);
		setNameWindow.transform.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
		setNameWindowInput = setNameWindow.transform.Find("Input Field").GetComponent<InputField>();
		//
		log = this.gameObject.transform.Find("Log").gameObject;
		log.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.height/60, Screen.height/60);
		log.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - Screen.height/30, Screen.height/30);
		
		// Activate/Deactivate
		createButton.SetActive(true);
		deleteButton.SetActive(false);
		selectQuestionsButton.SetActive(false);
		selectUsersButton.SetActive(false);
		
		buttonPrefab = Resources.Load("Prefab/Database Button Prefab") as GameObject;
		GetListOfEntries();
		
		// Database Entry Selection
		selectedEntry = "";
		selectedEntryIndex = -1;
		newEntryCreated = "";
		
		// Welcome
		Log("Welcome, " + AuthenticationScript.userName);
		
		// Miscellaneous
		 DatabaseScript.thisScene = SceneManager.GetActiveScene().name;
		 refereshList = false;	
	}
	
	void Update () {
		
	}
	
	public void GetListOfEntries(){
		List <DatabaseEntry> databaseEntriesList = DatabaseScript.GetAllClients();
		databaseEntryButtonList = new List <DatabaseEntryButton>();
		for (int i = 0; i < databaseEntriesList.Count; i++){
			GameObject newButton = Instantiate(buttonPrefab) as GameObject;
			newButton.transform.SetParent(this.gameObject.transform.Find("Database List Window/Viewport/Content"), false);
			databaseEntryButtonList.Add(new DatabaseEntryButton(databaseEntriesList[i], newButton));
			string entryString = databaseEntriesList[i].entry;
			int entryIndex = databaseEntriesList[i].index;
			newButton.GetComponent<Button>().onClick.AddListener(delegate{ChooseDatabaseEntry(entryString, entryIndex);});
		}
		OrderListOfEntries();
	}
	
	public void OrderListOfEntries(){
		for (int i = 0; i < databaseEntryButtonList.Count; i ++){
			databaseEntryButtonList[i].button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * Screen.height/15);
			databaseEntryButtonList[i].button.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3 - Screen.width/15 - Screen.width/60, Screen.height/15);
			databaseEntryButtonList[i].button.gameObject.transform.Find("Text").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/75, 0);
			databaseEntryButtonList[i].button.gameObject.transform.Find("Text").GetComponent<RectTransform>().sizeDelta = new Vector2(- Screen.width/50, 0);
			databaseEntryButtonList[i].button.gameObject.transform.Find("Text").GetComponent<Text>().text = databaseEntryButtonList[i].entry.entry;
			this.gameObject.transform.Find("Database List Window/Viewport/Content").GetComponent<RectTransform>().sizeDelta = new Vector2(0, (1 + i) * Screen.height/15);
		}
	}
	
	public void RefreshListOfEntries(){
		// Called by DatabaseScript in a OnValueChanged listener
		for (int i = 0; i < databaseEntryButtonList.Count; i++){
			Destroy(databaseEntryButtonList[i].button);
		}
		CancelCreateEntry();
		ClearDatabaseEntrySelection();
		GetListOfEntries();
	}
	
	public void ChooseDatabaseEntry(string entry, int index){
		selectedEntry = entry;
		selectedEntryIndex = index;
		deleteButton.SetActive(true);
		selectQuestionsButton.SetActive(true);
		selectUsersButton.SetActive(true);
		selectionButtonsVisible = true;
		print ("ChooseDatabaseEntry: " +  entry + "(index: " + index + ")");
	}
	
	public void CreateEntryButton(){
		setNameWindow.SetActive(true);
		ClearDatabaseEntrySelection();
		print ("CreatEntry()");
	}
	
	public void SetNewEntryName(string newEntry){
		newEntryCreated = newEntry;
	}
	
	public void ConfirmCreateEntry(){
		if (newEntryCreated == ""){
			return;
		}
		setNameWindow.SetActive(false);
		DatabaseScript.getNewSnapshot = true;
		refereshList = true;
		DatabaseScript.AddClient(newEntryCreated);
		print ("ConfirmCreateEntry()");
	}
	
	public void CancelCreateEntry(){
		setNameWindow.SetActive(false);
		newEntryCreated = "";
		print ("CancelCreateEntry()");
	}
	
	public void SelectQuestions(){
		// sound
		waitingWindow.SetActive(true);
		DatabaseScript.currentClient = selectedEntryIndex;
		print ("currentClient = " + DatabaseScript.currentClient);
		nextScene = "BncQ";
		Invoke("NextScene", 1f);
	}
	
	public void SelectUsers(){
		// sound
		waitingWindow.SetActive(true);
		DatabaseScript.currentClient = selectedEntryIndex;
		DatabaseScript.currentClientName = 
		nextScene = "Users";
		Invoke("NextScene", 1f);
	}
	
	public void ClearDatabaseEntrySelection(){
		if (!selectionButtonsVisible) return;
		selectedEntry = "";
		selectedEntryIndex = -1;
		deleteButton.SetActive(false);
		selectQuestionsButton.SetActive(false);
		selectUsersButton.SetActive(false);
		selectionButtonsVisible = false;
		print ("ClearDatabaseEntrySelection()");
	}
	
	public void NextScene(){
		SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
	}
	
	public void Quit (){
		Application.Quit();
	}
	
	void Log (string newString){
		log.GetComponent<InputField>().text = DateTime.Now + " : " + newString;
	}
}

public class DatabaseEntryButton {
	
	public DatabaseEntry entry;	// entry.entry: name; entry.index: path index
	public GameObject button;
	
	public DatabaseEntryButton (DatabaseEntry newEntry, GameObject newButton){
		entry = newEntry;
		button = newButton;
	}
}

public class DatabaseEntry {
	
	public string entry;	// name
	public int index;		// path index
	
	public DatabaseEntry (string newEntry, int newIndex){
		entry = newEntry;
		index = newIndex;
	}
}