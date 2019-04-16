using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimSuccessScript : MonoBehaviour{

	public GameObject sucessAnimation;
	public GameObject shadow;
	public RawImage shadowImage;
	
	float alpha;
	bool fadeIn;
	bool fadeOut;
	
	void Update(){
		if (fadeIn){
			if (alpha < 0.5f){
				alpha = shadowImage.color.a + Time.deltaTime;
				shadowImage.color = new Color(0f, 0f, 0f, alpha);
			}
		}
		if (alpha > 0.5f){
			fadeIn = false;
		}
		if (fadeOut){
			if (alpha > 0.0f){
				alpha = shadowImage.color.a - Time.deltaTime;
				shadowImage.color = new Color(0f, 0f, 0f, alpha);
			}
		}
		if (alpha <= 0.0f){
			fadeOut = false;
		}
	}

	public void PlayAnimation(){
		alpha = 0;
		shadowImage.color = new Color(0f, 0f, 0f, alpha);
		fadeIn = true;
		Invoke("Animation", 0.25f);
		Invoke("FadeOut", 2f);
		Invoke("Reset", 2.25f);
		Invoke("ResetShadow", 2.5f);
	}
	
	void Animation(){
		sucessAnimation.SetActive(true);
		shadow.SetActive(true);
	}
	
	void FadeOut(){
		fadeOut = true;
	}

	void Reset(){
		sucessAnimation.SetActive(false);
	}
	
	void ResetShadow(){
		shadow.SetActive(false);
	}
	
//		DESAFIO QUIZ, version alpha 0.6
//		developed by ROCKET PRO GAMES, rocketprogames@gmail.com
//		script by Eduardo Siqueira
//		São Paulo, Brasil, 2019
}

