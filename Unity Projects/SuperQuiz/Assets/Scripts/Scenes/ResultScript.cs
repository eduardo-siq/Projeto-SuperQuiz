using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScript : MonoBehaviour{

	// UI
	public RectTransform resultRect;
	public bool endScene;
	public static RankingWindow rankingWindow;
	public GameObject avatar;
	public GameObject resultLinesViewport;
	public GameObject resultLinePrefab;
	public Sprite resultLineTexture1;
	public Sprite resultLineTexture2;
	public Text scoreValueText;
	public List <Player> playerRankingList;
	private bool quit;

	// Result variables
	private int scoreValue = 0;

	// Navigation
	string nextScene = "";


	void Start(){
		// StartCoroutine(StartScene());
		resultRect = GameObject.Find("Canvas/Scroll View/Viewport/Result").GetComponent<RectTransform>();
		rankingWindow = GameObject.Find("Canvas/Scroll View/Viewport/Result/RankingWindow").GetComponent<RankingWindow>();
		avatar = GameObject.Find("Canvas/Scroll View/Viewport/Result/Scroll View/Viewport/Avatar").gameObject;
		// avatar.transform.Find("Item1").GetComponent<RawImage>().texture = SessionScript.avatarItem1[SessionScript.selectedItem1];
		// avatar.transform.Find("Item2").GetComponent<RawImage>().texture = SessionScript.avatarItem2[SessionScript.selectedItem2];
		// avatar.transform.Find("Item3").GetComponent<RawImage>().texture = SessionScript.avatarItem3[SessionScript.selectedItem3];
		scoreValueText = GameObject.Find("Canvas/Scroll View/Viewport/Result/ScoreValue").GetComponent<Text>();
		scoreValue = SessionScript.player.score;
		scoreValueText.text = scoreValue.ToString();
		resultLinesViewport = GameObject.Find("Canvas/Scroll View/Viewport/Result/Scroll View/Viewport/Content").gameObject;
		// resultLinePrefab = Resources.Load("Prefabs/ResultLine") as GameObject;
		// resultLineTexture1 = Resources.Load("Textures/UI/button_result_list", typeof(Sprite)) as Sprite;
		// resultLineTexture2 = Resources.Load("Textures/UI/button_result_list2", typeof(Sprite)) as Sprite;
		playerRankingList = new List <Player>();
		SetPlayerRankingList();
		SortPlayerListByScore();
		ResultList();
		rankingWindow.content.SetActive(false);
		rankingWindow.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0f, 0f);	// Objeto começa fora de cena para que seu Start() ocorra, mas fora da visão do player
	}

	IEnumerator StartScene(){
		yield return null;
		resultRect = GameObject.Find("Canvas/Scroll View/Viewport/Result").GetComponent<RectTransform>();
		rankingWindow = GameObject.Find("Canvas/Scroll View/Viewport/Result/RankingWindow").GetComponent<RankingWindow>();
		avatar = GameObject.Find("Canvas/Scroll View/Viewport/Result/Scroll View/Viewport/Avatar").gameObject;
		// avatar.transform.Find("Item1").GetComponent<RawImage>().texture = SessionScript.avatarItem1[SessionScript.selectedItem1];
		// avatar.transform.Find("Item2").GetComponent<RawImage>().texture = SessionScript.avatarItem2[SessionScript.selectedItem2];
		// avatar.transform.Find("Item3").GetComponent<RawImage>().texture = SessionScript.avatarItem3[SessionScript.selectedItem3];
		scoreValueText = GameObject.Find("Canvas/Scroll View/Viewport/Result/ScoreValue").GetComponent<Text>();
		scoreValue = SessionScript.player.score;
		scoreValueText.text = scoreValue.ToString();
		resultLinesViewport = GameObject.Find("Canvas/Scroll View/Viewport/Result/Scroll View/Viewport/Content").gameObject;
		// resultLinePrefab = Resources.Load("Prefabs/ResultLine") as GameObject;
		// resultLineTexture1 = Resources.Load("Textures/UI/button_result_list", typeof(Sprite)) as Sprite;
		// resultLineTexture2 = Resources.Load("Textures/UI/button_result_list2", typeof(Sprite)) as Sprite;
		playerRankingList = new List <Player>();
		SetPlayerRankingList();
		SortPlayerListByScore();
		ResultList();
		rankingWindow.content.SetActive(false);
		rankingWindow.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0f, 0f);	// Objeto começa fora de cena para que seu Start() ocorra, mas fora da visão do player
	}


	void Update(){
		// if (endScene){
		// resultRect.anchoredPosition = new Vector2 (resultRect.anchoredPosition.x, resultRect.anchoredPosition.y - Time.deltaTime * 1200);
		// return;
		// }
		if (quit){
			SessionScript.songAudio.volume = SessionScript.songAudio.volume - (Time.deltaTime * 2);
		}
	}
	
	void SetPlayerRankingList(){
		if (playerRankingList.Count > 0) playerRankingList.Clear();
		for (int i = 0; i < SessionScript.playerList.Count; i++){
			playerRankingList.Add(new Player(SessionScript.playerList[i]));
		}
		bool playerFound = false;
		for (int i = 0; i < playerRankingList.Count; i++){
			if (playerRankingList[i].id == SessionScript.player.id){
				playerFound = true;
			}
		}
		if (!playerFound) playerRankingList.Add(new Player(SessionScript.player.id, "você", SessionScript.player.avatar, SessionScript.player.score));
	}
	
	public void SortPlayerListByScore(){
		print ("SortPlayerListByScore");
		List <Player> auxPlayerList = new List<Player>();
		int loop = 0;
		bool done = false;
		
		int currentScore = -999;
		do{
			for (int i = 0; i < playerRankingList.Count; i++){
				if (playerRankingList[i].score > currentScore) currentScore = playerRankingList[i].score;
			}
			print ("currentScore = " + currentScore);
			for (int i = 0; i < playerRankingList.Count; i++){
				if (playerRankingList[i].score == currentScore){
					auxPlayerList.Add(new Player(playerRankingList[i]));
					print ("auxPlayerList count: " + auxPlayerList.Count);
					print ("playerRankingList count: " + playerRankingList.Count);
					playerRankingList.RemoveAt(i);
				}
			}
			currentScore = -999;
			if (loop >= 100){
				print ("INFINITE LOOP!");
				done = true;
			}
			if (playerRankingList.Count == 0) done = true;
		} while (!done);
		playerRankingList = auxPlayerList;
	}
	
	public void ResultList(){
		print ("RESULT LIST METHOD!! playerRankingList count: " + playerRankingList.Count);
		int index = 0;
		bool variation1 = true;
		resultLinesViewport.transform.parent.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
		resultLinesViewport.transform.parent.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
		for (int i = 0; i < playerRankingList.Count; i++){
			print ("resultList() i: " + i);
			index = i + 1;

			GameObject newResultLine = Instantiate(resultLinePrefab);
			newResultLine.transform.SetParent(resultLinesViewport.transform, true);
			newResultLine.GetComponent<ResultLine>().id = playerRankingList[i].id;
			newResultLine.GetComponent<ResultLine>().rank = (1 + i);
			string lineNameString = playerRankingList[i].name;
			if (lineNameString.Length > 23){
				lineNameString = lineNameString.Substring(0, 23) + "...";
			}
			newResultLine.transform.Find("Text").GetComponent<Text>().text = (1 + i) + "# " + lineNameString;	// + ": " + playerRankingList[i].score + " pontos";
			newResultLine.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, - 20f - 30 * i, 0f);
			if (variation1){
				newResultLine.GetComponent<Image>().sprite = resultLineTexture1;
			}
			else{
				newResultLine.GetComponent<Image>().sprite = resultLineTexture2;
			}
			variation1 = !variation1;
			float sizeY = 30 * (i + 1);
			if (sizeY < 200) sizeY = 200f;
			resultLinesViewport.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, sizeY);
			newResultLine.transform.localScale = new Vector3(1, 1, 1);
		}
		resultLinesViewport.transform.parent.transform.parent.Find("Scrollbar Vertical").GetComponent<Scrollbar>().value = 1;
		// answerLinesViewport.transform.parent.transform.parent.Find("Scrollbar Vertical").GetComponent<Scrollbar>().size = 0;
	}
	
	public static void SelectResultLine(int id, int rank){
		SessionScript.ButtonAudio(SessionScript.neutral);
		if (id == SessionScript.player.id){
			rankingWindow.OpenWindow(SessionScript.player.avatar, rank, SessionScript.player.name, "", SessionScript.player.score);
		}else{
			for (int i = 0; i < SessionScript.playerList.Count; i++){
				if (id == SessionScript.playerList[i].id){
					rankingWindow.OpenWindow(SessionScript.playerList[i].avatar, rank, SessionScript.playerList[i].name, "", SessionScript.playerList[i].score);
					break;
				}
			}
		}
	}

	public void SelectMenu(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		nextScene = "menu";
		Invoke("EndScene", 0.25f);
		Invoke("NextScene", 0.5f);

	}

	public void SelectAnswers(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		nextScene = "answers";
		Invoke("EndScene", 1.2f);
		Invoke("NextScene", 0.2f);
		// TransitionScript.PlayAnimation();
		// TransitionScript.StartAnimation();
		TransitionScript.EndAnimation();
	}

	public void SelectQuit(){
		SessionScript.ButtonAudio(SessionScript.neutral);
		quit = true;
		print("QUIT");
		Invoke("EndScene", 0.25f);
		Invoke("Quit", 0.5f);
	}

	void Quit(){
		Application.Quit();
	}

	public void NextScene(){
		SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
	}

	void EndScene(){	// OBSOLETE?
		endScene = true;
	}
	
//		DESAFIO QUIZ, version alpha 0.7
//		developed by ROCKET PRO GAMES, rocketprogames@gmail.com
//		script by Eduardo Siqueira
//		São Paulo, Brasil, 2019

}
