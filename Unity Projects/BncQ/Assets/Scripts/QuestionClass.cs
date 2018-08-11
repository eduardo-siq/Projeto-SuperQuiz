using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question {
	
	public int index;
	public int questionType;
	public string text;
	public string answer0;	// Right answer
	public string answer1;
	public string answer2;
	public string answer3;
	public string answer4;
	public List <bool> subjects;
	public List <bool> userGroups;
	
	public Question(){
		index = 0;
		text = "";
		answer0 = "";
		answer1 = "";
		answer2 = "";
		answer3 = "";
		answer4 = "";
		subjects = new List <bool>();
		userGroups = new List <bool>();
	}
	
	public Question (int newIndex, int newQuestionType, string newT, string newA0, string newA1, string newA2, string newA3, string newA4){
		index = newIndex;
		questionType = newQuestionType;
		text = newT;
		answer0 = newA0;
		answer1 = newA1;
		answer2 = newA2;
		answer3 = newA3;
		answer4 = newA4;
		subjects = new List <bool>();
		//subjects = newSubjects;
		userGroups = new List <bool>();
		//userGroups = newUserGroups;
	}

	public Question (int newIndex, int newQuestionType, string newT, string newA0, string newA1, string newA2, string newA3, string newA4, List<bool> newSubjects, List<bool> newUserGroups){
		index = newIndex;
		questionType = newQuestionType;
		text = newT;
		answer0 = newA0;
		answer1 = newA1;
		answer2 = newA2;
		answer3 = newA3;
		answer4 = newA4;
		subjects = new List <bool>();
		subjects = newSubjects;
		userGroups = new List <bool>();
		userGroups = newUserGroups;
	}
	
	public void ClientSpecification (List<bool> newSubjects, List<bool> newUserGroups){
		subjects = new List <bool>();
		subjects = newSubjects;
		userGroups = new List <bool>();
		userGroups = newUserGroups;
	}
	
	public Question (Question baseQuestion){
		index = baseQuestion.index;
		questionType = baseQuestion.questionType;
		text = baseQuestion.text;
		answer0 = baseQuestion.answer0;
		answer1 = baseQuestion.answer1;
		answer2 = baseQuestion.answer2;
		answer3 = baseQuestion.answer3;
		answer4 = baseQuestion.answer4;
		subjects = new List <bool>();
		userGroups = new List <bool>();
	}
	
	public void Index(int newIndex){
		index = newIndex;
	}
	
	public void Text(string newT){
		text = newT;
	}
	
	public void LoadUserGroups (string userGroupsString){
		
	}
}

public class QuestionButton {
	
	public Question question;
	public GameObject button;
	
	public QuestionButton (Question newQuestion, GameObject newButton){
		question = newQuestion;
		button = newButton;
	}
	
	public QuestionButton (QuestionButton baseQuestionButton){
		question = baseQuestionButton.question;
		button = baseQuestionButton.button;
	}
}

