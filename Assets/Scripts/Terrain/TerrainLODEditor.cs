using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainLOD))]
public class TerrainLODEditor : Editor
{
    TerrainLOD terrainLOD;

    int tab;

    private void Awake() {
        terrainLOD = (target as TerrainLOD);
    }

    [CustomEditor(typeof(TerrainLOD))]
    public override void OnInspectorGUI(){
        tab = GUILayout.Toolbar (tab, new string[] {"Setup", "View"});
        switch (tab) {
            case 0:
                EditorGUILayout.BeginHorizontal ();
                EditorGUILayout.PrefixLabel ("Size");
                Settings.size = EditorGUILayout.IntField(Settings.size);
                EditorGUILayout.EndHorizontal ();

                EditorGUILayout.BeginHorizontal ();
                EditorGUILayout.PrefixLabel ("Material");
                terrainLOD.material = (Material) EditorGUILayout.ObjectField(terrainLOD.material, typeof(Material),true);
                EditorGUILayout.EndHorizontal ();

                EditorGUILayout.BeginHorizontal ();
                EditorGUILayout.PrefixLabel ("Player");
                terrainLOD.player = (Transform) EditorGUILayout.ObjectField(terrainLOD.player, typeof(Transform),true);
                EditorGUILayout.EndHorizontal ();
            break;
            case 1:
                EditorGUILayout.BeginHorizontal ();
                EditorGUILayout.PrefixLabel ("High Level Dist");
                Settings.dist = EditorGUILayout.FloatField(Settings.dist);
                EditorGUILayout.EndHorizontal ();

                EditorGUILayout.BeginHorizontal ();
                EditorGUILayout.PrefixLabel ("Recalculate Dist");
                Settings.distToRec = EditorGUILayout.FloatField(Settings.distToRec);
                EditorGUILayout.EndHorizontal ();

                EditorGUILayout.BeginHorizontal ();
                EditorGUILayout.PrefixLabel ("Wireframe");
                terrainLOD.wireframe = EditorGUILayout.Toggle(terrainLOD.wireframe);
                EditorGUILayout.EndHorizontal ();
            break;
        }
    }
}
