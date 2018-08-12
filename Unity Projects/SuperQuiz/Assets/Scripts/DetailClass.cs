using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Detail {
	
	public Vector3 colorCode;
	public Texture texture;
	
	public Detail (float r, float b, float g, string t){
		colorCode = new Vector3 (r, b, g);
		texture = Resources.Load("Textures/PointAndClick/detail" + t) as Texture;
		if (texture == null){
			Debug.Log ("detail" + t + " file not found");
		}
	}
	
	public static List<Detail> GetBuiltInList(){
		List <Detail> detailList = new List<Detail>();
		detailList.Add(new Detail(1,0,0,"0"));	// vermelho
		detailList.Add(new Detail(0,1,0,"1"));	// azul
		detailList.Add(new Detail(0,0,1,"2"));	// verde
		detailList.Add(new Detail(1,0,1,"3"));	// amarelo
		detailList.Add(new Detail(0.5f,0.5f,0.5f,"4"));	// cinza
		return detailList;
	}
}

