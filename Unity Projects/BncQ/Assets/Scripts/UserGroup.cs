using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserGroup {
	
	// Data
	public string name;
	// Editor
	public GameObject clientButton;	// Button in Client Editor Window (input field)
	public GameObject filterButton;	// Button in Filter Selection Window (toggle)
	public GameObject questionButton;	// Button in Question Editor Window (toggle)
	public bool selectedInQuestion;	// Currently selected in Question Editor Window
	public bool selectedFilter;	// Currently selected in Filter Selection Window
	
	public UserGroup (){
		name = "newUserGroup";
		clientButton = null;
		filterButton = null;
		questionButton = null;
		selectedInQuestion = false;
		selectedFilter = false;
	}
	
	public UserGroup (string newUserGroupName, GameObject newUserGroupClientButtonuserGroupFilterButton, GameObject newUserGroupFilterButton, GameObject newUserGroupQuestionButton){
		name = newUserGroupName;
		clientButton = newUserGroupClientButtonuserGroupFilterButton;
		filterButton = newUserGroupFilterButton;
		questionButton = newUserGroupQuestionButton;
		selectedInQuestion = false;
		selectedFilter = false;
	}
	
	int ThisIndex(){
		int index = 0;
		for (int i = 0; i < TextEditorClass.userGroup.Count; i++){
			if (TextEditorClass.userGroup[i] == this){
				index = i;
				break;
			}
		}
		return index;
	}
	
	public void SetUserGroup(){
		if (name != null){
			if (clientButton.GetComponent<InputField>().text != ""){
				name = clientButton.GetComponent<InputField>().text;
			}
			if (clientButton.GetComponent<InputField>().text == ""){
				int index = ThisIndex();
				name = "grupo " + index.ToString();
			}
			if (filterButton != null){
				filterButton.transform.Find("Label").GetComponent<Text>().text = name;
			}
			if (questionButton != null){
				questionButton.transform.Find("Label").GetComponent<Text>().text = name;
			}
		}
	}
	
	public void LoadUserGroup(){
		if (name != null){
			if (name == ""){
				int index = ThisIndex();
				name = "grupo " + index.ToString();
			}
			clientButton.GetComponent<InputField>().text = name;
			if (filterButton != null){
				filterButton.transform.Find("Label").GetComponent<Text>().text = name;
			}
			if (questionButton != null){
				questionButton.transform.Find("Label").GetComponent<Text>().text = name;
			}
		}
	}
	
	public void DestroyButtons(){
		Object.Destroy (clientButton);
		Object.Destroy (filterButton);
		Object.Destroy (questionButton);
	}
	
	public void ToggleInQuestionWindow (){
		selectedInQuestion = questionButton.GetComponent<Toggle>().isOn;
	}
}