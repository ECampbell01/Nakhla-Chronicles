using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera; // variable for main camera
    public Camera mapCamera; // variabnle for map camera
    private bool isMapActive = false; // bool for showing map

    void Start()
    {
        //making sure cameras persists across scene loads
        DontDestroyOnLoad(mainCamera.gameObject);
        DontDestroyOnLoad(mapCamera.gameObject);

        // Initialize Cameras variables
        mainCamera.enabled = true;
        mapCamera.enabled = false;
    }

    void Update()
    {
        // if statement for showing mapCamera if "M" key is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMapActive = !isMapActive;
            mainCamera.enabled = !isMapActive;
            mapCamera.enabled = isMapActive;

        }
    }
}
