using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternLineRenderer : MonoBehaviour
{
    public Material lineMaterial;
    public Texture2D patternTexture;
    public float patternScale = 1f;

    void Start()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.textureMode = LineTextureMode.Tile;
        lineRenderer.alignment = LineAlignment.View;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
        lineRenderer.SetPosition(1, new Vector3(5, 5, 0));

        ApplyPatternTexture(lineRenderer);
    }

    void ApplyPatternTexture(LineRenderer lineRenderer)
    {
        float length = Vector3.Distance(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));
        lineRenderer.material.mainTexture = patternTexture;
        lineRenderer.material.mainTextureScale = new Vector2(length / patternScale, 1f);
    }
}
