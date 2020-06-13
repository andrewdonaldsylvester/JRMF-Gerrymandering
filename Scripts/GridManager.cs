using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GridManager : MonoBehaviour{

	public int rows;

	public int cols;

	private string B = "B";

	private string R = "R";

	private bool generated = false;

	private GameObject[][] tiles;

	private string[][] districtMap;


	// These variables are displayed in the inspector
	[Header("Tile Dimensions")]

	public float tileWidth = 1;

	public float tileHeight = 1;

	[Header("Tile Margins")]

	public float xMargin = 0.1f;

	public float yMargin = 0.1f;

	[Header("Map Assets")]

	public TextAsset mapFile;

	public GameObject tilePrefab;


	[Header("Center the Graph")]

	public bool autoCenter = true;



	public void GenerateGrid() {

		// Destroy all of the old tiles

		while (transform.childCount > 0) {

			GameObject.DestroyImmediate (transform.GetChild (0).gameObject);

		}

		// Load in the file for the map and set our grid dimensions according to the file 

		MapLoader mapLoader = new MapLoader ();

		string[][] districtMap = mapLoader.Read (mapFile);

		rows = districtMap.GetLength (0);
		cols = districtMap[0].GetLength (0);


		GameObject[][] tiles = new GameObject[rows][];


		// Loop through our array and create a tile for each entry

		for (int row = 0; row < rows; row++) {

			for (int col = 0; col < cols; col++) {

				Debug.Log ("Row: " + row + "\nCol: " + col);

				float posX = (col * tileWidth) + (xMargin * (col - 1));
				float posY = (row * -tileHeight) - (yMargin * (row - 1));

				GameObject tile = Instantiate (tilePrefab, transform);

				tile.transform.localPosition = new Vector2 (posX, posY);

				tile.transform.localScale = new Vector2 (tileWidth, tileHeight);

				tile.GetComponent<TileManager> ().Init (districtMap[row][col]);

				Debug.Log (tiles[row]);
				tiles [row] [col] = tile;

			}

		}

		if (autoCenter) {

			float gridWidth = (cols * tileWidth) + ((cols - 1) * xMargin);
			float gridHeight = (rows * tileHeight) + ((rows - 1) * yMargin);

			transform.position = new Vector2 ((tileWidth - gridWidth) / 2, (gridHeight - tileHeight) / 2);

		}
			
		generated = true;

	}


	public void UpdateGrid() {

		if (!generated) {

			GenerateGrid ();

			return;

		}

		MapLoader mapLoader = new MapLoader ();
		string[][] districtMap = mapLoader.Read (mapFile);

		rows = districtMap.GetLength (0);
		cols = districtMap[0].GetLength (0);

		for (int row = 0; row < rows; row++) {

			for (int col = 0; col < cols; col++) {

				float posX = (col * tileWidth) + (xMargin * (col - 1));
				float posY = (row * -tileHeight) - (yMargin * (row - 1));

				GameObject tile = transform.GetChild(col + (rows * row)).gameObject;

				tile.transform.localPosition = new Vector2 (posX, posY);

				tile.transform.localScale = new Vector2 (tileWidth, tileHeight);

				tile.GetComponent<TileManager> ().Init (districtMap[row][col]);

				tiles [row] [col] = tile;

			}

		}



		if (autoCenter) {

			float gridWidth = (cols * tileWidth) + ((cols - 1) * xMargin);
			float gridHeight = (rows * tileHeight) + ((rows - 1) * yMargin);

			transform.position = new Vector2 ((tileWidth - gridWidth) / 2, (gridHeight - tileHeight) / 2);

		}

	}

	public IEnumerable<GameObject> GetNeighbors(GameObject tile) {

		System.Tuple<int?, int?> index = GetIndex (tiles, tile);

		if ((index) != null) {

			// Check above the tile

			if (-1 < index.Item2 && index.Item2 < cols) {

				yield return tiles[cols - 1][rows];

			}


		} else {

			yield break;

		}

	}

	public System.Tuple<int?, int?> GetIndex<GameObject>(GameObject[][] array, GameObject entry) {

		for (int row = 0; row < array.GetLength(0); row++) {

			for (int col = 0; col < array [row].GetLength (0); col++) {

				if (object.ReferenceEquals(array[row][col], entry)) {

					return new System.Tuple<int?, int?> (row, col);

				}

			}

		}

		return new System.Tuple<int?, int?> (null, null); // Replace with IEnumerable? 

	}

}
