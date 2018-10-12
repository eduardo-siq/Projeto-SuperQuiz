using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Crossword {
	
	public List<string> characters;
	public string text;
	
	public Crossword (){
		characters = new List<string> ();
	}
	
	public Crossword (string word){
		characters = new List<string> ();
		for (int i = 0; i < word.Length; i++){
			characters.Add(word[i]);
		}
	}
	
	public Crossword (string word, string newText){
		characters = new List<string> ();
		for (int i = 0; i < word.Length; i++){
			characters.Add(word[i]);
			text = newText;
		}
	}
}