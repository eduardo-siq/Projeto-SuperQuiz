using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingWindow : MonoBehaviour {

	public GameObject content;
	public AvatarPortrait portrait;
	public Text rankText;
	public Text nameText;
	public Text infoText;
	public Text scoreText;
	
	public void OpenWindow(Avatar newAvatar, int newRank, string newName, string newInfo, int newScore){
		content.SetActive(true);
		portrait.SpecificAvatar(newAvatar);
		rankText.text = "#" + newRank.ToString();
		nameText.text = "nome: " + newName;
		infoText.text = ""; // infoText.text = "setor: " + newInfo;
		scoreText.text = "pontuação: " + newScore.ToString();
	}
	
	public void ExitWindow(){
		content.SetActive(false);
		SessionScript.ButtonAudio(SessionScript.subtle);
	}
	
//		DESAFIO QUIZ, version alpha 0.6
//		developed by ROCKET PRO GAMES, rocketprogames@gmail.com
//		script by Eduardo Siqueira
//		São Paulo, Brasil, 2019
}
