using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictManager : MonoBehaviour {

	private List<TileManager> tiles = new List<TileManager>();

	private const string BLUE = "B";

	private const string RED = "R";

	private const string GREEN = "G";



	public void AddTile(TileManager tile) {

		if (!tiles.Contains (tile)) {
		
			tiles.Add (tile);

		}

		TallyVotes ();

	}

	public void RemoveTile(TileManager tile) {

		tiles.Remove (tile);

		if (tiles.Count == 0) {

			Destroy (gameObject);

		}

		TallyVotes ();

	}

	public void TallyVotes() {

		int rVotes = 0;
		int bVotes = 0;
		int gVotes = 0;

		foreach (TileManager tile in tiles) {

			switch (tile.party) {

			case RED:
				rVotes++; break;
			case BLUE:
				bVotes++; break;
			case GREEN:
				gVotes++; break;
			default:
				break;

			}

		}

		if (rVotes > bVotes && rVotes > gVotes) {

			foreach (TileManager tile in tiles) {

				tile.transform.GetComponent<SpriteRenderer>().sprite = tile.redSprite;

			}

		} else if (bVotes > rVotes && bVotes > gVotes) {

			foreach (TileManager tile in tiles) {

				tile.transform.GetComponent<SpriteRenderer>().sprite = tile.blueSprite;

			}

		} else if (gVotes > bVotes && gVotes > rVotes) {

			foreach (TileManager tile in tiles) {

				tile.transform.GetComponent<SpriteRenderer>().sprite = tile.greenSprite;

			}

		} else {

			foreach (TileManager tile in tiles) {

				tile.transform.GetComponent<SpriteRenderer> ().sprite = tile.tieSprite;

			}

		}

	}

}
