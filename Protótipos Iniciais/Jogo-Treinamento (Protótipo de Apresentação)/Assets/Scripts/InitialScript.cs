using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class InitialScript : MonoBehaviour {
	
	GameObject exitWindow;
	GameObject errorWindow;
	GameObject frame;
	
	void Start () {	
		NewBackground();
		exitWindow = GameObject.FindWithTag("UI").transform.Find("Exit").gameObject;
		errorWindow = GameObject.FindWithTag("UI").transform.Find("Error").gameObject;
		frame = GameObject.FindWithTag("UI").transform.Find("Frame").gameObject;
		exitWindow.SetActive(false);
		errorWindow.SetActive(false);
		if (!File.Exists(Application.persistentDataPath + "/RPG_test_BncQ.dat")){
			MissingBncQ();
		}
	}
	
	public void NewBackground(){
		int b = Random.Range(1,6);
		string background = "background" + b.ToString();
		Texture backgroundImage = Resources.Load ("Images/background" + b.ToString(), typeof(Texture)) as Texture;
		Material mat = Resources.Load ("BackgroundMaterial", typeof(Material)) as Material;
		mat.mainTexture = backgroundImage;
	}
	
	
	public void StartGame () {
		AudioSource buttonAudio = this.gameObject.AddComponent<AudioSource> ();
		AudioClip positive = Resources.Load ("Sound/positive_sound", typeof(AudioClip)) as AudioClip;
		buttonAudio.PlayOneShot(positive, 1f);
		Invoke ("NextScene", 1);
	}
	
	public void NextScene(){
		SceneManager.LoadScene("gameplay1", LoadSceneMode.Single);
	}
	
	void Update (){
		if (Input.GetKeyDown (KeyCode.R)){
			print ("Application.LoadLevel");
			SceneManager.LoadScene("inicio", LoadSceneMode.Single);
		}
		if (Input.GetKeyDown (KeyCode.Escape)){
			ExitWindow();
		}
	}
	
	void ExitWindow(){
		exitWindow.SetActive(!exitWindow.activeSelf);
		errorWindow.SetActive(false);
		frame.SetActive(!exitWindow.activeSelf);
	}
	
	public void Quit(){
		print ("Application.Quit");
		Application.Quit();
	}
	
	void MissingBncQ(){
		print ("there is no file to be loaded");
		errorWindow.SetActive(true);
		GameObject.FindWithTag("UI").transform.Find("Frame").gameObject.SetActive(false);
		errorWindow.transform.Find("Text").gameObject.GetComponent<Text>().text = "	Não foi possível localizar o banco de questões (" + Application.persistentDataPath + "/RPG_test_BncQ.dat) . Providencie o banco de quesões e reinicie o programa.";
	}
}
