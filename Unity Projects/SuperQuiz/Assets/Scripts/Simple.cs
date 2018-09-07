using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple : MonoBehaviour {

	public static void SimpleText(string text){
		print ("TEST REMOVE DIACRITIC");
		// string sentence = "Roma";
		// sentence = sentence.Remove(0,1);
		// sentence = sentence.Insert(0,"C");
		// print (sentence);
		
		string character = "";
		for (int i = 0; i < text.Length; i++){
			character.Substring(i, i + 1);
			if (character == "à"){
				text = text.Remove(i, i + 1);
				text = text.Insert(0,"a");
			}
		}
		print (text);
	}
}
