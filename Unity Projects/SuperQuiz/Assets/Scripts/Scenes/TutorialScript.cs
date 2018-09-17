using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour{

	// Tutorial
	int instruction;	// max 3
	string instructionText0;
	string instructionText1;
	string instructionText2;
	string instructionText3;
	string instructionText4;
	string instructionText5;
	string instructionText6;
	string instructionText7;
	string instructionText8;
	string instructionText9;
	string instructionText10;
	string instructionText11;
	Sprite instructionImage0;
	Sprite instructionImage1;
	Sprite instructionImage2;
	Sprite instructionImage3;
	Sprite instructionImage4;
	Sprite instructionImage5;
	Sprite instructionImage6;
	Sprite instructionImage7;
	Sprite instructionImage8;
	Sprite instructionImage9;
	Sprite instructionImage10;
	Sprite instructionImage11;

	// UI
	public RectTransform tutorialRect;
	public GameObject instructionWindow;
	public GameObject changeInstruction;
	public GameObject toMenu;
	public GameObject toMenuEnd;
	public Text instructionWindowText;
	public Image instructionWindowImage;
	public bool endScene;
	private bool quit;

	void Start(){
		StartCoroutine(StartScene());
	}

	void Update(){
		// if (endScene){
		// aboutRect.anchoredPosition = new Vector2 (aboutRect.anchoredPosition.x, aboutRect.anchoredPosition.y - Time.deltaTime * 1200);
		// return;
		// }
		if (quit){
			SessionScript.songAudio.volume = SessionScript.songAudio.volume - (Time.deltaTime * 2);
		}
	}

	IEnumerator StartScene(){
		yield return null;
		tutorialRect = GameObject.Find("Canvas/Scroll View/Viewport/Tutorial").GetComponent<RectTransform>();
		instruction = 0;
		instructionText0 = "Bem vindo ao SuperQuiz!";
		instructionText1 = "Este é um jogo de perguntas e respostas, e permite que você crie e customize seu personagem";
		instructionText2 = "No menu principal, acessamos diferentes áreas do jogo";
		instructionText3 = "Aqui você inicia uma nova partida";
		instructionText4 = "Aqui você cria e edita seu personagem";
		instructionText5 = "Use as setas para selecionar seus acessórios";
		instructionText6 = "Pronto. Agora você já pode jogar o SuperQuiz!";
		instructionImage0 = Resources.Load("Textures/Tutorial/tutorial_0", typeof(Sprite)) as Sprite;
		instructionImage1 = Resources.Load("Textures/Tutorial/tutorial_1", typeof(Sprite)) as Sprite;
		instructionImage2 = Resources.Load("Textures/Tutorial/tutorial_2", typeof(Sprite)) as Sprite;
		instructionImage3 = Resources.Load("Textures/Tutorial/tutorial_3", typeof(Sprite)) as Sprite;
		instructionImage4 = Resources.Load("Textures/Tutorial/tutorial_4", typeof(Sprite)) as Sprite;
		instructionImage5 = Resources.Load("Textures/Tutorial/tutorial_5", typeof(Sprite)) as Sprite;
		instructionImage6 = Resources.Load("Textures/Tutorial/tutorial_6", typeof(Sprite)) as Sprite;
		instructionWindow = GameObject.Find("Canvas/Scroll View/Viewport/Tutorial/InstructionWindow").gameObject;
		changeInstruction = GameObject.Find("Canvas/Scroll View/Viewport/Tutorial/ChangeInstruction").gameObject;
		toMenu = GameObject.Find("Canvas/Scroll View/Viewport/Tutorial/ToMenu").gameObject;
		toMenuEnd = GameObject.Find("Canvas/Scroll View/Viewport/Tutorial/ToMenuEnd").gameObject;
		instructionWindowText = instructionWindow.transform.Find("Text").GetComponent<Text>();
		instructionWindowImage = instructionWindow.transform.Find("Image").GetComponent<Image>();
		Tutorial();
	}
	
	public void Tutorial(){
		switch (instruction){
			case 0:
				instructionWindow.SetActive(true);
				changeInstruction.SetActive(true);
				toMenu.SetActive(true);
				toMenuEnd.SetActive(false);
				instructionWindowText.text = instructionText0;
				instructionWindowImage.sprite = instructionImage0;
				break;
			case 1:
				instructionWindow.SetActive(true);
				changeInstruction.SetActive(true);
				toMenu.SetActive(true);
				toMenuEnd.SetActive(false);
				instructionWindowText.text = instructionText1;
				instructionWindowImage.sprite = instructionImage1;
				break;
			case 2:
				instructionWindow.SetActive(true);
				changeInstruction.SetActive(true);
				toMenu.SetActive(true);
				toMenuEnd.SetActive(false);
				instructionWindowText.text = instructionText2;
				instructionWindowImage.sprite = instructionImage2;
				break;
			case 3:
				instructionWindow.SetActive(true);
				changeInstruction.SetActive(true);
				toMenu.SetActive(true);
				toMenuEnd.SetActive(false);
				instructionWindowText.text = instructionText3;
				instructionWindowImage.sprite = instructionImage3;
				break;
			case 4:
				instructionWindow.SetActive(true);
				changeInstruction.SetActive(true);
				toMenu.SetActive(true);
				toMenuEnd.SetActive(false);
				instructionWindowText.text = instructionText4;
				instructionWindowImage.sprite = instructionImage4;
				break;
			case 5:
				instructionWindow.SetActive(true);
				changeInstruction.SetActive(true);
				toMenu.SetActive(true);
				toMenuEnd.SetActive(false);
				instructionWindowText.text = instructionText5;
				instructionWindowImage.sprite = instructionImage5;
				break;
			case 6:
				instructionWindow.SetActive(true);
				changeInstruction.SetActive(true);
				toMenu.SetActive(true);
				toMenuEnd.SetActive(false);
				instructionWindowText.text = instructionText6;
				instructionWindowImage.sprite = instructionImage6;
				break;
			// case 7:
				// instructionWindow.SetActive(true);
				// changeInstruction.SetActive(true);
				// toMenu.SetActive(true);
				// toMenuEnd.SetActive(false);
				// instructionWindowText.text = instructionText7;
				// instructionWindowImage.sprite = instructionImage7;
				// break;
			// case 8:
				// instructionWindow.SetActive(true);
				// changeInstruction.SetActive(true);
				// toMenu.SetActive(true);
				// toMenuEnd.SetActive(false);
				// instructionWindowText.text = instructionText8;
				// instructionWindowImage.sprite = instructionImage8;
				// break;
			// case 9:
				// instructionWindow.SetActive(true);
				// changeInstruction.SetActive(true);
				// toMenu.SetActive(true);
				// toMenuEnd.SetActive(false);
				// instructionWindowText.text = instructionText9;
				// instructionWindowImage.sprite = instructionImage9;
				// break;
			// case 10:
				// instructionWindow.SetActive(true);
				// changeInstruction.SetActive(true);
				// toMenu.SetActive(true);
				// toMenuEnd.SetActive(false);
				// instructionWindowText.text = instructionText10;
				// instructionWindowImage.sprite = instructionImage10;
				// break;
			// case 11:
				// instructionWindow.SetActive(true);
				// changeInstruction.SetActive(true);
				// toMenu.SetActive(true);
				// toMenuEnd.SetActive(false);
				// instructionWindowText.text = instructionText11;
				// instructionWindowImage.sprite = instructionImage11;
				// break;
		default:
			instructionWindow.SetActive(false);
			changeInstruction.SetActive(false);
			toMenu.SetActive(false);
			toMenuEnd.SetActive(true);
			break;
		}
	}
	
	public void SelectNextInstruction(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		instruction = instruction + 1;
		Tutorial();
	}
	
	public void SelectPreviousInstruction(){
		if (instruction == 0){
			SessionScript.ButtonAudio(SessionScript.subtle);
			return;
		}
		instruction = instruction - 1;
		Tutorial();
	}

	public void SelectMenu(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		Invoke("EndScene", 1.2f);
		Invoke("NextScene", 1.2f);
		// TransitionScript.PlayAnimation();
		// TransitionScript.StartAnimation();
		TransitionScript.EndAnimation();
	}
	
	public void SelectMenuEnd(){
		SessionScript.ButtonAudio(SessionScript.positive);
		Invoke("EndScene", 1.2f);
		Invoke("NextScene", 1.2f);
		// TransitionScript.PlayAnimation();
		// TransitionScript.StartAnimation();
		TransitionScript.EndAnimation();
	}

	public void NextScene(){
		SceneManager.LoadScene("menu", LoadSceneMode.Single);
	}

	void EndScene(){	// OBSOLETE?
		endScene = true;
	}
}

