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

	public Answer(int newIndex, int newAlternative, bool newRight, bool newTimeout, float newTime){
		index = newIndex;
		alternative = newAlternative;
		right = newRight;
		timeout = newTimeout;
		time = newTime;
	}
	
	public Answer(int newIndex, int newAlternative, float newTime){
		index = newIndex;
		alternative = newAlternative;
		if (newAlternative == 0){
			right = true;
		} else right = false;
		if (newAlternative == 5){
			timeout = true;
		} else timeout = false;
		subject = new List <bool>();
		time = newTime;
	}
	
	public void SetAlternative(int newAlternative){
		alternative = newAlternative;
		if (newAlternative == 0){
			right = true;
		} else right = false;
		if (newAlternative == 5){
			right = false;
			timeout = true;
		}
	}
	
	public void SetTime(float newTime){
		Debug.Log ("NEW TIME: " + newTime);
		time = newTime;
	}
	
//		DESAFIO QUIZ, version alpha 0.7
//		developed by ROCKET PRO GAMES, rocketprogames@gmail.com
//		script by Eduardo Siqueira
//		São Paulo, Brasil, 2019
}

