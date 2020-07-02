using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager {

	private const string PARTY_1 = "B";

	private const string PARTY_2 = "R";

	private const string PARTY_3 = "G";

	public static void DeselectAllDistricts() {

		foreach (DistrictManager district in GameObject.FindObjectsOfType<DistrictManager>()) {

			district.selected = false;
//			district.GetComponent<MeshRenderer> ().enabled = false;

		}

	}

	public static void CheckRules() {

		bool allTilesSelected = true;

		foreach (TileManager tile in GameObject.FindObjectsOfType<TileManager>()) {

			if (tile.district == null) {

				allTilesSelected = false;

			}

		}

		if (allTilesSelected) {

			GameObject.Find ("Rule 2").GetComponent<Text> ().color = Color.green;

		} else {
			
			GameObject.Find ("Rule 2").GetComponent<Text> ().color = Color.red;

		}

		int p1 = 0;
		int p2 = 0;
		int p3 = 0;

		foreach (DistrictManager district in GameObject.FindObjectsOfType<DistrictManager>()) {

			if (district.tiles.Count == GameObject.Find("Grid").GetComponent<GridManager>().districtSize) {

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

		bool p1Wins = false;

		if (p1 > p2 && p1 > p3) {
			
			p1Wins = true;
			GameObject.Find ("Rule 1").GetComponent<Text> ().color = Color.green;

		} else {

			GameObject.Find ("Rule 1").GetComponent<Text> ().color = Color.red;

		}

		if (p1Wins && allTilesSelected) {

			GameObject.Find ("Level UI").transform.Find ("Next Map").gameObject.SetActive (true);

		}

	}

}
