using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LoadScriptLimited : MonoBehaviour {
	
	// Restriction
	public float limitYear;
	public float limitMonth;
	public float limitDay;
	public float minYear;
	public float minMonth;
	public float minDay;

	
	public bool CheckRestriction(){
		bool lockGame = false;
		string currentDate = DateTime.Now.ToString("yyyy/MM/dd");
		string[] space = new string[] { "/" };
		string[] currentDateArray = currentDate.Split(space, StringSplitOptions.None);
		float currentYear = float.Parse(currentDateArray[0]);
		float currentMonth = float.Parse(currentDateArray[1]);
		float currentDay = float.Parse(currentDateArray[2]);

		// if (currentYear > limitYear) lockGame = true;
		// if (currentMonth > limitMonth) lockGame = true;
		// if (currentDay > limitDay) lockGame = true;
		// if (currentYear < minYear) lockGame = true;
		// if (currentMonth < minMonth) lockGame = true;
		// if (currentDay < minDay) lockGame = true;

		bool done = false;
		if (currentYear < minYear){
			lockGame = true;
			done = true;
		}
		if (currentYear > limitYear){
			lockGame = true;
			done = true;
		}
		if (!done){
			if (currentYear == minYear){
				if (currentMonth < minMonth){
					lockGame = true;
					done = true;
				}
				if (currentMonth == minMonth){
					if (currentDay < minDay){
						lockGame = true;
						done = true;
					}
				}
			}
		}
		if (!done){
			if (currentYear == limitYear){
				if (currentMonth > limitMonth){
					lockGame = true;
					done = true;
				}
				if (currentMonth == limitMonth){
					if (currentDay > limitDay){
						lockGame = true;
						done = true;
					}
				}
			}
		}


		Debug.Log ("lock = " + lockGame);
		return lockGame;
	}
	
//		DESAFIO QUIZ, version alpha 0.6
//		developed by ROCKET PRO GAMES, rocketprogames@gmail.com
//		script by Eduardo Siqueira
//		São Paulo, Brasil, 2019
}