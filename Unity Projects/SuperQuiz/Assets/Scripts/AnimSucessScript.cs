using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimSucessScript : MonoBehaviour  {

	float amountCircle = 0;
	float amountSymbol = 0;
	public Image animationCircle;
	public Image animationSymbol;
	bool circle = false;
	bool symbol = false;

	void Start(){
		print ("START ANIM");
		animationCircle = this.gameObject.transform.Find ("Circle").GetComponent<Image>();
		animationSymbol = this.gameObject.transform.Find ("Symbol").GetComponent<Image>();
		animationCircle.fillAmount = amountCircle;
		animationSymbol.fillAmount = amountSymbol;
		Invoke ("Circle", 0.1f);
		Invoke ("Symbol", 0.75f);
	}

	void Update(){
		if (circle){
			amountCircle = amountCircle + Time.deltaTime * 1.25f;
			animationCircle.fillAmount = amountCircle;
		}
		if (amountCircle >= 1){
			circle = false;
		}
		if (symbol){
			amountSymbol = amountSymbol + Time.deltaTime * 1.25f;
			animationSymbol.fillAmount = amountSymbol;
		}
		if (amountSymbol >= 1){
			symbol = false;
		}
	}
	
	void Circle(){
		circle = true;
	}
	
	void Symbol(){
		symbol = true;
	}	
}

