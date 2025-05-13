// Contributions: Chance Daigle and Ethan Campbell

using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera; // variable for main camera
    public Camera mapCamera; // variable for map camera
    private bool isMapActive = false; // bool for showing map
    private bool isHealthActive = false;
    private bool isXpActive = false;
    private bool isToolbarActive = false;
    public bool isPauseMenuActive = false;
    public GameObject healthBar;
    public GameObject xpBar;
    public GameObject pauseMenu;
    public GameObject toolBar;
    public PauseMenuController pauseMenuController;
    public ShopTrigger shopTrigger;
    public PetShopTrigger petShopTrigger;

    void Start()
    {
        // Initialize Cameras variables
        mainCamera.enabled = true;
        mapCamera.enabled = false;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Dungeon")
        {
            // Toggle Map if "M" key is pressed, if Pause Menu is not open and shop UI isn't open
            if (Input.GetKeyDown(KeyCode.M) && !isPauseMenuActive && 
                (shopTrigger == null || !shopTrigger.isShopOpen) && (petShopTrigger == null || !petShopTrigger.isPetShopOpen))
            {
                isMapActive = !isMapActive;

                // Freeze game while map is active
                if (isMapActive)
                {
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
                mainCamera.enabled = !isMapActive;
                mapCamera.enabled = isMapActive;
                healthBar.SetActive(isHealthActive);
                xpBar.SetActive(isXpActive);
                toolBar.SetActive(isToolbarActive);
                isXpActive = !isXpActive;
                isHealthActive = !isHealthActive;
                isToolbarActive = !isToolbarActive;
            }


            // Toggle Pause Menu if "F" key is pressed, if Map is not open and shop UI isn't open
            if (Input.GetKeyDown(KeyCode.F) && !isMapActive && 
                (shopTrigger == null || !shopTrigger.isShopOpen) && (petShopTrigger == null || !petShopTrigger.isPetShopOpen))
            {
                isPauseMenuActive = !isPauseMenuActive;
                pauseMenu.SetActive(isPauseMenuActive);
                pauseMenuController.UpdateStatsUI();
                pauseMenuController.UpdateButtonStates();

                if (isPauseMenuActive)
                {
                    pauseMenuController.ShowPlayerInventory(); // Ensure that everytime the player pauses the game, it opens with the inventory menu
                    Time.timeScale = 0; // Freeze game
                }
                else
                {
                    Time.timeScale = 1; // Resume game
                    pauseMenuController.playerToolbar.SetActive(true); // Show toolbar after exiting pause menu
                }
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.F) && !isMapActive)
            {
                isPauseMenuActive = !isPauseMenuActive;
                pauseMenu.SetActive(isPauseMenuActive);
                pauseMenuController.UpdateStatsUI();
                pauseMenuController.UpdateButtonStates();

                if (isPauseMenuActive)
                {
                    pauseMenuController.ShowPlayerInventory(); // Ensure that everytime the player pauses the game, it opens with the inventory menu
                    Time.timeScale = 0; // Freeze game
                }
                else
                {
                    Time.timeScale = 1; // Resume game
                    pauseMenuController.playerToolbar.SetActive(true); // Show toolbar after exiting pause menu
                }
            }
        }
    }
}
