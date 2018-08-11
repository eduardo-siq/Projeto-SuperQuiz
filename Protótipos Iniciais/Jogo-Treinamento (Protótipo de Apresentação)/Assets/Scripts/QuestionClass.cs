using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question {
	
	public int index;
	public string text;
	public string answer0;	// Right answer
	public string answer1;
	public string answer2;
	public string answer3;
	public string answer4;

	public Question (int newIndex, string newT, string newA0, string newA1, string newA2, string newA3, string newA4){
		index = newIndex;
		text = newT;
		answer0 = newA0;
		answer1 = newA1;
		answer2 = newA2;
		answer3 = newA3;
		answer4 = newA4;
	}
	
	public Question(){
		index = 0;
		text = "";
		answer0 = "";
		answer1 = "";
		answer2 = "";
		answer3 = "";
		answer4 = "";	
	}
	
	public Question (Question baseQuestion){
		index = baseQuestion.index;
		text = baseQuestion.text;
		answer0 = baseQuestion.answer0;
		answer1 = baseQuestion.answer1;
		answer2 = baseQuestion.answer2;
		answer3 = baseQuestion.answer3;
		answer4 = baseQuestion.answer4;
	}
	
	public void Index(int newIndex){
		index = newIndex;
	}
	
	public void Text (string newT){
		text = newT;
	}
}

public class Questions {
	
	public List<Question> questions;
	public int numberOfQuestions = 0;
	
