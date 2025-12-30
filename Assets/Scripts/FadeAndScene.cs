using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FadeAndSceneChanger : MonoBehaviour
{
    public Image fadeImage;           // Assign in Inspector
    public float fadeDuration = 3f;   // Fade time
    public float delayBeforeFade = 2f; // Optional delay before starting fade
    public string nextSceneName;      // Scene to load

    private void Start()
    {
        if (fadeImage)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0f);
            fadeImage.gameObject.SetActive(false); // Start hidden
        }
    }

    public void StartFade()
    {
        if (fadeImage)
            fadeImage.gameObject.SetActive(true);

        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        // Optional delay before fade starts
        yield return new WaitForSeconds(delayBeforeFade);

        float timer = 0f;
        Color startColor = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        SceneManager.LoadScene(nextSceneName);
    }
}
