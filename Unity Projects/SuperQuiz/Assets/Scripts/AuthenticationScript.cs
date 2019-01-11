using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class AuthenticationScript : MonoBehaviour {
	
	public static AuthenticationScript instance = null;
	public SessionScript sessionScript;

	// Firebase variables
	static Firebase.Auth.FirebaseAuth auth;
	protected Firebase.Auth.FirebaseAuth otherAuth;
	private Firebase.AppOptions otherAuthOptions = new Firebase.AppOptions {ApiKey = "", AppId = "", ProjectId = ""};
	Dictionary <string, Firebase.Auth.FirebaseUser> userByAuth = new Dictionary <string, Firebase.Auth.FirebaseUser>();
	Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
	 
	// Simple variables
	private string logText = "";
	// protected string email = "";		// Handled by LoginScript
	// protected string password = "";	// Handled by LoginScript
	protected string displayName = "";
	protected string phoneNumber = "";
	protected string receivedCode = "";
	private bool fetchingToken = false;
	protected bool signInAndFetchProfile = false;
	
	// Database
	public DatabaseReference dbRoot;
	public static DatabaseReference dbRefClientUsers;		
	public static DatabaseReference dbRefClientQuestions;	// OBSOLETE?
	public static DatabaseReference dbRefClientDetail;		// OBSOLETE?

	// This User
	public static int client;	// client Database entry
	public static int userEntry;	// user Database entry
	public static string email;
	public static int user;	// user ID
	public static bool userError = false;
	
	// Event Listener Holder
	public static UserListenerHolderScript usersEventHolder;
	public static QuestionListenerHolderScript questionsEventHolder;
	public static ClientListenerHolderScript clientEventHolder;
	
	// SnapShot
	// public static DataSnapshot dbSnapshot;
	public static DataSnapshot dbUsers;
	public static DataSnapshot dbQuestions;
	public static DataSnapshot dbClient;
	
	void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            if (instance != this){
                Destroy(gameObject);
            }
        }
    }
	
	 
	public virtual void Start(){
		sessionScript = GameObject.Find("Session").GetComponent<SessionScript>();
		Debug.Log ("Start AuthenticationScript");
		Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
			dependencyStatus = task.Result;
			if (dependencyStatus == Firebase.DependencyStatus.Available) {
				InitializeFirebase();
			}else {
			Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
			}
		});
	}
	
	// Handle initialization of the necessary firebase modules:
	protected void InitializeFirebase() {
		Debug.Log("Setting up Firebase Auth");
		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		auth.StateChanged += AuthStateChanged;	// AuthStateChanged é um event handler para o qual auth.StateChanged é subscrito
		auth.IdTokenChanged += IdTokenChanged;		// IdTokenChanged é um event handler para o qual auth.IdTokenChanged é subscrito
		// Specify valid options to construct a secondary authentication object.
		if (otherAuthOptions != null && !(String.IsNullOrEmpty(otherAuthOptions.ApiKey) || String.IsNullOrEmpty(otherAuthOptions.AppId) || String.IsNullOrEmpty(otherAuthOptions.ProjectId))){
			try {
				otherAuth = Firebase.Auth.FirebaseAuth.GetAuth(Firebase.FirebaseApp.Create(otherAuthOptions, "Secondary"));
				otherAuth.StateChanged += AuthStateChanged;
				otherAuth.IdTokenChanged += IdTokenChanged;
			}catch (Exception) {
				Debug.Log("ERROR: Failed to initialize secondary authentication object.");
			}
		}
		AuthStateChanged(this, null);
	}
	
	void AuthStateChanged(object sender, System.EventArgs eventArgs) {	  // Track state changes of the auth object.
		Firebase.Auth.FirebaseAuth senderAuth = sender as Firebase.Auth.FirebaseAuth;
		Firebase.Auth.FirebaseUser user = null;
		if (senderAuth != null) userByAuth.TryGetValue(senderAuth.App.Name, out user);
		if (senderAuth == auth && senderAuth.CurrentUser != user) {
			bool signedIn = user != senderAuth.CurrentUser && senderAuth.CurrentUser != null;
			if (!signedIn && user != null) {
				Debug.Log("Signed out " + user.UserId);
			}
			user = senderAuth.CurrentUser;
			userByAuth[senderAuth.App.Name] = user;
			if (signedIn) {
				Debug.Log("Signed in " + user.UserId);
				displayName = user.DisplayName ?? "";
				DisplayDetailedUserInfo(user, 1);
			}
		}
	}
	
	void IdTokenChanged(object sender, System.EventArgs eventArgs) {	// Track ID token changes.
		Firebase.Auth.FirebaseAuth senderAuth = sender as Firebase.Auth.FirebaseAuth;
		if (senderAuth == auth && senderAuth.CurrentUser != null && !fetchingToken) {
			senderAuth.CurrentUser.TokenAsync(false).ContinueWith(task => Debug.Log(String.Format("Token[0:8] = {0}", task.Result.Substring(0, 8))));
		}
	}
	
	protected void DisplayUserInfo(Firebase.Auth.IUserInfo userInfo, int indentLevel) {	// Display user information.
		string indent = new String(' ', indentLevel * 2);
		var userProperties = new Dictionary<string, string> {
			{"Display Name", userInfo.DisplayName},
			{"Email", userInfo.Email},
			{"Photo URL", userInfo.PhotoUrl != null ? userInfo.PhotoUrl.ToString() : null},
			{"Provider ID", userInfo.ProviderId},
			{"User ID", userInfo.UserId}
		};
		foreach (var property in userProperties) {
			if (!String.IsNullOrEmpty(property.Value)) {
				Debug.Log(String.Format("{0}{1}: {2}", indent, property.Key, property.Value));
			}
		}
	}
	
	protected void DisplayDetailedUserInfo(Firebase.Auth.FirebaseUser user, int indentLevel) {	// Display a more detailed view of a FirebaseUser.
		string indent = new String(' ', indentLevel * 2);
		DisplayUserInfo(user, indentLevel);
		Debug.Log(String.Format("{0}Anonymous: {1}", indent, user.IsAnonymous));
		Debug.Log(String.Format("{0}Email Verified: {1}", indent, user.IsEmailVerified));
		Debug.Log(String.Format("{0}Phone Number: {1}", indent, user.PhoneNumber));
		var providerDataList = new List<Firebase.Auth.IUserInfo>(user.ProviderData);
		var numberOfProviders = providerDataList.Count;
		if (numberOfProviders > 0) {
			for (int i = 0; i < numberOfProviders; ++i) {
				Debug.Log(String.Format("{0}Provider Data: {1}", indent, i));
				DisplayUserInfo(providerDataList[i], indentLevel + 2);
			}
		}
	}
	
	protected void DisplaySignInResult(Firebase.Auth.SignInResult result, int indentLevel) {	// Display user information reported
		string indent = new String(' ', indentLevel * 2);
		DisplayDetailedUserInfo(result.User, indentLevel);
		var metadata = result.Meta;
		if (metadata != null) {
			Debug.Log(String.Format("{0}Created: {1}", indent, metadata.CreationTimestamp));
			Debug.Log(String.Format("{0}Last Sign-in: {1}", indent, metadata.LastSignInTimestamp));
		}
		var info = result.Info;
		if (info != null) {
			Debug.Log(String.Format("{0}Additional User Info:", indent));
			Debug.Log(String.Format("{0}  User Name: {1}", indent, info.UserName));
			Debug.Log(String.Format("{0}  Provider ID: {1}", indent, info.ProviderId));
			//DisplayProfile<string>(info.Profile, indentLevel + 1);
		}
	}
	
	public Task SigninWithEmailAsync(string email, string password) {	// Sign-in with an email and password.
		Debug.Log(String.Format("Attempting to sign in as {0}...", email));
		// DisableUI();
		if (signInAndFetchProfile) {
			return auth.SignInAndRetrieveDataWithCredentialAsync(Firebase.Auth.EmailAuthProvider.GetCredential(email, password)).ContinueWith(HandleSignInWithSignInResult);
		} else {
			return auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(HandleSignInWithUser);
		}
	}

	public Task SigninWithEmailCredentialAsync(string email, string password) {	// This is functionally equivalent to the Signin() function.  However, it illustrates the use of Credentials, which can be aquired from many different sources of authentication.
		Debug.Log(String.Format("Attempting to sign in as {0}...", email));
		// DisableUI();
		if (signInAndFetchProfile) {
			return auth.SignInAndRetrieveDataWithCredentialAsync(Firebase.Auth.EmailAuthProvider.GetCredential(email, password)).ContinueWith(HandleSignInWithSignInResult);
		} else {
			return auth.SignInWithCredentialAsync(Firebase.Auth.EmailAuthProvider.GetCredential(email, password)).ContinueWith(HandleSignInWithUser);
		}
	}
	
	void HandleSignInWithSignInResult(Task<Firebase.Auth.SignInResult> task) {	// Called when a sign-in with profile data completes.
		// EnableUI();
		if (LogTaskCompletion(task, "Sign-in (HandleSignInWithSignInResult)")) {
			DisplaySignInResult(task.Result, 1);
			LoginScript.loginSucess = true;	// On Update, LoginScript checks if login was made and, if so, moves on with game
			GetUserSnapshot();
		} else{
			LoginScript.loginFail = true;	// Same, for failure
		}
	}
	
	void HandleSignInWithUser(Task<Firebase.Auth.FirebaseUser> task) {	// Called when a sign-in without fetching profile data completes.
	// EnableUI();
		if (LogTaskCompletion(task, "Sign-in (HandleSignInWithUser)")) {
			Debug.Log(String.Format("{0} signed in", task.Result.DisplayName));
			LoginScript.loginSucess = true;	// On Update, LoginScript checks if login was made and, if so, moves on with game
			GetUserSnapshot();
		} else{
			LoginScript.loginFail = true;	// Same, for failure
		}
	}
	
	void GetUserSnapshot(){
		usersEventHolder = new UserListenerHolderScript();
		usersEventHolder.StartListener();
	}
	
	protected bool LogTaskCompletion(Task task, string operation) {	// Log the result of the specified task, returning true if the task, completed successfully, false otherwise.
		bool complete = false;
		if (task.IsCanceled) {
			Debug.Log(operation + " canceled.");
		} else if (task.IsFaulted) {
			Debug.Log(operation + " encounted an error.");
			foreach (Exception exception in task.Exception.Flatten().InnerExceptions) {
				string authErrorCode = "";
				Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
				if (firebaseEx != null) {
					authErrorCode = String.Format("AuthError.{0}: ",((Firebase.Auth.AuthError)firebaseEx.ErrorCode).ToString());
				}
				Debug.Log(authErrorCode + exception.ToString());
			}
		} else if (task.IsCompleted) {
			Debug.Log(operation + " completed");
			complete = true;
		}
		return complete;
	}
	
	public static void SignOut() {	// Sign out the current user.
		Debug.Log("Signing out.");
		auth.SignOut();
	}

	// Database
	
	public static void FindThisUser(){
		print ("FindThisUser FindThisUser FindThisUser");
		int y = 0;
		int targetY = 5;
		bool hasTargetY = false;
		bool done = false;	
		bool clientExists = false;
		userError = true;
		do {
			string pathY = "client_" + y.ToString() + "/dummy";
			clientExists = ValueExistsAsString(dbUsers, pathY);
			if (clientExists){
				print ("client " + y + " exists");
				if (hasTargetY){
					hasTargetY = false;
				}
				bool doneUser = false;
				int i = 0;
				int targetI = 20;
				bool hasTargetI = false;
				bool userExists = false;
				do{
					string pathI = "client_" + y.ToString() + "/user_" + i.ToString() + "/index";
					userExists = ValueExistsAsLong(dbUsers, pathI);
					if (userExists){
						print ("user " + i + " exists");
						string thatUser = (string) dbUsers.Child("client_" + y.ToString() + "/user_" + i.ToString() + "/email").Value;
						if (thatUser == email){
							print ("user found!");
							userEntry = i;
							long longIndex = (long) dbUsers.Child(pathI).Value;
							print ("longIndex: " + longIndex);
							user = Convert.ToInt32(longIndex);
							client = y;
							print ("user: " + user);
							doneUser = true;
							done = true;
							userError = false;
							GetUserInfo();
							GetClientInfo();
							GetQuestions();
							dbRefClientUsers = FirebaseDatabase.DefaultInstance.GetReference("users/client_" + y.ToString() + "/user_" + i.ToString());
						}
						if (hasTargetI){
							hasTargetI = false;
						}
					}
					else {
						print ("user " + i + " does not exist");
						if (!hasTargetI){
							hasTargetI = true;
							targetI = i + 20;
						}
					}
					if (i == targetI){
						doneUser = true;
					}
					i = i + 1;
				} while(!doneUser);
			}
			else{ // client does not exist
				print ("y = " + y);
				if (!hasTargetY){
					hasTargetY = true;
					targetY = y + 5;
					print ("new target: " + targetY + ", has target: " + hasTargetY);
				}
			}
			if (y == targetY){
				done = true;
			}
			y = y + 1;
		} while (!done);
		if (userError){
			// ERROR MESSAGE
			print ("ERROR ERROR ERROR ERROR");
		}
	}
	
	public static void FindThisUser(int setClient){
		bool doneUser = false;
		int i = 0;
		int targetI = 20;
		bool hasTargetI = false;
		bool userExists = false;
		string clientString = setClient.ToString();
		do{
			string pathI = "client_" + clientString + "/user_" + i.ToString() + "/index";
			userExists = ValueExistsAsLong(dbUsers, pathI);
			if (userExists){
				print ("user " + i + " exists");
				string thatUser = (string) dbUsers.Child("client_" + clientString + "/user_" + i.ToString() + "/email").Value;
				if (thatUser == email){
					print ("user found!");
					userEntry = i;
					long longIndex = (long) dbUsers.Child(pathI).Value;
					print ("longIndex: " + longIndex);
					user = Convert.ToInt32(longIndex);
					print ("user: " + user);
					doneUser = true;
					userError = false;
					GetUserInfo();
					GetClientInfo();
					GetQuestions();
					dbRefClientUsers = FirebaseDatabase.DefaultInstance.GetReference("users/client_" + clientString + "/user_" + i.ToString());
				}
				if (hasTargetI){
					hasTargetI = false;
				}
			}
			else {
				print ("user " + i + " does not exist");
				if (!hasTargetI){
					hasTargetI = true;
					targetI = i + 20;
				}
			}
			if (i == targetI){
				doneUser = true;
			}
			i = i + 1;
		} while(!doneUser);
		if (userError){
			// ERROR MESSAGE
			print ("ERROR ERROR ERROR ERROR");
		}
	}
	
	public static void GetClientInfo(){
		print ("GetClientInfo");
		clientEventHolder = new ClientListenerHolderScript();
		clientEventHolder.StartListener();
	}
	
	public static void GetClientInfoFromSnapshot(){
		print ("GetClientInfoFromSnapshot");
		// Client Info
		bool rightScoreExist = ValueExistsAsLong(dbClient, "rightScore");
		if (rightScoreExist){
			long longValue = (long) dbClient.Child("rightScore").Value;
			SessionScript.rightScore = Convert.ToInt32(longValue);
		} else SessionScript.rightScore = 10;
		bool timeoutScoreExist = ValueExistsAsLong(dbClient, "timeoutScore");
		if (timeoutScoreExist){
			long longValue = (long) dbClient.Child("timeoutScore").Value;
			SessionScript.timeoutScore = Convert.ToInt32(longValue);
		} else SessionScript.timeoutScore = 10;
		bool wrongScoreExist = ValueExistsAsLong(dbClient, "wrongScore");
		if (wrongScoreExist){
			long longValue = (long) dbClient.Child("wrongScore").Value;
			SessionScript.wrongScore = Convert.ToInt32(longValue);
		} else SessionScript.wrongScore = 10;
		bool questionTimeExist = ValueExistsAsLong(dbClient, "questionTime");
		if (questionTimeExist){
			long longValue = (long) dbClient.Child("questionTime").Value;
			SessionScript.questionTimeShort = Convert.ToInt32(longValue);
		} else SessionScript.wrongScore = 30;
		bool questionTimeLongExist = ValueExistsAsLong(dbClient, "questionTimeLong");
		if (questionTimeLongExist){
			long longValue = (long) dbClient.Child("questionTimeLong").Value;
			SessionScript.questionTimeLong = Convert.ToInt32(longValue);
		} else SessionScript.wrongScore = 60;
		bool questionDemandedExist = ValueExistsAsLong(dbClient, "numberOfQuestionsDemanded");
		if (questionDemandedExist){
			long longValue = (long) dbClient.Child("numberOfQuestionsDemanded").Value;
			SessionScript.numberOfQuestionsDemanded = Convert.ToInt32(longValue);
		} else SessionScript.numberOfQuestionsDemanded = 5;
		// bool singleRunExists = ValueExistsAsString (dbClient, "singleRun");
		// if (singleRunExists){
			// string newValue = (string) dbClient.Child("singleRun").Value;
			// if (newValue == "false"){
				// SessionScript.singleRun = false;
			// } else SessionScript.singleRun = true;
		// } else SessionScript.singleRun = true;
		bool thresholdTier1Exist = ValueExistsAsLong(dbClient, "thresholdTier1");
		if (thresholdTier1Exist){
			long longValue = (long) dbClient.Child("thresholdTier1").Value;
			SessionScript.thresholdTier1 = Convert.ToInt32(longValue);
		} else SessionScript.thresholdTier1 = 20;
		bool thresholdTier2Exist = ValueExistsAsLong(dbClient, "thresholdTier2");
		if (thresholdTier2Exist){
			long longValue = (long) dbClient.Child("thresholdTier2").Value;
			SessionScript.thresholdTier2 = Convert.ToInt32(longValue);
		} else SessionScript.thresholdTier2 = 40;
	}
		
	public static void GetUserInfo(){
		print ("GetUserInfo");
		// User Info
		SessionScript.player = new Player();
		SessionScript.player.name = "você";
		SessionScript.player.id = user;
		string clientString = client.ToString();
		print ("clientString " + clientString);
		string userString = userEntry.ToString();
		print ("userString " + userString);
		// User Group
		bool userGrpoupExists = ValueExistsAsLong (dbUsers, "/client_" + clientString + "/user_" +  userString + "/userGroups");
		if (userGrpoupExists){
			long newValue = (long) dbUsers.Child("/client_" + clientString + "/user_" +  userString + "/userGroups").Value;
			SessionScript.userGroup = Convert.ToInt32(newValue);
		} else SessionScript.userGroup = 0;
		// First Login
		bool firstLoginExists = ValueExistsAsString (dbUsers, "/client_" + clientString + "/user_" +  userString + "/firstLogin");
		if (firstLoginExists){
			string newValue = (string) dbUsers.Child("/client_" + clientString + "/user_" +  userString + "/firstLogin").Value;
			if (newValue == "F" || newValue == "f" || newValue == "false" || newValue == "FALSE"){
				SessionScript.firstLogIn = false;
			} else SessionScript.firstLogIn = true;
		} else SessionScript.firstLogIn = true;
		// Score
		bool scoreExists = ValueExistsAsLong (dbUsers, "/client_" + clientString + "/user_" +  userString + "/score");
		if (scoreExists){
			long newValue = (long) dbUsers.Child("/client_" + clientString + "/user_" +  userString + "/score").Value;
			SessionScript.player.score = Convert.ToInt32(newValue);
		} else SessionScript.player.score = 0;
		// Avatar
		SessionScript.player.avatar = new Avatar();
		bool avatarExists = ValueExistsAsString(dbUsers, "/client_" + clientString + "/user_" +  userString + "/avatar");
		if (avatarExists){
			string avatarRawString = (string) dbUsers.Child("/client_" + clientString + "/user_" +  userString + "/avatar").Value;
			if (avatarRawString != "X" && avatarRawString != null && avatarRawString != ""){
				string[] avatarString;
				string[] space = new string[] {" "};
				avatarString = avatarRawString.Split(space, StringSplitOptions.None);
				if (avatarString.Length > 5){
					SessionScript.player.avatar.skin = Convert.ToInt32(avatarString[0]);
					SessionScript.player.avatar.hair = Convert.ToInt32(avatarString[1]);
					SessionScript.player.avatar.item0 = Convert.ToInt32(avatarString[2]);
					SessionScript.player.avatar.item1 = Convert.ToInt32(avatarString[3]);
					SessionScript.player.avatar.item2 = Convert.ToInt32(avatarString[4]);
					SessionScript.player.avatar.item3 = Convert.ToInt32(avatarString[5]);
					if (SessionScript.player.avatar.skin > -1){
						SessionScript.customizationStage = 2;
					}
				}
			} else {
				SessionScript.player.avatar.skin = -1;
			}
		} else {
			SessionScript.player.avatar.skin = -1;
		}
		bool answersExist = ValueExistsAsString(dbUsers, "/client_" + clientString + "/user_" +  userString + "/answers");
		bool answersIndexExist = ValueExistsAsString(dbUsers, "/client_" + clientString + "/user_" +  userString + "/answersIndex");
		bool timeExist = ValueExistsAsString(dbUsers, "/client_" + clientString + "/user_" +  userString + "/time");
		if (answersExist && answersIndexExist && timeExist){
			string answersRawString = (string) dbUsers.Child("/client_" + clientString + "/user_" +  userString + "/answers").Value;
			string answersIndexRawString = (string) dbUsers.Child("/client_" + clientString + "/user_" +  userString + "/answersIndex").Value;
			string timeRawString = (string) dbUsers.Child("/client_" + clientString + "/user_" +  userString + "/time").Value;
			if (answersRawString != "X" && answersIndexRawString != "X" && timeRawString != "X" && answersRawString != "" && answersIndexRawString != "" && timeRawString != ""){
				string[] space = new string[] {" "};
				string[] answersString;
				string[] answersIndexString;
				string[] timeString;
				answersString = answersRawString.Split(space, StringSplitOptions.None);
				answersIndexString = answersIndexRawString.Split(space, StringSplitOptions.None);
				timeString = timeRawString.Split(space, StringSplitOptions.None);
				for (int i = 0; i < answersString.Length; i++){
					SessionScript.answersList.Add(new Answer(int.Parse(answersString[i]), int.Parse(answersIndexString[i]), float.Parse(timeString[i])));
					SessionScript.questionsAskedList.Add(int.Parse(answersIndexString[i]));
				}
			}
		}
	}
	
	public static void SaveAnswers(){
		string answersString = "";
		string answersIndexString = "";
		string timeString = "";
		for (int i = 0; i < SessionScript.answersList.Count; i++){
			if (i != 0){
				answersString = answersString + " ";
				answersIndexString = answersIndexString + " ";
				timeString = timeString + " ";
			}
			answersString = answersString + SessionScript.answersList[i].alternative.ToString();
			answersIndexString = answersIndexString + SessionScript.answersList[i].index.ToString();
			timeString = timeString + SessionScript.answersList[i].time.ToString("0.00");
		}
		dbRefClientUsers.Child("answers").SetValueAsync(answersString);
		dbRefClientUsers.Child("answersIndex").SetValueAsync(answersIndexString);
		dbRefClientUsers.Child("time").SetValueAsync(timeString);
		dbRefClientUsers.Child("time").SetValueAsync(SessionScript.player.score);
		
	}
	
	public static void FirstLoginCompleted(){
		string f = "F";
		dbRefClientUsers.Child("firstLogin").SetValueAsync(f);	
	}
	
	public static void SaveAvatar(){
		string avatarString = "";
		avatarString = avatarString + SessionScript.player.avatar.skin.ToString() + " ";
		avatarString = avatarString + SessionScript.player.avatar.hair.ToString() + " ";
		avatarString = avatarString + SessionScript.player.avatar.item0.ToString() + " ";
		avatarString = avatarString + SessionScript.player.avatar.item1.ToString() + " ";
		avatarString = avatarString + SessionScript.player.avatar.item2.ToString() + " ";
		avatarString = avatarString + SessionScript.player.avatar.item3.ToString();
		dbRefClientUsers.Child("avatar").SetValueAsync(avatarString);
	}
	
	public static void GetQuestions(){
		print ("GetClientInfo");
		questionsEventHolder = new QuestionListenerHolderScript();
		questionsEventHolder.StartListener();
	}
	
	public static void GetQuestionsFromSnapshot(){
		SessionScript.questionListPreLoad = new List<QuestionPreLoad>();
		int i = 0;
		int target = 20;
		bool hasTarget = false;;
		bool done = false;	
		do {
			string path = "/question_" + i.ToString();
			bool questionExists = ValueExistsAsLong(dbQuestions, path + "/index");
			if (questionExists){
				long newValue = (long) dbQuestions.Child(path + "/index").Value;
				int newIndex = Convert.ToInt32(newValue);
				int newType = 0;
				string newQuestion = "ERRO - NÃO CARREGADA!";
				string newAlt0 = "ERRO - NÃO CARREGADA!";
				string newAlt1 = "ERRO - NÃO CARREGADA!";
				string newAlt2 = "ERRO - NÃO CARREGADA!";
				string newAlt3 = "ERRO - NÃO CARREGADA!";
				string newAlt4 = "ERRO - NÃO CARREGADA!";
				string newUserGroups = "X";
				string newSubjects = "X";
				
				// Type
				bool typeExists = ValueExistsAsLong (dbQuestions, path + "/type");
				if (typeExists){
					long newTypeLong = (long) dbQuestions.Child(path + "/type").Value;
					newType = Convert.ToInt32(newTypeLong);
				}
				// Question Text
				bool questionTextExists = ValueExistsAsString (dbQuestions, path + "/question");
				if (questionTextExists){
					newQuestion = (string) dbQuestions.Child(path + "/question").Value;
				}
				// Alternative a)
				bool aExists = ValueExistsAsString (dbQuestions, path + "/alt0");
				if (aExists){
					newAlt0 = (string) dbQuestions.Child(path + "/alt0").Value;
				}
				// Alternative b)
				bool bExists = ValueExistsAsString (dbQuestions, path + "/alt0");
				if (bExists){
					newAlt1 = (string) dbQuestions.Child(path + "/alt1").Value;
				}
				// Alternative c)
				bool cExists = ValueExistsAsString (dbQuestions, path + "/alt0");
				if (cExists){
					newAlt2 = (string) dbQuestions.Child(path + "/alt2").Value;
				}
				// Alternative d)
				bool dExists = ValueExistsAsString (dbQuestions, path + "/alt0");
				if (dExists){
					newAlt3 = (string) dbQuestions.Child(path + "/alt3").Value;
				}
				// Alternative e)
				bool eExists = ValueExistsAsString (dbQuestions, path + "/alt0");
				if (eExists){
					newAlt4 = (string) dbQuestions.Child(path + "/alt4").Value;
				}
				// User Groups
				bool userGroupsExists = ValueExistsAsString (dbQuestions, path + "/userGroups");
				if (userGroupsExists){
					newUserGroups = (string) dbQuestions.Child(path + "/userGroups").Value;
				}
				// Subjetcs
				bool subjectsExists = ValueExistsAsString (dbQuestions, path + "/subjects");
				if (subjectsExists){
					newSubjects = (string) dbQuestions.Child(path + "/subjects").Value;
				}				
				if (hasTarget){
					hasTarget = false;
				}
				SessionScript.questionListPreLoad.Add(new QuestionPreLoad(newIndex, newType, newQuestion, newAlt0, newAlt1, newAlt2, newAlt3, newAlt4, newUserGroups, newSubjects));
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
		SessionScript.getQuestionListNow = true;
		print ("SessionScript.questionListPreLoad.Count = " + SessionScript.questionListPreLoad.Count);
	}
	
	public static void GetOtherPlayers(){
		print ("GetOtherPlayers");
		bool doneUser = false;
		int i = 0;
		int targetI = 20;
		bool hasTargetI = false;
		bool userExists = false;
		string clientString = client.ToString();
		do{
			string pathI = "client_" + clientString + "/user_" + i.ToString() + "/index";
			userExists = ValueExistsAsLong(dbUsers, pathI);
			if (userExists){
				print ("user entry found: " + i);
				if (i != userEntry){
					AddOtherPlayer(i);
				}
				if (hasTargetI){
					hasTargetI = false;
				}
			}
			else {
				print ("user " + i + " does not exist");
				if (!hasTargetI){
					hasTargetI = true;
					targetI = i + 20;
				}
			}
			if (i == targetI){
				doneUser = true;
			}
			i = i + 1;
		} while(!doneUser);
	}
	
	static void AddOtherPlayer(int entry){
		print ("AddOtherPlayer(" + entry + ")");
		string path = "/client_" + client.ToString() + "/user_" + entry.ToString();
		int newId;
		string newName;
		string avatarRawString;
		int newScore;
		bool indexExists = ValueExistsAsLong (dbUsers, path + "/index");
		if (indexExists){
			long newValue = (long) dbUsers.Child(path + "/index").Value;
			newId = Convert.ToInt32(newValue);
		} else return;
		bool nameExists = ValueExistsAsString (dbUsers, path + "/name");
		if (nameExists){
			newName = (string) dbUsers.Child(path + "/name").Value;
		} else return;	
		bool avatarExists = ValueExistsAsString (dbUsers, path + "/avatar");
		if (avatarExists){
			avatarRawString = (string) dbUsers.Child(path + "/avatar").Value;
		} else return;
		bool scoreExists = ValueExistsAsLong (dbUsers, path + "/score");
		if (scoreExists){
			long newValue = (long) dbUsers.Child(path + "/score").Value;
			newScore = Convert.ToInt32(newValue);
		} else return;
		Player newPlayer = new Player(newId, newName, avatarRawString, newScore);
		SessionScript.playerList.Add(newPlayer);
	}
	
	public static bool ValueExistsAsLong(DataSnapshot dbRef, string path){
		// Checks if a value in a node exists and returns true or false;
		bool exists = false;
		try{
			long newValue = (long) dbRef.Child(path).Value;
			if (newValue != null){
				exists = true;
			}
		}
		catch (InvalidCastException exception){
			exists = false;
		}
		catch (NullReferenceException exception){
			exists = false;
		}
		return exists;
	}
	
	public static bool ValueExistsAsString(DataSnapshot dbRef, string path){
		// Checks if a value in a node exists and it's not null and returns true or false;
		bool exists = false;
		try{
			string newValue = (string) dbRef.Child(path).Value;
			if (newValue != null && newValue != ""){
				exists = true;
			}
		}
		catch (InvalidCastException exception){
			print ("InvalidCastException");
			exists = false;
		}
		catch (NullReferenceException exception){
			print ("NullReferenceException");
			exists = false;
		}
		return exists;
	}
	
	// INTERFACE DE TESTE
	
	public void BotaoLogInTeste(){
		Debug.Log ("TESTANDO LOGIN");
		LoginScript loginScript = GameObject.Find("Login").GetComponent<LoginScript>();
		string email = LoginScript.userInput;
		string password = LoginScript.passwordInput;
		SigninWithEmailAsync(email, password);
	}
	
	public void BotaoLogOutTeste(){
		Debug.Log ("TESTANDO LOGOUT");
		SignOut();
	}


}
