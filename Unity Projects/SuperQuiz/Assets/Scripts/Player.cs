using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{

	public int id;
	public string name;
	public Avatar avatar;
	public int score;

	public Player(){
		id = 0;
		name = "";
		avatar = new Avatar(Avatar.UndefinedAvatar());
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
	
	public static Player RandomPlayer(){
		Player newPlayer = new Player();
		newPlayer.name = RandomPlayerScript.RandomName();
		return newPlayer;
	}
	
	// public static Player RandomPlayerWithAvatar(){
		// print ("hello, wordl");
	// }
}