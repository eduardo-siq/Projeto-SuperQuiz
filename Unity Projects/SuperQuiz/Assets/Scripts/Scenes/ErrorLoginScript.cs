using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ErrorLoginScript : MonoBehaviour{

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
	// string instructionText9;
	// string instructionText10;
	// string instructionText11;
	Sprite instructionImage0;
	Sprite instructionImage1;
	Sprite instructionImage2;
	Sprite instructionImage3;
	Sprite instructionImage4;
	Sprite instructionImage5;
	Sprite instructionImage6;
	Sprite instructionImage7;
	Sprite instructionImage8;
	// Sprite instructionImage9;
	// Sprite instructionImage10;
	// Sprite instructionImage11;

	// UI
	public RectTransform tutorialRect;
	public GameObject instructionWindow;
	public GameObject changeInstruction;
	public GameObject logout;
	public Text instructionWindowText;
	public Image instructionWindowImage;
	public bool endScene;
	private bool quit;
	
	// Other
	public float timer = 0;
	public bool runTimer = false;
	public GameObject buttonHelp;

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
		if (runTimer){
			timer = timer + Time.deltaTime;
		}
		if (timer > 5f){
			runTimer = false;
			timer = 0f;
		}
	}

	IEnumerator StartScene(){
		yield return null;
		tutorialRect = GameObject.Find("Canvas/Scroll View/Viewport/ErrorLogin").GetComponent<RectTransform>();
		instruction = 0;
		instructionText0 = "\nInfelizmente houve um erro\n\n\nSeu cadastro não foi encontrado!";
		// instructionText1 = "Este é um jogo de perguntas e respostas que permite que você crie e customize seu personagem\n\n\n\n\n\n";
		// instructionText2 = "No menu principal, você pode acessar diferentes áreas do jogo\n\n\n\n\n\n\n";
		// instructionText3 = "\nAqui você inicia uma nova partida\n\n\n\n\n\n\n";
		// instructionText4 = "\nAqui, você cria e edita seu personagem\n\n\n\n\n\n";
		// instructionText5 = "Use as setas para selecionar seus acessórios\n\n\n\n\n\n\n";
		// instructionText6 = "\nAqui você vê o ranking e sua pontuação\n\n\n\n\n\n\n";
		// instructionText7 = "\nVocê também pode verificar as suas respostas\n\n\n\n\n\n";
		// instructionText8 = "\nPronto!\nAgora você já pode jogar o Desafio Quiz!\n\n\n\n\n";
		// instructionImage0 = Resources.Load("Textures/ErrorLogin/errorLogin_0", typeof(Sprite)) as Sprite;
		// instructionImage1 = Resources.Load("Textures/ErrorLogin/errorLogin_1", typeof(Sprite)) as Sprite;
		// instructionImage2 = Resources.Load("Textures/ErrorLogin/errorLogin_2", typeof(Sprite)) as Sprite;
		// instructionImage3 = Resources.Load("Textures/ErrorLogin/errorLogin_3", typeof(Sprite)) as Sprite;
		// instructionImage4 = Resources.Load("Textures/ErrorLogin/errorLogin_4", typeof(Sprite)) as Sprite;
		// instructionImage5 = Resources.Load("Textures/ErrorLogin/errorLogin_5", typeof(Sprite)) as Sprite;
		// instructionImage6 = Resources.Load("Textures/ErrorLogin/errorLogin_6", typeof(Sprite)) as Sprite;
		// instructionImage7 = Resources.Load("Textures/ErrorLogin/errorLogin_7", typeof(Sprite)) as Sprite;
		// instructionImage8 = Resources.Load("Textures/ErrorLogin/errorLogin_8", typeof(Sprite)) as Sprite;
		instructionWindow = GameObject.Find("Canvas/Scroll View/Viewport/ErrorLogin/InstructionWindow").gameObject;
		changeInstruction = GameObject.Find("Canvas/Scroll View/Viewport/ErrorLogin/ChangeInstruction").gameObject;
		logout = GameObject.Find("Canvas/Scroll View/Viewport/ErrorLogin/ToLogin").gameObject;
		instructionWindowText = instructionWindow.transform.Find("Text").GetComponent<Text>();
		instructionWindowImage = instructionWindow.transform.Find("Image").GetComponent<Image>();
		buttonHelp = GameObject.Find("Canvas/Scroll View/Viewport/ErrorLogin/Help").gameObject;
		
		// Message
		instructionWindow.SetActive(true);
		changeInstruction.SetActive(true);
		logout.SetActive(true);
		instructionWindowText.text = instructionText0;
		// instructionWindowImage.sprite = instructionImage0;
		
		print ("Text: " + instructionText0);
	}

	public void SelectReturn(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		Invoke("EndScene", 1.2f);
		Invoke("NextScene", 0.2f);
		// TransitionScript.PlayAnimation();
		// TransitionScript.StartAnimation();
		TransitionScript.EndAnimation();
	}

	public void NextScene(){
		AuthenticationScript.SignOut();
		SceneManager.LoadScene("login", LoadSceneMode.Single);
	}

	void EndScene(){	// OBSOLETE?
		endScene = true;
	}
}

