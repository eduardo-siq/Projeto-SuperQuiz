using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientListenerHolderScript {
	
	public DatabaseReference refClient;
	public bool getSnapshot;
	
	public void StartListener() {
		refClient = FirebaseDatabase.DefaultInstance.GetReference("clients/client_" + AuthenticationScript.client.ToString());
		getSnapshot = true;
		refClient.ValueChanged += (object sender, ValueChangedEventArgs e2) => {
			if (e2.DatabaseError != null) {
				Debug.LogError(e2.DatabaseError.Message);
				return;
			}
			if (e2.Snapshot != null || e2.Snapshot.Value != null){
				if (getSnapshot){
					AuthenticationScript.dbClient = e2.Snapshot;
					getSnapshot = false;
					AuthenticationScript.GetClientInfoFromSnapshot();
				}
				string newString = (string) e2.Snapshot.Value;	// REMOVE LATER
			}
		};
	}

//		DESAFIO QUIZ, version alpha 0.6
//		developed by ROCKET PRO GAMES, rocketprogames@gmail.com
//		script by Eduardo Siqueira
//		São Paulo, Brasil, 2019
}
