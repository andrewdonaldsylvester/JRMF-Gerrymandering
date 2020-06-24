using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridManager))]
public class GridEditor : Editor {

	public override void OnInspectorGUI() {

		base.OnInspectorGUI ();

		GridManager grid = (GridManager)target;


		EditorGUI.BeginChangeCheck ();

		EditorGUILayout.PropertyField (serializedObject.FindProperty("mapFile"), new GUIContent("Map File"));

		serializedObject.ApplyModifiedProperties ();

		if (EditorGUI.EndChangeCheck()) {
			
			grid.CreateTiles();

		}

		if (GUILayout.Button("Center")) {

			grid.Center();

		}

		// Update the grid if any parameters were changed

		if (GUI.changed) {

			grid.PositionTiles ();

		}

	}

}
