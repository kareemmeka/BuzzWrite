using UnityEngine;

public class SimpleHandPointer : MonoBehaviour
{
    [Header("Hand Tracking")]
    public Transform rightHand;
    
    [Header("Laser Settings")]
    public float laserLength = 0.5f;  // Medium length laser
    public float laserWidth = 0.005f;  // Thin laser
    
    private LineRenderer laser;
    private Vector3 penTip;
    
    void Start()
    {
        CreateLaser();
    }
    
    void CreateLaser()
    {
        // Create laser line
        GameObject laserObj = new GameObject("Laser");
        laserObj.transform.SetParent(transform);
        laser = laserObj.AddComponent<LineRenderer>();
        
        laser.positionCount = 2;
        laser.startWidth = laserWidth;
        laser.endWidth = laserWidth;
        laser.useWorldSpace = true;
        
        // Make it yellow/visible
        Material mat = new Material(Shader.Find("Sprites/Default"));
        mat.color = Color.yellow;
        laser.material = mat;
        
        Debug.Log("✏️ Laser created");
    }
    
    void Update()
    {
        if (rightHand == null || laser == null) return;
        
        // Update laser position
        Vector3 start = rightHand.position;
        penTip = start + rightHand.forward * laserLength;
        
        laser.SetPosition(0, start);
        laser.SetPosition(1, penTip);
        
        transform.position = penTip;
    }
    
    public Vector3 GetPenTip()
    {
        return penTip;
    }
    
    public void SetPenColor(Color color)
    {
        if (laser != null)
        {
            laser.material.color = color;
        }
    }
}