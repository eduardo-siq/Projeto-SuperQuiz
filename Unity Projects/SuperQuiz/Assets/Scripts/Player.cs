using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player{

	public int id;
	public string name;
	public Avatar avatar;
	public int score;

	public Player(){
		id = 0;
		name = "";
		avatar = new Avatar();
		avatar.skin = -1;
		score = 0;
	}
	
	public Player(Player newPlayer){
		id = newPlayer.id;
		name = newPlayer.name;
		avatar = newPlayer.avatar;
		score = newPlayer.score;
	}
	
	public Player(int newId, string newName, Avatar newAvatar, int newScore){
		id = newId;
		name = newName;
		avatar = newAvatar;
		score = newScore;
	}
	
	public Player(int newId, string newName, string avatarRawString, int newScore){
		id = newId;
		name = newName;
		score = newScore;
		avatar = new Avatar();
		if (avatarRawString != "X" && avatarRawString != null && avatarRawString != ""){
			string[] avatarString;
			string[] space = new string[] {" "};
			avatarString = avatarRawString.Split(space, StringSplitOptions.None);
			if (avatarString.Length > 5){
				avatar.skin = Convert.ToInt32(avatarString[0]);
				avatar.hair = Convert.ToInt32(avatarString[1]);
				avatar.item0 = Convert.ToInt32(avatarString[2]);
				avatar.item1 = Convert.ToInt32(avatarString[3]);
				avatar.item2 = Convert.ToInt32(avatarString[4]);
				avatar.item3 = Convert.ToInt32(avatarString[5]);
			}
		} else {
			avatar.skin = -1;
			Debug.Log ("NO AVATAR FOR : " + newName);
		}
	}
	
	public static Player RandomPlayer(){
		Player newPlayer = new Player();
		newPlayer.name = RandomPlayerScript.RandomName();
		return newPlayer;
	}
	
	public static Player RandomPlayerWithAvatar(){
		Player newPlayer = new Player();
		newPlayer.name = RandomPlayerScript.RandomName();
		Avatar.RandomAvatar(newPlayer.avatar);
		return newPlayer;
	}
}