using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlayerScript{

	public static List<string> nameList;

	public static string RandomName(){
		string newName;
		if (nameList == null){
			CreateNameList();
		}
		return newName = nameList[Random.Range(0, nameList.Count)];
	}
	
	static void CreateNameList(){
		nameList = new List<string>();
		nameList.Add("Arthur");
		nameList.Add("Eduardo");
		nameList.Add("Fernando");
		nameList.Add("Lucas");
		nameList.Add("João");
		nameList.Add("Victor");
		nameList.Add("Carla");
		nameList.Add("Mariana");
		nameList.Add("Roberto");
		nameList.Add("Mario");
		nameList.Add("Maria");
		nameList.Add("Alfredo");
		nameList.Add("Fernanda");
		nameList.Add("Felipe");
		nameList.Add("Carol");
		nameList.Add("Débora");
		nameList.Add("Rodrigo");
	}
}