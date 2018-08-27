using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour  {
	
	public void Add15Answers(){
		SessionScript.ButtonAudio(SessionScript.positive);
		SessionScript.answersList = new List <Answer>();
		List <bool> sub = new List <bool>();
		sub.Add(true);
		for (int i = 0; i < 15; i++){
			SessionScript.answersList.Add(new Answer(i, true, false, sub, 1f));
		}
		print ("SessionScript.answersList: " + SessionScript.answersList.Count);
	}
	
}