using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAnswer {
	
	public int index;
	public float red;
	public float green;
	public float blue;	// merge as color?
	public bool right;
	public bool pointed;
	public Detail detail;	// obsolete?
	public string text;
	
	public PointAnswer(){
		index = 0;
		red = 0;
		green = 0;
		blue = 0;
		right = false;
	}
	
	public PointAnswer(int newIndex, float newRed, float newGreen, float newBlue, bool newRight){
		index = newIndex;
		red = newRed;
		green = newGreen;
		blue = newBlue;
		right = newRight;
		pointed = false;
		detail = new Detail (newRed, newBlue, newGreen);
	}
	
	public PointAnswer(string questionString){
		if (questionString == ""){
			red = 0;
			green = 0;
			blue = 0;
			right = true;
			pointed = false;
			return;
		}
		string[] question;
		string[] space = new string[] { "_" };
		question = questionString.Split(space, System.StringSplitOptions.None);
		red = float.Parse(question[0]);
		green = float.Parse(question[1]);
		blue = float.Parse(question[2]);
		text = question[3];
		detail = new Detail(red, green, blue);
		if (red == 0 && green == 0 && blue == 0){
			right = true;
		} else {
			right = false;
		}
	} 
	
	public void GetFromQuestion(string questionString){
		string[] question;
		string[] space = new string[] { "_" };
		question = questionString.Split(space, System.StringSplitOptions.None);
		red = float.Parse(question[0]);
		green = float.Parse(question[1]);
		blue = float.Parse(question[2]);
		text = question[3];
		detail = new Detail(red, green, blue);
	}
}

