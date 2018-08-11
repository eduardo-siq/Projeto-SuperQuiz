using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ClickSelect : MonoBehaviour, IPointerClickHandler {
    public InputField inputField;
    public void OnPointerClick (PointerEventData eventData)
    {
		this.gameObject.transform.parent.GetComponent<TextEditorClass>().SelectInputField(this.gameObject.name);
    }
}