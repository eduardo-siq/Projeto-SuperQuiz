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
		nameList.Add("arthur");
		nameList.Add("eduardo");
		nameList.Add("fernando");
		nameList.Add("lucas");
		nameList.Add("joão");
		nameList.Add("victor");
		nameList.Add("carla");
		nameList.Add("mariana");
		nameList.Add("roberto");
		nameList.Add("mario");
		nameList.Add("maria");
		nameList.Add("alfredo");
		nameList.Add("fernanda");
		nameList.Add("felipe");
		nameList.Add("carol");
		nameList.Add("débora");
		nameList.Add("rodrigo");
	}
}