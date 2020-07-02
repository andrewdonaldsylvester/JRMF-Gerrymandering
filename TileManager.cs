using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	[HideInInspector]
	public Sprite defaultSprite;

	[HideInInspector]
	public Sprite redSprite;

	[HideInInspector]
	public Sprite blueSprite;

	[HideInInspector]
	public Sprite greenSprite;

	[HideInInspector]
	public Sprite tieSprite;

	[HideInInspector]
	public GridManager grid;


	public TileDisplayer display = new TileDisplayer();

	public string party;

	public bool selected = false;

	public GameObject districtPrefab;

	public DistrictManager district;



	public void Init(string party) {

		// Match the defaultColor variable to the party
		
		this.party = party;

//		switch (this.party) { 
//				
//		this will need to be refactored for the party names
//
//			case RED:
//				defaultSprite = Resources.Load<Sprite> ("Sprites/red"); 
//				redSprite = Resources.Load<Sprite> ("Sprites/redR"); 
//				blueSprite = Resources.Load<Sprite> ("Sprites/redB"); 
//				greenSprite = Resources.Load<Sprite>("Sprites/redG"); 
//				tieSprite = Resources.Load<Sprite>("Sprites/redT"); 
//				break;
//			case BLUE:
//				defaultSprite = Resources.Load<Sprite> ("Sprites/blue"); 
//				redSprite = Resources.Load<Sprite> ("Sprites/blueR");
//				blueSprite = Resources.Load<Sprite> ("Sprites/blueB");
//				greenSprite = Resources.Load<Sprite> ("Sprites/blueG");
//				tieSprite = Resources.Load<Sprite>("Sprites/blueT"); 
//				break;
//			case GREEN:
//				defaultSprite = Resources.Load<Sprite> ("Sprites/green");
//				redSprite = Resources.Load<Sprite> ("Sprites/greenR");
//				blueSprite = Resources.Load<Sprite> ("Sprites/greenB");
//				greenSprite = Resources.Load<Sprite> ("Sprites/greenG");
//				tieSprite = Resources.Load<Sprite>("Sprites/greenT"); 
//				break;
//			default:
//				defaultSprite = Resources.Load<Sprite> ("notfound");
//				redSprite = Resources.Load<Sprite> ("notfound");
//				blueSprite = Resources.Load<Sprite> ("notfound");
//				greenSprite = Resources.Load<Sprite> ("notfound");
//				tieSprite = Resources.Load<Sprite>("notfound"); 
//				break;
//
//			}
//
//		GetComponent<SpriteRenderer>().sprite = defaultSprite;
//
//		DrawHighlights ();

		display = GetComponent<TileDisplayer> ();

		display.Init (this);
	
	}



	void OnMouseDown() {

		// Iterate through each of the adjacent tiles
		// Add this tile to a district if a neighbor is in the selected district

		if (district == null) {

			foreach (TileManager neighbor in grid.GetNeighbors (this)) {

				if (neighbor != null) {

					if (neighbor.district != null) {

						if (neighbor.district.selected && neighbor.district.tiles.Count < grid.districtSize) {

							neighbor.district.AddTile (this);

						}

					}

				}

			}

			// If none of the adjacent tiles were in a selected district, create a new district and select it

			if (district == null) {

				district = Instantiate (districtPrefab).GetComponent<DistrictManager> ();
				district.grid = grid;

				district.transform.position = transform.position;

				district.Select ();

				district.AddTile (this);

			}
				
		} else {

			// Select the district that the tile is in when it is clicked on

			if (!district.selected) {

				district.Select ();

			}

		}

	}



	void OnMouseEnter() {

		if (district == null && Input.GetMouseButton (0)) {

			foreach (TileManager neighbor in grid.GetNeighbors (this)) {

				if (neighbor != null) {

					if (neighbor.district != null) {

						if (neighbor.district.selected && neighbor.district.tiles.Count < grid.districtSize) {

							neighbor.district.AddTile (this);

						}

					}

				}

			}

		} else if (Input.GetMouseButton (1) && district != null) {

			if (!district.selected) {

				district.Select ();

			} else if (district.Removeable (this)) {

				district.RemoveTile (this);



			} else {

				// Here we can show the player that the tile can't be removed if we want to do that

			}
		}

	}

	void OnMouseOver() {

		if (Input.GetMouseButtonDown (1) && district != null) {

			if (!district.selected) {

				district.Select ();

			} else if (district.Removeable (this)) {

				district.RemoveTile (this);

			} else {

				// Here we can show the player that the tile can't be removed if we want to do that

			}

		}

	}

}
