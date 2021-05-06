using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum Algorithm
{
     RandomWalk,
     Corridors
}

[System.Serializable]
[CreateAssetMenu(fileName ="NewMapSettings", menuName = "Map Settings", order = 0)]
public class MapSettings : ScriptableObject
{
    public Algorithm algorithm;
    public int walkLen;
    public int iterations;
    public int corridorCount;
    public int corridorLen;
    public int roomPercent;
    private static System.Random rnd = new System.Random();

}

[CustomEditor(typeof(MapSettings))]
public class MapSettings_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        MapSettings mapLayer = (MapSettings)target;
		GUI.changed = false;
		EditorGUILayout.LabelField(mapLayer.name, EditorStyles.boldLabel);

		mapLayer.algorithm = (Algorithm)EditorGUILayout.EnumPopup(new GUIContent("Generation Method", "The generation method we want to use to generate the map"), mapLayer.algorithm);

        //Shows different options depending on what algorithm is selected
        switch (mapLayer.algorithm)
        {
            case Algorithm.RandomWalk:
                mapLayer.iterations = EditorGUILayout.IntSlider("Interval Of Points",mapLayer.iterations, 1, 100);
                mapLayer.walkLen = EditorGUILayout.IntSlider("Walk Length",mapLayer.walkLen, 1, 100);
                break;
            case Algorithm.Corridors:
	            mapLayer.corridorCount = EditorGUILayout.IntSlider("Count of corridors",mapLayer.corridorCount, 1, 50);
	            mapLayer.corridorLen = EditorGUILayout.IntSlider("Corridor Length",mapLayer.corridorLen, 1, 100);
	            mapLayer.iterations = EditorGUILayout.IntSlider("Iterations",mapLayer.iterations, 1, 100);
	            mapLayer.walkLen = EditorGUILayout.IntSlider("Room build length",mapLayer.walkLen, 1, 100);
	            mapLayer.roomPercent = EditorGUILayout.IntSlider("Room Percent",mapLayer.roomPercent, 1, 100);

	            break;
            
        }

		EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
		AssetDatabase.SaveAssets();

		if(GUI.changed)
			EditorUtility.SetDirty(mapLayer);
    }
}
