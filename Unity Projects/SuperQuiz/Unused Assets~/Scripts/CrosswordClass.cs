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
	
//		DESAFIO QUIZ, version alpha 0.5
//		developed by ROCKET PRO GAMES, rocketprogames@gmail.com
//		script by Eduardo Siqueira
//		São Paulo, Brasil, 2019
}