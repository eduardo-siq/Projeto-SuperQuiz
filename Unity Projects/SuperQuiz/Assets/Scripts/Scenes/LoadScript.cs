using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScript : MonoBehaviour{

	//public Slider loadingBar;
	public Image bar;

	public float fillBar;
	public float maxProgress;
	
	public static bool loadedAvatarResources = false;
	public static bool loadedSoundResources = true;
	public static bool loading = false;
	
	// Restriction
	public bool checkRestriction;
	bool lockGame;
	public LoadScriptLimited restrictions;

	void Start(){
		maxProgress = 0.25f;
		fillBar = 0;
	
		// loadingBar = GameObject.Find("Canvas/LoadingBar").GetComponent<Slider>();
		bar = GameObject.Find("Canvas/Scroll View/Viewport/Load/LoadingBar2/Fill").GetComponent<Image>();
		// Invoke("StartLoading", 0.5f);	// Process started by SessionScript
		
		if (checkRestriction){
			restrictions = GetComponent<LoadScriptLimited>();
			lockGame = restrictions.CheckRestriction();
		}
	}

	void Update(){
		fillBar = Mathf.Clamp01(fillBar + Time.deltaTime);
		if (!loading){
			if (fillBar > 0.5f) fillBar = 0.5f;
		}
		// loadingBar.value = fillBar;
		bar.fillAmount = fillBar;
		
		if (loadedSoundResources && loadedAvatarResources && !loading){
			loading = true;
			StartCoroutine(LoadAsynchronously());
		}
	}

	IEnumerator LoadAsynchronously(){
		print ("LoadAsynchronously");
		if (!lockGame){
			AsyncOperation operation = SceneManager.LoadSceneAsync("login");
			while (!operation.isDone){
				if (operation.progress > 0.5f && fillBar < 0.5f){
					fillBar = 0.5f;
				}
				if (operation.progress > 0.75f && fillBar < 0.75f){
					fillBar = 0.75f;
				}
				if (operation.progress > 0.95f && fillBar < 0.95f){
					fillBar = 0.995f;
				}
				// float progress = Mathf.Clamp01(operation.progress / 0.9f);  // otherwise, 'progress' would never be 1
				// maxProgress = Mathf.Clamp(maxProgress + Time.deltaTime * 5, 0f, progress);
				yield return null;
			}
		} if (lockGame){
			GameObject.Find("Canvas/Scroll View/Viewport/Load/LoadingBar2").SetActive(false);
			PopUpScript.InstantiatePopUp("O prazo de acesso dessa demo expirou", "OK");
		}
	}
//		DESAFIO QUIZ, version alpha 0.6
//		developed by ROCKET PRO GAMES, rocketprogames@gmail.com
//		script by Eduardo Siqueira
//		São Paulo, Brasil, 2019
}
