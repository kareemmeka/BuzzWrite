using UnityEngine;

public class LetterSwitcher : MonoBehaviour
{
    [Header("References")]
    public LetterManager letterManager;
    public BoundaryDetector boundaryDetector;
    public DrawingTrail drawingTrail;  // NEW: to clear when switching
    
    private char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    private int currentIndex = 0;
    
    void Start()
    {
        Invoke("UpdateLetter", 0.5f);
        Debug.Log("✅ A=Previous B=Next");
    }
    
    void Update()
    {
        // A button = PREVIOUS letter
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            PreviousLetter();
        }
        
        // B button = NEXT letter
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            NextLetter();
        }
    }
    
    void NextLetter()
    {
        currentIndex = (currentIndex + 1) % alphabet.Length;
        UpdateLetter();
    }
    
    void PreviousLetter()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = alphabet.Length - 1;
        UpdateLetter();
    }
    
    void UpdateLetter()
    {
        char letter = alphabet[currentIndex];
        
        if (letterManager != null)
        {
            letterManager.SetLetter(letter);
            
            GameObject letterObj = letterManager.GetLetterObject();
            
            if (boundaryDetector != null && letterObj != null)
            {
                boundaryDetector.letter = letterObj;
                Debug.Log($"📝 Letter {letter} ({currentIndex + 1}/26)");
            }
        }
        
        // Clear drawings when switching letters
        if (drawingTrail != null)
        {
            drawingTrail.ClearAllDrawings();
        }
    }
}