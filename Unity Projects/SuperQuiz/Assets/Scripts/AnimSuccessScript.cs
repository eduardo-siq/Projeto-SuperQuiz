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
	
	void Update(){
		if (fadeIn){
			if (alpha < 0.5){
				alpha = shadowImage.color.a + Time.deltaTime / 2;
				shadowImage.color = new Color(0f, 0f, 0f, alpha);
			}
		}
		if (alpha > 0.5){
			fadeIn = false;
		}
	}

	public void PlayAnimation(){
		alpha = 0;
		shadowImage.color = new Color(0f, 0f, 0f, alpha);
		fadeIn = true;
		Invoke("Animation", 0.25f);
		Invoke("Reset", 2.25f);
	}
	
	void Animation(){
		sucessAnimation.SetActive(true);
		shadow.SetActive(true);
	}

	void Reset(){
		sucessAnimation.SetActive(false);
		shadow.SetActive(false);
	}
}

