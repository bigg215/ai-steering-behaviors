using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(Waypoint))]
public class WaypointInspector : Editor
{
    private void OnSceneGUI()
    {
        Waypoint waypoint = target as Waypoint;

        for (int i = 0; i <= waypoint.points.Length - 1; i++)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 point = Handles.DoPositionHandle(waypoint.points[i], new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(waypoint, "Move Waypoint");
                EditorUtility.SetDirty(waypoint);
                waypoint.points[i] = point;
            }
        }
                      
        if (waypoint.points.Length > 1)
        {
            Handles.color = Color.white;
            Vector3 lineStart = waypoint.points[0];
            for (int i = 1; i <= waypoint.points.Length - 1; i++)
            {
                Vector3 lineEnd = waypoint.points[i];
                Handles.DrawLine(lineStart, lineEnd);
                lineStart = lineEnd;
            }
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Waypoint waypoint = target as Waypoint;
        if(GUILayout.Button("Add Waypoint"))
        {
            Undo.RecordObject(waypoint, "Add Waypoint");
            waypoint.AddWaypoint();
            EditorUtility.SetDirty(waypoint);
        }
    }
}
