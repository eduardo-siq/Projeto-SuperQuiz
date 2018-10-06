using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Detail {	// OBSOLETE?
	
	public Vector3 colorCode;
	public Sprite texture;
	
	public Detail (float r, float g, float b){
		colorCode = new Vector3 (r, g, b);
		// texture = Resources.Load("Textures/PointAndClick/detail_" + 100*r + "_" + 100*g + "_" + 100*b, typeof(Sprite)) as Sprite;
		// if (texture != null){
			// Debug.Log ("detail " + colorCode + " loaded");
		// }
		// if (texture == null){
			// Debug.Log ("detail " + colorCode + " file not found");
		// }
	}
	
	public Detail (string rString, string gString, string bString){
		float r = float.Parse(rString);
		float g = float.Parse(gString);
		float b = float.Parse(bString);
		colorCode = new Vector3 (r, g, b);
		// texture = Resources.Load("Textures/PointAndClick/detail_" + 100*r + "_" + 100*g + "_" + 100*b, typeof(Sprite)) as Sprite;
		// if (texture != null){
			// Debug.Log ("detail " + colorCode + " loaded");
		// }
		// if (texture == null){
			// Debug.Log ("detail " + colorCode + " file not found");
		// }
	}
	
	// public static List<Detail> GetBuiltInList(){
		// List <Detail> detailList = new List<Detail>();
		// detailList.Add(new Detail(1,0,0,"0"));	// sul
		// detailList.Add(new Detail(0,0,1,"1"));	// sudeste
		// detailList.Add(new Detail(0,1,0,"2"));	// nordeste
		// detailList.Add(new Detail(1,0,1,"3"));	// centro-oeste
		// detailList.Add(new Detail(1,1,0,"4"));	// norte
		// return detailList;
	// }
}

