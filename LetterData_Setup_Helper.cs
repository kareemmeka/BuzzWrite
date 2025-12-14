// Helper script to create LetterData ScriptableObjects programmatically
// Place this in Assets/Editor/ folder (create Editor folder if it doesn't exist)

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class LetterDataSetupHelper : EditorWindow
{
    private char selectedLetter = 'A';
    private float expectedTime = 10f;
    private int difficulty = 1;
    private List<Vector3> checkpointPositions = new List<Vector3>();
    private Vector3 newCheckpoint = Vector3.zero;
    
    [MenuItem("BuzzWrite/Create Letter Data")]
    public static void ShowWindow()
    {
        GetWindow<LetterDataSetupHelper>("Letter Data Creator");
    }
    
    void OnGUI()
    {
        GUILayout.Label("Create Letter Data ScriptableObject", EditorStyles.boldLabel);
        
        EditorGUILayout.Space();
        
        // Letter selection
        string letterString = EditorGUILayout.TextField("Letter", selectedLetter.ToString());
        if (letterString.Length > 0)
        {
            selectedLetter = letterString.ToUpper()[0];
        }
        
        // Expected time
        expectedTime = EditorGUILayout.FloatField("Expected Time (seconds)", expectedTime);
        
        // Difficulty
        difficulty = EditorGUILayout.IntField("Difficulty (1-5)", difficulty);
        
        EditorGUILayout.Space();
        GUILayout.Label("Checkpoint Positions (Local to Letter)", EditorStyles.boldLabel);
        
        // Add new checkpoint
        EditorGUILayout.BeginHorizontal();
        newCheckpoint = EditorGUILayout.Vector3Field("New Checkpoint", newCheckpoint);
        if (GUILayout.Button("Add", GUILayout.Width(60)))
        {
            checkpointPositions.Add(newCheckpoint);
            newCheckpoint = Vector3.zero;
        }
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.Space();
        
        // List existing checkpoints
        GUILayout.Label($"Checkpoints ({checkpointPositions.Count}):", EditorStyles.boldLabel);
        for (int i = checkpointPositions.Count - 1; i >= 0; i--)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Vector3Field($"CP {i}", checkpointPositions[i]);
            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                checkpointPositions.RemoveAt(i);
            }
            EditorGUILayout.EndHorizontal();
        }
        
        EditorGUILayout.Space();
        
        // Buttons
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Letter Data"))
        {
            CreateLetterData();
        }
        if (GUILayout.Button("Clear All"))
        {
            checkpointPositions.Clear();
        }
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.Space();
        
        // Quick preset buttons for common letters
        GUILayout.Label("Quick Presets (Simple Letters)", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Letter A"))
        {
            SetupLetterA();
        }
        if (GUILayout.Button("Letter I"))
        {
            SetupLetterI();
        }
        if (GUILayout.Button("Letter L"))
        {
            SetupLetterL();
        }
        EditorGUILayout.EndHorizontal();
    }
    
    void CreateLetterData()
    {
        LetterData data = ScriptableObject.CreateInstance<LetterData>();
        data.letter = selectedLetter;
        data.expectedTime = expectedTime;
        data.difficulty = difficulty;
        data.checkpointPositions = new List<Vector3>(checkpointPositions);
        
        string path = $"Assets/LetterData/Letter_{selectedLetter}.asset";
        
        // Create directory if it doesn't exist
        string directory = System.IO.Path.GetDirectoryName(path);
        if (!System.IO.Directory.Exists(directory))
        {
            System.IO.Directory.CreateDirectory(directory);
        }
        
        AssetDatabase.CreateAsset(data, path);
        AssetDatabase.SaveAssets();
        
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = data;
        
        Debug.Log($"Created LetterData for '{selectedLetter}' at {path}");
    }
    
    // Preset configurations for simple letters
    void SetupLetterA()
    {
        selectedLetter = 'A';
        expectedTime = 8f;
        difficulty = 2;
        checkpointPositions.Clear();
        
        // A shape: triangle with crossbar
        checkpointPositions.Add(new Vector3(-0.3f, 0.5f, 0f));  // Top left
        checkpointPositions.Add(new Vector3(0f, -0.5f, 0f));    // Bottom center
        checkpointPositions.Add(new Vector3(0.3f, 0.5f, 0f));   // Top right
        checkpointPositions.Add(new Vector3(-0.2f, 0f, 0f));    // Crossbar left
        checkpointPositions.Add(new Vector3(0.2f, 0f, 0f));     // Crossbar right
    }
    
    void SetupLetterI()
    {
        selectedLetter = 'I';
        expectedTime = 5f;
        difficulty = 1;
        checkpointPositions.Clear();
        
        // I shape: vertical line
        checkpointPositions.Add(new Vector3(0f, 0.5f, 0f));     // Top
        checkpointPositions.Add(new Vector3(0f, 0f, 0f));      // Middle
        checkpointPositions.Add(new Vector3(0f, -0.5f, 0f));   // Bottom
    }
    
    void SetupLetterL()
    {
        selectedLetter = 'L';
        expectedTime = 6f;
        difficulty = 1;
        checkpointPositions.Clear();
        
        // L shape: vertical line + horizontal line
        checkpointPositions.Add(new Vector3(-0.3f, 0.5f, 0f));  // Top
        checkpointPositions.Add(new Vector3(-0.3f, -0.5f, 0f)); // Bottom left
        checkpointPositions.Add(new Vector3(0.3f, -0.5f, 0f));  // Bottom right
    }
}

// Utility class to auto-generate checkpoints from letter mesh
public static class LetterCheckpointGenerator
{
    [MenuItem("BuzzWrite/Generate Checkpoints from Selected Mesh")]
    public static void GenerateCheckpointsFromMesh()
    {
        GameObject selected = Selection.activeGameObject;
        if (selected == null)
        {
            Debug.LogError("No GameObject selected!");
            return;
        }
        
        MeshFilter meshFilter = selected.GetComponent<MeshFilter>();
        if (meshFilter == null || meshFilter.sharedMesh == null)
        {
            Debug.LogError("Selected object has no mesh!");
            return;
        }
        
        Mesh mesh = meshFilter.sharedMesh;
        Vector3[] vertices = mesh.vertices;
        
        // Simple algorithm: sample points along the mesh
        List<Vector3> checkpoints = new List<Vector3>();
        
        // Get bounds
        Bounds bounds = mesh.bounds;
        
        // Generate checkpoints along the mesh outline
        // This is a simplified version - you may need to adjust based on your letter models
        int numCheckpoints = 10;
        for (int i = 0; i < numCheckpoints; i++)
        {
            float t = (float)i / (numCheckpoints - 1);
            Vector3 checkpoint = Vector3.Lerp(bounds.min, bounds.max, t);
            checkpoints.Add(checkpoint);
        }
        
        Debug.Log($"Generated {checkpoints.Count} checkpoints for {selected.name}");
        Debug.Log("Checkpoints (local space):");
        foreach (Vector3 cp in checkpoints)
        {
            Debug.Log($"  {cp}");
        }
        
        // You can copy these values to the LetterDataSetupHelper
    }
}
#endif

