using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionListenerHolderScript {
	
	public DatabaseReference refQuestion;
	public bool getSnapshot;
	
	public void StartListener() {
		refQuestion = FirebaseDatabase.DefaultInstance.GetReference("questions/client_" + AuthenticationScript.client.ToString());
		getSnapshot = true;
		refQuestion.ValueChanged += (object sender, ValueChangedEventArgs e2) => {
			if (e2.DatabaseError != null) {
				Debug.LogError(e2.DatabaseError.Message);
				return;
			}
			if (e2.Snapshot != null || e2.Snapshot.Value != null){
				if (getSnapshot){
					AuthenticationScript.dbQuestions = e2.Snapshot;
					getSnapshot = false;
					AuthenticationScript.GetQuestionsFromSnapshot();
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
