using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class GridManager : MonoBehaviour{
	
	private bool generated = false;

	private string[,] districtMap;

	public int districtSize = 5;

	private int rows;
	private int cols;

	[HideInInspector]
	public TextAsset mapFile;



	// The following variables are editable in the inspector



	[SerializeField]
	private GameObject tilePrefab;

	[Header("Tile Dimensions")]

	[SerializeField]
	private float tileWidth = 1;

	[SerializeField]
	private float tileHeight = 1;

	[Header("Tile Margins")]

	[SerializeField]
	private float xMargin = 0.1f;

	[SerializeField]
	private float yMargin = 0.1f;



	public void CreateTiles() {

		// Destroy all of the old tiles

		while (transform.childCount > 0) {

			GameObject.DestroyImmediate (transform.GetChild (0).gameObject);

		}
			
		// Load in the map file 

		string[,] districtMap = MapLoader.Load (mapFile);

		rows = districtMap.GetLength (0);
		cols = districtMap.GetLength (1);

		// Loop through our array and create a tile for each entry

		for (int row = 0; row < rows; row++) {

			for (int col = 0; col < cols; col++) {
				
				GameObject tile = Instantiate (tilePrefab, transform); // Create the tile

				tile.GetComponent<TileManager> ().grid = this;
				tile.GetComponent<TileManager> ().Init (districtMap[row, col]); // Initialize it 

				tile.name = row + ", " + col;

			}

		}
			
		generated = true;

		PositionTiles ();

	}


	public void PositionTiles() {

		// Make sure that the grid has created tiles

		if (!generated) {

			CreateTiles ();

		}

		// Loop over each tile and reposition and resize it 

		for (int row = 0; row < rows; row++) {

			for (int col = 0; col < cols; col++) {

				TileManager tile = GetTile(row, col);

				float posX = (col * tileWidth) + (xMargin * (col));
				float posY = (row * -tileHeight) - (yMargin * (row));

				tile.gameObject.transform.localPosition = new Vector2 (posX, posY);
				tile.gameObject.transform.localScale = new Vector2 (tileWidth, tileHeight);

			}

		}

	}

	public void Center() {

		float gridWidth = (cols * tileWidth) + ((cols - 1) * xMargin);
		float gridHeight = (rows * tileHeight) + ((rows - 1) * yMargin);

		transform.position = new Vector2 ((tileWidth - gridWidth) / 2, (gridHeight - tileHeight) / 2);

	}

	public TileManager GetTile(int row, int col) {

		if (GameObject.Find (row + ", " + col) != null) {
			
			return GameObject.Find (row + ", " + col).GetComponent<TileManager> ();

		} else {

			return null;

		}

	}

	public List<TileManager> GetNeighbors(TileManager tile) {

		// Returns a List with all of the tiles neighboring a given tile in clock wise order (up, right, down, left)
		// This does not count diagonal tiles. (RW)

		List<TileManager> returnList = new List<TileManager> {null, null, null, null};


		string[] index = Regex.Split (tile.name, @"\D+");

		int row = int.Parse(index[0]);
		int col = int.Parse(index[1]);
		print (row);
		print (col);

		// Check above the tile

		returnList[0] = GetTile(row - 1, col);

		// Check to the right of the tile

		returnList[1] = GetTile(row, col + 1);

		// Check below the tile

		returnList[2] = GetTile(row + 1, col);

		// Check to the left of the tile

		returnList[3] = GetTile(row, col - 1);

		return returnList;

	}

}
