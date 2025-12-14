using UnityEngine;
using UnityEngine.UI;

public class SimpleMenuPointer : MonoBehaviour
{
    private LineRenderer laser;
    public float laserLength = 10f;
    
    void Start()
    {
        CreateLaser();
    }
    
    void CreateLaser()
    {
        GameObject laserObj = new GameObject("MenuLaser");
        laserObj.transform.SetParent(transform);
        laser = laserObj.AddComponent<LineRenderer>();
        
        laser.positionCount = 2;
        laser.startWidth = 0.01f;
        laser.endWidth = 0.01f;
        
        Material mat = new Material(Shader.Find("Sprites/Default"));
        mat.color = Color.cyan;
        laser.material = mat;
        
        Debug.Log("Menu laser created");
    }
    
    void Update()
    {
        if (laser == null) return;
        
        Vector3 start = transform.position;
        Vector3 forward = transform.forward;
        Vector3 end = start + forward * laserLength;
        
        laser.SetPosition(0, start);
        laser.SetPosition(1, end);
        
        // Check for button hits
        RaycastHit hit;
        if (Physics.Raycast(start, forward, out hit, laserLength))
        {
            laser.SetPosition(1, hit.point);
            
            // Trigger pressed = click button
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                Button button = hit.collider.GetComponentInParent<Button>();
                if (button != null)
                {
                    Debug.Log($"Clicked button: {button.name}");
                    button.onClick.Invoke();
                }
            }
        }
    }
}