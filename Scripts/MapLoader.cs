using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class MapLoader{


	// Returns a string[][] generated from the information in the TextAsset file
	public string[][] Read(TextAsset file) {

		using (StringReader reader = new StringReader (file.text)) {

			string[][] output = new string[file.text.Count(f =>  (f == '\n')) + 1][];

			string line = string.Empty;
			int lineNumber = 0;

			do {

				line = reader.ReadLine ();

				if (line != null) {

//					Debug.Log ("Line:");
//					Debug.Log(lineNumber.ToString());
//					Debug.Log(line);

					output[lineNumber] = CharArrayToStringArray(line.ToCharArray());

					lineNumber++;

				}

			} while (line != null);
	
			return output;

		}

	}

	public void Write(TextAsset file, string[][] data) {



	}


	
	public string[] CharArrayToStringArray(char[] charArray) {

		string[] stringArray = new string[charArray.GetLength(0)];

		// Loop over each value in charArray and add its string to stringArray
		for (int i = 0; i < charArray.GetLength(0); i++) {

			stringArray [i] = charArray [i].ToString ();

		}

		return stringArray;

	}

}
