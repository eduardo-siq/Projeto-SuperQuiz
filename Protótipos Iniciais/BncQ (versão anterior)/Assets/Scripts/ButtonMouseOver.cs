using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMouseOver : MonoBehaviour {

	public void MouseOver(){
		this.gameObject.transform.parent.parent.parent.parent.GetComponent<TextEditorClass>().MouseOverQuestion(this.gameObject);
	}
	
	public void MouseNoLongerOver(){
		this.gameObject.transform.parent.parent.parent.parent.GetComponent<TextEditorClass>().MouseOverQuestion();
	}
}
