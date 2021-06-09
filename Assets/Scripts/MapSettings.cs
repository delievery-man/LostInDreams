#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;


public enum Algorithm
{
     RandomWalk,
     Corridors,
     BSP
}

[Serializable]
[CreateAssetMenu(fileName ="NewMapSettings", menuName = "Maps Settings", order = 0)]
public class MapSettings : ScriptableObject
{
    public Algorithm algorithm;
    public int walkLen;
    public int iterations;
    public int corridorCount;
    public int corridorLen;
    public int roomPercent;
    public int minRoomWidth;
    public int minRoomHeight;
    public int dungeonWidth;
    public int dungeonHeight;
    public int offset;


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
            case Algorithm.BSP:
	            mapLayer.dungeonHeight = EditorGUILayout.IntSlider("Height of map", mapLayer.dungeonHeight, 1, 200);
	            mapLayer.dungeonWidth = EditorGUILayout.IntSlider("Width of map", mapLayer.dungeonWidth, 1, 200);
	            mapLayer.minRoomWidth = EditorGUILayout.IntSlider("Minimal room width", mapLayer.minRoomWidth, 1, 100);
	            mapLayer.minRoomHeight = EditorGUILayout.IntSlider("Minimal room height", mapLayer.minRoomHeight, 1, 100);
	            mapLayer.offset = EditorGUILayout.IntSlider("offset", mapLayer.offset, 1, 10);
	            break;
            
        }

		EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
		AssetDatabase.SaveAssets();

		if(GUI.changed)
			EditorUtility.SetDirty(mapLayer);
    }
}
#endif