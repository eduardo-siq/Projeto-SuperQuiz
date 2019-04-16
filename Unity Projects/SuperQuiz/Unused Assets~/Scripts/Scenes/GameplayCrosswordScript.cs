using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayCrosswordScript : MonoBehaviour{

	// UI
	
	// Crossword
	public List <Crossword> crossword;
	
	// Prefab
	public GameObject crosswordLine;
	public GameObject crosswordCharacter;
	
	// Other
	public bool getBuiltInCrossword;
	
	
	void Start(){
		StartCoroutine(StartScene());
	}

	// void Update(){
		
	// }

	IEnumerator StartScene(){
		yield return null;
		// UI
		
		// Crossword
		crossword = new List <Crossword>();
		
		// Prefab
		crosswordLine = Resources.Load("Prefabs/CrosswordLine") as GameObject;
		crosswordCharacter = Resources.Load("Prefabs/CrosswordCharacter") as GameObject;

		// Other
		
		// Methods
		if (getBuiltInCrossword) GetBuiltInCrossword();
		InstantiateLines();
	}
	
	public void InstantiateLines(){
		
	}
	
	public void GetBuiltInCrossword(){
		crossword = BuiltInCrossword.GetCrossword();
	}
}

