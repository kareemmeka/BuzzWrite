using UnityEngine;
using System.Collections.Generic;

public class DrawingTrail : MonoBehaviour
{
    [Header("Settings")]
    public float trailWidth = 0.015f;
    public Color onPathColor = Color.blue;
    public Color offPathColor = Color.red;
    public float minDistance = 0.005f;
    
    private LineRenderer currentTrail;
    private List<Vector3> trailPoints = new List<Vector3>();
    private List<GameObject> allTrails = new List<GameObject>();
    private bool isDrawing = false;
    
    void Update()
    {
        // TRIGGER (index finger) = DRAW
        bool triggerHeld = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
        
        // GRIP (middle finger) = DELETE/CLEAR
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            ClearAllDrawings();
        }
        
        // Start/stop drawing
        if (triggerHeld && !isDrawing)
        {
            StartDrawing();
        }
        else if (!triggerHeld && isDrawing)
        {
            StopDrawing();
        }
        
        // Add points while drawing
        if (isDrawing)
        {
            AddPoint(transform.position);
        }
    }
    
    void StartDrawing()
    {
        Debug.Log("🖊️ DRAWING STARTED");
        isDrawing = true;
        
        GameObject trailObj = new GameObject("Trail");
        currentTrail = trailObj.AddComponent<LineRenderer>();
        
        currentTrail.startWidth = trailWidth;
        currentTrail.endWidth = trailWidth;
        currentTrail.useWorldSpace = true;
        currentTrail.positionCount = 0;
        
        Material mat = new Material(Shader.Find("Sprites/Default"));
        mat.color = onPathColor;
        currentTrail.material = mat;
        
        trailPoints.Clear();
        allTrails.Add(trailObj);
    }
    
    void StopDrawing()
    {
        Debug.Log("🖊️ DRAWING STOPPED");
        isDrawing = false;
        currentTrail = null;
    }
    
    void AddPoint(Vector3 point)
    {
        if (trailPoints.Count == 0)
        {
            trailPoints.Add(point);
            UpdateLineRenderer();
            return;
        }
        
        float dist = Vector3.Distance(trailPoints[trailPoints.Count - 1], point);
        
        if (dist >= minDistance)
        {
            trailPoints.Add(point);
            UpdateLineRenderer();
        }
    }
    
    void UpdateLineRenderer()
    {
        if (currentTrail != null && trailPoints.Count > 0)
        {
            currentTrail.positionCount = trailPoints.Count;
            currentTrail.SetPositions(trailPoints.ToArray());
        }
    }
    
    public void SetTrailColor(bool onPath)
    {
        if (currentTrail != null)
        {
            currentTrail.material.color = onPath ? onPathColor : offPathColor;
        }
    }
    
    public bool IsDrawing()
    {
        return isDrawing;
    }
    
    public void ClearAllDrawings()
    {
        Debug.Log("🗑️ DELETING EVERYTHING!");
        
        foreach (GameObject trail in allTrails)
        {
            if (trail != null) Destroy(trail);
        }
        
        allTrails.Clear();
        trailPoints.Clear();
    }
}