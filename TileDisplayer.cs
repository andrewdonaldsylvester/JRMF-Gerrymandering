using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDisplayer : MonoBehaviour {

	[HideInInspector]
	public string party;

	public TileManager tile;

	public GridManager grid;

	public void Init(TileManager tileScript) {

		tile = tileScript;

		grid = tile.grid;

		party = tile.party;

	}

	public void DrawHighlights() {

		if (tile.district == null) {

			foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()) {

				sr.enabled = false;

			}

			transform.GetComponent<SpriteRenderer> ().enabled = true;

		} else {

			List<TileManager> neighbors = grid.GetNeighbors (gameObject.GetComponent<TileManager>());
			SpriteRenderer[] srList = GetComponentsInChildren<SpriteRenderer> ();

			for (int i = 1; i < srList.Length; i++) {

				srList[i].enabled = true;

				if (neighbors [i-1] != null) {

					if (neighbors [i-1].district == tile.district) {

						srList[i].enabled = false;

					}

				}

			}

		}

	}
}
