using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	private const string BLUE = "B";

	private const string RED = "R";

	private const string GREEN = "G";

	private SpriteRenderer sr;

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



	public string party;

	public bool selected = false;

	public GameObject districtPrefab;

	public DistrictManager district;



	public void Init(string party) {

		// Match the defaultColor variable to the party
		
		this.party = party;

		switch (this.party) {
			
		case RED:
			defaultSprite = Resources.Load<Sprite> ("Sprites/red"); 
			redSprite = Resources.Load<Sprite> ("Sprites/redR"); 
			blueSprite = Resources.Load<Sprite> ("Sprites/redB"); 
			greenSprite = Resources.Load<Sprite>("Sprites/redG"); 
			tieSprite = Resources.Load<Sprite>("Sprites/redT"); 
			break;
		case BLUE:
			defaultSprite = Resources.Load<Sprite> ("Sprites/blue"); 
			redSprite = Resources.Load<Sprite> ("Sprites/blueR");
			blueSprite = Resources.Load<Sprite> ("Sprites/blueB");
			greenSprite = Resources.Load<Sprite> ("Sprites/blueG");
			tieSprite = Resources.Load<Sprite>("Sprites/blueT"); 
			break;
		case GREEN:
			defaultSprite = Resources.Load<Sprite> ("Sprites/green");
			redSprite = Resources.Load<Sprite> ("Sprites/greenR");
			blueSprite = Resources.Load<Sprite> ("Sprites/greenB");
			greenSprite = Resources.Load<Sprite> ("Sprites/greenG");
			tieSprite = Resources.Load<Sprite>("Sprites/greenT"); 
			break;
		default:
			defaultSprite = Resources.Load<Sprite> ("notfound");
			redSprite = Resources.Load<Sprite> ("notfound");
			blueSprite = Resources.Load<Sprite> ("notfound");
			greenSprite = Resources.Load<Sprite> ("notfound");
			tieSprite = Resources.Load<Sprite>("notfound"); 
			break;

		}

		GetComponent<SpriteRenderer>().sprite = defaultSprite;
	
	}



	void OnMouseDown() {

		if (!selected && NeighborSelected () && SelectionManager.CountSelectedTiles () < grid.districtSize) {
			
			// Set to selected when dragged to from a selected neighbor
			Select ();

		} else if (!selected) {

			SelectionManager.DeselectAll ();
			Select ();

		} else if (selected) {

			Deselect ();

		}

	}

	void OnMouseEnter() {

		if (!selected && Input.GetMouseButton (0) && NeighborSelected () && SelectionManager.CountSelectedTiles () < grid.districtSize) {

			Select ();

		} 

	}

	public bool NeighborSelected() {

		foreach (TileManager neighbor in grid.GetNeighbors (this)) {

			if (neighbor != null) {

				if (neighbor.selected == true) {

					return true;

				}

			}

		}

		return false;

	}

	public int NumNeighborsSelected() {

		int numSelected = 0;

		foreach (TileManager neighbor in grid.GetNeighbors(this)) {

			if (neighbor != null) {

				if (neighbor.selected == true) {

							numSelected++;

				}

			}

		}

		return numSelected;

	}

	public void Select() {

		selected = true;

		if (SelectionManager.CountSelectedTiles () == 1) {

			district = Instantiate (districtPrefab).GetComponent<DistrictManager>();
			district.AddTile (this);

		} else {

			foreach (TileManager neighbor in grid.GetNeighbors (this)) {

				if (neighbor != null) {

					if (neighbor.selected == true && neighbor.district != null) {

						neighbor.district.AddTile (this);
						district = neighbor.district;

					}

				}

			}

		}

	}

	public void Deselect() {

		selected = false;
		GetComponent<SpriteRenderer>().sprite = defaultSprite;

		if (district != null) {
			
			district.RemoveTile (this);
			district = null;

		}
	}

}
