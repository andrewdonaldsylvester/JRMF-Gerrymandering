using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictManager : MonoBehaviour {

	public List<TileManager> tiles;

	private const string PARTY_1 = "B";

	private const string PARTY_2 = "R";

	private const string PARTY_3 = "G";

	public bool selected = false;

	public GridManager grid;

	public void Select() {

		SelectionManager.DeselectAllDistricts ();
		selected = true;

		DrawHighlights();

	}


	public void AddTile(TileManager tile) {

		if (!tiles.Contains (tile)) {
		
			tile.district = this;
			tiles.Add (tile);

		}

		TallyVotes ();
		DrawHighlights ();

		FindObjectOfType<ScoreDisplay> ().DrawScores ();

		SelectionManager.CheckRules ();

		GetComponent<TextMesh> ().text = tiles.Count.ToString ();

		if (tiles.Count < grid.districtSize) {

			GetComponent<MeshRenderer> ().enabled = true;

		} else {

			GetComponent<MeshRenderer> ().enabled = false;

		}

	}

	public void RemoveTile(TileManager tile) {

		tile.district = null;
		tiles.Remove (tile);

		TallyVotes ();

		tile.display.DrawTile ();
		DrawHighlights ();

		if (tiles.Count == 0) {

			Destroy (gameObject);

		}

		FindObjectOfType<ScoreDisplay> ().DrawScores ();

		SelectionManager.CheckRules ();

		GetComponent<TextMesh> ().text = tiles.Count.ToString ();

		if (tiles.Count < grid.districtSize) {

			GetComponent<MeshRenderer> ().enabled = true;

		} else {

			GetComponent<MeshRenderer> ().enabled = false;

		}
	
	}

	public string TallyVotes() {

		int p1Votes = 0;
		int p2Votes = 0;
		int p3Votes = 0;

		foreach (TileManager tile in tiles) {

			switch (tile.party) {

			case PARTY_2:
				p1Votes++; break;
			case PARTY_1:
				p2Votes++; break;
			case PARTY_3:
				p3Votes++; break;
			default:
				break;

			}

		}

		if (p1Votes > p2Votes && p1Votes > p3Votes) {

//			foreach (TileManager tile in tiles) {
//
//				tile.transform.GetComponent<SpriteRenderer>().sprite = tile.redSprite;
//
//			}

			return PARTY_2;

		} else if (p2Votes > p1Votes && p2Votes > p3Votes) {

//			foreach (TileManager tile in tiles) {
//
//				tile.transform.GetComponent<SpriteRenderer>().sprite = tile.blueSprite;
//
//			}

			return PARTY_1;

		} else if (p3Votes > p2Votes && p3Votes > p1Votes) {

//			foreach (TileManager tile in tiles) {
//
//				tile.transform.GetComponent<SpriteRenderer>().sprite = tile.greenSprite;
//
//			}

			return PARTY_3;

		} else {

//			foreach (TileManager tile in tiles) {
//
//				tile.transform.GetComponent<SpriteRenderer> ().sprite = tile.tieSprite;
//
//			}

			return "";

		}

	}

	private void DrawHighlights() {

		foreach (Transform tile in GameObject.Find("Grid").transform) {

			tile.GetComponent<TileDisplayer>().DrawTile ();

		}

	}

	public bool Removeable (TileManager tile) {

		int connections = 0;

		foreach (TileManager neighbor in grid.GetNeighbors(tile)) {

			if (neighbor != null) {

				if (neighbor.district == this) {

					connections -= 2;

				}

			}

		}

		foreach (TileManager t in tiles) {

			foreach (TileManager neighbor in grid.GetNeighbors(t)) {

				if (neighbor != null) {

					if (neighbor.district == this) {

						connections += 1;

					}

				}

			}

		}

		if (connections >= 2 * (tiles.Count - 2)) {

			return true;

		} else {

			return false;

		}

	}

}
