using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResizeUIScript : MonoBehaviour{

	public static bool animOn;
	public static RectTransform rect;
	public static float size;

	void Start(){
		rect = this.gameObject.GetComponent<RectTransform>();
		animOn = false;
		if (SessionScript.resizeToSmall){
			rect.localScale = new Vector3 (0.9f, 0.9f, 0.9f);
			size = 0.9f;
		} else {
			size = 1f;
		}
	}
	
	void Update(){
		if (animOn && SessionScript.resizeToSmall){
			size = size - Time.deltaTime * 0.1f;
			if (size <= 0.9f){
				rect.localScale = new Vector3 (0.9f, 0.9f, 0.9f);
				animOn = false;
				size = 0.9f;
			}
			rect.localScale = new Vector3 (size, size, size);
		}
		if (animOn && !SessionScript.resizeToSmall){
			size = size + Time.deltaTime * 0.1f;
			if (size >= 1f){
				rect.localScale = new Vector3 (1f, 1f, 1f);
				animOn = false;
				size = 1f;
			}
			rect.localScale = new Vector3(size, size, size);
		}
	}
	
	public static void Resize(){
		animOn = true;
	}
    
}
