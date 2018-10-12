using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuiltInCrossword  {
	
	public static List<Crossword> GetCrossword(){
		List <Crossword> newCrossword = new List <Crossword>();
		string newWord;
		string newText;
		newWord = "OBIWAN";
		newText = "Mestre jedi do filme Guerra nas Estrelas";
		newCrossword.Add(new Crossword(newWord, newText));
		Debug.Log ("Added crossword: " + newWord);
		newWord = "CLARKE";
		newText = "Arthur ______, autor de ficção científica";
		newCrossword.Add(new Crossword(newWord, newText));
		Debug.Log ("Added crossword: " + newWord);
		newWord = "MATRIX";
		newText = "Filme no qual a humanidade vive em uma realidade virtual controlada por máquinas";
		newCrossword.Add(new Crossword(newWord, newText));
		Debug.Log ("Added crossword: " + newWord);
		newWord = "EDWARD";
		newText = "______ Harris, ator americano. Trabalhou em Apollo 13 e Snowpiercer";
		newCrossword.Add(new Crossword(newWord, newText));
		Debug.Log ("Added crossword: " + newWord);
		newWord = "DEUSES";
		newText = "Os Próprios _______, livro de Isaac Asimov";
		newCrossword.Add(new Crossword(newWord, newText));
		Debug.Log ("Added crossword: " + newWord);
		newWord = "BRAZIL";
		newText = "Filme de Terry Gilliam sobre um futuro distópico";
		newCrossword.Add(new Crossword(newWord, newText));
		Debug.Log ("Added crossword: " + newWord);
		newWord = "HUXLEY";
		newText = "Aldous ______, autor de Admirável Mundo Novo";
		newCrossword.Add(new Crossword(newWord, newText));
		Debug.Log ("Added crossword: " + newWord);
		newWord = "ENGINE";
		newText = "The Difference ______, livro de William Gibson e Bruce Sterling";
		newCrossword.Add(new Crossword(newWord, newText));
		Debug.Log ("Added crossword: " + newWord);
		newWord = "ANIMAL";
		newText = "______ Farm, livro de George Orwell";
		newCrossword.Add(new Crossword(newWord, newText));
		Debug.Log ("Added crossword: " + newWord);
		newWord = "RECALL";
		newText = "Total ______, filme de Arnold Schwarzenegger";
		newCrossword.Add(new Crossword(newWord, newText));
		Debug.Log ("Added crossword: " + newWord);
		newWord = "ORWELL";
		newText = "George ______, autor de 1984";
		newCrossword.Add(new Crossword(newWord, newText));
		Debug.Log ("Added crossword: " + newWord);
		return newCrossword;
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

