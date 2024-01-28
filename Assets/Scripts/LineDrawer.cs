using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LineDrawer : MonoBehaviour
{ 
    public Transform player; // Assign the player object in the inspector
    public GameObject targetObject; // Assign the target object in the inspector

    public LineRenderer line;

    private void Start()
    {
        player = gameObject.transform;
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        DrawLine();
    }

    void DrawLine()
    {
        targetObject = GameObject.FindWithTag("CheckPoint");
        // Ensure that both player and targetObject are assigned
        
        var checkPlayer = player.GetComponent<Player>();
        if (player != null  && !checkPlayer.IsBullying && targetObject != null)
        {
            // Debug.LogWarning("Assign player and targetObject in the inspector.");
            // Calculate the direction vector from player to targetObject
            Vector3 direction = targetObject.transform.position - player.position;

            // Draw the line using Debug.DrawLine
            // Debug.DrawLine(player.position, targetObject.transform.position, Color.red);
            
            line.SetPosition(0, player.position);
            line.SetPosition(1, targetObject.transform.position);
        }

        if (player != null && checkPlayer.IsBullying && targetObject != null || targetObject == null || checkPlayer.IsDead)
        {
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);
        }

    }
}
