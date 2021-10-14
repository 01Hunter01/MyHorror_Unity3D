using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(IkControl))]

public class PlayerViewInspector : Editor
{
    private void OnSceneGUI()
    {

        IkControl ikControl = (IkControl)target;

        Handles.color = Color.red;
        Handles.DrawWireDisc(ikControl.transform.position, new Vector3(0, 0, 0), ikControl.radiusView);
    }
}
