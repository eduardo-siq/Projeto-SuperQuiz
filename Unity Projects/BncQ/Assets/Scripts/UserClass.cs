using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User {
	
	public int index;
	public string name;
	public string email;
	// public string password;
	public string telephone;
	public string cpf;
	// public string departament;
	public string localization;
	public bool firstLogin;
	public string answers;
	public string time;
	public int score;
	public List <bool> userGroups;
	public bool edited;
	
	public User(){
		index = 0;
		name = "";
		email = "";
		// password = "";
		telephone = "";
		cpf = "";
		// departament = "";
		localization = "";
		firstLogin = true;
		answers = "";
		time = "";
		score = 0;
		userGroups = new List <bool>();
		edited = false;
	}
	
	public User (int newIndex, string newName, string newEmail, /*string newPassword, */string newTelephone, string newCpf, /*string newDepartament, */string newLocalization, bool newFirstLogin, string newAnswers, string newTime, int newScore){
		index = newIndex;
		name = newName;
		email = newEmail;
		// password = newPassword;
		telephone = newTelephone;
		cpf = newCpf;
		// departament = newDepartament;
		localization = newLocalization;
		firstLogin = newFirstLogin;
		answers = newAnswers;
		time = newTime;
		score = newScore;
		userGroups = new List <bool>();
		//userGroups = newUserGroups;
		edited = false;
	}

	public User (int newIndex, string newName, string newEmail, /*string newPassword, */string newTelephone, string newCpf, /*string newDepartament, */string newLocalization, bool newFirstLogin, string newAnswers, string newTime, int newScore, List<bool> newUserGroups){
		index = newIndex;
		name = newName;
		email = newEmail;
		// password = newPassword;
		telephone = newTelephone;
		cpf = newCpf;
		// departament = newDepartament;
		localization = newLocalization;
		firstLogin = newFirstLogin;
		answers = newAnswers;
		time = newTime;
		score = newScore; 
		userGroups = new List <bool>();
		userGroups = newUserGroups;
		edited = false;
	}
	
	public User (int newIndex, string newName, string newEmail, /*string newPassword, */string newTelephone, string newCpf, /*string newDepartament, */string newLocalization, bool newFirstLogin, string newAnswers, string newTime, int newScore, List<bool> newUserGroups, bool newEdit){
		index = newIndex;
		name = newName;
		email = newEmail;
		// password = newPassword;
		telephone = newTelephone;
		cpf = newCpf;
		// departament = newDepartament;
		localization = newLocalization;
		firstLogin = newFirstLogin;
		answers = newAnswers;
		time = newTime;
		score = newScore;
		userGroups = new List <bool>();
		userGroups = newUserGroups;
		edited = newEdit;
	}
	
	public void ClientSpecification (List<bool> newUserGroups){
		userGroups = new List <bool>();
		userGroups = newUserGroups;
	}
	
	public User (User baseUser){
		index = baseUser.index;
		name = baseUser.name;
		email = baseUser.email;
		// password = baseUser.password;
		telephone = baseUser.telephone;
		cpf = baseUser.cpf;
		// departament = baseUser.departament;
		localization = baseUser.localization;
		firstLogin = baseUser.firstLogin;
		answers = baseUser.answers;
		time = baseUser.time;
		score = baseUser.score;
		userGroups = new List <bool>();
		edited = false;
	}
	
	public void LoadUserGroups (string userGroupsString){
		
	}
	
	public string GetUserGroupString(){
		string userGroupString = "";
		if (userGroups.Count == 0 || userGroups == null){
			userGroupString = "X";
		}else{
			for (int y = 0; y < userGroups.Count; y++){
				if (y != 0){
					userGroupString = userGroupString + " ";
				}
				if (userGroups[y]){
					userGroupString = userGroupString + "T";
				}
				if (!userGroups[y]){
					userGroupString = userGroupString + "F";
				}
			}
		}
		return userGroupString;
	}

}

public class UserButton {
	
	public User user;
	public GameObject button;
	
	public UserButton (User newUser, GameObject newButton){
		user = newUser;
		button = newButton;
	}
	
	public UserButton (UserButton baseUserButton){
		user = baseUserButton.user;
		button = baseUserButton.button;
	}
}

