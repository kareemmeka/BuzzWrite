using UnityEngine;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LetterManager : MonoBehaviour
{
    [Header("Letter Settings")]
    public TextMeshPro letterText;
    public BoxCollider letterCollider;
    
    [Header("Current Letter")]
    public char currentLetter = 'A';
    
    [Header("Appearance")]
    public float fontSize = 10f;
    public Color letterColor = Color.white;
    
    [Header("Collider Settings")]
    public float colliderWidthMultiplier = 0.1f;   // Adjust for precision
    public float colliderHeightMultiplier = 0.15f; // Adjust for precision
    public float colliderDepth = 0.3f;
    
    private GameObject letterObject;
    
    void OnValidate()
    {
        if (Application.isPlaying) return;
        
        #if UNITY_EDITOR
        EditorApplication.delayCall += () =>
        {
            if (this != null)
            {
                CreateOrUpdateLetter();
            }
        };
        #endif
    }
    
    void Awake()
    {
        CreateOrUpdateLetter();
    }
    
    void Start()
    {
        if (letterObject == null)
        {
            CreateOrUpdateLetter();
        }
    }
    
    void CreateOrUpdateLetter()
    {
        // Find or create letter object
        if (letterObject == null)
        {
            Transform existingLetter = transform.Find("Letter_TMP");
            if (existingLetter != null)
            {
                letterObject = existingLetter.gameObject;
                letterText = letterObject.GetComponent<TextMeshPro>();
                letterCollider = letterObject.GetComponent<BoxCollider>();
            }
        }
        
        if (letterObject == null)
        {
            letterObject = new GameObject("Letter_TMP");
            letterObject.transform.SetParent(transform);
            letterObject.transform.localPosition = Vector3.zero;
            letterObject.transform.localRotation = Quaternion.identity;
            
            letterText = letterObject.AddComponent<TextMeshPro>();
            letterCollider = letterObject.AddComponent<BoxCollider>();
        }
        
        // Configure text
        if (letterText != null)
        {
            letterText.text = currentLetter.ToString();
            letterText.fontSize = fontSize;
            letterText.alignment = TextAlignmentOptions.Center;
            letterText.color = letterColor;
            letterText.outlineWidth = 0.2f;
            letterText.outlineColor = Color.black;
        }
        
        // Configure Box Collider - PRECISE SIZE
        if (letterCollider != null)
        {
            float width = fontSize * colliderWidthMultiplier;
            float height = fontSize * colliderHeightMultiplier;
            
            letterCollider.size = new Vector3(width, height, colliderDepth);
            letterCollider.center = Vector3.zero;
            
            Debug.Log($"✅ Letter '{currentLetter}' - Collider: ({width:F2}, {height:F2}, {colliderDepth})");
        }
    }
    
    public void SetLetter(char letter)
    {
        currentLetter = char.ToUpper(letter);
        if (letterText != null)
        {
            letterText.text = currentLetter.ToString();
        }
    }
    
    public GameObject GetLetterObject()
    {
        return letterObject;
    }
}