using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseScript : MonoBehaviour {
	
	public static DatabaseScript database = null;
	public static TextEditorClass bncQ;
	
	public DatabaseReference dbTest1;
	public DatabaseReference dbTest2;
	//public ValueEventListener dbvel1;
	
	
	string testString = "";
	string testString2 = "";
	
	float timer = 0;
	bool setValues = false;
	
	public static string thisScene;
	
	// Database variables
	public static DatabaseReference dbRoot;
	public static bool getNewSnapshot;
	public static bool getNewSnapshotAndRefreshQuestions;
	public static bool getNewSnapshotAndRefreshUsers;
	
	// Temporary Local Variables
	public static int currentClient;
	public static string currentClientName;
	public static DatabaseReference dbQuestions;
	public static DatabaseReference dbUsers;
	public static List<LocalDatabaseEntry> questionListLocal;
	public static List<LocalDatabaseEntry> usersListLocal;
	
	// Chat
	static DatabaseReference dbChat;
	
	// TEST
	int dice1;
	int dice2;
	public static DataSnapshot dbSnapshot;
	
	void Awake(){
        if (database == null){
            database = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            if (database != this){
                Destroy(this);
            }
        }
    }	
	
	void Start(){
		dice1 = UnityEngine.Random.Range(0, 5);
		if (dice1 == 0){ testString = "These are not the droids we are looking for."; testString2 = "Star Wars";}
		if (dice1 == 1){ testString = "Say 'hello' to my little friend!"; testString2 = "Scarface";}
		if (dice1 == 2){ testString = "I will strike down upon thee with great vengeance and furious anger!"; testString2 = "Pulp Fiction";}
		if (dice1 == 3){ testString = "Get to the chopper!"; testString2 = "Predator";}
		if (dice1 == 4){ testString = "Quiet an experience to live in fear, isn't it? That's what it is to be a slave."; testString2 = "Blade Runner";}
		print ("DatabaseScript: " + testString);
		
		// dbRoot = FirebaseDatabase.DefaultInstance.GetReference("");	// set by timer in Update
		// dbTest1 = dbRoot.Child("test1");
		// dbTest2 = dbRoot.Child("test2");
		
		// Add event listeners to references
		
		// dbTest1.SetValueAsync(testString);	// set by timer in Update
		// dbTest2.SetValueAsync(testString2);
		
		// Use event listeners to retrieve data from firebase
		Invoke("SetRerefences", 0.5f);
		Invoke("StartListener", 0.75f);
		// Invoke("StartListenerChat", 0.75f);
		// Invoke("TestDatabase", 1.5f);
		
	}
	
	void Update(){
	}
	
	void SetRerefences(){
		dbRoot = FirebaseDatabase.DefaultInstance.GetReference("");
		GetQuestionsReference();
		
		// Test
		dbTest1 = dbRoot.Child("test1");
		dbTest2 = dbRoot.Child("test2");
		
		// Test Chat
		dbChat = dbRoot.Child("Chat");
	}
	
	public static void GetQuestionsReference(){
		DatabaseReference dbQuestions = dbRoot.Child("client_" + currentClient.ToString() + "/questions");
	}
	
	public static void GetUsersReference(){
		DatabaseReference dbUsers = dbRoot.Child("client_" + currentClient.ToString() + "/userss");
	}
	
	// Test	//
		// Test	//
			// Test	//
	void TestDatabase(){
		dice2 = UnityEngine.Random.Range(0,9);
		dbRoot.Child("movie0" + dice2.ToString() + "/phrase").SetValueAsync(testString);
		dbRoot.Child("movie0" + dice2.ToString() + "/title").SetValueAsync(testString2);
		dbTest1.SetValueAsync(testString);
		dbTest2.SetValueAsync(testString2);
		dbTest1.SetValueAsync(testString);
		dbTest2.SetValueAsync(testString2);		
		//
		if (dbSnapshot.HasChild("test1")) print ("dbSnapshot.HasChild('test1')"); else print ("!dbSnapshot.HasChild('test1')");
		if (dbSnapshot.HasChild("test2")) print ("dbSnapshot.HasChild('test2')"); else print ("!dbSnapshot.HasChild('test2')");
		if (dbSnapshot.HasChild("test3")) print ("dbSnapshot.HasChild('test3')"); else print ("!dbSnapshot.HasChild('test3')");
	}
			// Test	//
		// Test	//
	// Test	//

	
	protected void StartListener() {
		print ("StartListener()");
		dbRoot.ValueChanged += (object sender, ValueChangedEventArgs e2) => {
			if (e2.DatabaseError != null) {
				print ("ERROR");
				Debug.LogError(e2.DatabaseError.Message);
				return;
			}
			if (e2.Snapshot != null || e2.Snapshot.Value != null){
				if (dbSnapshot == null || getNewSnapshot){
					dbSnapshot = e2.Snapshot;
					getNewSnapshot = false;
					//
					if (thisScene == "DatabaseSelection"){
						if (DatabaseSelectionScript.refereshList){
							GameObject.Find("Canvas").GetComponent<DatabaseSelectionScript>().RefreshListOfEntries();
							DatabaseSelectionScript.refereshList = false;
						}
					}
					if (thisScene == "BncQ"){
						GetLocalQuestionList();
					}
					print ("StartListener() new data snapshot");
				} else print ("StartListener() data snapshot already exists");
				string newString = (string) e2.Snapshot.Value;	// REMOVE LATER
			}
		};
	}
	
	// Chat test //
		// Chat test //
			// Chat test //
	void StartListenerChat(){
		dbChat.ValueChanged += (object sender, ValueChangedEventArgs e2) => {
			if (e2.DatabaseError != null) {
				print ("ERROR");
				Debug.LogError(e2.DatabaseError.Message);
				return;
			}
			if (e2.Snapshot != null || e2.Snapshot.Value != null){
				string newString = (string) e2.Snapshot.Value;
				ChatScript.chat.NewMessage(newString);
			}
		};
	}
	
	public static void SendMessage(string message){
		dbChat.SetValueAsync(message);
	}
			// Chat test //
		// Chat test //
	// Chat test //
	
	public static List<DatabaseEntry> GetAllClients(){
		// Gets a list of 20 clients;
		print ("CheckAllClients()");
		List <DatabaseEntry> clientList = new List<DatabaseEntry>();
		for (int i = 0; i < 20; i++){
			if (dbSnapshot.HasChild("client_" + i.ToString())){
				string newClient = (string) dbSnapshot.Child("client_" + i.ToString() + "/name").Value;
				clientList.Add(new DatabaseEntry(newClient, i));
				print ("clientList.Add(" + clientList[i].entry + ")");
			} else { print ("index " + i + " is empty");}
		}
		return clientList;
	}
	
	public static void AddClient(string newName){
		print ("AddClient: " + newName);
		int nextIndex = GetNextClientIndex();
		dbRoot.Child("client_" + nextIndex.ToString() + "/name").SetValueAsync(newName);
		dbRoot.Child("client_" + nextIndex.ToString() + "/questions/question_dummy").SetValueAsync("dummy");
		dbRoot.Child("client_" + nextIndex.ToString() + "/users/user_dummy").SetValueAsync("dummy");
	}
	
	public void UploadAllQuestions(){
		for (int i = 0; i < questionListLocal.Count; i++){
			if (!questionListLocal[i].delete){
				int questionIndex = questionListLocal[i].index;
				//UploadQuestion(currentClient, questionIndex, questions[questionIndex].text etc...);
			}
		}
		RemoveSpecifiedQuestions();
	}
	
	public void UploadAllUsers(){
		for (int i = 0; i < usersListLocal.Count; i++){
			if (!usersListLocal[i].delete){
				int userIndex = usersListLocal[i].index;
				UploadUser(currentClient);
			}
		}
		RemoveSpecifiedUsers();
	}
	
	public static void UploadQuestion(int client, int index, int type, string userGroup, string subjects, string question, string alt0, string alt1, string alt2, string alt3, string alt4){
		// Checks if question with this index already exists, if so edits it, if not adds a new one
		print ("UploadQuestion() ");
	
		int databaseEntryIndex = 0;
		bool editQuestion = false;
		DatabaseReference node = dbRoot.Child("client_" + client.ToString() + "/questions");
		if (questionListLocal.Count > 0){
			for (int i = 0; i < questionListLocal.Count; i ++){
				print ("for (int i = 0; i < questionListLocal.Count; i ++): " + i);
				if (questionListLocal[i].index == index){
					editQuestion = true;
					print ("editQuestion = true;");
					databaseEntryIndex = i;
					break;
				}
			}
		}
		if (editQuestion){
			EditQuestion(databaseEntryIndex, client, index, type, userGroup, subjects, question, alt0, alt1, alt2, alt3, alt4);
			print ("EditQuestion");
			}
		if (!editQuestion){
			AddQuestion(client, index, type, userGroup, subjects, question, alt0, alt1, alt2, alt3, alt4);
			print ("AddQuestion");
			}
	}
	
	public static void UploadUser(int client){
		int databaseEntryIndex = 0;
		bool editUser = false;
		
		if (editUser){
			EditUser();
			print ("EditUser");
		}
		if (!editUser){
			AddUser();
			print ("AddUser");
		}
		print ("UploadQuestion() ");
	}
	
	public static void AddQuestion(int client, int index, int type, string userGroup, string subjects, string question, string alt0, string alt1, string alt2, string alt3, string alt4){
		// int databaseEntryIndex = GetNextQuestionIndex(client);
		int databaseEntryIndex = questionListLocal.Count;
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/index").SetValueAsync(index);
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/type").SetValueAsync(type);
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/userGroup").SetValueAsync(userGroup);
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/subjects").SetValueAsync(subjects);
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/question").SetValueAsync(question);
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/alt0").SetValueAsync(alt0);
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/alt1").SetValueAsync(alt1);
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/alt2").SetValueAsync(alt2);
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/alt3").SetValueAsync(alt3);
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/alt4").SetValueAsync(alt4);
		questionListLocal.Add(new LocalDatabaseEntry(databaseEntryIndex, index));
	}
	
	public static void EditQuestion(int databaseEntryIndex, int client, int index, int type, string userGroup, string subjects, string question, string alt0, string alt1, string alt2, string alt3, string alt4){
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/index").SetValueAsync(index);
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/type").SetValueAsync(type);
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/userGroup").SetValueAsync(userGroup);
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/subjects").SetValueAsync(subjects);
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/question").SetValueAsync(question);
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/alt0").SetValueAsync(alt0);
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/alt1").SetValueAsync(alt1);
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/alt2").SetValueAsync(alt2);
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/alt3").SetValueAsync(alt3);
		dbRoot.Child("client_" + client.ToString() + "/questions/question_" + databaseEntryIndex.ToString() + "/alt4").SetValueAsync(alt4);
	}
	
	public static void AddUser(){
		
	}
	
	public static void EditUser(){
		
	}
	
	public static void GetLocalQuestionList(){
		questionListLocal = new List<LocalDatabaseEntry>();
		// Replicates the snapshot in order to keep track of edits
		int numberOfQuestions = 0;
		int i = 0;
		int target = 20;
		bool hasTarget = false;;
		bool done = false;	
		bool exists;
		do {
			string path = "client_" + currentClient.ToString() + "/questions/question_" + i.ToString() + "/index";
			exists = ValueExistsAsLong(path);
			print ("ValueExistsAsLong " + i + " " + exists);
			if (exists){
				long newValue = (long) dbSnapshot.Child(path).Value;
				int y = Convert.ToInt32(newValue);
				questionListLocal.Add(new LocalDatabaseEntry(i, y));
				print ("questionListLocal.Add(new LocalDatabaseEntry(" + i + ", " + y + "));");
				if (hasTarget){
					hasTarget = false;
				}
				Question newQuestion = LoadQuestion(currentClient, i);
				bncQ.AddQuestion(newQuestion);
			}
			else {
				if (!hasTarget){
					hasTarget = true;
					target = i + 20;
				}
			}
			if (i == target) done = true;
			i = i + 1;
		} while (!done);
		print ("GetLocalQuestionList: " + questionListLocal.Count);
	}
	
	public static void GetLocalClientList(){	// MAYBE REVIEW LATER	// MAYBE REVIEW LATER	// MAYBE REVIEW LATER	
		usersListLocal = new List<LocalDatabaseEntry>();
		// Replicates the snapshot in order to keep track of edits
		int numberOfQuestions = 0;
		int i = 0;
		int target;
		bool hasTarget = false;;
		bool done = false;
		DataSnapshot node = dbSnapshot.Child(currentClient + "/questions");
		do {
			target = 20;
			if (node.HasChild("question_" + i.ToString())){
				int y = (int) node.Child("question_" + i.ToString() + "/index").Value;
				usersListLocal.Add(new LocalDatabaseEntry(i, y));
				if (hasTarget){
					hasTarget = false;
				}
			}
			else {
				if (!hasTarget){
					hasTarget = true;
					target = i + 20;
				}
			}
			if (i == target) done = true;
			i = i + 1;
		} while (!done);
		print ("GetLocalUsersList: " + usersListLocal.Count);
	}
	
	public static int GetNextClientIndex(){
		int nextIndex = 0;
		bool done = false;
		do {
			if (!dbSnapshot.HasChild("client_" + nextIndex.ToString())) done = true;
			else nextIndex = nextIndex + 1;
			if (nextIndex > 100) done = true;	// Infinity loop safety check
		} while (!done);
		print ("GetNextClientIndex: " + nextIndex);
		return nextIndex;
	}
	
	public static int GetNextQuestionIndex(int client){
		int nextIndex = 0;
		bool done = false;
		do {
			if (!dbSnapshot.HasChild("client_" + client.ToString() + "/questions/question_" + nextIndex.ToString())) done = true;
			else nextIndex = nextIndex + 1;
			if (nextIndex > 1000) done = true;	// Infinity loop safety check
		} while (!done);
		print ("GetNextQuestionIndex: " + nextIndex);
		return nextIndex;
	}
	
	public static int GetNextUserIndex(int client){
		int nextIndex = 0;
		bool done = false;
		do {
			if (!dbSnapshot.HasChild("client_" + client.ToString() + "/users/user_" + nextIndex.ToString())) done = true;
			else nextIndex = nextIndex + 1;
			if (nextIndex > 1000) done = true;	// Infinity loop safety check
		} while (!done);
		print ("GetNextUserIndex: " + nextIndex);
		return nextIndex;
	}
	
	public static void RemoveSpecifiedQuestions(){	// FIX LATER
		for (int i = 0; i < questionListLocal.Count; i++){
			if (questionListLocal[i].delete){
				//firebase method for deleting entries
				// dbQuestions.Child("question_" + questionListLocal[i].entry).Remove();
				getNewSnapshot = true;
			}
		}
	}
	
	public static void RemoveSpecifiedUsers(){
		for (int i = 0; i < usersListLocal.Count; i++){
			if (usersListLocal[i].delete){
				//firebase method for deleting entries
				//dbQuestions.Child("question_" + usersListLocal[i].entry).Remove();
				getNewSnapshot = true;
			}
		}
	}
	
	public static void FindAndDeleteQuestion(int index){
		for (int i = 0; i < questionListLocal.Count; i++){
			if (questionListLocal[i].index == index){
				questionListLocal[i].Delete();
			}
		}
	}
	
	public void DeleteAllQuestions(){
		for (int i = 0; i < questionListLocal.Count; i++){
			questionListLocal[i].Delete();
		}
		RemoveSpecifiedQuestions();
	}
	
	public void DeleteAllUsers(){
		for (int i = 0; i < usersListLocal.Count; i++){
			usersListLocal[i].Delete();
		}
		RemoveSpecifiedUsers();
	}
	
	public void DeleteQuestion(int index){
		questionListLocal[index].Delete();
	}
	
	public void DeleteUsers(int index){
		usersListLocal[index].Delete();
	}

	public static Question LoadQuestion(int client, int entryIndex){
		Question newQuestion = new Question();
		string path = "client_" + currentClient.ToString() + "/questions/question_" + entryIndex.ToString();
		long newIndex = (long) dbSnapshot.Child(path + "/index").Value;
		newQuestion.index = Convert.ToInt32(newIndex);
		long newType = (long)dbSnapshot.Child(path + "/type").Value;
		newQuestion.questionType = Convert.ToInt32(newType);
		newQuestion.text = (string) dbSnapshot.Child(path + "/question").Value;
		newQuestion.answer0 = (string) dbSnapshot.Child(path + "/alt0").Value;
		newQuestion.answer1 = (string) dbSnapshot.Child(path + "/alt1").Value;
		newQuestion.answer2 = (string) dbSnapshot.Child(path + "/alt2").Value;
		newQuestion.answer3 = (string) dbSnapshot.Child(path + "/alt3").Value;
		newQuestion.answer4 = (string) dbSnapshot.Child(path + "/alt4").Value;
		newQuestion.subjects = new List <bool>();
		string newSubjects = (string) dbSnapshot.Child(path + "/subjects").Value;
		if (newSubjects != ""){
			string[] questionSubjects;
			string[] space = new string[] {" "};
			questionSubjects = newSubjects.Split(space, StringSplitOptions.None);
			for (int y = 0; y < questionSubjects.Length; y++){
				if (questionSubjects[y] == "T"){
					newQuestion.subjects.Add(true);
				}
				if (questionSubjects[y] == "F"){
					newQuestion.subjects.Add(false);
				}
			}
		}
		newQuestion.userGroups = new List <bool>();
		string newUserGroups = (string) dbSnapshot.Child(path + "/userGroup").Value;
		if (newUserGroups != ""){
			string[] questionUserGroups;
			string[] space = new string[] {" "};
			questionUserGroups = newUserGroups.Split(space, StringSplitOptions.None);
			for (int y = 0; y < questionUserGroups.Length; y++){
				if (questionUserGroups[y] == "T"){
					newQuestion.userGroups.Add(true);
				}
				if (questionUserGroups[y] == "F"){
					newQuestion.userGroups.Add(false);
				}
			}
		}
		return newQuestion;
	}
	
	// Test //
		// Test //
			// Test //
	public static void DeleteTest(int databaseEntryIndex){
		dbRoot.Child("client_" + currentClient + "/questions/question_" + databaseEntryIndex.ToString() + "/index").RemoveValueAsync();
		dbRoot.Child("client_" + currentClient + "/questions/question_" + databaseEntryIndex.ToString() + "/type").RemoveValueAsync();
		dbRoot.Child("client_" + currentClient + "/questions/question_" + databaseEntryIndex.ToString() + "/userGroup").RemoveValueAsync();
		dbRoot.Child("client_" + currentClient + "/questions/question_" + databaseEntryIndex.ToString() + "/subjects").RemoveValueAsync();
		dbRoot.Child("client_" + currentClient + "/questions/question_" + databaseEntryIndex.ToString() + "/question").RemoveValueAsync();
		dbRoot.Child("client_" + currentClient + "/questions/question_" + databaseEntryIndex.ToString() + "/alt0").RemoveValueAsync();
		dbRoot.Child("client_" + currentClient + "/questions/question_" + databaseEntryIndex.ToString() + "/alt1").RemoveValueAsync();
		dbRoot.Child("client_" + currentClient + "/questions/question_" + databaseEntryIndex.ToString() + "/alt2").RemoveValueAsync();
		dbRoot.Child("client_" + currentClient + "/questions/question_" + databaseEntryIndex.ToString() + "/alt3").RemoveValueAsync();
		dbRoot.Child("client_" + currentClient + "/questions/question_" + databaseEntryIndex.ToString() + "/alt4").RemoveValueAsync();
	}
			// Test //
		// Test //
	// Test //
	
	public static bool ValueExistsAsLong(string path){
		// Checks if a value in a node exists and returns true or false;
		bool exists = false;
		string message = "";
		try{
			long newValue = (long) dbSnapshot.Child(path).Value;
			exists = true;
		}
		catch (InvalidCastException exception){
			exists = false;
			message = ", InvalidCastException";
		}
		catch (NullReferenceException exception){
			exists = false;
			message = ", NullReferenceException";
		}
		print ("ValueExists( " + path + "): " + exists + message);
		return exists;
	}
	
	public static bool ValueExistsAsString(string path){
		// Checks if a value in a node exists and returns true or false;
		bool exists = false;
		string message = "";
		try{
			string newValue = (string) dbSnapshot.Child(path).Value;
			exists = true;
		}
		catch (InvalidCastException exception){
			exists = false;
			message = ", InvalidCastException";
		}
		catch (NullReferenceException exception){
			exists = false;
			message = ", NullReferenceException";
		}
		print ("ValueExists( " + path + "): " + exists + message);
		return exists;
	}
}

public class LocalDatabaseEntry {
	
	public int entry;
	public int index;
	public bool delete;

	public LocalDatabaseEntry(int newEntry, int newIndex){
		entry = newEntry;
		index = newIndex;
		delete = false;
	}

	public LocalDatabaseEntry(int newEntry, int newIndex, bool newDelete){
		entry = newEntry;
		index = newIndex;
		delete = true;
	}

	public void Delete(){
		delete = true;
	}

}