using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingSpin : MonoBehaviour {

	RectTransform image;
	
	void OnEnable(){
		image = this.gameObject.transform.Find("Image").GetComponent<RectTransform>();
		image.rotation =  new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
	}
	
	void Update () {
		image.transform.Rotate(Vector3.forward *Time.deltaTime *100);
	}
}
