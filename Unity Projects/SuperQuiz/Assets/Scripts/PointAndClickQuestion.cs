using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointAndClickQuestion  {
	
	public int index;
	public Sprite sprite;
	public Texture2D source;
	public List<int> rightItemIndex;	// QuestionPreLoad.answer0 index for right items, separated by '_'
	public List<string> itemName;		// QuestionPreLoad.answer1 holds name of items, separated by '_'
	public List<Sprite> itemSprite;		// Image names are derived from itemName index
	public List<Vector3> itemColor;		// QuestionPreLoad.answer2 holds color codes for items, separated by '_', written as '(1, 0.2, 1)'
   
	public PointAndClickQuestion (QuestionPreLoad newQuestion){
		index = newQuestion.index;
		sprite = Resources.Load("Textures/PointAndClick/pointAndClickSprite_" + index.ToString(), typeof(Sprite)) as Sprite;
		source = Resources.Load("Textures/PointAndClick/pointAndClickSource_" + index.ToString()) as Texture2D;
		
		if (sprite == null){ Debug.Log ("No sprite loaded!");} else { Debug.Log ("Sprite loaded!");}
		if (source == null){ Debug.Log ("No source loaded!");} else { Debug.Log ("Source loaded!");}
			
		rightItemIndex = new List<int>();
		itemName = new List<string>();
		itemSprite = new List<Sprite>();
		itemColor = new List<Vector3>();
		
		string[] itemIndexSplit;
		string[] itemNameSplit;
		string[] itemColorSplit;
		string[] space = new string[] { "_" };
		itemIndexSplit = newQuestion.answer0.Split(space, System.StringSplitOptions.None);
		itemNameSplit = newQuestion.answer1.Split(space, System.StringSplitOptions.None);
		itemColorSplit = newQuestion.answer2.Split(space, System.StringSplitOptions.None);
		int itemIndexParsed;
		for (int i = 0; i < itemIndexSplit.Length; i++){ 
			int.TryParse(itemIndexSplit[i], out itemIndexParsed);
			rightItemIndex.Add(itemIndexParsed);
			Debug.Log ("rightItemIndex: " + rightItemIndex[i]);
		}
		for (int i = 0; i < itemNameSplit.Length; i++){ 
			itemName.Add(itemNameSplit[i]);
			Sprite newSprite = Resources.Load("Textures/PointAndClick/item_" + index.ToString() + "_" + i.ToString(), typeof (Sprite)) as Sprite;
			if (newSprite == null) Debug.Log ("Failed to load sprite: item_" + index.ToString() + "_" + i.ToString());
			itemSprite.Add(newSprite);
			Vector3 newVector3 = StringToVector3(itemColorSplit[i]);
			itemColor.Add(newVector3);
			Debug.Log ("itemName: " + itemName[i]);
			Debug.Log ("itemColor: " + itemColor[i]);
		}
	}
	
	public static Vector3 StringToVector3(string vectorString){
		if (vectorString.StartsWith ("(") && vectorString.EndsWith (")")){
			vectorString = vectorString.Substring(1, vectorString.Length-2);
		}
		string[] stringFloat = vectorString.Split(',');
		Vector3 newVector = new Vector3(
		float.Parse(stringFloat[0]),
		float.Parse(stringFloat[1]),
		float.Parse(stringFloat[2]));
		return newVector;
	}

//		DESAFIO QUIZ, version alpha 0.7
//		developed by ROCKET PRO GAMES, rocketprogames@gmail.com
//		script by Eduardo Siqueira
//		São Paulo, Brasil, 2019
}