using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseScript : MonoBehaviour {
	
	public DatabaseReference dbTest1;
	public DatabaseReference dbTest2;
	//public ValueEventListener dbvel1;
	
	
	string testString = "";
	string testString2 = "";
	
	float timer = 0;
	bool setValues = false;
	
	// TEST
	int dice1;
	int dice2;
	DataSnapshot dbSnapshot;
	
	// Database variables
	public DatabaseReference dbRoot;
	
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
		//Invoke("TestDatabase", 0.99f);
		 Invoke("TestDatabase", 3f);
		 Invoke("TestDatabase", 6f);
		 Invoke("TestDatabase", 9f);
		
	}
	
	void Update(){
	}
	
	void SetRerefences(){
		dbRoot = FirebaseDatabase.DefaultInstance.GetReference("");
		dbTest1 = dbRoot.Child("test1");
		dbTest2 = dbRoot.Child("test2");
	}
	
	void TestDatabase(){
		dice2 = UnityEngine.Random.Range(0,9);
		dbRoot.Child("movie0" + dice2.ToString() + "/phrase").SetValueAsync(testString);
		dbRoot.Child("movie0" + dice2.ToString() + "/title").SetValueAsync(testString2);
		dbTest1.SetValueAsync(testString);
		dbTest2.SetValueAsync(testString2);
		dbTest1.SetValueAsync(testString);
		dbTest2.SetValueAsync(testString2);		
		print ("TestDatabase(): " + dbTest1.GetValueAsync().ToString());
		//
		if (dbSnapshot.HasChild("test1")) print ("dbSnapshot.HasChild('test1')"); else print ("!dbSnapshot.HasChild('test1')");
		if (dbSnapshot.HasChild("test2")) print ("dbSnapshot.HasChild('test2')"); else print ("!dbSnapshot.HasChild('test2')");
		if (dbSnapshot.HasChild("test3")) print ("dbSnapshot.HasChild('test3')"); else print ("!dbSnapshot.HasChild('test3')");
		
		/*
		print ("TEST SET VALUE: dice = " + dice2 + " - (" + Time.deltaTime.ToString() + ")");
		bool exists = false;		
	 
	  
  
  
		if (exists){
			print ("movie0" + dice2.ToString() + " exists - (" + Time.deltaTime.ToString() + ")");
			dbRoot.Child("movie0" + dice2.ToString() + "/phrase").SetValueAsync(testString);
			dbRoot.Child("movie0" + dice2.ToString() + "/title").SetValueAsync(testString2);
			dbTest1.SetValueAsync(testString);
			dbTest2.SetValueAsync(testString2);
		} else{
			print ("movie0" + dice2.ToString() + " does not exist - (" + Time.deltaTime.ToString() + ")");
			dbTest1.SetValueAsync(testString);
			dbTest2.SetValueAsync(testString2);
		}
		*/
	}

	
	protected void StartListener() {
		print ("StartListener()");
		dbRoot.ValueChanged += (
		object sender2, ValueChangedEventArgs e2) => { if (e2.DatabaseError != null) {
			Debug.LogError(e2.DatabaseError.Message);
			return;
		}
		Debug.Log("ValueChangedEventArgs");
		if (e2.Snapshot != null || e2.Snapshot.Value != null){
			dbSnapshot = e2.Snapshot;
			string newString = (string) e2.Snapshot.Value;
			print ("DATABASE TEST: " + newString);
		}
		// if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0) {
			// foreach (var childSnapshot in e2.Snapshot.Children) {
				// if (childSnapshot.Child("score") == null || childSnapshot.Child("score").Value == null) {
					// Debug.LogError("Bad data in sample.  Did you forget to call SetEditorDatabaseUrl with your project id?");
					// break;
				// } else {
				// title = childSnapshot.Child("score").Value.ToString() + "  " + childSnapshot.Child("email").Value.ToString();
				// print ("DATABASE TEST: " + title);
				// }
			// }
		// }
		};
	}
	
	public static void CheckAllClients(){
		print ("CheckAllClients()");
	}
	

}