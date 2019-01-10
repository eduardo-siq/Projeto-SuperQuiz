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
	public static UserEditorClass userEditor;
	
	bool setValues = false;
	
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
	public static DataSnapshot dbSnapshot;
	
	
	// Miscellaneous
	float timer = 0;
	public static string thisScene;
	
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
		Invoke("SetRerefences", 0.5f);
		Invoke("StartListener", 0.75f);
	}
	
	void SetRerefences(){
		dbRoot = FirebaseDatabase.DefaultInstance.GetReference("");
		GetQuestionsReference();
	}
	
	public static void GetQuestionsReference(){
		DatabaseReference dbQuestions = dbRoot.Child("questions/client_" + currentClient.ToString());
	}
	
	public static void GetUsersReference(){
		DatabaseReference dbUsers = dbRoot.Child("users/client_" + currentClient.ToString());
	}
	
	public void StartListener() {
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
	
	// public static List<DatabaseEntry> GetAllClients(){	// REMOVE LATER
		// Gets a list of 20 clients;
		// print ("GetAllClients()");
		// List <DatabaseEntry> clientList = new List<DatabaseEntry>();
		// for (int i = 0; i < 20; i++){
			// if (dbSnapshot.HasChild("clients/client_" + i.ToString())){
				// string newClient = (string) dbSnapshot.Child("clients/client_" + i.ToString() + "/name").Value;
				// clientList.Add(new DatabaseEntry(newClient, i));
				// print ("clientList.Add(" + clientList[i].entry + ")");
			// } else { print ("index " + i + " is empty");}
		// }
		// return clientList;
	// }
	

	public static List<DatabaseEntry> GetAllClients(){
		print ("GetAllClients()");
		List <DatabaseEntry> clientList = new List<DatabaseEntry>();
		// Replicates the snapshot in order to keep track of edits
		int i = 0;
		int target = 20;
		bool hasTarget = false;;
		bool done = false;	
		bool exists;
		do {
			string path = "clients/client_" + i.ToString() + "/name";
			exists = ValueExistsAsString(path);
			print ("ValueExistsAsString " + i + " " + exists);
			if (exists){
				string newClient = (string) dbSnapshot.Child(path).Value;
				print ("newClient: " + newClient);
				clientList.Add(new DatabaseEntry(newClient, i));
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
		print ("GetAllClients: " + clientList.Count + " entries");
		return clientList;
	}
	
	public static void AddClient(string newName){
		print ("AddClient: " + newName);
		int nextIndex = GetNextClientIndex();
		dbRoot.Child("clients/client_" + nextIndex.ToString() + "/name").SetValueAsync(newName);
		dbRoot.Child("clients/client_" + nextIndex.ToString() + "/userGroups").SetValueAsync("X");
		dbRoot.Child("clients/client_" + nextIndex.ToString() + "/subjects").SetValueAsync("X");
	}
	
	public static string GetSubjectsFromFirebase(){
		string newString;
		newString = (string) dbSnapshot.Child("clients/client_" + currentClient.ToString() + "/subjects").Value;
		if (newString == null){
			newString = "X";
		}
		return newString;
	}
	
	public static string GetUsersFromFirebase(){
		string newString;
		newString = (string) dbSnapshot.Child("clients/client_" + currentClient.ToString() + "/userGroups").Value;
		return newString;
	}
	
	public static void UploadQuestion(int client, int index, int type, string userGroup, string subjects, string question, string alt0, string alt1, string alt2, string alt3, string alt4){
		// Checks if question with this index already exists, if so edits it, if not adds a new one
		print ("UploadQuestion() ");
		int databaseEntryIndex = 0;
		bool editQuestion = false;
		DatabaseReference node = dbRoot.Child("questions/client_" + client.ToString());
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
	
	public static void UploadUser(int client, int index, string name, string email, /*string password,*/ string telephone, string cpf, /*string department,*/ string localization, bool firstLogin, string answers, string time, int score, string userGroups){
		int databaseEntryIndex = 0;
		bool editUser = false;
		DatabaseReference node = dbRoot.Child("users/client_" + client.ToString());
		if (usersListLocal.Count > 0){
			for (int i = 0; i < usersListLocal.Count; i ++){
				print ("for (int i = 0; i < usersListLocal.Count; i ++): " + i);
				if (usersListLocal[i].index == index){
					editUser = true;
					print ("editUser = true;");
					databaseEntryIndex = i;
					break;
				}
			}
		}
		if (editUser){
			EditUser(databaseEntryIndex, client, index, name, email, /*password,*/ telephone, cpf, /*department,*/ localization, firstLogin, answers, time, score, userGroups);
			print ("EditUser");
		}
		if (!editUser){
			AddUser(client, index, name, email, /*password,*/ telephone, cpf, /*department,*/ localization, firstLogin, answers, time, score, userGroups);
			print ("AddUser");
		}
		print ("UploadQuestion() ");
	}
	
	public static void AddQuestion(int client, int index, int type, string userGroup, string subjects, string question, string alt0, string alt1, string alt2, string alt3, string alt4){
		int databaseEntryIndex = questionListLocal.Count;
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/index").SetValueAsync(index);
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/type").SetValueAsync(type);
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/userGroup").SetValueAsync(userGroup);
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/subjects").SetValueAsync(subjects);
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/question").SetValueAsync(question);
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/alt0").SetValueAsync(alt0);
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/alt1").SetValueAsync(alt1);
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/alt2").SetValueAsync(alt2);
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/alt3").SetValueAsync(alt3);
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/alt4").SetValueAsync(alt4);
		questionListLocal.Add(new LocalDatabaseEntry(databaseEntryIndex, index));
	}
	
	public static void EditQuestion(int databaseEntryIndex, int client, int index, int type, string userGroup, string subjects, string question, string alt0, string alt1, string alt2, string alt3, string alt4){
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/index").SetValueAsync(index);
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/type").SetValueAsync(type);
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/userGroup").SetValueAsync(userGroup);
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/subjects").SetValueAsync(subjects);
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/question").SetValueAsync(question);
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/alt0").SetValueAsync(alt0);
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/alt1").SetValueAsync(alt1);
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/alt2").SetValueAsync(alt2);
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/alt3").SetValueAsync(alt3);
		dbRoot.Child("questions/client_" + client.ToString() + "/question_" + databaseEntryIndex.ToString() + "/alt4").SetValueAsync(alt4);
	}
	
	public static void AddUser(int client, int index, string name, string email, /*string password,*/ string telephone, string cpf, /*string department,*/ string localization, bool firstLogin, string answers, string time, int score, string userGroups){
		int databaseEntryIndex = usersListLocal.Count;
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/index").SetValueAsync(index);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/name").SetValueAsync(name);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/email").SetValueAsync(email);
		// dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/password").SetValueAsync(password);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/telephone").SetValueAsync(telephone);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/cpf").SetValueAsync(cpf);
		// dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/department").SetValueAsync(department);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/localization").SetValueAsync(localization);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/firstLogin").SetValueAsync(firstLogin);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/answers").SetValueAsync(answers);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/time").SetValueAsync(time);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/score").SetValueAsync(score);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/userGroups").SetValueAsync(userGroups);
		usersListLocal.Add(new LocalDatabaseEntry(databaseEntryIndex, index));
	}
	
	public static void EditUser(int databaseEntryIndex, int client, int index, string name, string email, /*string password,*/ string telephone, string cpf, /*string department,*/ string localization, bool firstLogin, string answers, string time, int score, string userGroups){
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/index").SetValueAsync(index);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/name").SetValueAsync(name);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/email").SetValueAsync(email);
		// dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/password").SetValueAsync(password);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/telephone").SetValueAsync(telephone);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/cpf").SetValueAsync(cpf);
		// dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/department").SetValueAsync(department);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/localization").SetValueAsync(localization);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/firstLogin").SetValueAsync(firstLogin);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/answers").SetValueAsync(answers);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/time").SetValueAsync(time);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/score").SetValueAsync(score);
		dbRoot.Child("users/client_" + client.ToString() + "/user_" + databaseEntryIndex.ToString() + "/userGroups").SetValueAsync(userGroups);
	}
	
	public static void GetLocalQuestionList(){
		questionListLocal = new List<LocalDatabaseEntry>();
		// Replicates the snapshot in order to keep track of edits
		int i = 0;
		int target = 20;
		bool hasTarget = false;;
		bool done = false;	
		bool exists;
		do {
			string path = "questions/client_" + currentClient.ToString() + "/question_" + i.ToString() + "/index";
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
	
	public static void GetLocalUserList(){	// MAYBE REVIEW LATER	// MAYBE REVIEW LATER	// MAYBE REVIEW LATER	
		usersListLocal = new List<LocalDatabaseEntry>();
		// Replicates the snapshot in order to keep track of edits
		int i = 0;
		int target = 20;
		bool hasTarget = false;;
		bool done = false;	
		bool exists;
		do {
			string path = "users/client_" + currentClient.ToString() + "/user_" + i.ToString() + "/index";
			exists = ValueExistsAsLong(path);
			print ("ValueExistsAsLong " + i + " " + exists);
			if (exists){
				long newValue = (long) dbSnapshot.Child(path).Value;
				int y = Convert.ToInt32(newValue);
				usersListLocal.Add(new LocalDatabaseEntry(i, y));
				print ("usersListLocal.Add(new LocalDatabaseEntry(" + i + ", " + y + "));");
				if (hasTarget){
					hasTarget = false;
				}
				User newUser = LoadUser(currentClient, i);
				userEditor.AddUser(newUser);
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
		print ("GetLocalUserList: " + usersListLocal.Count);
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
			if (!dbSnapshot.HasChild("questions/client_" + client.ToString() + "/question_" + nextIndex.ToString())) done = true;
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
	
	public int GetQuestionEntryIndexByIndex(int index){
		int entryIndex = -1;
		for (int i = 0; i < questionListLocal.Count; i++){
			if (questionListLocal[i].index == index){
				entryIndex = i;
			}
		}
		if (entryIndex == -1){
			entryIndex = 0;
			print ("Entry Index not found. Set to zero");
		}
		return entryIndex;
	}

	public int GetUserEntryIndexByIndex(int index){
		int entryIndex = -1;
		for (int i = 0; i < usersListLocal.Count; i++){
			if (usersListLocal[i].index == index){
				entryIndex = i;
			}
		}
		if (entryIndex == -1){
			entryIndex = 0;
			print ("Entry Index not found. Set to zero");
		}
		return entryIndex;
	}
	
	public static void RemoveSpecifiedQuestions(){	// FIX LATER
		for (int i = 0; i < questionListLocal.Count; i++){
			if (questionListLocal[i].delete){
				//firebase method for deleting entries
				DeleteQuestionFromFirebase(i);
			}
		}
	}
	
	public static void RemoveSpecifiedUsers(){
		for (int i = 0; i < usersListLocal.Count; i++){
			if (usersListLocal[i].delete){
				//firebase method for deleting entries
				DeleteUserFromFirebase(i);
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
	
	public static void FindAndDeleteUser(int index){
		for (int i = 0; i < usersListLocal.Count; i++){
			if (usersListLocal[i].index == index){
				usersListLocal[i].Delete();
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
		string path = "questions/client_" + currentClient.ToString() + "/question_" + entryIndex.ToString();
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
	
	public static User LoadUser(int client, int entryIndex){
		User newUser = new User();
		string path = "users/client_" + currentClient.ToString() + "/user_" + entryIndex.ToString();
		long newIndex = (long) dbSnapshot.Child(path + "/index").Value;
		newUser.index = Convert.ToInt32(newIndex);
		newUser.name = (string) dbSnapshot.Child(path + "/name").Value;
		newUser.email = (string) dbSnapshot.Child(path + "/email").Value;
		// newUser.password = (string) dbSnapshot.Child(path + "/password").Value;
		newUser.telephone = (string) dbSnapshot.Child(path + "/telephone").Value;
		newUser.cpf = (string) dbSnapshot.Child(path + "/cpf").Value;
		// newUser.departament = (string) dbSnapshot.Child(path + "/departament").Value;
		newUser.localization = (string) dbSnapshot.Child(path + "/localization").Value;
		newUser.firstLogin = (bool) dbSnapshot.Child(path + "/firstLogin").Value;
		newUser.answers = (string) dbSnapshot.Child(path + "/answers").Value;
		newUser.time = (string) dbSnapshot.Child(path + "/time").Value;
		// string newScore = (string) dbSnapshot.Child(path + "/score").Value;
		long newScore = (long) dbSnapshot.Child(path + "/score").Value;
		newUser.score = Convert.ToInt32(newScore);
		newUser.userGroups = new List <bool>();
		string newUserGroups = (string) dbSnapshot.Child(path + "/userGroup").Value;
		if (newUserGroups != "" && newUserGroups != null){
			string[] userUserGroups;
			string[] space = new string[] {" "};
			userUserGroups = newUserGroups.Split(space, StringSplitOptions.None);
			for (int y = 0; y < userUserGroups.Length; y++){
				if (userUserGroups[y] == "T"){
					newUser.userGroups.Add(true);
				}
				if (userUserGroups[y] == "F"){
					newUser.userGroups.Add(false);
				}
			}
		}
		if (newUserGroups == "" || newUserGroups == null){
			print ("no user groups");
		}
		print ("new user created:");
		print (newUser.index);
		print (newUser.name);
		print (newUser.email);
		print (newUser.telephone);
		print (newUser.cpf);
		print (newUser.localization);
		print (newUser.firstLogin);
		print (newUser.answers);
		print (newUser.time);
		print (newUser.score);
		print (newUserGroups);
		return newUser;
	}
	
	public static void DeleteQuestionFromFirebase(int databaseEntryIndex){
		dbRoot.Child("questions/client_" + currentClient + "/question_" + databaseEntryIndex.ToString() + "/index").RemoveValueAsync();
		dbRoot.Child("questions/client_" + currentClient + "/question_" + databaseEntryIndex.ToString() + "/type").RemoveValueAsync();
		dbRoot.Child("questions/client_" + currentClient + "/question_" + databaseEntryIndex.ToString() + "/userGroup").RemoveValueAsync();
		dbRoot.Child("questions/client_" + currentClient + "/question_" + databaseEntryIndex.ToString() + "/subjects").RemoveValueAsync();
		dbRoot.Child("questions/client_" + currentClient + "/question_" + databaseEntryIndex.ToString() + "/question").RemoveValueAsync();
		dbRoot.Child("questions/client_" + currentClient + "/question_" + databaseEntryIndex.ToString() + "/alt0").RemoveValueAsync();
		dbRoot.Child("questions/client_" + currentClient + "/question_" + databaseEntryIndex.ToString() + "/alt1").RemoveValueAsync();
		dbRoot.Child("questions/client_" + currentClient + "/question_" + databaseEntryIndex.ToString() + "/alt2").RemoveValueAsync();
		dbRoot.Child("questions/client_" + currentClient + "/question_" + databaseEntryIndex.ToString() + "/alt3").RemoveValueAsync();
		dbRoot.Child("questions/client_" + currentClient + "/question_" + databaseEntryIndex.ToString() + "/alt4").RemoveValueAsync();
	}
	
	public static void DeleteUserFromFirebase(int databaseEntryIndex){	// CHANGE LATER
		dbRoot.Child("users/client_" + currentClient + "/user_" + databaseEntryIndex.ToString() + "/index").RemoveValueAsync();
		dbRoot.Child("users/client_" + currentClient + "/user_" + databaseEntryIndex.ToString() + "/name").RemoveValueAsync();
		dbRoot.Child("users/client_" + currentClient + "/user_" + databaseEntryIndex.ToString() + "/email").RemoveValueAsync();
		// dbRoot.Child("users/client_" + currentClient + "/user_" + databaseEntryIndex.ToString() + "/password").RemoveValueAsync();
		dbRoot.Child("users/client_" + currentClient + "/user_" + databaseEntryIndex.ToString() + "/telephone").RemoveValueAsync();
		dbRoot.Child("users/client_" + currentClient + "/user_" + databaseEntryIndex.ToString() + "/cpf").RemoveValueAsync();
		// dbRoot.Child("users/client_" + currentClient + "/user_" + databaseEntryIndex.ToString() + "/department").RemoveValueAsync();
		dbRoot.Child("users/client_" + currentClient + "/user_" + databaseEntryIndex.ToString() + "/localization").RemoveValueAsync();
		dbRoot.Child("users/client_" + currentClient + "/user_" + databaseEntryIndex.ToString() + "/firstLogin").RemoveValueAsync();
		dbRoot.Child("users/client_" + currentClient + "/user_" + databaseEntryIndex.ToString() + "/answers").RemoveValueAsync();
		dbRoot.Child("users/client_" + currentClient + "/user_" + databaseEntryIndex.ToString() + "/time").RemoveValueAsync();
		dbRoot.Child("users/client_" + currentClient + "/user_" + databaseEntryIndex.ToString() + "/score").RemoveValueAsync();
		dbRoot.Child("users/client_" + currentClient + "/user_" + databaseEntryIndex.ToString() + "/userGroups").RemoveValueAsync();
	}
	
	public static bool ValueExistsAsLong(string path){
		// Checks if a value in a node exists and returns true or false;
		bool exists = false;
		string message = "";
		try{
			long newValue = (long) dbSnapshot.Child(path).Value;
			if (newValue != null){
				exists = true;
			}
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
		// Checks if a value in a node exists and it's not null and returns true or false;
		bool exists = false;
		string message = "";
		try{
			string newValue = (string) dbSnapshot.Child(path).Value;
			if (newValue != null && newValue != ""){
				exists = true;
			}
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