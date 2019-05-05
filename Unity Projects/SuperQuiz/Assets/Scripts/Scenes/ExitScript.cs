using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour{

	void Start(){
		if(SessionScript.soundOn){
			SessionScript.fadeOutSong = true;
		}
		if (Application.platform == RuntimePlatform.Android){
			Invoke ("KillApplication", 0.5f);
		} else{
			GameObject.Find("Canvas/Scroll View/Viewport/Exit/Message").gameObject.SetActive(true);
			Invoke ("QuitApplication", 1f);
		}
	}
	
	
	void KillApplication(){
		System.Diagnostics.Process.GetCurrentProcess().Kill();
	}
	
	void QuitApplication(){
		Application.Quit();
	}
		
//		DESAFIO QUIZ, version alpha 0.7
//		developed by ROCKET PRO GAMES, rocketprogames@gmail.com
//		script by Eduardo Siqueira
//		São Paulo, Brasil, 2019
}