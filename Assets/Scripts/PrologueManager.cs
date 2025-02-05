// Contributions: Ethan Campbell
// Date Created: 2/4/2025

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PrologueManager : MonoBehaviour
{
    [System.Serializable]
    public struct PrologueStep
    {
        [TextArea(3, 5)] public string text; 
        public Sprite image; 
    }

    public PrologueStep[] steps;
    public TextMeshProUGUI prologueText;
    public Image prologueImage;
    public Image fadePanel;
    public Vector2 centerTextPosition = new Vector2(0, 0);
    public Vector2 bottomTextPosition = new Vector2(0, -300);
    public float displayTime = 10f;
    public float fadeDuration = 1.5f;

    private void Start()
    {
        StartCoroutine(ShowPrologue());
    }

    private IEnumerator ShowPrologue()
    {
        for (int i = 0; i < steps.Length; i++)
        {
            prologueText.text = steps[i].text;
            prologueImage.sprite = steps[i].image;

            if (i == 0)
            {
                // First text: Centered, No Image
                prologueText.rectTransform.anchoredPosition = centerTextPosition;
                prologueImage.gameObject.SetActive(false);
            }
            else
            {
                // Other text: Move to bottom, Show Image
                prologueText.rectTransform.anchoredPosition = bottomTextPosition;
                prologueImage.gameObject.SetActive(true);
            }

            yield return StartCoroutine(FadeInText(prologueText));
            yield return new WaitForSeconds(displayTime);
        }

        yield return StartCoroutine(FadeOutScreen());

        SceneManager.LoadScene("Hubworld");
    }

    private IEnumerator FadeInText(TextMeshProUGUI textElement)
    {
        Color color = textElement.color;
        color.a = 0;
        textElement.color = color;

        float duration = 1.5f;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, elapsed / duration);
            textElement.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeOutScreen()
    {
        Color color = fadePanel.color;
        color.a = 0;
        fadePanel.gameObject.SetActive(true);

        float elapsed = 0;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, elapsed / fadeDuration);
            fadePanel.color = color;
            yield return null;
        }
    }
}