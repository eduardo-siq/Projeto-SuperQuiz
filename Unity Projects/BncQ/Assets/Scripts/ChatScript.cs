using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatScript : MonoBehaviour {
	
	public static ChatScript chat = null;
	
	GameObject chatWindow;
	GameObject minMaxChatButton;
	GameObject chatHeader;
	RawImage chatIcon;
	GameObject chatSendButton;
	GameObject chatTextField;
	InputField chatInput;
	GameObject chatLinePrefab;
	List<GameObject> chatLineList;
	GameObject chatDisplay;
	RectTransform chatDisplayContent;
	bool chatOpen;
	bool newMessage;
	bool chatSelected;
	float chatTimer;
	bool switchedColor;
	string message;	// currently written message
	bool messageSendByUser;
	
	void Awake(){
        if (chat == null){
            chat = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            if (chat != this){
                Destroy(this);
            }
        }
    }	
	
	void Start(){
		chatWindow = GameObject.Find("Canvas/Chat Window").gameObject;
		chatWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3f, Screen.height/2f);
		//chatWindow.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/2 - Screen.height/9, Screen.height/2 - Screen.height/9);
		chatHeader = chatWindow.transform.Find("Header").gameObject;
		chatHeader.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3f, Screen.height/15f);
		minMaxChatButton = chatWindow.transform.Find("Min Max Button").gameObject;
		minMaxChatButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height/15f, Screen.height/15f);
		chatIcon = minMaxChatButton.transform.Find("Icon").GetComponent<RawImage>();
		chatSendButton = chatWindow.transform.Find("Send Button").gameObject;
		chatSendButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/8f, Screen.height/15f);
		chatTextField = chatWindow.transform.Find("Text Field").gameObject;
		chatTextField.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3f - Screen.width/8f, Screen.height/15f);
		chatInput = chatTextField.GetComponent<InputField>();
		chatLinePrefab = Resources.Load("Prefab/Chat Line Prefab") as GameObject;
		chatLineList = new List <GameObject>();
		chatDisplay = chatWindow.transform.Find("Chat Display").gameObject;
		chatDisplay.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3f, Screen.height/2f - 2*Screen.height/15f);
		chatDisplay.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, Screen.height/15f);
		chatDisplayContent = chatDisplay.transform.Find("Viewport/Content").GetComponent<RectTransform>();
		chatOpen = false;
		newMessage = false;
		chatSelected = false;
		chatTimer = 0f;
		switchedColor = false;
		message = "";
	}
	
	void Update(){
		if (Input.GetKeyDown(KeyCode.Return)){
			WriteMessage();
			SendMessageButton();
		}
		if (newMessage){
			chatTimer = chatTimer + Time.deltaTime;
			if (chatTimer >= 2.5f){
				if (switchedColor){
					switchedColor = false;
					// Header: switch color to normal
					return;
				}
				if (!switchedColor){
					switchedColor = true;
					// Header: switch color to alternative
					return;
				}
			}
		}
	}
	
	public void SendMessageButton(){
		if (chatInput.text == "") return;
		DatabaseScript.SendMessage(AuthenticationScript.userName + ": " + message);
		chatInput.text = "";
		message = "";
		messageSendByUser = true;
		chatInput.ActivateInputField();
	}
	
	public void WriteMessage(){
		message = chatInput.text;
	}
	
	void OpenChat(){
		if (chatOpen) return;
		chatOpen = true;
	}
	
	void CloseChat(){
		if (!chatOpen) return;
		chatOpen = false;
		chatSelected = false;
	}
	
	public void NewMessage(string incomingMessage){
		if (chatOpen){
			// subtle sound
			if (!chatSelected){
				newMessage = true;
			}
		}
		if (!chatOpen){
			// sound
			newMessage = true;
		}
		print ("NewMessage: " + incomingMessage);
		
		GameObject newLine = Instantiate(chatLinePrefab) as GameObject;
		newLine.transform.SetParent(chatDisplay.transform.Find("Viewport/Content"), false);
		chatLineList.Add(newLine);
		newLine.GetComponent<Text>().text = incomingMessage;
		int lines = Mathf.CeilToInt(incomingMessage.Length / 50f);
		print ("Lines " + lines);
		newLine.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3f - Screen.width/60, lines * Screen.height/30f);
		if (messageSendByUser){ // If so, aligns the chat line to the left
			
		}
		OrganizeChatLines();
	}
	
	void OrganizeChatLines(){
		float sizeY = Screen.height/90f;
		for (int i = 0; i < chatLineList.Count; i ++){
			if (i == 0){
				chatLineList[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
			} else{
			chatLineList[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, chatLineList[i - 1].GetComponent<RectTransform>().anchoredPosition.y + chatLineList[i - 1].GetComponent<RectTransform>().sizeDelta.y);
			}
			sizeY = sizeY + chatLineList[i].GetComponent<RectTransform>().sizeDelta.y;
			// if (sizeY > Screen.height/2f - 2*Screen.height/15f){
				// chatDisplayContent.sizeDelta = new Vector2(0, sizeY - Screen.height/2f + 2*Screen.height/15f);
				// chatDisplayContent.anchoredPosition = new Vector2(0f, 0f);
			// }
		}
		chatDisplayContent.sizeDelta = new Vector2(0, sizeY - Screen.height/2f + 2*Screen.height/15f);
		chatDisplayContent.anchoredPosition = new Vector2(0f, 0f);
	}
	
	public void SelectChat(){
		chatSelected = true;
	}

	
}
