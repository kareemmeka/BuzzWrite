using UnityEngine;
using UnityEngine.UI;

public class VRButtonInteraction : MonoBehaviour
{
    private Button button;
    private bool wasTriggered = false;
    
    void Start()
    {
        button = GetComponent<Button>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Check if hand or pointer entered
        if (other.CompareTag("Hand") || other.name.Contains("Hand"))
        {
            if (!wasTriggered && button != null)
            {
                button.onClick.Invoke();
                wasTriggered = true;
                Debug.Log("Button pressed in VR!");
                Invoke("ResetTrigger", 1f); // Prevent rapid re-triggering
            }
        }
    }
    
    void ResetTrigger()
    {
        wasTriggered = false;
    }
}