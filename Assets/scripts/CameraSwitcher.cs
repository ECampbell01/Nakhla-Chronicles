using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera; // variable for main camera
    public Camera mapCamera; // variabnle for map camera
    private bool isMapActive = false; // bool for showing map\
    private bool isHealthActive = false;
    private bool isXpActive = false;
    public GameObject healthBar;
    public GameObject xpBar;

    void Start()
    {
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
            healthBar.SetActive(isHealthActive);
            xpBar.SetActive(isXpActive);
            isXpActive = !isXpActive;
            isHealthActive = !isHealthActive;

        }
    }
}
