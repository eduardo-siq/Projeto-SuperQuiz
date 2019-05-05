using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultLine : MonoBehaviour{

	public int id;
	public int rank;
	
	public void SelectThisLine(){
		ResultScript.SelectResultLine(id, rank);
	}
	
//		DESAFIO QUIZ, version alpha 0.7
//		developed by ROCKET PRO GAMES, rocketprogames@gmail.com
//		script by Eduardo Siqueira
//		São Paulo, Brasil, 2019
}

