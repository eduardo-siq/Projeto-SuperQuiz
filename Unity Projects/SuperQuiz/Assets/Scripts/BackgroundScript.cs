using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScript : MonoBehaviour {

	public GameObject spin;
	public float spinSpeed;
	
	void Update () {
		spin.transform.Rotate(Vector3.forward * Time.deltaTime * (- spinSpeed));
	}
	
//		DESAFIO QUIZ, version alpha 0.6
//		developed by ROCKET PRO GAMES, rocketprogames@gmail.com
//		script by Eduardo Siqueira
//		São Paulo, Brasil, 2019
}
