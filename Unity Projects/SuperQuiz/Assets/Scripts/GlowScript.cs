using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowScript : MonoBehaviour  {
	
	RectTransform rect;
	Vector2 baseSize;
	float angle;
	
	void Start(){
		rect = this.gameObject.GetComponent<RectTransform>();
		baseSize = rect.sizeDelta;
	}
	
	void Update(){
		angle = angle + Time.deltaTime;
		rect.sizeDelta = new Vector2 (baseSize.x + 5 * Mathf.Cos(angle), baseSize.y + 5 * Mathf.Cos(angle));
	}
	
	
}

