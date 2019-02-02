using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour{

	bool fadeOutSound;
	
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
}

