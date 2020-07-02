using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDisplayer : MonoBehaviour {

	private const string PARTY_1 = "B";

	private const string PARTY_2 = "R";

	private const string PARTY_3 = "G";

	public Color defaultColor = new Color(0, 0, 0, 0);
	public Color selectedColor = new Color(0, 0, 0, 0);

	[HideInInspector]
	public string party;

	public TileManager tile;

	public GridManager grid;

	private SpriteRenderer sr = new SpriteRenderer();

	public void Init(TileManager tileScript) {

		tile = tileScript;

		grid = tile.grid;

		party = tile.party;

		sr = GetComponent<SpriteRenderer> ();

		switch (party) {

		case PARTY_1:
			defaultColor = grid.p1Color;
			selectedColor = grid.p1SelectedColor;
			sr.color = defaultColor;
			break;

		case PARTY_2:
			defaultColor = grid.p2Color;
			selectedColor = grid.p2SelectedColor;
			sr.color = defaultColor;
			break;

		case PARTY_3:
			defaultColor = grid.p3Color;
			selectedColor = grid.p3SelectedColor;
			sr.color = defaultColor;
			break;

		default:
			defaultColor = Color.white;
			selectedColor = Color.white;
			sr.color = defaultColor;
			break;

		}

		DrawTile ();

	}

	public void DrawTile() {

		if (tile.district 	== null) {

			// Don't draw anything if the tile isn't in a district

			GetComponent<SpriteRenderer>().color = defaultColor;

			foreach (SpriteRenderer childSr in GetComponentsInChildren<SpriteRenderer>()) {

				childSr.enabled = false;

			}

			GetComponent<SpriteRenderer>().enabled = true;

		} else {

			// Highlight the tile if it's in the selected district

			if (tile.district.selected) {

				GetComponent<SpriteRenderer>().color = selectedColor;

			} else {

				GetComponent<SpriteRenderer>().color = defaultColor;

			}

			// Draw the outlines for the district and color it according to the winner of the district

			List<TileManager> neighbors = grid.GetNeighbors (gameObject.GetComponent<TileManager>());
			SpriteRenderer[] srList = GetComponentsInChildren<SpriteRenderer> ();

			for (int i = 1; i <= 4; i++) {

				switch (tile.district.TallyVotes ()) {

				case PARTY_1:
					srList [i].color = grid.p1WinColor;
					break;
				case PARTY_2:
					srList [i].color = grid.p2WinColor;
					break;
				case PARTY_3:
					srList [i].color = grid.p3WinColor;
					break;
				default:
					srList [i].color = grid.tieColor;
					break;

				}

				srList[i].enabled = true;

				if (neighbors [i-1] != null) {

					if (neighbors [i-1].district == tile.district) {

						srList[i].enabled = false;

					}

				}

			}

			List<TileManager> diagonals = grid.GetDiagonals (tile);

			for (int i = 5; i <= 8; i++) {

				switch (tile.district.TallyVotes ()) {

				case PARTY_1:
					srList [i].color = grid.p1WinColor;
					break;
				case PARTY_2:
					srList [i].color = grid.p2WinColor;
					break;
				case PARTY_3:
					srList [i].color = grid.p3WinColor;
					break;
				default:
					srList [i].color = grid.tieColor;
					break;

				}

				srList[i].enabled = true;

				if (diagonals [i - 5] != null) {

					if (diagonals [i - 5].district == tile.district) {

						srList [i].enabled = false;

					}

				} else {

					srList [i].enabled = false;

				}

			}

		}

	}

}
