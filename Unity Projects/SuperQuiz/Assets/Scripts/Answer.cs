using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer {
	
	public int index;
	public int alternative;	// 0-4: regular alternative, 5: timeout
	public bool right;
	public bool timeout;	// maybe obsolete
	public List <bool> subject;
	public float time;
	
	public Answer (){
		index = 0;
		right = false;
		alternative = 0;
		timeout = false;
		subject = new List <bool>();
		time = 0f;
	}
	
	public Answer(int newIndex, int newAlternative, bool newRight, bool newTimeout, List <bool> newSubject, float newTime){
		index = newIndex;
		alternative = newAlternative;
		right = newRight;
		timeout = newTimeout;
		subject = new List <bool>();
		subject = newSubject;
		time = newTime;
	}
	
	public Answer(int newIndex, int newAlternative, float newTime){
		index = newIndex;
		alternative = newAlternative;
		if (newAlternative == 0){
		right = true;
		} else right = false;
		timeout = false;
		subject = new List <bool>();
		time = newTime;
	}
}

