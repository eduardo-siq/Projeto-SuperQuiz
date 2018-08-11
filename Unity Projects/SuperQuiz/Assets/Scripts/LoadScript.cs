using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScript : MonoBehaviour {
	
	public Slider loadingBar;

	void Start (){
		loadingBar = GameObject.Find("Canvas/LoadingBar").GetComponent<Slider>();
		StartCoroutine(LoadAsynchronously());
	}
	
	IEnumerator LoadAsynchronously (){
		AsyncOperation operation = SceneManager.LoadSceneAsync("login");
		while(!operation.isDone){
			float progress = Mathf.Clamp01(operation.progress/0.9f);	// otherwise, 'progress' would never be 1
			loadingBar.value = progress;
			yield return null;
		}
	}


}
