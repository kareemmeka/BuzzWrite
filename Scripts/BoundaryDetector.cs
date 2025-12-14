using UnityEngine;

public class BoundaryDetector : MonoBehaviour
{
    [Header("References")]
    public SimpleHandPointer pen;
    public DrawingTrail drawingTrail;
    public HapticFeedback haptic;
    public GameObject letter;
    
    [Header("Detection Settings")]
    public float touchDistance = 0.3f;
    
    private bool wasOnPath = true;
    private Collider letterCollider;
    private int frameCount = 0;
    
    void Start()
    {
        Debug.Log("=== BOUNDARY DETECTOR START ===");
        
        // Immediate checks
        Debug.Log($"Pen: {(pen != null ? "✅" : "❌")}");
        Debug.Log($"DrawingTrail: {(drawingTrail != null ? "✅" : "❌")}");
        Debug.Log($"Haptic: {(haptic != null ? "✅" : "❌")}");
        Debug.Log($"Letter: {(letter != null ? "✅" : "❌")}");
        
        if (letter != null)
        {
            Debug.Log($"Letter name: {letter.name}");
            Debug.Log($"Letter position: {letter.transform.position}");
            
            letterCollider = letter.GetComponent<Collider>();
            
            if (letterCollider != null)
            {
                Debug.Log($"✅ Collider found: {letterCollider.GetType().Name}");
                Debug.Log($"Collider bounds center: {letterCollider.bounds.center}");
                Debug.Log($"Collider bounds size: {letterCollider.bounds.size}");
            }
            else
            {
                Debug.LogError("❌ LETTER HAS NO COLLIDER!");
            }
        }
        else
        {
            Debug.LogError("❌ LETTER REFERENCE IS NULL!");
        }
    }
    
    void Update()
    {
        frameCount++;
        
        // Check every frame for first 3 seconds
        bool debugMode = Time.time < 3f || frameCount % 60 == 0;
        
        // Verify references
        if (pen == null)
        {
            if (debugMode) Debug.LogError("❌ Pen is NULL");
            return;
        }
        
        if (haptic == null)
        {
            if (debugMode) Debug.LogError("❌ Haptic is NULL");
            return;
        }
        
        if (drawingTrail == null)
        {
            if (debugMode) Debug.LogError("❌ DrawingTrail is NULL");
            return;
        }
        
        if (letter == null)
        {
            if (debugMode) Debug.LogError("❌ Letter is NULL");
            return;
        }
        
        // Get collider if we don't have it
        if (letterCollider == null)
        {
            letterCollider = letter.GetComponent<Collider>();
            
            if (letterCollider == null)
            {
                if (debugMode) Debug.LogError("❌ Letter has NO COLLIDER!");
                return;
            }
            else
            {
                Debug.Log($"✅ Collider acquired: {letterCollider.GetType().Name}");
            }
        }
        
        // Only detect when drawing
        bool isDrawingNow = drawingTrail.IsDrawing();
        
        if (debugMode)
        {
            Debug.Log($"🎮 Drawing mode: {isDrawingNow}");
        }
        
        if (!isDrawingNow)
        {
            if (!wasOnPath)
            {
                wasOnPath = true;
                haptic.StopVibration();
                pen.SetPenColor(Color.yellow);
            }
            return;
        }
        
        // Get positions
        Vector3 penPos = pen.GetPenTip();
        Vector3 letterPos = letter.transform.position;
        Vector3 closestPoint = letterCollider.ClosestPoint(penPos);
        float distance = Vector3.Distance(penPos, closestPoint);
        
        // Check if on path
        bool isOnPath = distance < touchDistance;
        
        // ALWAYS log for debugging
        Debug.Log($"📏 Pen:{penPos:F2} Letter:{letterPos:F2} Closest:{closestPoint:F2}");
        Debug.Log($"   Distance:{distance:F3}m TouchDist:{touchDistance}m OnPath:{isOnPath}");
        
        // Force update every frame for testing
        UpdateFeedback(isOnPath);
        wasOnPath = isOnPath;
    }
    
    void UpdateFeedback(bool onPath)
    {
        Debug.Log(onPath ? "🟦 BLUE (on path)" : "🟥 RED (off path) + VIBRATE");
        
        if (drawingTrail != null)
        {
            drawingTrail.SetTrailColor(onPath);
        }
        
        if (pen != null)
        {
            pen.SetPenColor(onPath ? Color.blue : Color.red);
        }
        
        if (haptic != null)
        {
            if (onPath)
            {
                haptic.StopVibration();
            }
            else
            {
                haptic.StartVibration();
            }
        }
    }
}