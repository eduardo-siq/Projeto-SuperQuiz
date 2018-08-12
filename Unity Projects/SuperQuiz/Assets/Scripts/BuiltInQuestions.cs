using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuiltInQuestions {
	
	public static List<QuestionPreLoad> GetBuildInQuestions(){
		List <QuestionPreLoad> questionsPreLoad = new List <QuestionPreLoad>();
		// questionsPreLoad.Add(new QuestionPreLoad(
		// int newIndex,
		// int newQuestionType,
		// string newT,
		// string newA0,
		// string newA1,
		// string newA2,
		// string newA3,
		// string newA4,
		// string newUserGroup,
		// string newSubject));
		questionsPreLoad.Add(new QuestionPreLoad(
		0,
		0,
		string "Qual país sediou a primeira copa do mundo?",
		string "Uruguai",
		string "Brasil",
		string "França",
		string "Alemanha",
		string "Inglaterra",
		string "X",
		string "X"));
	}
}

