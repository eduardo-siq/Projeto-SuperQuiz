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
		 "1",
		 "0",
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
		questionsPreLoad.Add(new QuestionPreLoad(
		5,
		0,
		"A Muralha da China foi construída como defesa contra invasores de que país?",
		"Mongólia",
		"Tibete",
		"Japão",
		"Índia",
		"Coréia",
		"X",
		"X"));
		questionsPreLoad.Add(new QuestionPreLoad(
		6,
		4,
		"Quem é o autor dessa obra?",
		"Michelangelo",
		"Leonardo da Vinci",
		"Botticelli",
		"El Greco",
		"Caravaggio",
		"X",
		"X"));
		questionsPreLoad.Add(new QuestionPreLoad(
		7,
		1,
		"A pólvora foi inventada em qual país?",
		"China",
		"",
		"",
		"",
		"",
		"X",
		"X"));
		questionsPreLoad.Add(new QuestionPreLoad(
		8,
		0,
		"Quem é o principal vilão da trilogia original de Guerra nas Estrelas?",
		"Darth Vader",
		"Khan",
		"Sauron",
		"Valdemort",
		"Roy Batty",
		"X",
		"X"));
		questionsPreLoad.Add(new QuestionPreLoad(
		9,
		3,
		"O Renascimento foi um movimento cultural e artístico que começou no final da Idade Média. Ele foi caracterizado pelo redescobrimento das tradições artíticas gregas e romadas, e participaram desse movimento figuras como Michelangelo e Leonardo da Vinci. Onde esse movimento se iníciou?",
		"Península Itálica",
		"Oriente Médio",
		"Península Ibérica",
		"Ilhas Britânicas",
		"Europa Central",
		"X",
		"X"));
		questionsPreLoad.Add(new QuestionPreLoad(
		10,
		5,
		"Quais são macro-regiões onde ficam estados de Minas Gerais e Mato Grosso?",
		"0_1_0_Sudeste",
		"1_1_0_Centro-Oeste",
		"",
		"",
		"",
		"X",
		"X"));
		return questionsPreLoad;
	}
}