using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Only needed if using UI Text

public class TeleportOnInteract : MonoBehaviour
{
    [Header("Target Scene")]
    public string sceneName; // Scene to load

    [Header("UI")]
    public GameObject interactText; // UI element for "Press E to interact"

    private bool playerInRange = false;

    void Start()
    {
        if (interactText != null)
            interactText.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactText != null)
                interactText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (interactText != null)
                interactText.SetActive(false);
        }
    }
}
