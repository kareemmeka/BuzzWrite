using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private bool isLoading = false;
    
    void Start()
    {
        Debug.Log("=== MENU READY ===");
        Debug.Log("Point at START button and pull trigger");
    }
    
    void Update()
    {
        // B button backup (if laser doesn't work)
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            StartGame();
        }
    }
    
    public void StartGame()
    {
        if (isLoading) return;
        
        isLoading = true;
        Debug.Log("LOADING GAME...");
        SceneManager.LoadScene(1);
    }
}