using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Detail {
	
	public Vector3 colorCode;
	public Sprite texture;
	
	public Detail (float r, float b, float g, string t){
		colorCode = new Vector3 (r, b, g);
		texture = Resources.Load("Textures/PointAndClick/detail" + t, typeof(Sprite)) as Sprite;
		if (texture != null){
			Debug.Log ("detail" + t + " loaded");
		}
		if (texture == null){
			Debug.Log ("detail" + t + " file not found");
		}
	}
	
	public static List<Detail> GetBuiltInList(){
		List <Detail> detailList = new List<Detail>();
		detailList.Add(new Detail(1,0,0,"0"));	// sul
		detailList.Add(new Detail(0,0,1,"1"));	// sudeste
		detailList.Add(new Detail(0,1,0,"2"));	// nordeste
		detailList.Add(new Detail(1,0,1,"3"));	// centro-oeste
		detailList.Add(new Detail(1,1,0,"4"));	// norte
		return detailList;
	}
}

