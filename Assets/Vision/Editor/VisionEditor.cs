using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Vision))]
public class VisionEditor : Editor {

	Vision vision;

	void OnEnable() {

		vision = (Vision) target;

	}

	public override void OnInspectorGUI() {
		
		if ( VisibleGrid.instance == null ) {
			EditorGUILayout.LabelField("Please, create VisibleGrid object.");
			return;
		}

		vision.visionDistance = EditorGUILayout.Slider(vision.visionDistance, 0, VisibleGrid.instance.gridStep);
		
	}

}
