using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseScript : MonoBehaviour {
	
	public DatabaseReference dbRoot;
	public DatabaseReference dbTest1;
	public DatabaseReference dbTest2;
	//public ValueEventListener dbvel1;
	
	
	string testString = "";
	string testString2 = "";
	
	float timer = 0;
	bool setValues = false;
	
	void Start(){
		int dice = UnityEngine.Random.Range(0, 5);
		if (dice == 0){ testString = "These are not the droids we are looking for."; testString2 = "Star Wars";}
		if (dice == 1){ testString = "Say 'hello' to my little friend!"; testString2 = "Scarface";}
		if (dice == 2){ testString = "I will strike down upon thee with great vengeance and furious anger!"; testString2 = "Pulp Fiction";}
		if (dice == 3){ testString = "Get to the chopper!"; testString2 = "Predator";}
		if (dice == 4){ testString = "Quiet an experience to live in fear, isn't it? That's what it is to be a slave."; testString2 = "Blade Runner";}
		print ("DatabaseScript: " + testString);
		
		// dbRoot = FirebaseDatabase.DefaultInstance.GetReference("");	// set by timer in Update
		// dbTest1 = dbRoot.Child("test1");
		// dbTest2 = dbRoot.Child("test2");
		
		// Add event listeners to references
		
		// dbTest1.SetValueAsync(testString);	// set by timer in Update
		// dbTest2.SetValueAsync(testString2);
		
		// Use event listeners to retrieve data from firebase
		
	}
	
	void Update(){
		if (!setValues){
			timer = timer + Time.deltaTime;
			if (timer > 1){
				dbRoot = FirebaseDatabase.DefaultInstance.GetReference("");
				dbTest1 = dbRoot.Child("test1");
				dbTest2 = dbRoot.Child("test2");
				dbTest1.SetValueAsync(testString);
				dbTest2.SetValueAsync(testString2);
				setValues = true;
				TestSetValue();
			}
		}
	}
	
	void TestSetValue(){
		int dice = UnityEngine.Random.Range(0,4);
		print ("TEST SET VALUE: dice = " + dice);
		dbRoot.Child("movie0" + dice.ToString() + "/phrase").SetValueAsync(testString);
		dbRoot.Child("movie0" + dice.ToString() + "/title").SetValueAsync(testString2);
	}
	

}