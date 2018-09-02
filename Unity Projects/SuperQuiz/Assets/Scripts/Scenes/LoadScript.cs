using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScript : MonoBehaviour {
	
	public Slider loadingBar;
	
	public float fillBar;
	public float maxProgress;

	void Start (){
		maxProgress = 0.25f;
		fillBar = 0;
		
		loadingBar = GameObject.Find("Canvas/LoadingBar").GetComponent<Slider>();
		Invoke ("StartLoading", maxProgress - 0.5f);
	}
	
	void StartLoading(){
		StartCoroutine(LoadAsynchronously());
	}
	
	void Update(){
		fillBar = Mathf.Clamp(fillBar + Time.deltaTime, 0f, maxProgress);
		loadingBar.value = fillBar;
	}
	
	IEnumerator LoadAsynchronously (){
		AsyncOperation operation = SceneManager.LoadSceneAsync("login");
		while(!operation.isDone){
			float progress = Mathf.Clamp01(operation.progress/0.9f);	// otherwise, 'progress' would never be 1
			maxProgress = Mathf.Clamp(maxProgress + Time.deltaTime * 5, 0f, progress);
			yield return null;
		}
	}


}
