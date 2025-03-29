// Contributors: Ethan Campbell
// Created: 1/17/2025

using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour
{
    public GameObject developedByScreen;
    public GameObject mainMenuScreen;
    public float delay = 3f;

    private void Start()
    {
        StartCoroutine(ShowMainMenuAfterDelay());
    }

    private IEnumerator ShowMainMenuAfterDelay()
    {
        // Ensure the developed by screen is active and the main menu is inactive initially
        developedByScreen.SetActive(true);
        mainMenuScreen.SetActive(false);

        // Add delay
        yield return new WaitForSeconds(delay);

        // Switch to the main menu
        developedByScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    } 
}
