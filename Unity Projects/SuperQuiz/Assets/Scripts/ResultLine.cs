using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultLine : MonoBehaviour{

	public int id;
	public int rank;
	
	public void SelectThisLine(){
		ResultScript.SelectResultLine(id, rank);
	}
}

