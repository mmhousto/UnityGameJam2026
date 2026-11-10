using UnityEngine;
using UnityEngine.InputSystem.HID;

public class HUDCanvas : MonoBehaviour
{
    private static HUDCanvas instance;
    public static HUDCanvas Instance { get { return instance; } }

    public GameObject contextPrompt;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
