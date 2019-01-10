using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ClickSelectUser : MonoBehaviour, IPointerClickHandler {
    public InputField inputField;
    public void OnPointerClick (PointerEventData eventData)
    {
		this.gameObject.transform.parent.GetComponent<UserEditorClass>().SelectInputField(this.gameObject.name);
    }
}