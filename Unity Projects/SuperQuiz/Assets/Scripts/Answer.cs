using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer {
	
	public int index;
	public bool right;
	public bool timeout;
	public List <bool> subject;
	public float time;
	
	public Answer (){
		index = 0;
		right = false;
		timeout = false;
		subject = new List <bool>();
		time = 0f;
	}
	
	public Answer(int newIndex, bool newRight, bool newTimeout, List <bool> newSubject, float newTime){
		index = newIndex;
		right = newRight;
		timeout = newTimeout;
		subject = new List <bool>();
		subject = newSubject;
		time = newTime;
	}
}

