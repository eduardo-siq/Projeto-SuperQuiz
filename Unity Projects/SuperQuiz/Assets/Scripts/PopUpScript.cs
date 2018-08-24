using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpScript : MonoBehaviour  {
	
	public RectTransform popUpRect;
	public RectTransform windowRect;
	public RawImage shadowImage;
	
	bool close = false;
	bool open = true;
	float alpha = 0;
	
	void Start(){
		this.gameObject.transform.SetParent(GameObject.Find("Canvas").transform);
		popUpRect.localScale = new Vector3 (1,1,1);
		popUpRect.anchoredPosition = new Vector2 (0, 0);
		windowRect.anchoredPosition = new Vector2 (0, 500);
		shadowImage.color = new Color (0f, 0f, 0f, 0.0f);
	}

	void Update(){
		if (open){
			if (alpha < 0.5){
				alpha = shadowImage.color.a + Time.deltaTime/2;
			}
			windowRect.anchoredPosition = new Vector2 (windowRect.anchoredPosition.x, windowRect.anchoredPosition.y - Time.deltaTime * 1200);
			shadowImage.color = new Color (0f, 0f, 0f, alpha);
			if (windowRect.anchoredPosition.y <= 0){
				windowRect.anchoredPosition = new Vector2 (windowRect.anchoredPosition.x, 0);
				shadowImage.color = new Color (0f, 0f, 0f, 0.5f);
				open = false;
			}
		}
		if (close){
			alpha = shadowImage.color.a - Time.deltaTime/2;
			windowRect.anchoredPosition = new Vector2 (windowRect.anchoredPosition.x, windowRect.anchoredPosition.y + Time.deltaTime * 1200);
			shadowImage.color = new Color (0f, 0f, 0f, alpha);
		}
	}
	
	public static GameObject InstantiatePopUp(string newMainText, string newButtonText){	// MAYBE RETURN TO TYPE VOID
		GameObject newPopUp = Instantiate(Resources.Load("Prefabs/PopUp") as GameObject);
		PopUpScript popUpScript = newPopUp.GetComponent<PopUpScript>();
		newPopUp.transform.Find("PopUpWindow/Text").GetComponent<Text>().text = newMainText;
		newPopUp.transform.Find("PopUpWindow/Button/Text").GetComponent<Text>().text = newButtonText;
		return newPopUp;
	}
	
	public void PressButton(){
		SessionScript.ButtonAudio(SessionScript.positive);
		Invoke ("ClosePopUp", 0.25f);
		Invoke ("DestroyPopUp", 1.25f);
	}
	
	void ClosePopUp(){
		close = true;
		open = false;
	}
	
	void DestroyPopUp(){
		print ("Delete!");
		Destroy(this.gameObject);
	}
}