	public Questions (){
		//if (i < 1) return;
		Question question;
		questions = new List<Question>();
//		for (int y = 0; y < i; y++){
//			questions.Add(new Question());
//		}
		// List of standard questions, each one added until their index surpasses i
		questions.Add(new Question(
		0 
		, "O autor do livro '2001: Uma Odisséia' no Espaço é..."
		, "Arthur C. Clark"
		, "Isaac Asimov"
		, "Neal Stephenson"
		, "Bruce Gibson"
		, "Philip K. Dick"
		));
		numberOfQuestions = questions.Count;
		questions.Add(new Question(
		1 
		, "O livro fundador do gênero 'Cyberpunk' é..."
		, "Neuromancer"
		, "Máquina Diferencial"
		, "Snow Crash"
		, "Fundação"
		, "O Fim da Infância"
		));
		numberOfQuestions = questions.Count;
		questions.Add(new Question(
		2 
		, "O livro fundador do gênero 'Steampunk' moderno é..."
		, "Máquina Diferencial"
		, "Neuromancer"
		, "Snow Crash"
		, "Fundação"
		, "O Fim da Infância"
		));
		numberOfQuestions = questions.Count;
		questions.Add(new Question(
		3 
		, "O filme 'Blade Runner', de 1982 e dirigido por Ridley Scott, é baseado no livro..."
		, "Andróides Sonham com Ovelhas Elétricas?"
		, "Eu, Robô"
		, "Virtual Light"
		, "Fundação"
		, "Ubik"
		));
		numberOfQuestions = questions.Count;
		questions.Add(new Question(
		4 
		, "A história do livro 'O Homem no Castelo Alto', de Philip K. Dick, se passa em um mundo alternativo onde..."
		, "O Eixo ganha a 2ª Guerra Mundial"
		, "Computadores existiam já no século XIX"
		, "EUA e URSS se destruíram em uma guerra"
		, "Cientistas se comunicam com outra dimensão"
		, "Extraterrestres acompanharam a evolução humana"
		));
		numberOfQuestions = questions.Count;
		questions.Add(new Question(
		5 
		,"Além do livro 'Neuromancer', fazem parte da chamda 'trilogia Sprawl' os livros..."
		, "Count Zero & Monalisa Overdrive"
		, "Burning Chrome & Virtual Light"
		, "Burning Chrome & Count Zero"
		, "Monalisa Overdrive & Virtual Light"
		, "Count Zero & Virtual Light"
		));
		numberOfQuestions = questions.Count;
		questions.Add(new Question(
		6 
		,"A inteligência artificial Neuromancer, do livro homônimo, tem uma 'irmã', cujo nome é..." 
		, "Wintermute"
		, "HAL 9000"
		, "Rama"
		, "Matrix"
		, "Aleph"
		));
		numberOfQuestions = questions.Count;
		questions.Add(new Question(
		7 
		, "Em 'Fundação', de Isaac Asimov, cientistas desenvolvem um novo campo de conhecimento..." 
		, "Que prevê o desenvolvimento histórico"
		, "Que permite a viagem no tempo"
		, "Que cria realidades virtuais"
		, "Que permite a construção de andróides"
		, "Que desenvolve habilidades telepáticas"
		));
		numberOfQuestions = questions.Count;
		questions.Add(new Question(
		8
		, "O autor do livro 'Duna' é..." 
		, "Frank Herbert"
		, "Niel Stephenson"
		, "Bruce Sterling"
		, "Isaac Asimov"
		, "Arthur C. Clark"
		));
		numberOfQuestions = questions.Count;
		questions.Add(new Question(
		9
		, "O livro 'Máquina Diferencial' foi escrito em conjunto por..." 
		, "Bruce Sterling & William Gibson"
		, "Bruce Sterling & Neal Stephenson"
		, "William Gibson & Arthur C. Clark"
		, "Isaac Asimov & Arthur C. Clark"
		, "Neal Stephenson & Isaac Asimov"
		));
		numberOfQuestions = questions.Count;
		questions.Add(new Question(
		10
		,"O 'Kwisatz Haderach' é um lider profetizado no universo ficcional de..." 
		, "Duna"
		, "Funcação"
		, "2001: uma Odisséia no Espaço"
		, "Ubik"
		, "Ao Cair da Noite"
		));
		numberOfQuestions = questions.Count;
		questions.Add(new Question(
		11 
		, "No conto 'Ao Cair da Noite', de Isaac Asimov, é apresentado um planeta que com vários sóis..." 
		, "E só há uma noite a cada 2000 anos"
		, "E eles nunca se põe ao mesmo tempo"
		, "Que tornam o planeta inabitável"
		, "Mas, apesar disso, o planeta é gelado"
		, "Prestes a ser colonizado pela primeira vez"
		));
		numberOfQuestions = questions.Count;
		questions.Add(new Question(
		12 
		, "No universo de 'Duna', há uma poderosa droga chamada 'melange'. A galáxia é governada por várias famílias nobres, que lutam principalmente..."  
		, "Pelo controle do comércio de 'melange'"
		, "Para produzir 'menalge' em seus planetas"
		, "Para proibir o consumo de 'melange'"
		, "Para dominar os planetas que produzem 'melange'"
		, "Para impor o consumo de 'melange'"
		));
		numberOfQuestions = questions.Count;
		questions.Add(new Question(
		13 
		, "Na trama de 'Duna'..."
		, "A Casa Atreides recebe o domínio de Arrakis"
		, "O povo 'Fremen' invade o planeta Arrakis"
		, "O imperador Padisha liberta o planeta Arrakis"
		, "A Guilda Espacial bloqueia o planeta Arrakis"
		, "Os soldados 'sardaukar' são treinados em Arrakis"
		));
		numberOfQuestions = questions.Count;
		questions.Add(new Question(
		14 
		, "Um autor considerado precursor da ficção científica é..." 
		, "Júlio Verne"
		, "Hugo Gernsback"
		, "H.P. Lovecraft"
		, "Robert E. Howard"
		, "Jean Giraud"
		));
		numberOfQuestions = questions.Count;
		// etc...
		
				
/*		questions.Add(new Question(
		i 
		,t 
		,1 
		,2 
		,3
		,4
		,5
		));
		numberOfQuestions = questions.Count;	*/
		
	}
}

