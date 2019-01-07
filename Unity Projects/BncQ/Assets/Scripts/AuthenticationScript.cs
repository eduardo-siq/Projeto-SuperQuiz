using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AuthenticationScript : MonoBehaviour {
	
	public static AuthenticationScript authentication = null;
	
	// Scripts
	DatabaseScript databaseScript;
	LoginScript loginScript;
	
	//UI Elements
	string userInput;
	string passwordInput;
	
	// User Data
	public static string userName;

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
	
	void Awake(){
        if (authentication == null){
            authentication = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            if (authentication != this){
                Destroy(this);
            }
        }
    }	
	 
	public virtual void Start(){
		databaseScript = this.GetComponent<DatabaseScript>();
		loginScript = GameObject.Find("Canvas").GetComponent<LoginScript>();
		
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
		} else{
			LoginScript.loginFail = true;	// Same, for failure
		}
	}
	
	void HandleSignInWithUser(Task<Firebase.Auth.FirebaseUser> task) {	// Called when a sign-in without fetching profile data completes.
	// EnableUI();
		if (LogTaskCompletion(task, "Sign-in (HandleSignInWithUser)")) {
			Debug.Log(String.Format("{0} signed in", task.Result.DisplayName));
			LoginScript.loginSucess = true;	// If login is successful, LoginScript moves on with game
		} else{
			LoginScript.loginFail = true;	// If not, resets login screen
		}
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
			LoginScript.loginFail = true;
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
	
	public void UserInput(string input){
		userInput = input;
	}
	
	public void PasswordInput(string input){
		passwordInput = input;
	}
	
	public void LoginButton(){
		SetUserName();	// Sets user name for public chat
		SigninWithEmailAsync(userInput, passwordInput);
		LoginScript.startLogin = true;	
	}
	
	void SetUserName(){
		// check if there are local user files. if not, use email as userName
		string[] emailSplit;
		string[] space = new string[] { "@" };
		emailSplit = userInput.Split(space, System.StringSplitOptions.None);
		userName = emailSplit[0];
	}
	
	
	
	// INTERFACE DE TESTE
	


}
