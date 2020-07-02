using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	private const string PARTY_1 = "B";
	private const string PARTY_2 = "R";
	private const string PARTY_3 = "G";

	public Text p1Score;
	public Text p2Score;
	public Text p3Score;

	public GridManager grid;

	public void DrawScores() {

		int p1 = 0;
		int p2 = 0;
		int p3 = 0;

		// Iterate through all the districts with the right amount of tiles and tally their votes

		foreach (DistrictManager district in FindObjectsOfType<DistrictManager>()) {

			if (district.tiles.Count == FindObjectOfType<GridManager>().districtSize) {
			
				switch (district.TallyVotes ()) {

				case PARTY_1:
					p1++;
					break;
				case PARTY_2:
					p2++;
					break;
				case PARTY_3:
					p3++;
					break;
				default:
					break;

				}

			}

		}

		// Draw the scores onto UI elements

		p1Score.text = p1.ToString ();
		p2Score.text = p2.ToString ();

		if (p3Score != null) {

			p3Score.text = p3.ToString ();

		}

	}

}
