using UnityEngine;

public class HapticFeedback : MonoBehaviour
{
    [Header("Haptic Settings")]
    public float vibrationIntensity = 0.6f;
    
    private bool isVibrating = false;
    private bool isReady = false;
    private float startTime;
    
    void Start()
    {
        startTime = Time.time;
        isVibrating = false;
        
        // Force stop
        OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RTouch);
        
        Debug.Log("🔇 Haptic initialized");
        
        // Test vibration after 3 seconds
        Invoke("TestVibration", 3f);
    }
    
    void TestVibration()
    {
        Debug.Log("🧪 TESTING VIBRATION FOR 1 SECOND");
        OVRInput.SetControllerVibration(1f, 0.8f, OVRInput.Controller.RTouch);
        Invoke("StopTestVibration", 1f);
    }
    
    void StopTestVibration()
    {
        OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RTouch);
        isReady = true;
        Debug.Log("✅ Haptic ready");
    }
    
    void Update()
    {
        if (!isReady) return;
        
        if (isVibrating)
        {
            OVRInput.SetControllerVibration(1.0f, vibrationIntensity, OVRInput.Controller.RTouch);
        }
    }
    
    public void StartVibration()
    {
        if (!isReady) return;
        
        if (!isVibrating)
        {
            isVibrating = true;
            Debug.Log("🔴 VIBRATION ON");
        }
    }
    
    public void StopVibration()
    {
        if (isVibrating)
        {
            isVibrating = false;
            OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RTouch);
            Debug.Log("🟢 VIBRATION OFF");
        }
    }
    
    void OnDisable()
    {
        isVibrating = false;
        OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RTouch);
    }
}