using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {

	GameObject unity;
	GameObject background;
	RawImage riUnity;
	RawImage riBackground;
	bool fadeOut;
	float f = 0;
	
	void Start () {
		fadeOut = AuthenticationScript.fadeOut;
		if (fadeOut){
			unity = this.gameObject.transform.Find("Unity Logo").gameObject;
			background = this.gameObject.transform.Find("Background").gameObject;
			background.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
			riUnity = unity.GetComponent<RawImage>();
			riBackground = background.GetComponent<RawImage>();
			AuthenticationScript.fadeOut = false;
		} else{
			background = this.gameObject.transform.Find("Background").gameObject;
			background.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 0f);
			Destroy(this.gameObject);
		}
	}
	
	void Update () {
		if (fadeOut){
			f = f + Time.deltaTime/1.5f;
			riBackground.color = new Color (1,1,1,1-f);
			riUnity.color = new Color (1,1,1,1-f);
			if (f >= 1) Destroy(this.gameObject);
		}
	}
}
