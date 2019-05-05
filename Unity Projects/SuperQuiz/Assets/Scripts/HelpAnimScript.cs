using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpAnimScript : MonoBehaviour{

	public bool animOn;
	public bool openHelp;
	
	public Text resize;
	public Text play;
	public Text avatar;
	public Text score;
	
	float alpha;
	Color color;

	void Start(){
		animOn = false;
		openHelp = false;
		alpha = 0f;
    }
	
	void Update(){
		if (animOn && !openHelp){
			alpha = alpha - Time.deltaTime;
			if (alpha <= 0f){
				animOn = false;
				alpha = 0;
			}
			color = new Color (1f, 1f, 1f, alpha);
			resize.color = color;
			play.color = color;
			avatar.color = color;
			score.color = color;
		}
		if (animOn && openHelp){
			alpha = alpha + Time.deltaTime;
			if (alpha >= 1f){
				animOn = false;
				alpha = 1;
			}
			color = new Color (1f, 1f, 1f, alpha);
			resize.color = color;
			play.color = color;
			avatar.color = color;
			score.color = color;
		}
	}
	
	public void ToggleHelp(){
		animOn = true;
		openHelp = !openHelp;
	}
	
	public void SelectHelp(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		Invoke("ToggleHelp", 0.25f);
	}
}
