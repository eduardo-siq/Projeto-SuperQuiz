using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question {
	
	public int index;
	public int questionType;	// 0: multiple answer, 1: fill the blanks, 2: point-and-click, 3: longa, 4: imagem
	public string text;
	public string answer0;	// Right answer
	public string answer1;
	public string answer2;
	public string answer3;
	public string answer4;
	public List <bool> subject;
	public Texture questionImage;
	
	public Question(){
		index = 0;
		questionType = 0;
		text = "";
		answer0 = "";
		answer1 = "";
		answer2 = "";
		answer3 = "";
		answer4 = "";	
		subject = new List <bool>();
		questionImage = null;
	}

	public Question (int newIndex, int newQuestionType, string newT, string newA0, string newA1, string newA2, string newA3, string newA4, int newSubject){
		index = newIndex;
		questionType = newQuestionType;
		text = newT;
		answer0 = newA0;
		answer1 = newA1;
		answer2 = newA2;
		answer3 = newA3;
		answer4 = newA4;
		subject = new List <bool>();
		questionImage = null;
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
		subject = new List <bool>();
		subject = baseQuestion.subject;
		questionImage = null;
	}
	
	public Question (QuestionPreLoad baseQuestion){
		index = baseQuestion.index;
		questionType = baseQuestion.questionType;
		text = baseQuestion.text;
		answer0 = baseQuestion.answer0;
		answer1 = baseQuestion.answer1;
		answer2 = baseQuestion.answer2;
		answer3 = baseQuestion.answer3;
		answer4 = baseQuestion.answer4;
		subject = new List <bool>();
		questionImage = null;
		if (baseQuestion.subject != null){
			if (baseQuestion.subject != ""){
				string[] questionSubjects;
				string[] space = new string[] {" "};
				questionSubjects = baseQuestion.subject.Split(space, System.StringSplitOptions.None);
				for (int i = 0; i < questionSubjects.Length; i++){
					if (questionSubjects[i] == "T"){
						subject.Add(true);
					}
					if (questionSubjects[i] == "F"){
						subject.Add(false);
					}
				}
			}
		}
	}
	
	public void Index(int newIndex){
		index = newIndex;
	}
	
	public void Text(string newT){
		text = newT;
	}
}

