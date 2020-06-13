using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	private SpriteRenderer sRenderer;

	private const string B = "B";

	private const string R = "R";

	private const string G = "G";

	private Color defaultColor;

	public string party;



	public void Init(string party) {

		// Match the defaultColor variable to the party

		sRenderer = GetComponent<SpriteRenderer> ();

		this.party = party;

		switch (this.party) {
			
		case R:
			defaultColor = Color.red;
			break;
		case B:
			defaultColor = Color.blue;
			break;
		case G:
			defaultColor = Color.green;
			break;
		default:
			defaultColor = Color.grey;
			break;

		}

		sRenderer.color = defaultColor;

	}



	void Update() {

		// Revert back to the default color when the mouse is released

		if (Input.GetMouseButtonUp (0)) {

			if (party == B) {

				defaultColor = Color.blue;

			} else if (party == R) {

				defaultColor = Color.red;

			}

			sRenderer = GetComponent<SpriteRenderer> ();

			sRenderer.color = defaultColor;

		}

	}



	void OnMouseOver() {

		if (Input.GetMouseButton(0)) {
			
			// Set the color to grey if the mouse clicks on this tile	

			sRenderer = GetComponent<SpriteRenderer> ();

			sRenderer.color = Color.grey;

		}

	}

}
