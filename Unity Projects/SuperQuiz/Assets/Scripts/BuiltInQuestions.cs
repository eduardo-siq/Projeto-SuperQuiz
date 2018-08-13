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
		"Qual país é famoso por suas pirâmides?",
		"Egito",
		"Itália",
		"Rússia",
		"Japão",
		"Índia",
		"X",
		"X"));
		questionsPreLoad.Add(new QuestionPreLoad(
		1,
		1,
		"Em qual país surgiu a democracia?",
		"Grécia",
		"",
		"",
		"",
		"",
		"X",
		"X"));
		questionsPreLoad.Add(new QuestionPreLoad(
		2,
		2,
		"Qual é a macro-região mais populosa do Brasil?",
		"Sudeste",
		 "0",
		 "0",
		 "1",
		 "",
		 "X",
		 "X"));
		questionsPreLoad.Add(new QuestionPreLoad(
		3,
		3,
		"A primeira edição da Copa do Mundo foi disputada no Uruguai, em Montevidéu, no ano de 1930. Apenas treze equipes nacionais reuniram-se nessa ocasião, sendo que somente quatro países europeus atravessaram o Oceano Atlântico de navio para competir o torneio. Qual país foi o vencedor?",
		"Uruguai",
		"Brasil",
		"França",
		"Alemanha",
		"Inglaterra",
		"X",
		"X"));
		questionsPreLoad.Add(new QuestionPreLoad(
		4,
		4,
		"De qual país é essa bandeira?",
		"Reino Unido",
		"Inglaterra",
		"Canadá",
		"EUA",
		"Escócia",
		"X",
		"X"));
		return questionsPreLoad;
	}
}