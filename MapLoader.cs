using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class MapLoader {

	// Returns a string[,] generated from the information in the TextAsset file

	public static string[,] Load(TextAsset file) {

		int arrayRows;
		int arrayCols;

		using (StringReader reader = new StringReader (file.text)) {

			// Read the file and determine how big the array should be 

			arrayRows = file.text.Count(f =>  (f == '\n')) + 1; // Count how many lines there are
			arrayCols = reader.ReadLine ().Length; // Count how many characters are in each line

		}

		using (StringReader reader = new StringReader (file.text)) {

			// Read the file again, this time populating the array

			string[,] returnArray = new string[arrayRows, arrayCols];

			string line = string.Empty;
			int lineNumber = 0;


			do {

				line = reader.ReadLine ();

				if (line != null) {

					for (int i = 0; i < line.Length; i++) {

						// Add each character in each line to the array

						char letter = line[i];

						if (letter != '\n') {
							
							returnArray[lineNumber, i] = letter.ToString();

						}

					}

					lineNumber++;

				}

			} while (line != null);

			return returnArray;

		}

	}

//	public void Write(TextAsset file, string[,] data) {
//
//		Might not need this
//
//	}

}
