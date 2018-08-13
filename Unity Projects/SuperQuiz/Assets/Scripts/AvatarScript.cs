using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AvatarScript : MonoBehaviour {

	// UI
	public RectTransform avatarRect;
	public bool endScene;
	
	// Avatar variables
	public GameObject portrait;
		public RawImage item1Texture;
		public RawImage item2Texture;
		public RawImage item3Texture;
	public GameObject item1Selection;
	public GameObject item2Selection;
	public GameObject item3Selection;
	
	void Start(){
		StartCoroutine (StartScene());
	}
	
	IEnumerator StartScene(){
		yield return null;
		avatarRect = GameObject.Find("Canvas/Scroll View/Viewport/Avatar").GetComponent<RectTransform>();
		portrait = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/Portrait").gameObject;
			item1Texture = portrait.transform.Find("Item1").GetComponent<RawImage>();
			item2Texture = portrait.transform.Find("Item2").GetComponent<RawImage>();
		item1Selection = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/SelectItem1").gameObject;
		item2Selection = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/SelectItem2").gameObject;
		item2Selection = GameObject.Find("Canvas/Scroll View/Viewport/Avatar/SelectItem3").gameObject;
		Portrait();
	}
	
	void Update(){
		if (endScene){
			avatarRect.anchoredPosition = new Vector2 (avatarRect.anchoredPosition.x, avatarRect.anchoredPosition.y - Time.deltaTime * 1200);
			return;
		}
	}
	public void NextItem(int item){
		switch (item){
			case 1:
				SessionScript.selectedItem1 = SessionScript.selectedItem1 + 1;
				if (SessionScript.selectedItem1 >= SessionScript.avatarItem1.Count){
					SessionScript.selectedItem1 = 0;
				}
				break;
			case 2:
				SessionScript.selectedItem2 = SessionScript.selectedItem2 + 1;
				if (SessionScript.selectedItem2 >= SessionScript.avatarItem2.Count){
					SessionScript.selectedItem2 = 0;
				}
				break;
			case 3:
				SessionScript.selectedItem3 = SessionScript.selectedItem3 + 1;
				if (SessionScript.selectedItem3 >= SessionScript.avatarItem3.Count){
					SessionScript.selectedItem3 = 0;
				}
				break;
			default:
				break;
		}
		SessionScript.ButtonAudio(SessionScript.subtle);
		Portrait();
	}
	
	public void PreviousItem(int item){
		switch (item){
			case 1:
				SessionScript.selectedItem1 = SessionScript.selectedItem1 - 1;
				if (SessionScript.selectedItem1 < 0){
					SessionScript.selectedItem1 = SessionScript.avatarItem1.Count - 1;
					
				}
				break;
			case 2:
				SessionScript.selectedItem2 = SessionScript.selectedItem2 - 1;
				if (SessionScript.selectedItem2 < 0){
					SessionScript.selectedItem2 = SessionScript.avatarItem2.Count - 1;
					
				}
				break;
			case 3:
				SessionScript.selectedItem3 = SessionScript.selectedItem3 - 1;
				if (SessionScript.selectedItem3 < 0){
					SessionScript.selectedItem3 = SessionScript.avatarItem3.Count - 1;
					
				}
				break;
			default:
				break;
		}
		SessionScript.ButtonAudio(SessionScript.subtle);
		Portrait();
	}
	
	public void Portrait(){
		item1Texture.texture = SessionScript.avatarItem1[SessionScript.selectedItem1];
		item2Texture.texture = SessionScript.avatarItem2[SessionScript.selectedItem2];
		item3Texture.texture = SessionScript.avatarItem3[SessionScript.selectedItem3];
		
		// Selection Options
		item1Selection.transform.Find("Item").GetComponent<RawImage>().texture = SessionScript.avatarItem1[SessionScript.selectedItem1];
		item2Selection.transform.Find("Item").GetComponent<RawImage>().texture = SessionScript.avatarItem2[SessionScript.selectedItem2];
		item3Selection.transform.Find("Item").GetComponent<RawImage>().texture = SessionScript.avatarItem2[SessionScript.selectedItem3];
		
	}
	
	public void SelectMenu(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		Invoke ("EndScene", 0.5f);
		Invoke ("NextScene", 1f);
		
	}
	
	public void NextScene(){
		SceneManager.LoadScene("menu", LoadSceneMode.Single);
	}
	
	void EndScene(){
		endScene = true;
	}
}
