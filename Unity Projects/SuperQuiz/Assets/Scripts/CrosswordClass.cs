using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Crossword {
	
	public List<string> characters;
	public string text;
	
	public Crossword (){
		characters = new List<string> ();
	}
	
	public Crossword (string word){
		Debug.Log ("Add word: " + word);
		characters = new List<string> ();
		for (int i = 0; i < word.Length; i++){
			characters.Add(Convert.ToString(word[i]));
			Debug.Log (characters[i]);
		}
	}
	
	public Crossword (string word, string newText){
		Debug.Log ("Add word: " + word);
		characters = new List<string> ();
		for (int i = 0; i < word.Length; i++){
			characters.Add(Convert.ToString(word[i]));
			text = newText;
			Debug.Log (characters[i]);
		}
	}
}