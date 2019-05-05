using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserListenerHolderScript {
	
	public DatabaseReference refUser;
	public bool getSnapshot;
	
	public void StartListener() {
		refUser = FirebaseDatabase.DefaultInstance.GetReference("users");
		getSnapshot = true;
		refUser.ValueChanged += (object sender, ValueChangedEventArgs e2) => {
			if (e2.DatabaseError != null) {
				Debug.LogError(e2.DatabaseError.Message);
				return;
			}
			if (e2.Snapshot != null || e2.Snapshot.Value != null){
				if (getSnapshot){
					AuthenticationScript.dbUsers = e2.Snapshot;
					getSnapshot = false;
					AuthenticationScript.FindThisUser();
				}
			}
		};
	}

	
//		DESAFIO QUIZ, version alpha 0.7
//		developed by ROCKET PRO GAMES, rocketprogames@gmail.com
//		script by Eduardo Siqueira
//		São Paulo, Brasil, 2019
}
