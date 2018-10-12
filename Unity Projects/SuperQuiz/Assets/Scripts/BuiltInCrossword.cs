using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuiltInCrossword : MonoBehaviour  {
	
	public GetCrossword(){
		string newWord;
		string newText;
		newWord = "Obiwan";
		newText = "Mestre jedi do filme Guerra nas Estrelas";
		GameplayCrosswordScript.crossword.Add(new Crossword(newWord, newText));
		print ("Added crossword: " + newWord);
		newWord = "Clarke";
		newText = "Arthur ______, autor de ficção científica";
		GameplayCrosswordScript.crossword.Add(new Crossword(newWord, newText));
		print ("Added crossword: " + newWord);
		newWord = "Matrix";
		newText = "Filme no qual a humanidade vive em uma realidade virtual controlada por máquinas";
		GameplayCrosswordScript.crossword.Add(new Crossword(newWord, newText));
		print ("Added crossword: " + newWord);
		newWord = "Edward";
		newText = "______ Harris, ator americano. Trabalhou em Apollo 13 e Snowpiercer";
		GameplayCrosswordScript.crossword.Add(new Crossword(newWord, newText));
		print ("Added crossword: " + newWord);
		newWord = "Deuses";
		newText = "Os Próprios _______, livro de Isaac Asimov";
		GameplayCrosswordScript.crossword.Add(new Crossword(newWord, newText));
		print ("Added crossword: " + newWord);
		newWord = "Brazil";
		newText = "Filme de Terry Gilliam sobre um futuro distópico";
		GameplayCrosswordScript.crossword.Add(new Crossword(newWord, newText));
		print ("Added crossword: " + newWord);
		newWord = "Huxley";
		newText = "Aldous ______, autor de Admirável Mundo Novo";
		GameplayCrosswordScript.crossword.Add(new Crossword(newWord, newText));
		print ("Added crossword: " + newWord);
		newWord = "Engine";
		newText = "The Difference ______, livro de William Gibson e Bruce Sterling";
		GameplayCrosswordScript.crossword.Add(new Crossword(newWord, newText));
		print ("Added crossword: " + newWord);
		newWord = "Animal";
		newText = "______ Farm, livro de George Orwell";
		GameplayCrosswordScript.crossword.Add(new Crossword(newWord, newText));
		print ("Added crossword: " + newWord)
		newWord = "Recall";
		newText = "Total ______, filme de Arnold Schwarzenegger";
		GameplayCrosswordScript.crossword.Add(new Crossword(newWord, newText));
		print ("Added crossword: " + newWord)
		newWord = "Orwell";
		newText = "George ______, autor de 1984";
		GameplayCrosswordScript.crossword.Add(new Crossword(newWord, newText));
		print ("Added crossword: " + newWord)
	}
// O B I W A N (mestre jedi)
// C L A R K E (Arthur Clark)
// M A T R I X (filme)
// E D W A R D (Edward Allen Harris)
// D E U S E S (os proprios deuses, by Isaac Asimov)
// B R A Z I L (filme, by Terry Gilliam)
// H U X L E Y (Aldous Huxley)
// E N G I N E (difference engine, by William Gibson)
// A N I M A L (animal farm, by George Orwell)
// R E C A L L (total recall, filme)
// O R W E L L (George Orwell)
}

