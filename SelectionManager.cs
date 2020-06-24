using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager {

	public static int CountSelectedTiles() {

		int numSelected = 0;

		foreach (TileManager tile in GameObject.FindObjectsOfType<TileManager>()) {

			if (tile.selected) {

				numSelected++;

			}

		}

		return numSelected;

	}

	public static void DeselectAll() {

		foreach (TileManager tile in GameObject.FindObjectsOfType<TileManager>()) {

			tile.Deselect ();

		}

	}
}
