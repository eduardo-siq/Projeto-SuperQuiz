using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayCrosswordScript : MonoBehaviour{

	// UI
	
	// Crossword
	public List <Crossword> crossword;
	
	// Other
	public bool getBuiltInCrossword;
	
	
	void Start(){
		StartCoroutine(StartScene());
	}

	// void Update(){
		
	// }

	IEnumerator StartScene(){
		
		if (getBuiltInCrossword) GetBuiltInCrossword();
	}
	
	GetBuiltInCrossword(){
		BuiltInCrossword.GetCrossword();
	}
}

