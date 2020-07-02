using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour {

	public void ResetSelectedDistrict() {

		foreach (DistrictManager district in GameObject.FindObjectsOfType<DistrictManager>()) {

			if (district.selected) {

				do {

					district.RemoveTile (district.tiles [0]);

				} while (district.tiles.Count > 0);

			}

		}

	}

	public void ResetMap() {

		foreach (DistrictManager district in GameObject.FindObjectsOfType<DistrictManager>()) {

			do {

				district.RemoveTile (district.tiles [0]);

			} while (district.tiles.Count > 0);

		}

	}

	public void ResetScene() {

		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);

	}

	public void ToScene(string sceneName) {

		SceneManager.LoadScene (sceneName);

	}
}
