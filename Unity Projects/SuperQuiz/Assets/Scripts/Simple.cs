using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple : MonoBehaviour {

	public static string SimpleText(string text){
		string character = "";
        for (int i = 0; i < text.Length; i++){
            print("index " + i);
            character = text.Substring(i, 1);
			// Diacritcs
            if (character == "à"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "a");
            }
            if (character == "Á"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "a");
            }
            if (character == "á"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "a");
            }
            if (character == "À"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "a");
            }
            if (character == "ä"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "a");
            }
            if (character == "Ä"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "a");
            }
            if (character == "ã"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "a");
            }
            if (character == "Ã"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "a");
            }
            if (character == "â"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "a");
            }
            if (character == "Â"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "a");
            }
            if (character == "è"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "a");
            }
            if (character == "È"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "e");
            }
            if (character == "é"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "e");
            }
            if (character == "É"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "e");
            }
            if (character == "ë"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "e");
            }
            if (character == "Ë"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "e");
            }
            if (character == "ê"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "e");
            }
            if (character == "Ê"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "e");
            }
            if (character == "ì"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "i");
            }
            if (character == "Ì"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "i");
            }
            if (character == "í"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "i");
            }
            if (character == "Í"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "i");
            }
            if (character == "ï"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "i");
            }
            if (character == "Ï"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "i");
            }
            if (character == "î"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "i");
            }
            if (character == "Î"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "i");
            }
            if (character == "ò"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "o");
            }
            if (character == "Ò"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "o");
            }
            if (character == "ó"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "o");
            }
            if (character == "Ó"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "o");
            }
            if (character == "õ"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "o");
            }
            if (character == "Õ"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "o");
            }
            if (character == "ô"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "o");
            }
            if (character == "Ô"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "o");
            }
            if (character == "ö"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "o");
            }
            if (character == "Ö"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "o");
            }
            if (character == "ù"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "u");
            }
            if (character == "Ù"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "u");
            }
            if (character == "ú"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "u");
            }
            if (character == "Ú"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "u");
            }
            if (character == "û"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "u");
            }
            if (character == "Û"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "u");
            }
            if (character == "ü"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "u");
            }
            if (character == "Û"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "u");
            }
            // Capital letters
            if (character == "A"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "a");
            }
            if (character == "B"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "b");
            }
            if (character == "C"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "c");
            }
            if (character == "Ç"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "ç");
            }
            if (character == "D"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "d");
            }
            if (character == "E"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "e");
            }
            if (character == "F"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "f");
            }
            if (character == "G"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "g");
            }
            if (character == "H"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "h");
            }
            if (character == "I"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "i");
            }
            if (character == "J"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "j");
            }
            if (character == "K"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "");
            }
            if (character == "L"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "l");
            }
            if (character == "M"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "m");
            }
            if (character == "N"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "n");
            }
            if (character == "O"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "o");
            }
            if (character == "P"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "p");
            }
            if (character == "Q"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "q");
            }
            if (character == "R"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "r");
            }
            if (character == "S"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "s");
            }
            if (character == "T"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "t");
            }
            if (character == "U"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "u");
            }
            if (character == "V"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "v");
            }
            if (character == "X"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "x");
            }
            if (character == "Z"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "z");
            }
            if (character == "W"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "w");
            }
            if (character == "Y"){
                text = text.Remove(i, 1);
                text = text.Insert(i, "y");
            }
        }
        print(text);
        return text;
    }
}
